using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
namespace ChessGame
{
    public class Square : INotifyPropertyChanged
    {
        private Cursor _cursor = new(new MemoryStream(Resource.Hover));
        public Cursor Cursor
        {
            get => _cursor;
            set
            {
                _cursor = value;
                OnPropertyChanged(nameof(Cursor));
            }
        }
        private string _ImagePath = "";
        public string ImagePath
        {
            get => _ImagePath;
            set
            {
                _ImagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }
        private Piece _Piece;
        public Piece Piece
        {
            get => _Piece;
            set
            {
                _Piece = value;
                ImagePath = Images.GetImage(Piece);
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
        private Position _PrePosition = new(0, 0);
        public Position PrePosition
        {
            get => _PrePosition;
            set
            {
                _PrePosition = value;
                OnPropertyChanged(nameof(PrePosition));
            }
        }
        private Visibility _Visibility = Visibility.Visible;
        public Visibility Visibility
        {
            get => _Visibility;
            set
            {
                if (_Visibility != value)
                {
                    _Visibility = value;
                    OnPropertyChanged(nameof(Visibility));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
        public Square(Piece piece, Position position)
        {
            Piece = piece;
            Position = position;
        }
        public override string ToString()
        {
            Console.ResetColor();
            return $"{Piece.Color} {Piece.Type} from {Position}";
        }
    }
}
