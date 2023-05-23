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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MarketPlacePraktuka.Models;
using MarketPlacePraktuka.Pages.Client;
using MarketPlacePraktuka.Pages.Salesman;

namespace MarketPlacePraktuka.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        #endregion
        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            new Regustr().Show();
            Close();
        }

        private void Avtor_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTb.Text.Trim() == "" || PasswordTb.Text.Trim() == "")
            {
                _ = MessageBox.Show("Заполните все поля", "Уведомления", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
            User cd = App.DB.User.FirstOrDefault(x => x.Login == LoginTb.Text.Trim() && x.Password == PasswordTb.Text.Trim());
            if (cd == null)
            {
                MessageBox.Show("Неверная авторизация");
            }
            else
            {
                SaveSomeData.user = cd;
                LoginTb.Text = string.Empty;
                PasswordTb.Text = string.Empty;
                    if (App.DB.Client.FirstOrDefault(x => x.User.ID == cd.ID) != null)
                    {
                        new MainPageClient().Show();
                        Close();
                    }
                    if (App.DB.Salesman.FirstOrDefault(x => x.User.ID == cd.ID) != null)
                    {
                        new MainPageSalesman().Show();
                        Close();
                    }
                }

            }
        }
           
        
    }
}
