namespace ChessGame
{
    public sealed class Queen(Player color) : Piece
    {
        public override PieceType Type => PieceType.Queen;
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
        public override IEnumerable<Move> GetMoves(Position from, EPiece[,] board, bool getall)
        {
            return MovePositionInDirS(from, board, dirs, getall).Select(to =>  new NormalMove(from, to));
        }
    }
}
