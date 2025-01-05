using System.IO;
using System.Windows;
namespace ChessGame
{
    public class PawnPromotion(Position from, Position to, PieceType newtype) : Move
    {
        public override MoveType Type => MoveType.PawnPromotion;
        public override Position FromPos { get; } = from;
        public override Position ToPos { get; } = to;
        public override Square Capture { get; set; }
        private Pawn Piece { get; set; }
        public PieceType newType = newtype;
        private Piece CreatePromotionPiece(Player color)
        {
            return newType switch
            {
                PieceType.Queen => new Queen(color),
                PieceType.Knight => new Knight(color),
                PieceType.Rook => new Rook(color),
                PieceType.Bishop => new Bishop(color),
                _ => null,
            };
        }
        private EPiece CreatePromotion_Piece(Player color)
        {
            return newType switch
            {
                PieceType.Queen => new EPiece(PieceType.Queen, color),
                PieceType.Knight => new EPiece(PieceType.Knight, color),
                PieceType.Rook => new EPiece(PieceType.Rook, color),
                PieceType.Bishop => new EPiece(PieceType.Bishop, color),
                _ => new EPiece(PieceType.None, color),
            };
        }
        public override void Execute(Board board, Square from = null)
        {
            if (board.Squares.FirstOrDefault(s => s.Position.Equals(ToPos)) is Square to)
            {
                board.Squares.Remove(to);
                Capture = to;
            }
            from ??= board.Squares.FirstOrDefault(s => s.Position.Equals(FromPos));
            if (from != null)
            {
                Piece = from.Piece as Pawn;
                from.Piece = CreatePromotionPiece(from.Piece.Color);
                from.Piece.MovedCount = Piece.MovedCount + 1;
            }
            new NormalMove(FromPos, ToPos) { Sound = Resource.promote }.Execute(board, from);
        }
        public override void UnExecute(Board board, Square from = null)
        {
            from ??= board.Squares.FirstOrDefault(s => s.Position.Equals(ToPos));
            if (from != null)
            {
                from.Piece = Piece;
            }
            if (Capture != null && !board.Squares.Contains(Capture))
            {
                board.Squares.Add(Capture);
            }
            new NormalMove(FromPos, ToPos) { Sound = Resource.promote }.UnExecute(board, from);
        }
        public override void Execute(EPiece[,] board)
        {
            if (board[FromPos.Row, FromPos.Column] is EPiece from)
            {
                board[FromPos.Row, FromPos.Column] = CreatePromotion_Piece(from.Color);
            }
            new NormalMove(FromPos, ToPos).Execute(board);
        }
    }
}
