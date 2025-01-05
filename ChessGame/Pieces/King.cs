namespace ChessGame
{
    public sealed class King(Player color) : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color => color;
        private static readonly Direction[] dirs =
        [
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.NorthEast,
            Direction.NorthWest,
            Direction.SouthEast,
            Direction.SouthWest,
        ];
        private static bool IsUnmovedRook(Position pos, EPiece[,] board)
        {
            if (board[pos.Row, pos.Column].Type == PieceType.None)
            {
                return false;
            }
            else
            {
                return board[pos.Row, pos.Column].Type == PieceType.Rook && board[pos.Row, pos.Column].HasMoved == false;
            }
        }
        private static bool AllEmpity(IEnumerable<Position> position, EPiece[,] board, bool getall)
        {
            return position.All(pos => board[pos.Row, pos.Column].Type == PieceType.None) || getall;
        }
        private bool CanCastlesKingSide(Position from, EPiece[,] board, bool getall)
        {
            if (MovedCount > 0)
            {
                return false;
            }
            Position rookPos = new(from.Row, 7);
            Position[] betweenPositions = [new(from.Row, 5), new(from.Row, 6)];
            return IsUnmovedRook(rookPos, board) && AllEmpity(betweenPositions, board, getall);
        }
        private bool CanCastlesQueenSide(Position from, EPiece[,] board, bool getall)
        {
            if (MovedCount > 0)
            {
                return false;
            }
            Position rookPos = new(from.Row, 0);
            Position[] betweenPositions = [new(from.Row, 1), new(from.Row, 2), new(from.Row, 3)];
            return IsUnmovedRook(rookPos, board) && AllEmpity(betweenPositions, board, getall);
        }

        private IEnumerable<Position> MovePositions(Position from, EPiece[,] board, bool getall)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;
                if (!Board.IsInside(to))
                {
                    continue;
                }
                if (board[to.Row, to.Column].Color != Color || getall)
                {
                    yield return to;
                }
            }
        }
        public override IEnumerable<Move> GetMoves(Position from, EPiece[,] board, bool getall)
        {
            foreach (Position to in MovePositions(from, board, getall))
            {
                yield return new NormalMove(from, to);
            }
            if (CanCastlesKingSide(from, board, getall))
            {
                yield return new Castle(MoveType.CastleKS, from);
            }
            if (CanCastlesQueenSide(from, board, getall))
            {
                yield return new Castle(MoveType.CastleQS, from);
            }
        }
        public override bool CanCaptureOpponentking(Position from, EPiece[,] board)
        {
            return MovePositions(from, board, false).Any(to => Board.IsInside(to) && board[to.Row, to.Column].Type == PieceType.King);
        }
    }
}