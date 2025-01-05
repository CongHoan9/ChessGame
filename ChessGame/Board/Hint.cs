using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChessGame
{
    public enum HintType : byte
    {
        Normal,
        Capture,
        None,
    }
    public class Hint : INotifyPropertyChanged
    {
        public SolidColorBrush FillColor { get; private set; } = Brushes.Transparent;
        public SolidColorBrush StrokeColor { get; private set; } = Brushes.Transparent;
        public double StrokeThickness { get; private set; } = 0;
        public double Width { get; private set; } = 0;
        public double Height { get; private set; } = 0;

        private HintType _HintType = HintType.None;
        public HintType HintType
        {
            get => _HintType;
            set
            {
                _HintType = value;
                if (_HintType == HintType.Normal)
                {
                    FillColor = new SolidColorBrush(Color.FromArgb(36, 0, 0, 0));
                    StrokeColor = Brushes.Transparent;
                    StrokeThickness = 0;
                    Width = 28;
                    Height = 28;
                }
                else if (HintType == HintType.Capture)
                {
                    FillColor = Brushes.Transparent;
                    StrokeColor = new SolidColorBrush(Color.FromArgb(36, 0, 0, 0));
                    StrokeThickness = 7;
                    Width = Double.NaN;
                    Height = Double.NaN;
                }
                OnPropertyChanged(nameof(FillColor));
                OnPropertyChanged(nameof(StrokeColor));
                OnPropertyChanged(nameof(StrokeThickness));
                OnPropertyChanged(nameof(Width));
                OnPropertyChanged(nameof(Height));
            }
        }
        private Position _Position = new(0, 0);
        public Position Position
        {
            get => _Position;
            set
            {
                _Position = value;
                OnPropertyChanged(nameof(Position));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
        public Hint(Position position, HintType hinttype)
        {
            Position = position;
            HintType = hinttype;
        }
    }
}
