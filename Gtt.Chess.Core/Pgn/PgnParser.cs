namespace Gtt.Chess.Core.Pgn
{
    public static class PgnParser
    {
        // http://www.saremba.de/chessgml/standards/pgn/pgn-complete.htm

        public static PgnGame ParseGame(string pgnData)
        {
            PgnGame game = new PgnGame
            {
                SevenTagRoster = PgnSevenTagRosterParser.Parse(pgnData), 
                Turns = PgnMoveTextParser.Parse(pgnData)
            };
            return game;
        }
    }
}
