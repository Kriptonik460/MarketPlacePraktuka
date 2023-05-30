using MarketPlacePraktuka.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public static MainPageSalesman Instance { get; private set; }
        public MainPageSalesman()
        {
            InitializeComponent();
            Instance = this;
            FrameSalesmen.Navigate(new ProductPageSalesmen());
            App.DB.Salesman.Load();
            NameCompany.Text = App.DB.Salesman.FirstOrDefault(s => s.User.ID == SaveSomeData.user.ID).NameCompany;
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

        private void AnimateTextBlock()
        {
            // Создаем анимацию смещения по оси X
            DoubleAnimation slideAnimation = new DoubleAnimation();
            slideAnimation.From = myBorder.ActualWidth;
            slideAnimation.To = -NameCompany.ActualWidth;
            slideAnimation.Duration = TimeSpan.FromSeconds(2);
            slideAnimation.Completed += (s, e) =>
            {
                // По завершении первой части анимации изменяем текст и запускаем ее заново
                NameCompany.Text = App.DB.Salesman.FirstOrDefault(g => g.User.ID == SaveSomeData.user.ID).NameCompany;
                slideAnimation.From = myBorder.ActualWidth;
                slideAnimation.To = -NameCompany.ActualWidth;
                NameCompany.BeginAnimation(Canvas.LeftProperty, slideAnimation);
            };

            // Добавляем анимацию к свойству Canvas.Left элемента TextBlock
            NameCompany.BeginAnimation(Canvas.LeftProperty, slideAnimation);
        }





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


