using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Gtt.Chess.Core.Pgn
{
    public class PgnMoveTextParser
    {
        public static IEnumerable<PgnTurn> Parse(string moveText)
        {
            StringBuilder sb = new StringBuilder();

            using (StringReader sr = new StringReader(moveText))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("["))
                    {
                        continue;
                    }

                    sb.AppendLine(line);
                }
            }

            var tokens = ConvertToTokens(sb.ToString());
            var groups = GetMoveGroups(tokens);
            var moves = groups.Select(NormalizeTurn).Select(ParseTurn).ToList();
            return moves;
        }

        public static string[] ConvertToTokens(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new string[0];
            }

            string dictionary = "abcdefgh1234567890+#PRNKQBO-x/.";
            HashSet<char> hash = new HashSet<char>(dictionary);
            List<char> group = new List<char>();
            List<string> results = new List<string>();
            foreach (var c in text)
            {
                if (hash.Contains(c))
                {
                    group.Add(c);
                }
                else
                {
                    if (!char.IsWhiteSpace(c))
                    {
                        throw new Exception($"Cannot parse {c}");
                    }

                    if (group.Count > 0)
                    {
                        results.Add(new string(group.ToArray()));
                        group.Clear();
                    }
                }
            }

            if (group.Count > 0)
            {
                results.Add(new string(group.ToArray()));
            }

            return results.ToArray();
        }

        public static bool IsMove(string text)
        {
            return Regex.IsMatch(text, "[a-h][1-8]");
        }

        public static int? GetMoveNumber(string[] move)
        {
            if (move.Length < 2 || move.Length > 3)
                throw new ArgumentException("move should be 2 or 3 in length", nameof(move));

            var digits = string.Join("", move[0].TakeWhile(char.IsDigit));
            if (string.IsNullOrWhiteSpace(digits)) return null;
            var number = Convert.ToInt32(digits);
            return number;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="move"></param>
        /// <param name="moveIndex">Zero based move index based on postion of move</param>
        /// <returns></returns>
        public static (int turnNumber, string white, string black) NormalizeTurn(string[] move, int moveIndex)
        {
            int turn = GetMoveNumber(move) ?? (moveIndex + 1);
            // IF THE MOVE GROUP HAS 2 ELEMENTS THE WHITE MOVE IS INDEX #0, IF 3 ELEMENTS THE #1
            string white = move.Length == 2 ? ExtractMove(move[0]) : ExtractMove(move[1]);
            // IF THE MOVE GROUP HAS 2 ELEMENTS THE BLACK MOVE IS INDEX #1, IF 3 ELEMENTS THE #2
            string black = move.Length == 2 ? ExtractMove(move[1]) : ExtractMove(move[2]);
            return (turn, white, black);
        }

        public static string ExtractMove(string move)
        {
            if (string.IsNullOrWhiteSpace(move))
            {
                throw new ArgumentException("move should have a value", nameof(move));
            }

            var segments = move.Split('.');
            return segments[segments.Length - 1];
        }

        public static string[][] GetMoveGroups(string[] tokens)
        {
            string notation = NotationStyle(tokens);

            switch (notation)
            {
                case "2":
                    return tokens.CreateBlock(2).Select(x => x.ToArray()).ToArray();
                case "3":
                    return tokens.CreateBlock(3).Select(x => x.ToArray()).ToArray();
                default:
                    throw new ArgumentException($"The value {notation} does not have a switch");
            }
        }

        public static string ExtractSquareFromMove(string move, Color color)
        {
            string[] gameEnding = { "1-0", "0-1", "*", "1/2-1/2" };
            if (gameEnding.Contains(move))
            {
                return "";
            }

            // KING SIDE CASTLE
            if (move == "O-O")
            {
                return color == Color.White ? "g1" : "g8";
            }

            // QUEEN SIDE CASTLE
            if (move == "O-O-O")
            {
                return color == Color.White ? "c1" : "c8";
            }

            // MOVE IS AT THE END OF THE STRING
            var matches = Regex.Matches(move, "[a-h][1-8]", RegexOptions.IgnoreCase);
            if (matches.Count == 1)
            {
                return matches[0].Value;
            }

            throw new Exception($"Cannot find square in move {move}");
        }

        public static string NotationStyle(string[] tokens)
        {
            if (tokens == null || tokens.Length < 1)
            {
                throw new ArgumentException("Must provide at least 1 tokens");
            }

            if (!IsMove(tokens[0]))
            {
                return "3";
            }

            return "2";
        }

        public static PgnTurn ParseTurn((int turnNumber, string white, string black) turn)
        {
            PlayResult result;
            string normalizedMoveText = $"{turn.turnNumber}. {turn.white} {turn.black}".Trim();

            var whiteResult = ExtractMoveTypeFromMove(turn.white);
            var blackResult = ExtractMoveTypeFromMove(turn.black);

            if (whiteResult == MoveType.GameEnd)
            {
                result = ExtractGameEndingResultFromMove(turn.white);
                return new PgnTurn(turn.turnNumber, null, null, result, normalizedMoveText);

            }

            PgnTurnMove whiteTurn = new PgnTurnMove(turn.white, Color.White);

            if (blackResult == MoveType.GameEnd)
            {
                result = ExtractGameEndingResultFromMove(turn.black);
                return new PgnTurn(turn.turnNumber, whiteTurn, null, result, normalizedMoveText);
            }

            PgnTurnMove blackTurn = new PgnTurnMove(turn.black, Color.Black);
            return new PgnTurn(turn.turnNumber, whiteTurn, blackTurn, blackTurn.Result, normalizedMoveText);
        }

        public static MoveType ExtractMoveTypeFromMove(string moveText)
        {
            string[] gameEnding = { "1-0", "0-1", "*", "1/2-1/2" };
            if (gameEnding.Contains(moveText))
            {
                return MoveType.GameEnd;
            }

            if (moveText.Contains("O-O"))
            {
                return MoveType.Castling;
            }

            return MoveType.Regular;
        }

        public static PlayResult ExtractGameEndingResultFromMove(string moveText)
        {
            switch (moveText)
            {
                case "1-0":
                case "0-1":
                    return PlayResult.Checkmate;
                case "1/2-1/2":
                    return PlayResult.Draw;
                case "*":
                    return PlayResult.Abandoned;
                default:
                    throw new ArgumentException($"Cannot parse game ending result from {moveText}");
            }
        }
    }
}