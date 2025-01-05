namespace ChessGame
{
    public sealed class Knight(Player color) : Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color => color;
        private static IEnumerable<Position> JumpToPosition(Position from)
        {
            foreach (Direction vDir in new Direction[] { Direction.North, Direction.South })
            {
                foreach (Direction hDir in new Direction[] { Direction.West, Direction.East })
                {
                    yield return from + 2 * vDir + hDir;
                    yield return from + 2 * hDir + vDir;
                }
            }
        }
        private IEnumerable<Position> JumpToPositions(Position from, EPiece[,] board, bool getall)
        {
            return JumpToPosition(from).Where(pos => Board.IsInside(pos) && ((board[pos.Row, pos.Column].Color != Color) || getall));
        }
        public override IEnumerable<Move> GetMoves(Position from, EPiece[,] board, bool getall)
        {
            return JumpToPositions(from, board, getall).Select(to => new NormalMove(from, to));
        }
    }
}