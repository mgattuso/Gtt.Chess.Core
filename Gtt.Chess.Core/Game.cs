using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtt.Chess.Core
{
    public class Game
    {
    }

    public class GameStatus
    {

    }

    public class Board
    {
        public Board()
        {
            Cells = new List<Cell>();
            for (int y = 1; y <= 8; y++)
                for (int x = 1; x <= 8; x++)
                    Cells.Add(new Cell(x, y, Cells.Count + 1));

            Pieces = new List<PieceInfo>();
            for (int i = 1; i <= 16; i++)
            {
                var w = i;
                var b = i + 16;
                var isOdd = i % 2 == 1;
                if (i < 8)
                {
                    Pieces.Add(new PieceInfo(w, Piece.Pawn, Color.White));
                    Cells.First(x => x.Y == 2 && x.X == i).PieceId = w;
                    Pieces.Add(new PieceInfo(b, Piece.Pawn, Color.Black));
                    Cells.First(x => x.Y == 7 && x.X == i).PieceId = b;
                }
                else if (i < 10)
                {
                    Pieces.Add(new PieceInfo(w, Piece.Rook, Color.White));
                    Cells.First(x => x.Y == 1 && x.X == (isOdd ? 1 : 8)).PieceId = w;
                    Pieces.Add(new PieceInfo(b, Piece.Rook, Color.Black));
                    Cells.First(x => x.Y == 8 && x.X == (isOdd ? 1 : 8)).PieceId = b;
                }
                else if (i < 12)
                {
                    Pieces.Add(new PieceInfo(w, Piece.Knight, Color.White));
                    Cells.First(x => x.Y == 1 && x.X == (isOdd ? 2 : 7)).PieceId = w;
                    Pieces.Add(new PieceInfo(b, Piece.Knight, Color.Black));
                    Cells.First(x => x.Y == 8 && x.X == (isOdd ? 2 : 7)).PieceId = b;
                }
                else if (i < 14)
                {
                    Pieces.Add(new PieceInfo(w, Piece.Bishop, Color.White));
                    Cells.First(x => x.Y == 1 && x.X == (isOdd ? 3 : 6)).PieceId = w;
                    Pieces.Add(new PieceInfo(b, Piece.Bishop, Color.Black));
                    Cells.First(x => x.Y == 8 && x.X == (isOdd ? 3 : 6)).PieceId = b;
                }
                else if (i < 15)
                {
                    Pieces.Add(new PieceInfo(w, Piece.Queen, Color.White));
                    Cells.First(x => x.Y == 1 && x.X == 5).PieceId = w;
                    Pieces.Add(new PieceInfo(b, Piece.Queen, Color.Black));
                    Cells.First(x => x.Y == 8 && x.X == 5).PieceId = b;
                }
                else
                {
                    Pieces.Add(new PieceInfo(w, Piece.King, Color.White));
                    Cells.First(x => x.Y == 1 && x.X == 4).PieceId = w;
                    Pieces.Add(new PieceInfo(b, Piece.King, Color.Black));
                    Cells.First(x => x.Y == 8 && x.X == 4).PieceId = b;
                }
            }
        }

        public IList<Cell> Cells { get; }
        public IList<PieceInfo> Pieces { get; }

        public IList<string> GetMoves(string reference)
        {
            var cell = Cells.FirstOrDefault(x => x.Name == reference);
            if (cell?.PieceId == null)
            {
                return new List<string>();
            }

            var piece = Pieces.FirstOrDefault(x => x.Id == cell.PieceId);
            if (piece == null)
            {
                return new List<string>();
            }

            var map = GetMoveMap(reference, piece.Piece, piece.Color, piece.MoveCount);
            //todo: filter for taken squares
            return map;
        }

        public IList<string> GetMoveMap(string reference, Piece piece, Color color, int moveCount)
        {
            var cell = Cells.FirstOrDefault(x => x.Name == reference);
            if (cell == null) return new List<string>();
            List<string> possibleMoves = new List<string>();
            switch (piece)
            {
                case Piece.Pawn:
                    if (cell.Rank == "B" || cell.Rank == "G")
                    {

                    }
                    break;
                case Piece.Rook:
                    break;
                case Piece.Knight:
                    break;
                case Piece.Bishop:
                    break;
                case Piece.Queen:
                    break;
                case Piece.King:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(piece), piece, null);
            }

            return possibleMoves;
        }
    }

    public class PieceInfo
    {
        public PieceInfo(int id, Piece piece, Color color)
        {
            Id = id;
            Piece = piece;
            Color = color;
        }

        public int Id { get; }
        public Piece Piece { get; }
        public Color Color { get; }
        public int MoveCount { get; }
    }

    public class Cell
    {
        public Cell(int x, int y, int id, int? pieceId = null)
        {
            X = x;
            Y = y;
            Id = id;
            PieceId = pieceId;
        }

        public int Id { get; set; }
        public int X { get; }
        public int Y { get; }
        public string File => Convert.ToChar(64 + X).ToString();
        public string Rank => Y.ToString();
        public string Name => $"{File}{Rank}";
        public int? PieceId { get; set; }
    }
}
