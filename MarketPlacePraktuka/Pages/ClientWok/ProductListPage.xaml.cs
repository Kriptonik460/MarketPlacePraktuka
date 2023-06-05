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

namespace MarketPlacePraktuka.Pages.ClientWok
{
    /// <summary>
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public ProductListPage()
        {
            DataContext = this;
            InitializeComponent();
            App.DB.Product.Load();
            App.DB.Basket.Local.ToList();
            App.DB.Client.Local.ToList();
            RefreshProductList();


        }

        private void RefreshProductList() =>
            ListProduct.ItemsSource = App.DB.ProductList.Where(e => e.ID_Basket == SaveSomeData.basket.ID).Select(p => p.Product).ToList();
        
        public ICommand _addCommand;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand<Product>((Product product) =>
        {
            if ((SaveSomeData.user.Client.FirstOrDefault().Basket.FirstOrDefault(b => b.Status == true) == null))
            {
                App.DB.Basket.Add(new Basket
                {
                    Client = SaveSomeData.user.Client.FirstOrDefault(),
                    Status = true
                });
                App.DB.SaveChanges();
            }
            else
            {
                var productListItem = App.DB.ProductList.FirstOrDefault(pl => pl.ID_Product == product.ID && pl.Basket.Status == true);
                var basket = (App.DB.Basket.FirstOrDefault(pl => pl.ID_Client == SaveSomeData.client.ID && pl.Status == true));
                if (productListItem == null)
                    App.DB.ProductList.Add(new ProductList
                    {
                        ID_Basket = basket.ID,
                        ID_Product = product.ID,
                        Count = 1
                    });
                else
                    productListItem.Count++;

                App.DB.SaveChanges();
                RefreshProductList();
            }

        }));
        public ICommand _delCommand;
        public ICommand DelCommand => _delCommand ?? (_delCommand = new RelayCommand<Product>((Product product) =>
        {


            var productListItem = App.DB.ProductList.FirstOrDefault(pl => pl.ID_Product == product.ID && pl.Basket.Status == true);
            if (productListItem.Count > 0)
            {
                productListItem.Count--;
                if (productListItem.Count == 0)
                {
                    App.DB.ProductList.Remove(productListItem);
                    App.DB.SaveChanges();
                    RefreshProductList();
                }
                App.DB.SaveChanges();
            }
            else
            {

                MessageBox.Show("В корзине не может быть отрицательное количество товара", "Ошибка", MessageBoxButton.YesNo);
            }
            RefreshProductList();
        }));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPageClientWok.Instance.FrameUser.Navigate(new ClientPageMap());
        }
    }
}
