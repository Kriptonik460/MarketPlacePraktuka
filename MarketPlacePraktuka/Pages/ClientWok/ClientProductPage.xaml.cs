using MarketPlacePraktuka.Models;
using MarketPlacePraktuka.Pages.Salesmen;
using MaterialDesignThemes.Wpf;
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
    /// Логика взаимодействия для ClientProductPage.xaml
    /// </summary>
    public partial class ClientProductPage : Page
    {


        private Orientation _orientation = Orientation.Horizontal;

        public ClientProductPage()
        {
            DataContext = this;
            InitializeComponent(); 
            App.DB.Product.Load();
            App.DB.Basket.Local.ToList();
            App.DB.Client.Local.ToList();
            RefreshProductList();
           

        }
      
        private void RefreshProductList() =>
            ListProduct.ItemsSource = App.DB.Product.Where( p => p.ID_Status == 1).ToList() ;


        public ICommand _addCommand;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand<Product>((Product product ) =>
        {
           if((SaveSomeData.user.Client.FirstOrDefault().Basket.FirstOrDefault(b => b.Status == true) == null))
            {
                App.DB.Basket.Add(new Basket
                    {
                    Client = SaveSomeData.user.Client.FirstOrDefault(),
                    Status = true
                });
                App.DB.SaveChanges();
                SaveSomeData.basket = App.DB.Basket.FirstOrDefault(pl => pl.ID_Client == SaveSomeData.client.ID && pl.Status == true);
            }
            else
            { 
                if(product.Count == 0)
                {
                    MessageBox.Show("Вы не можете заказать больше, так как данный товар отсутствует у поставщика");
                }
                else
                {

                product.Count--;
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
                SaveSomeData.basket = basket;
                }
            }

        }));
        public ICommand _delCommand;
        public ICommand DelCommand => _delCommand ?? (_delCommand = new RelayCommand<Product>((Product product) =>
        {
            
            product.Count++;
            var productListItem = App.DB.ProductList.FirstOrDefault(pl => pl.ID_Product == product.ID && pl.Basket.Status == true);
            if (productListItem.Count > 0)
            {
            productListItem.Count--;
                
                if(productListItem.Count == 0)
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

       
    }
}
