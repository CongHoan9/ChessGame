using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ChessGame
{
    /// <summary>
    /// Interaction logic for ChessMain.xaml
    /// </summary>
    public partial class ChessMain : Window
    {
        readonly Play Play = new();
        public ChessMain()
        {
            InitializeComponent();
            MainFrame.Navigate(Play);
        }
        protected override void OnInitialized(EventArgs e)
        {
            MainFrame.Navigate(Play);
            base.OnInitialized(e);
        }
        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(Play);
        }

        private void BtnPuzzles_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
