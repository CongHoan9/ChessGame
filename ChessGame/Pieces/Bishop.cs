namespace ChessGame
{
    public sealed class Bishop(Player color) : Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Color => color;
        private static readonly Direction[] dirs =
        [
            Direction.NorthEast,
            Direction.NorthWest,
            Direction.SouthEast,
            Direction.SouthWest,
        ];
        public override IEnumerable<Move> GetMoves(Position from, EPiece[,] board, bool getall)
        {
            return MovePositionInDirS(from, board, dirs, getall).Select(to => new NormalMove(from, to));
        }
    }
}
