using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ChessGame
{
    public partial class Play : Page, INotifyPropertyChanged
    {
        private ObservableCollection<uint> _Numbers = [8, 7, 6, 5, 4, 3, 2, 1];
        public ObservableCollection<uint> Numbers
        {
            get => _Numbers;
            set
            {
                HashSet<uint> Check = new(value.Where(n => n <= 8 && n > 0).Take(8));
                if (!_Numbers.SequenceEqual(Check))
                {
                    _Numbers = new(Check);
                    OnPropertyChanged(nameof(Numbers));
                }
            }
        }
        private ObservableCollection<char> _Letters = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];

        public ObservableCollection<char> Letters
        {
            get => _Letters;
            set
            {
                HashSet<char> Check = new(value.Where(l => l >= 'a' && l <= 'h').Take(8));
                if (!_Letters.SequenceEqual(Check))
                {
                    _Letters = new(Check);
                    OnPropertyChanged(nameof(Letters));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
        public Play()
        {
            InitializeComponent();
            DataContext = this; 
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            TabControlBar.SelectedItem = TabOnPlay;
            BoardPlay.IsEnabled = true;
            BoardPlay.NewGame();
        }

        private void BtnResign_Click(object sender, RoutedEventArgs e)
        {
            TabControlBar.SelectedItem = TabOnStart;
            BoardPlay.IsEnabled = false;
            Sounds.PlaySound(Resource.game_end);
        }
        private void BtnBackMove_Click(object sender, RoutedEventArgs e)
        {
            BoardPlay.GameState.UnMakeMove();
        }

        private void BtnNextMove_Click(object sender, RoutedEventArgs e)
        {
            BoardPlay.GameState.MakeMove();
        }
    }
}