using System.Linq;

namespace Gtt.Chess.Core.Pgn
{
    public class PgnTurnMove
    {
        public PgnTurnMove(string moveText, Color color)
        {
            Result = PlayResult.InProgress;

            var endPosition = PgnMoveTextParser.ExtractSquareFromMove(moveText, color);
            Color = color;
            MoveType = PgnMoveTextParser.ExtractMoveTypeFromMove(moveText);

            if (MoveType == MoveType.GameEnd)
            {
                Result = PgnMoveTextParser.ExtractGameEndingResultFromMove(moveText);
                return;
            }

            if (MoveType == MoveType.Castling)
            {
                // HANDLE CASTLING
                Piece = Piece.King;
                CastlingInfo = new PgnTurnMoveCastle(endPosition);
            }

            Piece = PieceParser.Parse(moveText.Substring(0, 1));
            EndPosition = endPosition;
            PieceTaken = moveText.Contains("x");

            if (moveText.Contains("+"))
            {
                Result = PlayResult.Check;
            }

            if (moveText.Contains("="))
            {
                MoveType = MoveType.Promotion;
                PromotionInfo = new PgnTurnMovePromotion(moveText);
            }

            DisambiguationFile = ExtractDisambiguationFile(moveText);
            DisambiguationRank = ExtractDisambiguationRank(moveText);
        }

        public static string ExtractDisambiguationFile(string moveText)
        {
            if (PgnMoveTextParser.ExtractMoveTypeFromMove(moveText) == MoveType.GameEnd)
                return "";

            char[] filterForText = moveText.Where(char.IsLetterOrDigit).ToArray();
            char[] withoutSquare = filterForText.Take(filterForText.Length - 2).Where(x => x != 'x' && (char.IsLower(x) || char.IsDigit(x))).ToArray();
            char letter = withoutSquare.Where(char.IsLower).FirstOrDefault();
            if (letter != default(char))
            {
                return letter.ToString();
            }

            return "";
        }

        public static string ExtractDisambiguationRank(string moveText)
        {
            if (PgnMoveTextParser.ExtractMoveTypeFromMove(moveText) == MoveType.GameEnd)
                return "";

            char[] filterForText = moveText.Where(char.IsLetterOrDigit).ToArray();
            char[] withoutSquare = filterForText.Take(filterForText.Length - 2).Where(x => x != 'x' && (char.IsLower(x) || char.IsDigit(x))).ToArray();
            char letter = withoutSquare.Where(char.IsDigit).FirstOrDefault();
            if (letter != default(char))
            {
                return letter.ToString();
            }

            return "";
        }

        public Color Color { get; }
        public string EndPosition { get; }
        public string DisambiguationFile { get; }
        public string DisambiguationRank { get; }
        public Piece Piece { get; }
        public bool PieceTaken { get; }
        public MoveType MoveType { get; }
        public PlayResult Result { get; }
        public PgnTurnMoveCastle CastlingInfo { get; }
        public PgnTurnMovePromotion PromotionInfo { get; }
    }
}