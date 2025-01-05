using System.Collections.ObjectModel;
using System.IO;

namespace ChessGame
{
    public enum MoveType : byte
    {
        Nomal,
        CastleKS,
        CastleQS,
        DoublePawn,
        EnPassant,
        PawnPromotion
    }
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Position FromPos { get; }
        public abstract Position ToPos { get; }
        public virtual Square Capture { get; set; }
        public abstract void Execute(Board board, Square from = null);
        public abstract void UnExecute(Board board, Square from = null);
        public abstract void Execute(EPiece[,] board);
        public virtual bool IsLegar(Player player , EPiece[,] board)
        {
            Execute(board);
            return !IsInCheck(player, board);
        }
        public static IEnumerable<Position> PiecePositionsFor(Player player, EPiece[,] board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (board[r, c].Color == player)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }
        public static bool IsInCheck(Player player, EPiece[,] board)
        {
            return PiecePositionsFor(player.Opponent(), board).Any(p => Board.IsInside(p) && Encrypt.DecryptPiece(board[p.Row, p.Column]).CanCaptureOpponentking(p, board));
        }
        public static Stream GetSoundForMove(MoveType? type)
        {
            return type switch
            {
                MoveType.Nomal => Resource.move_self,
                MoveType.CastleKS => Resource.castle,
                MoveType.CastleQS => Resource.castle,
                MoveType.DoublePawn => Resource.move_self,
                MoveType.EnPassant => Resource.capture,
                MoveType.PawnPromotion => Resource.promote,
                _ => null,
            };
        }
        public override bool Equals(object obj)
        {
            return obj is Move move && move.FromPos.Equals(FromPos) && move.ToPos.Equals(ToPos) && move.Type == Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FromPos, ToPos, Type);
        }
    }
}
