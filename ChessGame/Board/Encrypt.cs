namespace ChessGame
{
    public struct EPiece(PieceType type, Player color)
    {
        public byte Data = (byte)((int)type << 4 | (int)color << 2);
        public PieceType Type
        {
            readonly get => (PieceType)(Data >> 4 & 0b_00001111);
            set => Data = (byte)((Data & 0b_00001111) | ((int)value << 4));
        }
        public Player Color
        {
            readonly get => (Player)(Data >> 2 & 0b_00000011);
            set => Data = (byte)((Data & 0b_11110011) | ((int)value << 2));
        }
        public bool HasMoved
        {
            readonly get => (Data & 0b_00000010) != 0;
            set => Data = (byte)((Data & 0b_11111101) | (value ? 1 << 1 : 0));
        }
        public bool HasSkip
        {
            readonly get => (Data & 0b_00000001) != 0;
            set => Data = (byte)((Data & 0b_11111110) | (value ? 1 : 0));
        }
    }
    public class Encrypt
    {
        public static EPiece EncryptPiece(Piece piece)
        {
            EPiece _piece = new(piece.Type, piece.Color)
            {
                HasMoved = piece.MovedCount > 0
            };
            if (piece is Pawn pawn)
            {
                _piece.HasSkip = pawn.HasSkip;
            }
            return _piece;
        }
        public static Piece DecryptPiece(EPiece _piece)
        {
            return _piece.Type switch
            {
                PieceType.Pawn => new Pawn(_piece.Color) { MovedCount = _piece.HasMoved ? (uint)1 : 0, HasSkip = _piece.HasSkip },
                PieceType.Knight => new Knight(_piece.Color) { MovedCount = _piece.HasMoved ? (uint)1 : 0 },
                PieceType.Bishop => new Bishop(_piece.Color) { MovedCount = _piece.HasMoved ? (uint)1 : 0 },
                PieceType.Rook => new Rook(_piece.Color) { MovedCount = _piece.HasMoved ? (uint)1 : 0 },
                PieceType.Queen => new Queen(_piece.Color) { MovedCount = _piece.HasMoved ? (uint)1 : 0 },
                PieceType.King => new King(_piece.Color) { MovedCount = _piece.HasMoved ? (uint)1 : 0 },
                _ => null
            };
        }
        public static EPiece[,] ListToArray(List<Square> squares)
        {
            EPiece[,] board = new EPiece[8, 8];
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (squares.Find(s => s.Position.Equals(new Position(row, col))) is Square square)
                    {
                        board[row, col] = EncryptPiece(square.Piece);
                    }
                    else
                    {
                        board[row, col] = new(PieceType.None, Player.None);
                    }
                }
            }
            return board;
        }
        public static string ListToFen(List<Square> squares)
        {
            string fen = "";
            for (int row = 0; row < 8; row++)
            {
                int numEmptycols = 0;
                for (int col = 0; col < 8; col++)
                {
                    if (squares.Find(s => s.Position.Equals(new Position(row, col))) is Square square)
                    {
                        if (numEmptycols != 0)
                        {
                            fen += numEmptycols;
                            numEmptycols = 0;
                        }
                        fen += square.Piece; 
                    }
                    else
                    {
                        numEmptycols++;
                    }
                }
                if (numEmptycols != 0)
                {
                    fen += numEmptycols;
                }
                if (row != 7)
                {
                    fen += '/';
                }
            }
            return fen;
        }
    }
}
