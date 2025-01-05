using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessGame
{
    public class DoublePawn(Position from, Position to) : Move
    {
        public override MoveType Type => MoveType.DoublePawn;
        public override Position FromPos { get; } = from;
        public override Position ToPos { get; } = to;
        public readonly Position skippedPos = new((from.Row + to.Row) / 2, from.Column);

        public override void Execute(Board board, Square from = null)
        {
            from ??= board.Squares.FirstOrDefault(s => s.Position.Equals(FromPos));
            if (from != null && from.Piece is Pawn pawn)
            {
                pawn.HasSkip = true;
            }
            new NormalMove(FromPos, ToPos) { Sound = Resource.move_self }.Execute(board, from);
        }
        public override void UnExecute(Board board, Square from = null)
        {
            from ??= board.Squares.FirstOrDefault(s => s.Position.Equals(ToPos));
            if (from != null && from.Piece is Pawn pawn)
            {
                pawn.HasSkip = false;
            }
            new NormalMove(FromPos, ToPos) { Sound = Resource.move_self }.UnExecute(board, from);
        }
        public override void Execute(EPiece[,] board)
        {
            board[FromPos.Row, FromPos.Column].HasSkip = true;
            new NormalMove(FromPos, ToPos).Execute(board);
        }
    }
}
