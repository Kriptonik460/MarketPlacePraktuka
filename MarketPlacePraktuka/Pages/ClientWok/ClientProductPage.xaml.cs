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
    /// Логика взаимодействия для ClientProductPage.xaml
    /// </summary>
    public partial class ClientProductPage : Page
    {




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
            ListProduct.ItemsSource = App.DB.Product.ToList() ;


        public ICommand _addCommand;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand<Product>(async (Product product ) =>
        {
           if(SaveSomeData.user.Client.FirstOrDefault().Basket.FirstOrDefault() == null)
            {
                App.DB.Basket.Add(new Basket
                    {
                    Client = SaveSomeData.user.Client.FirstOrDefault(),
                });
                App.DB.SaveChanges();
            }
            else
            {
                var productListItem = App.DB.ProductList.FirstOrDefault(pl => pl.ID_Product == product.ID);
                var basket = App.DB.Basket.FirstOrDefault(pl => pl.ID_Client == SaveSomeData.client.ID);
                if (productListItem == null)
                    App.DB.ProductList.Add(new ProductList
                    {
                        ID_Basket = basket.ID,
                        ID_Product = product.ID,
                        Count = 1
                    });
                else
                    productListItem.Count++;    
              
              await  App.DB.SaveChangesAsync();
                RefreshProductList();
            }

        }));
        public ICommand _delCommand;
        public ICommand DelCommand => _delCommand ?? (_delCommand = new RelayCommand<Product>((Product product) =>
        {


            var productListItem = App.DB.ProductList.FirstOrDefault(pl => pl.ID_Product == product.ID);

            productListItem.Count--;

            await App.DB.SaveChangesAsync();
            RefreshProductList();
        }));



    }
}
