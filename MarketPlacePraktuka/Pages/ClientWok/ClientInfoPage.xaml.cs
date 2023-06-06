using MarketPlacePraktuka.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarketPlacePraktuka.Pages.ClientWok
{
    /// <summary>
    /// Логика взаимодействия для ClientInfoPage.xaml
    /// </summary>
    public partial class ClientInfoPage : Page
    {
        
        public ClientInfoPage()
        {
            InitializeComponent();
            App.DB.Client.Local.ToList();
            App.DB.User.Local.ToList();
            client = SaveSomeData.client;
           
            
        }



        public Client client
        {
            get { return (Client)GetValue(clientProperty); }
            set { SetValue(clientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for client.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty clientProperty =
            DependencyProperty.Register("client", typeof(Client), typeof(ClientInfoPage));
    
        
        #region Валидация данных
        private void PatronomicComp_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = ((TextBox)sender).Text;

            // Создаем регулярное выражение
            Regex regex = new Regex("^[а-яА-ЯёЁ]+$");

            // Проверяем ввод пользователя на соответствие шаблону
            if (!regex.IsMatch(input))
            {
                // Если введены неверные символы, удаляем их из текстового поля
                ((TextBox)sender).Text = Regex.Replace(input, "[^а-яА-ЯёЁ]", "");
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
        }

        private void FamComp_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = ((TextBox)sender).Text;

            // Создаем регулярное выражение
            Regex regex = new Regex("^[а-яА-ЯёЁ]+$");

            // Проверяем ввод пользователя на соответствие шаблону
            if (!regex.IsMatch(input))
            {
                // Если введены неверные символы, удаляем их из текстового поля
                ((TextBox)sender).Text = Regex.Replace(input, "[^а-яА-ЯёЁ]", "");
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
        }

        private void NameComp_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = ((TextBox)sender).Text;

            // Создаем регулярное выражение
            Regex regex = new Regex("^[а-яА-ЯёЁ]+$");

            // Проверяем ввод пользователя на соответствие шаблону
            if (!regex.IsMatch(input))
            {
                // Если введены неверные символы, удаляем их из текстового поля
                ((TextBox)sender).Text = Regex.Replace(input, "[^а-яА-ЯёЁ]", "");
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            TextBox textBox = (TextBox)sender;

            if (!regex.IsMatch(textBox.Text))
            {
                // Если введены символы, отличные от цифр, то удаляем их из текста в поле ввода.
                textBox.Text = new string(textBox.Text.Where(char.IsDigit).ToArray());
                // Перемещаем курсор в конец поля ввода.
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void DayCarg_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            TextBox textBox = (TextBox)sender;

            if (!regex.IsMatch(textBox.Text))
            {
                // Если введены символы, отличные от цифр, то удаляем их из текста в поле ввода.
                textBox.Text = new string(textBox.Text.Where(char.IsDigit).ToArray());
                // Перемещаем курсор в конец поля ввода.
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void YearCarg_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            TextBox textBox = (TextBox)sender;

            if (!regex.IsMatch(textBox.Text))
            {
                // Если введены символы, отличные от цифр, то удаляем их из текста в поле ввода.
                textBox.Text = new string(textBox.Text.Where(char.IsDigit).ToArray());
                // Перемещаем курсор в конец поля ввода.
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void CVVCarg_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            TextBox textBox = (TextBox)sender;

            if (!regex.IsMatch(textBox.Text))
            {
                // Если введены символы, отличные от цифр, то удаляем их из текста в поле ввода.
                textBox.Text = new string(textBox.Text.Where(char.IsDigit).ToArray());
                // Перемещаем курсор в конец поля ввода.
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void PassCorrect_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (pass.Text == PassCorrect.Text)
            {
                SaveChangeInBase.Visibility = Visibility.Visible;
            }
            else
            {
                SaveChangeInBase.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        private void SaveChangeInBase_Click(object sender, RoutedEventArgs e)
        {
            App.DB.SaveChanges();
        }
    }
}
