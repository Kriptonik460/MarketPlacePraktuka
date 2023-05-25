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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace MarketPlacePraktuka.Pages.Salesman
{
    public partial class MainPageSalesman : Window
    {
        public MainPageSalesman()
        {
            InitializeComponent();
            FrameSalesmen.Navigate(new ProductPageSalesmen());

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

      

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            FrameSalesmen.Navigate(new SettingPageSaleaman());
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            FrameSalesmen.Navigate(new ProductPageSalesmen());
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


