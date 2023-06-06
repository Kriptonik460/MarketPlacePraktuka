using MarketPlacePraktuka.Models;
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
        public static MainPageClientWok Instance { get; private set; }
        public MainPageClientWok()
        {
            InitializeComponent();
            Instance = this;
            SaveSomeData.basket = App.DB.Basket.FirstOrDefault(pl => pl.ID_Client == SaveSomeData.client.ID && pl.Status == true);
            FrameUser.Navigate(new ClientProductPage());
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
            FrameUser.Navigate(new ProductListPage());
        }

       

        private void EmojiManProfile_Click(object sender, RoutedEventArgs e)
        {
            FrameUser.Navigate(new ClientInfoPage());
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            FrameUser.Navigate(new ClientProductPage());
        }
    }
}
