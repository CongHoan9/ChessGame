using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ChessGame
{
    public partial class PromotionControl : UserControl
    {
        public event Action<PieceType?> PieceSelected;
        public PromotionControl(Player player)
        {
            InitializeComponent();
            DataContext = this;
            Queen.Source = new BitmapImage(new Uri(Images.GetImage(player, PieceType.Queen)));
            Knight.Source = new BitmapImage(new Uri(Images.GetImage(player, PieceType.Knight)));
            Rook.Source = new BitmapImage(new Uri(Images.GetImage(player, PieceType.Rook)));
            Bishop.Source = new BitmapImage(new Uri(Images.GetImage(player, PieceType.Bishop)));
            if (player == Player.White)
            {
                GridSelect.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                GridSelect.RowDefinitions[4].Height = new GridLength(43.75);
                Grid.SetRow(Queen, 0);
                Grid.SetRow(Knight, 1);
                Grid.SetRow(Rook, 2);
                Grid.SetRow(Bishop, 3);
                Grid.SetRow(BtnClose, 4);
            }
            else if (player == Player.Black)
            {
                GridSelect.RowDefinitions[0].Height = new GridLength(43.75);
                GridSelect.RowDefinitions[4].Height = new GridLength(1, GridUnitType.Star);
                Grid.SetRow(Queen, 4);
                Grid.SetRow(Knight, 3);
                Grid.SetRow(Rook, 2);
                Grid.SetRow(Bishop, 1);
                Grid.SetRow(BtnClose, 0);
            }
        }
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(null);
        }
        private void Queen_Click(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Queen);
        }
        private void Knight_Click(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Knight);
        }
        private void Rook_Click(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Rook);
        }
        private void Bishop_Click(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Bishop);
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            PieceSelected?.Invoke(null);
        }
    }
}
