using MarketPlacePraktuka.Models;
using MarketPlacePraktuka.Pages.Salesmen;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
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

namespace MarketPlacePraktuka.Pages.Employee
{
    /// <summary>
    /// Логика взаимодействия для UserPageControl.xaml
    /// </summary>
    public partial class UserPageControl : Page
    {
        public UserPageControl()
        {
            InitializeComponent();
            DataContext = this;

            App.DB.User.Load();
            App.DB.Client.Load();
            App.DB.Salesman.Load();
            RefreshProductList();

            
           
        }
        private void RefreshProductList() =>
          UserList.ItemsSource = App.DB.Client.ToList();





        public Client client
        {
            get { return (Client)GetValue(clientProperty); }
            set { SetValue(clientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for client.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty clientProperty =
            DependencyProperty.Register("client", typeof(Client), typeof(UserPageControl));



        public User user
        {
            get { return (User)GetValue(userProperty); }
            set { SetValue(userProperty, value); }
        }

        // Using a DependencyProperty as the backing store for user.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty userProperty =
            DependencyProperty.Register("user", typeof(User), typeof(UserPageControl));






        public ObservableCollection<List<Client>> clients { get; private set; } = new ObservableCollection<List<Client>>();


        public ICommand _delCommand;
        public ICommand DelCommand => _delCommand ?? (_delCommand = new RelayCommand<Client>((Client client) =>
        {
            client.User.Removed = true;
            App.DB.SaveChanges();

            RefreshProductList();
        }));

        public ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand<Client>((Client client) =>
        {
            client.User.Removed = false;
            App.DB.SaveChanges();

            RefreshProductList();
        }));


    }

    public class BoolPresenter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b == false)
                return null;

            return b ? "В бане" : "Живет";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
