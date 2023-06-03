using MarketPlacePraktuka.Pages.Salesmen;
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

namespace MarketPlacePraktuka.Pages.ClientWok
{
    /// <summary>
    /// Логика взаимодействия для MainPageClientWok.xaml
    /// </summary>
    public partial class MainPageClientWok : Window
    {
        public MainPageClientWok()
        {
            InitializeComponent();
        }

        #region Функционал сверху
        private void MinBut2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void MinBut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {

            this.DragMove();

        }
        #endregion

        private void Basket_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Map_Click(object sender, RoutedEventArgs e)
        {
            FrameUser.Navigate(new SettingPageSaleaman());
        }

        private void EmojiManProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            FrameUser.Navigate(new ClientProductPage());
        }
    }
}
