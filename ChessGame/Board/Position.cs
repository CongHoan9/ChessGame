namespace ChessGame
{
    public class Position(int row, int column)
    {
        public int Row { get; set; } = row;
        public int Column { get; set; } = column;

        public override string ToString()
        {
            return $"{(char)('a' + Column)}{8 - Row}";
        }
        public override bool Equals(object obj)
        {
            return obj is Position position && position.Row == Row && position.Column == Column;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }
        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
        public static Position operator +(Position pos, Direction dir)
        {
            return new(pos.Row + dir.RowDelta, pos.Column + dir.ColumnDelta);
        }
        public static Position operator -(Position left, Position right)
        {
            return new(left.Row - right.Row, left.Column - right.Column);
        }
        public static Position operator *(Position pos, int num)
        {
            return new(pos.Row * num, pos.Column * num);
        }
        public static bool operator <(Position pos, Position pos1)
        {
            return pos.Row < pos1.Row && pos.Column < pos1.Column;
        }
        public static bool operator >(Position pos, Position pos1)
        {
            return pos.Row > pos1.Row && pos.Column > pos1.Column;
        }
        public static bool operator >=(Position pos, Position pos1)
        {
            return pos.Row >= pos1.Row && pos.Column >= pos1.Column;
        }
        public static bool operator <=(Position pos, Position pos1)
        {
            return pos.Row <= pos1.Row && pos.Column <= pos1.Column;
        }
    }
}
