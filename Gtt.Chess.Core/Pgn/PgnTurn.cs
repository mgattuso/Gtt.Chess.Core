namespace Gtt.Chess.Core.Pgn
{
    public class PgnTurn
    {
        public PgnTurn(int turnNumber, PgnTurnMove white, PgnTurnMove black,  PlayResult result, string normalizedMoveText)
        {
            TurnNumber = turnNumber;
            Result = result;
            WhiteMove = white;
            BlackMove = black;
            MoveText = normalizedMoveText;
        }

        public PlayResult Result { get; }
        public int TurnNumber { get; }
        public PgnTurnMove WhiteMove { get; }
        public PgnTurnMove BlackMove { get; }
        public string MoveText { get; }
    }
}