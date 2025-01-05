using System.Windows.Documents;

namespace ChessGame
{
    public enum PieceType : byte
    {
        None,
        Pawn,
        Knight,
        Bishop,
        Rook, 
        Queen, 
        King,
    }
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public uint MovedCount { get; set; } = 0;
        public abstract IEnumerable<Move> GetMoves(Position from, EPiece[,] board, bool getall);
        public virtual IEnumerable<Position> MovePositionInDir(Position from, EPiece[,] board, Direction dir, bool getall)
        {
            for (Position pos = from + dir; Board.IsInside(pos); pos += dir)
            {
                if (board[pos.Row, pos.Column].Type == PieceType.None || getall)
                {
                    yield return pos;
                    continue;
                }
                if (board[pos.Row, pos.Column].Color != Color || getall)
                {
                    yield return pos;
                }
                yield break;
            }
        }
        public virtual IEnumerable<Position> MovePositionInDirS(Position from, EPiece[,] board, Direction[] dirs, bool getall)
        {
            return dirs.SelectMany(dir => MovePositionInDir(from, board, dir, getall));
        }
        public virtual bool CanCaptureOpponentking(Position from, EPiece[,] board)
        {
            return GetMoves(from, board, false).Any(move => Board.IsInside(move.ToPos) && board[move.ToPos.Row, move.ToPos.Column].Type == PieceType.King);
        }
        public override string ToString()
        {
            return Type switch
            {
                PieceType.Pawn => $"{(Color == Player.White ? "P" : "p")}",
                PieceType.Knight => $"{(Color == Player.White ? "N" : "n")}",
                PieceType.Bishop => $"{(Color == Player.White ? "B" : "b")}",
                PieceType.Rook => $"{(Color == Player.White ? "R" : "r")}",
                PieceType.Queen => $"{(Color == Player.White ? "Q" : "q")}",
                PieceType.King => $"{(Color == Player.White ? "K" : "k")}",
                _ => ""
            };
        }
        public static Piece GetPiece(char piece)
        {
            return piece switch
            {
                'P' => new Pawn(Player.White),
                'N' => new Knight(Player.White),
                'B' => new Bishop(Player.White),
                'R' => new Rook(Player.White),
                'Q' => new Queen(Player.White),
                'K' => new King(Player.White),
                'p' => new Pawn(Player.Black),
                'n' => new Knight(Player.Black),
                'b' => new Bishop(Player.Black),
                'r' => new Rook(Player.Black),
                'q' => new Queen(Player.Black),
                'k' => new King(Player.Black),
                _ => null,
            };
        }
    }
}