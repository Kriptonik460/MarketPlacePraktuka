using MarketPlacePraktuka.Models;
using MarketPlacePraktuka.Pages.Salesmen;
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

namespace MarketPlacePraktuka.Pages.Employee
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            DataContext = this;

            App.DB.Product.Load();
            RefreshProductList();
        }

        private void RefreshProductList() =>
            ListProduct.ItemsSource = App.DB.Product.Where(e => e.ID_Status == 3).ToList();


        private ICommand _yesCommand;
        public ICommand YesCommand => _yesCommand ?? (_yesCommand = new RelayCommand<Product>((Product product) =>
        {
            product.ID_Status = 1;
            App.DB.SaveChanges();

            RefreshProductList();
        }));

        public ICommand _noCommand;
        public ICommand NoCommand => _noCommand ?? (_noCommand = new RelayCommand<Product>((Product product) =>
        {
            product.ID_Status = 2;
            App.DB.SaveChanges();

            RefreshProductList();
        }));
    }
}
