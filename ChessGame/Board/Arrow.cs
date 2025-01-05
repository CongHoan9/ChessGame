using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ChessGame
{
    public enum HighLightArrow : byte
    {
        Red,
        Orange,
        Blue,
        Green,
        Move,
        None,
    }
    public enum EvaluateArrow : byte
    {
        Best,
        Attack,
        None,
    }
    public class Arrow : INotifyPropertyChanged
    {
        private Point _Start = new();
        public Point Start
        {
            get => _Start;
            set
            {
                _Start = value;
                OnPropertyChanged(nameof(Start));
            }
        }
        private (Position Start, Position End) _Points = new(new(0, 0), new(0, 0));
        public (Position Start, Position End) Points
        {
            get => _Points;
            set
            {
                _Points = value;
                Start = Board.ToPoint(Points.Start);
                PointCollection = new(GetPoint(_Points.Start, _Points.End));
            }
        }
        private PointCollection _PointCollection = [];
        public PointCollection PointCollection
        {
            get => _PointCollection;
            set
            {
                _PointCollection = value;
                OnPropertyChanged(nameof(PointCollection));
            }
        }
        private SolidColorBrush _FillColor = new();
        public SolidColorBrush SolidColorBrush
        {
            get => _FillColor;
            set
            {
                _FillColor = value;
                OnPropertyChanged(nameof(SolidColorBrush));
            }
        }
        private HighLightArrow _HighLightArrow = HighLightArrow.None;
        public HighLightArrow HighLightArrow
        {
            get => _HighLightArrow;
            set
            {
                _HighLightArrow = value;
                SolidColorBrush = new(GetColorArrow());
            }
        }
        private EvaluateArrow _EvaluateArrow = EvaluateArrow.None;
        public EvaluateArrow EvaluateArrow
        {
            get => _EvaluateArrow;
            set
            {
                _EvaluateArrow = value;
                SolidColorBrush = new(GetColorArrow());
            }
        }
        public Board Board { get; set; } = new();
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
        public Arrow(Position start, Position end, Board board)
        {
            Board = board;
            Points = new(start, end);
        }
        public Point[] GetPoint(Position start, Position end)
        {
            Point startt = Board.ToPoint(start), endd = Board.ToPoint(end);
            double dxp = end.Column - start.Column, dyp = end.Row - start.Row;
            double dx = endd.X - startt.X, dy = endd.Y - startt.Y;
            if ((Math.Abs(dxp) == 2 && Math.Abs(dyp) == 1) || (Math.Abs(dxp) == 1 && Math.Abs(dyp) == 2)) // if arrow for knight
            {
                bool isHorizontal = Math.Abs(dx) > Math.Abs(dy);
                Point corner = isHorizontal ? new(endd.X, startt.Y) : new(startt.X, endd.Y);
                double dxsc = corner.X - startt.X, dysc = corner.Y - startt.Y;
                double dxce = endd.X - corner.X, dyce = endd.Y - corner.Y;
                double lengthsc = Math.Sqrt(dxsc * dxsc + dysc * dysc), lengthce = Math.Sqrt(dxce * dxce + dyce * dyce);
                double uxsc = dxsc / lengthsc, uysc = dysc / lengthsc;
                double uxce = dxce / lengthce, uyce = dyce / lengthce;
                startt = new(uxsc * 32, uysc * 32);
                endd = new(dx - uxce * 32, dy - uyce * 32);
                corner = isHorizontal ? new(endd.X, startt.Y) : new(startt.X, endd.Y);
                return
                [
                    new(startt.X - uysc * 10, startt.Y + uxsc * 10),
                    new(corner.X - (isHorizontal ? uyce : uysc) * 10, corner.Y + (isHorizontal ? uxsc : uxce) * 10),
                    new(endd.X - uyce * 10, endd.Y + uxce * 10),
                    new(endd.X - uyce * (isHorizontal ? 23 : 10), endd.Y + uxce * (isHorizontal ? 10 : 23)),
                    new(dx, dy), // arrow head
                    new(endd.X + uyce * (isHorizontal ? 23 : 10), endd.Y - uxce * (isHorizontal ? 10 : 23)),
                    new(endd.X + uyce * 10, endd.Y - uxce * 10),
                    new(corner.X + (isHorizontal ? uyce : uysc) * 10, corner.Y - (isHorizontal ? uxsc : uxce) * 10),
                    new(startt.X + uysc * 10, startt.Y - uxsc * 10),
                ];
            }
            else
            {
                double length = Math.Sqrt(dx * dx + dy * dy), ux = dx / length, uy = dy / length;
                startt = new(ux * 32, uy * 32);
                endd = new(dx - ux * 32, dy - uy * 32);
                return
                [
                    new(startt.X - uy * 10, startt.Y + ux * 10),
                    new(endd.X - uy * 10, endd.Y + ux * 10),
                    new(endd.X - uy * 23, endd.Y + ux * 23),
                    new(dx, dy), // arrow head
                    new(endd.X + uy * 23, endd.Y - ux * 23),
                    new(endd.X + uy * 10, endd.Y - ux * 10),
                    new(startt.X + uy * 10, startt.Y - ux * 10)
                ];
            }
        }
        public void BlurColorArrow()
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                HighLightArrow = HighLightArrow != HighLightArrow.Red ? HighLightArrow.Red : HighLightArrow.None;
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
            {
                HighLightArrow = HighLightArrow != HighLightArrow.Blue ? HighLightArrow.Blue : HighLightArrow.None;
            }
            else if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                HighLightArrow = HighLightArrow != HighLightArrow.Green ? HighLightArrow.Green : HighLightArrow.None;
            }
            else
            {
                HighLightArrow = HighLightArrow != HighLightArrow.Orange ? HighLightArrow.Orange : HighLightArrow.None;
            }
        }
        public Color GetColorArrow()
        {
            return HighLightArrow switch
            {
                HighLightArrow.Red => Color.FromArgb(163, 248, 85, 63),
                HighLightArrow.Orange => Color.FromArgb(163, 255, 170, 0), //standard:163, 255, 170, 0 lichess: 128, 20, 85, 30
                HighLightArrow.Blue => Color.FromArgb(163, 72, 193, 249),
                HighLightArrow.Green => Color.FromArgb(163, 159, 207, 63),
                HighLightArrow.None => EvaluateArrow switch
                {
                    EvaluateArrow.Best => Color.FromArgb(204, 150, 190, 70),
                    EvaluateArrow.Attack => Color.FromArgb(191, 202, 52, 49),
                    EvaluateArrow.None => Color.FromArgb(0, 0, 0, 0),
                    _ => Color.FromArgb(0, 0, 0, 0),
                },
                _ => Color.FromArgb(0, 0, 0, 0),
            };
        }
        public static Position ToPosition(Point point)
        {
            return new(Math.Max(0, Math.Min((int)(point.Y / (600 / 8)), 7)), Math.Max(0, Math.Min((int)(point.X / (600 / 8)), 7)));
        }
        public override bool Equals(object obj)
        {
            return obj is Arrow arrow && arrow.Points.Start.Equals(Points.Start) && arrow.Points.End.Equals(Points.End);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Points);
        }
    }
}
