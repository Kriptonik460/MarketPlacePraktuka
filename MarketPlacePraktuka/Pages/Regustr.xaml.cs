using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MarketPlacePraktuka.Models;
using MarketPlacePraktuka.Pages;

namespace MarketPlacePraktuka.Pages
{
    /// <summary>
    /// Логика взаимодействия для Regustr.xaml
    /// </summary>
    public partial class Regustr : Window
    {
        MarketPlacePraktuka.Models.Client client;

        MarketPlacePraktuka.Models.Salesman salesman;
        User user;
        
        public Regustr()
        {
            InitializeComponent();
            

            TypeUserCB.SelectionChanged += (sender, e) =>
            {
                switch (TypeUserCB.SelectedIndex)
                {
                    case 0:
                        NameField.Text = "Имя";
                        FieldsNamesClient.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        NameField.Text = "Название компании";
                        FieldsNamesClient.Visibility = Visibility.Hidden;
                        break;
                }
            };
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
            if (TypeUserCB.SelectedIndex == 0)
            {
                if ((string.IsNullOrEmpty(PatrTb.Text)) && (string.IsNullOrEmpty(LoginTb.Text)) && (string.IsNullOrEmpty(PasswordTb.Text)) && (string.IsNullOrEmpty(NameTb.Text)) && (string.IsNullOrEmpty(FamTb.Text)))
                {
                    MessageBox.Show("Поля пустые");
                    return;

                }
                user = new User()
                {
                    Login = LoginTb.Text.Trim(),
                    Password = PasswordTb.Text.Trim()
                };
                App.DB.User.Add(user);
                client = new MarketPlacePraktuka.Models.Client()
                {
                    Name = NameTb.Text.Trim(),
                    Surname = FamTb.Text.Trim(),
                    Patronymic = PatrTb.Text.Trim(),
                    ID_User = user.ID
                };
                App.DB.Client.Add(client);
                App.DB.SaveChanges();
                new MainWindow().Show();
                Close();

            }



            else
            {

                if ((string.IsNullOrEmpty(NameField.Text)) && (string.IsNullOrEmpty(LoginTb.Text)) && (string.IsNullOrEmpty(PasswordTb.Text)))
                {
                    MessageBox.Show("Поля пустые");
                    return;
                }
                user = new User()
                {
                    Login = LoginTb.Text.Trim(),
                    Password = PasswordTb.Text.Trim()
                };
                App.DB.User.Add(user);
                salesman = new MarketPlacePraktuka.Models.Salesman()
                {
                    Username = NameTb.Text.Trim(),
                    ID_User = user.ID
                };
                App.DB.Salesman.Add(salesman);
                App.DB.SaveChanges();
                new MainWindow().Show();
                Close();



            }
        }
        #region Проверка Полей
        private void ValidationLoginPassword(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender; string input = textBox.Text;
            // Паттерн для проверки латинских символов
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            // Если не прошло проверку, то удаляем последний символ из текстбокса
            if (!regex.IsMatch(input))
            {
                int caretPosition = textBox.SelectionStart - 1;
                if (caretPosition >= 0)
                {
                    textBox.Text = textBox.Text.Remove(caretPosition, 1); textBox.SelectionStart = caretPosition;
                    textBox.SelectionLength = 0;
                }
            }
        }
        private void ValidationUser(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender; string input = textBox.Text;
            // Паттерн для проверки латинских символов
            Regex regex = new Regex("^[а-яА-Я]*$");
            // Если не прошло проверку, то удаляем последний символ из текстбокса
            if (!regex.IsMatch(input))
            {
                int caretPosition = textBox.SelectionStart - 1;
                if (caretPosition >= 0)
                {
                    textBox.Text = textBox.Text.Remove(caretPosition, 1); textBox.SelectionStart = caretPosition;
                    textBox.SelectionLength = 0;
                }
            }
        }
        #endregion

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
