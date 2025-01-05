using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
namespace ChessGame
{
    public enum EvaluateOverlay : byte
    {
        Brilliant,
        Great,
        Best,
        Excellent,
        Good,
        Book,
        Inaccuracy,
        Mistake,
        Miss,
        Blunder,
        Correct,
        Incorrect,
        Move,
        None,
    }
    public enum HighLightOverlay : byte
    {
        Check,
        Move,
        None,
    }
    public enum BlurOverlay : byte
    {
        Premove,
        Red,
        Orange,
        Blue,
        Green,
        None,
    }
    public class Overlay : INotifyPropertyChanged
    {
        public bool IsWarning = false; 
        private readonly ObservableCollection<Overlay> Overlays = [];
        private SolidColorBrush _SolidColorBrush = new();
        public SolidColorBrush SolidColorBrush
        {
            get => _SolidColorBrush;
            set
            {
                _SolidColorBrush = value;
                OnPropertyChanged(nameof(SolidColorBrush));
            }
        }
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

        private EvaluateOverlay _EvaluateOverlay = EvaluateOverlay.None;
        public EvaluateOverlay EvaluateOverlay
        {
            get => _EvaluateOverlay;
            set
            {
                BlurOverlay = BlurOverlay.None;
                _EvaluateOverlay = value;
                SolidColorBrush = new SolidColorBrush(GetColorOverlay());
            }
        }
        private HighLightOverlay _HighLightOverlay = HighLightOverlay.None;
        public HighLightOverlay HighLightOverlay
        {
            get => _HighLightOverlay;
            set
            {
                _HighLightOverlay = value;
                SolidColorBrush = new SolidColorBrush(GetColorOverlay());
            }
        }
        private BlurOverlay _BlurOverlay = BlurOverlay.None;
        public BlurOverlay BlurOverlay
        {
            get => _BlurOverlay;
            set
            {
                _BlurOverlay = value;
                SolidColorBrush = new SolidColorBrush(GetColorOverlay());
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
            if (HighLightOverlay == HighLightOverlay.None && EvaluateOverlay == EvaluateOverlay.None && BlurOverlay == BlurOverlay.None && !IsWarning)
            {
                Overlays.Remove(this);
            }
        }
        public Overlay(Position position, ObservableCollection<Overlay> overlays)
        {
            Position = position;
            Overlays = overlays;
        }
        private Color GetColorOverlay()
        {
            return BlurOverlay switch
            {
                BlurOverlay.Premove => Color.FromArgb(128, 244, 42, 50),
                BlurOverlay.Red => Color.FromArgb(204, 235, 97, 80),
                BlurOverlay.Orange => Color.FromArgb(204, 255, 170, 0),
                BlurOverlay.Blue => Color.FromArgb(204, 82, 176, 220),
                BlurOverlay.Green => Color.FromArgb(204, 172, 206, 89),
                BlurOverlay.None => EvaluateOverlay switch
                {
                    EvaluateOverlay.Brilliant => Color.FromArgb(128, 38, 194, 163),
                    EvaluateOverlay.Great => Color.FromArgb(128, 116, 155, 191),
                    EvaluateOverlay.Best => Color.FromArgb(128, 129, 182, 76),
                    EvaluateOverlay.Excellent => Color.FromArgb(128, 129, 182, 76),
                    EvaluateOverlay.Good => Color.FromArgb(128, 149, 183, 118),
                    EvaluateOverlay.Book => Color.FromArgb(128, 213, 164, 125),
                    EvaluateOverlay.Inaccuracy => Color.FromArgb(128, 247, 198, 49),
                    EvaluateOverlay.Mistake => Color.FromArgb(128, 255, 164, 89),
                    EvaluateOverlay.Miss => Color.FromArgb(128, 255, 119, 105),
                    EvaluateOverlay.Blunder => Color.FromArgb(128, 250, 65, 45),
                    EvaluateOverlay.Correct => Color.FromArgb(128, 172, 206, 89),
                    EvaluateOverlay.Incorrect => Color.FromArgb(128, 201, 52, 48),
                    EvaluateOverlay.Move => Color.FromArgb(128, 255, 255, 51),
                    EvaluateOverlay.None => HighLightOverlay switch
                    {
                        HighLightOverlay.Check => Color.FromArgb(125, 255, 0, 0),
                        HighLightOverlay.Move => Color.FromArgb(128, 255, 255, 51),
                        HighLightOverlay.None => Color.FromArgb(0, 0, 0, 0),
                        _ => Color.FromArgb(0, 0, 0, 0),
                    },
                    _ => Color.FromArgb(0, 0, 0, 0),
                },
                _ => Color.FromArgb(0, 0, 0, 0),
            };
        }
        public void BlurColorOverlay()
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                BlurOverlay = BlurOverlay != BlurOverlay.Orange ? BlurOverlay.Orange : BlurOverlay.None;
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
            {
                BlurOverlay = BlurOverlay != BlurOverlay.Blue ? BlurOverlay.Blue : BlurOverlay.None;
            }
            else if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                BlurOverlay = BlurOverlay != BlurOverlay.Green ? BlurOverlay.Green : BlurOverlay.None;
            }
            else
            {
                BlurOverlay = BlurOverlay != BlurOverlay.Red ? BlurOverlay.Red : BlurOverlay.None;
            }
        }
        public async Task Warning()
        {
            IsWarning = true;
            for (int i = 0; i < 6; i++)
            {
                HighLightOverlay = HighLightOverlay != HighLightOverlay.Check ? HighLightOverlay.Check : HighLightOverlay.None;
                await Task.Delay(250);
            }
            IsWarning = false;
            HighLightOverlay = HighLightOverlay.None;
            OnPropertyChanged(nameof(SolidColorBrush));
        }
    }
}
