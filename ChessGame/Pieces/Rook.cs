namespace ChessGame
{
    public sealed class Rook(Player color) : Piece
    {
        public override PieceType Type => PieceType.Rook;
        public override Player Color => color;
        private static readonly Direction[] dirs =
        [
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
        ];
        public override IEnumerable<Move> GetMoves(Position from, EPiece[,] board, bool getall)
        {
            return MovePositionInDirS(from, board, dirs, getall).Select(to => new NormalMove(from, to));
        }
    }
}
