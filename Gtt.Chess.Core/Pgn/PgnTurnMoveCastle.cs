using System;

namespace Gtt.Chess.Core.Pgn
{
    public class PgnTurnMoveCastle
    {
        public PgnTurnMoveCastle(string kingEndPiece)
        {

            EndPosition = CalculateRookEndPositionFromKingPosition(kingEndPiece);
        }
        public Piece Piece => Piece.Rook;
        public string EndPosition { get; }

        public string CalculateRookEndPositionFromKingPosition(string kingPosition)
        {
            switch (kingPosition)
            {
                case "g1":
                    return "f1";
                case "g8":
                    return "f8";
                case "c1":
                    return "d1";
                case "c8":
                    return "d8";
                default:
                    throw new ArgumentException($"The end position of the king at {kingPosition} is not valid for castling");
            }
        }
    }
}