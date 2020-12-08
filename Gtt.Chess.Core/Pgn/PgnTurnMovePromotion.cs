using System;

namespace Gtt.Chess.Core.Pgn
{
    public class PgnTurnMovePromotion
    {
        public PgnTurnMovePromotion(string moveText)
        {
            var parts = moveText.Split('=');
            if (parts.Length != 2)
            {
                throw new ArgumentException($"Expected a movetext that contained a single '=' sign. Found {moveText}");
            }
            PromotedTo = PieceParser.Parse(parts[1]);
        }
        public Piece PromotedTo { get; }
    }
}