using System.IO;
using System.Windows;

namespace ChessGame
{
    public class NormalMove(Position from, Position to) : Move
    {
        public override MoveType Type => MoveType.Nomal;
        public override Position FromPos { get; } = from;
        public override Position ToPos { get; } = to;
        public override Square Capture { get; set; }
        public Stream Sound { get; set; } = null;
        public override void Execute(Board board, Square from = null)
        {
            from ??= board.Squares.FirstOrDefault(s => s.Position.Equals(FromPos));
            if (from != null)
            {
                board.Squares.Where(s => s.Piece is Pawn && !s.Position.Equals(FromPos)).Select(s => s.Piece as Pawn).ToList().ForEach(p => p.HasSkip = false);
                if (board.Squares.FirstOrDefault(s => s.Position.Equals(ToPos)) is Square to)
                {
                    board.Squares.Remove(to);
                    Capture = to;
                    Sound ??= Resource.capture;
                }
                from.Position = ToPos;
                from.Visibility = Visibility.Visible;
                from.Piece.MovedCount++;
                if (IsInCheck(from.Piece.Color.Opponent(), Encrypt.ListToArray(new(board.Squares))))
                {
                    Sound = Resource.move_check;
                }
                Sounds.PlaySound(Sound ?? Resource.move_self);
                Sound = null;
            }
        }
        public override void UnExecute(Board board, Square from = null)
        {
            from ??= board.Squares.FirstOrDefault(s => s.Position.Equals(ToPos));
            if (from != null)
            {
                board.Squares.Where(s => s.Piece is Pawn && !s.Position.Equals(ToPos)).Select(s => s.Piece as Pawn).ToList().ForEach(p => p.HasSkip = false);
                if (IsInCheck(from.Piece.Color.Opponent(), Encrypt.ListToArray(new(board.Squares))))
                {
                    Sound = Resource.move_check;
                }
                from.Position = FromPos;
                from.Visibility = Visibility.Visible;
                from.Piece.MovedCount--;
                if (Capture != null && !board.Squares.Contains(Capture))
                {
                    board.Squares.Add(Capture);
                    Sound ??= Resource.capture;
                }
                Sounds.PlaySound(Sound ?? Resource.move_self);
                Sound = null;
            }
        }
        public override void Execute(EPiece[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col].HasSkip = false;
                }
            }
            if (Board.IsInside(FromPos) && Board.IsInside(ToPos))
            {
                board[ToPos.Row, ToPos.Column] = board[FromPos.Row, FromPos.Column];
                board[FromPos.Row, FromPos.Column] = new(PieceType.None, Player.None);
            }
        }
    }
}