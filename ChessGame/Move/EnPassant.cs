using System.Collections.ObjectModel;
using System.IO;
namespace ChessGame
{
    public class EnPassant(Position from, Position to) : Move
    {
        public override MoveType Type => MoveType.EnPassant;
        public override Position FromPos { get; } = from;
        public override Position ToPos { get; } = to;
        public override Square Capture { get; set; }
        public override void Execute(Board board, Square from = null)
        {
            Square capture = board.Squares.FirstOrDefault(s => s.Piece is Pawn pawn && pawn.HasSkip);
            board.Squares.Remove(capture);
            Capture = capture;
            new NormalMove(FromPos, ToPos) { Sound = Resource.capture }.Execute(board, from);
        }
        public override void UnExecute(Board board, Square from = null)
        {
            new NormalMove(FromPos, ToPos) { Sound = Resource.capture }.UnExecute(board, from);
            if (Capture != null && !board.Squares.Contains(Capture))
            {
                board.Squares.Add(Capture);
                if (Capture.Piece is Pawn pawn)
                {
                    pawn.HasSkip = true;
                }
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
            new NormalMove(FromPos, ToPos).Execute(board);
        }
    }
}
