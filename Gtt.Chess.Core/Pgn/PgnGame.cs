using System.Collections.Generic;

namespace Gtt.Chess.Core.Pgn
{
    public class PgnGame
    {
        public PgnSevenTagRoster SevenTagRoster { get; set; }
        public IEnumerable<PgnTurn> Turns { get; set; }
    }
}