namespace ChessGame
{
    public sealed class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }
        public bool HasSkip { get; set; } = false;
        public Direction Forward { get; set; }
        public Pawn(Player color)
        {
            Color = color;
            Forward = (Color == Player.White) ? Direction.North : Direction.South;
        }
        private static bool CanMoveTo(Position pos, EPiece[,] board, bool getall)
        {
            return Board.IsInside(pos) && (board[pos.Row, pos.Column].Type == PieceType.None || getall);
        }
        private bool CanCaptureAt(Position pos, EPiece[,] board, bool getall)
        {
            return Board.IsInside(pos) && ((board[pos.Row, pos.Column].Type != PieceType.None && board[pos.Row, pos.Column].Color != Color) || getall);
        }
        private static IEnumerable<Move> PromotionMoves(Position from, Position to)
        {
            yield return new PawnPromotion(from, to, PieceType.Knight);
            yield return new PawnPromotion(from, to, PieceType.Bishop);
            yield return new PawnPromotion(from, to, PieceType.Rook);
            yield return new PawnPromotion(from, to, PieceType.Queen);
        }
        private IEnumerable<Move> ForwardMoves(Position from, EPiece[,] board, bool getall)
        {
            Position oneMovePos = from + Forward;
            if (CanMoveTo(oneMovePos, board, getall))
            {
                if (oneMovePos.Row == 0 || oneMovePos.Row == 7)
                {
                    foreach (Move promMove in PromotionMoves(from, oneMovePos))
                    {
                        yield return promMove;
                    }
                }
                else
                {
                    yield return new NormalMove(from, oneMovePos);
                }
                Position twoMovesPos = oneMovePos + Forward;
                if (MovedCount == 0 && CanMoveTo(twoMovesPos, board, getall) && (from.Row == 1 || from.Row == 6))
                {
                    yield return new DoublePawn(from, twoMovesPos);
                }
            }
        }
        private IEnumerable<Move> DiagonalMoves(Position from, EPiece[,] board, bool getall)
        {
            foreach (Direction dir in new Direction[] { Direction.West, Direction.East })
            {
                Position to = from + Forward + dir;
                Position skip = new(to.Row - Forward.RowDelta, to.Column);
                if (Board.IsInside(skip) && (board[skip.Row, skip.Column].HasSkip || getall))
                {
                    yield return new EnPassant(from, to);
                }
                if (CanCaptureAt(to, board, getall))
                {
                    if (to.Row == 0 || to.Row == 7)
                    {
                        foreach (Move promMove in PromotionMoves(from, to))
                        {
                            yield return promMove;
                        }
                    }
                    else
                    {
                        yield return new NormalMove(from, to);
                    }
                }
            }
        }
        public override IEnumerable<Move> GetMoves(Position from, EPiece[,] board, bool getall)
        {
            return ForwardMoves(from, board, getall ).Concat(DiagonalMoves(from, board, getall));
        }
        public override bool CanCaptureOpponentking(Position from, EPiece[,] board)
        {
            return DiagonalMoves(from, board, false).Any(move => Board.IsInside(move.ToPos) && board[move.ToPos.Row, move.ToPos.Column].Type == PieceType.King);
        }
    }
}