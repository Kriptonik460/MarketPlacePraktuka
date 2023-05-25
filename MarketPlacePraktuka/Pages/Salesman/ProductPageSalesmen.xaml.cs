using MarketPlacePraktuka.Models;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarketPlacePraktuka.Pages.Salesman
{
    /// <summary>
    /// Логика взаимодействия для ProductPageSalesmen.xaml
    /// </summary>
    public partial class ProductPageSalesmen : Page
    {

        List<CheckBox> checkBoxes = new List<CheckBox>();
        public List<Product> Products
        {
            get { return (List<Product>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductsProperty =
            DependencyProperty.Register("Products", typeof(List<Product>), typeof(Product));


        public ProductPageSalesmen()
        {
            Products = App.DB.Product.Where(p => p.Salesman.ID_User == SaveSomeData.user.ID).ToList();
            
            InitializeComponent();
            
            foreach (var categoria in App.DB.Category)
            {
                var checkBox = new CheckBox();
                checkBox.Foreground = new SolidColorBrush(Colors.White);
                checkBox.Content = categoria.Name;
                checkBox.DataContext = categoria;
                checkBox.IsChecked = true;
                checkBox.Checked += CheckBox_Checked;
                checkBox.Unchecked += CheckBox_Unchecked;
                Categ.Children.Add(checkBox);
                checkBoxes.Add(checkBox);
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var itemCategoria = (sender as CheckBox).DataContext as Category;
            Products.AddRange(App.DB.Product.Where(p => p.Category.ID == itemCategoria.ID));
            ListProduct.ItemsSource = Products;
            ListProduct.Items.Refresh();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var itemCategoria = (sender as CheckBox).DataContext as Category;
            foreach(var item in App.DB.Product.Where(p => p.Category.ID == itemCategoria.ID))
            {
                Products.Remove(item);
            }
            ListProduct.ItemsSource = Products;
            ListProduct.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           var idProduct =  ((sender as Button).DataContext as Product).ID;
            int currentColumn = Grid.GetColumnSpan(ListProduct);
            if (currentColumn != 1)
            { 
                Grid.SetColumnSpan(ListProduct, currentColumn - 1);
                Opusanue.Visibility = Visibility.Visible;
            }
          
        }

        private void MinBut2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListProduct.SelectedItem = null;
        }

        private void da_Checked(object sender, RoutedEventArgs e)
        {

        }


    }
}
