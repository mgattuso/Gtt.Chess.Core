using System;
using System.Collections.Generic;
using System.Text;

namespace Gtt.Chess.Core
{
    public static class PieceParser
    {
        public static Piece Parse(string text)
        {
            text = string.IsNullOrWhiteSpace(text) ? "" : text.Trim();
            if (text.Length > 1)
            {
                throw new ArgumentException($"Expected a blank or single line of text. Found {text}", nameof(text));
            }

            var firstChar = text[0];
            switch (firstChar)
            {
                case 'K':
                    return Piece.King;
                case 'Q':
                    return Piece.Queen;
                case 'B':
                    return Piece.Bishop;
                case 'N':
                    return Piece.Knight;
                case 'R':
                    return Piece.Rook;
                default:
                    return Piece.Pawn;
            }
        }
    }
}
