using System.ComponentModel;
namespace ChessGame
{
    public class Hover : INotifyPropertyChanged
    {
        public Position _Position = new(0, 0);
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
        public Hover(Position position)
        {
            Position = position;
        }
    }
}
