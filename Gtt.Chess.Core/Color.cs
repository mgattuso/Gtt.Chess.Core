using System;
using System.Collections.Generic;
using System.Text;

namespace Gtt.Chess.Core
{
    public enum Color
    {
        White = 0,
        Black = 1
    }

    public enum PlayResult
    {
        InProgress,
        Abandoned,
        Draw,
        Check,
        Checkmate,
    }

    public enum Piece {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }

    public enum MoveType
    {
        Regular,
        Castling,
        Promotion,
        GameEnd
    }
}
