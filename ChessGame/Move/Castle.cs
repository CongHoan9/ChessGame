using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public sealed class Castle : Move
    {
        public override MoveType Type { get; }
        public override Position FromPos { get; } 
        public override Position ToPos { get; }
        private readonly Direction kingMoveDir;
        private readonly Position rookFromPos;
        private readonly Position rookToPos;
        public Castle(MoveType type, Position kingPos)
        {
            Type = type;
            FromPos = kingPos;
            if (type == MoveType.CastleKS)
            {
                kingMoveDir = Direction.East;
                ToPos = new Position(kingPos.Row, 6);
                rookFromPos = new Position(kingPos.Row, 7);
                rookToPos = new Position(kingPos.Row, 5);
            }
            else if (type == MoveType.CastleQS)
            {
                kingMoveDir = Direction.West;
                ToPos = new Position(kingPos.Row, 2);
                rookFromPos = new Position(kingPos.Row, 0);
                rookToPos = new Position(kingPos.Row, 3);
            }
        }
        public override void Execute(Board board, Square from = null)
        {
            new NormalMove(FromPos, ToPos) { Sound = Resource.castle }.Execute(board, from);
            new NormalMove(rookFromPos, rookToPos) { Sound = Resource.castle }.Execute(board);
        }
        public override void UnExecute(Board board, Square from = null)
        {
            new NormalMove(FromPos, ToPos) { Sound = Resource.castle }.UnExecute(board, from);
            new NormalMove(rookFromPos, rookToPos) { Sound = Resource.castle }.UnExecute(board);
        }
        public override void Execute(EPiece[,] board)
        {
            new NormalMove(FromPos, ToPos).Execute(board);
            new NormalMove(rookFromPos, rookToPos).Execute(board);
        }
        public override bool IsLegar(Player player , EPiece[,] board)
        {
            if (!IsInCheck(player, board))
            {
                EPiece[,] copy = board.Clone() as EPiece[,];
                Position pos = FromPos;
                for (int i = 0; i < 2; i++)
                {
                    new NormalMove(pos, pos + kingMoveDir).Execute(copy);
                    pos += kingMoveDir;
                    if (IsInCheck(player, copy))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
