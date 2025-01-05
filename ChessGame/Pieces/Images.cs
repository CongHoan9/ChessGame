namespace ChessGame
{
    public static partial class Images
    {
        public static string GetImageWhite(PieceType type)
        {
            return type switch
            {
                PieceType.Pawn => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/wp.png",
                PieceType.Knight => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/wn.png",
                PieceType.Bishop => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/wb.png",
                PieceType.Rook => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/wr.png",
                PieceType.Queen => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/wq.png",
                PieceType.King => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/wk.png",
                _ => null,
            };
        }
        public static string GetImageBlack(PieceType type)
        {
            return type switch
            {
                PieceType.Pawn => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/bp.png",
                PieceType.Knight => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/bn.png",
                PieceType.Bishop => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/bb.png",
                PieceType.Rook => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/br.png",
                PieceType.Queen => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/bq.png",
                PieceType.King => "pack://application:,,,/ChessGame;component/Assets/Board-Pieces/bk.png",
                _ => null,
            };
        }
        public static string GetImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.White => GetImageWhite(type),
                Player.Black => GetImageBlack(type),
                _ => null
            };
        }
        public static string GetImage(Piece piece)
        {
            return GetImage(piece.Color, piece.Type);
        }
    }
}
