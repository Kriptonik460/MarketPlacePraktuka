using MarketPlacePraktuka.HeplClasses;
using MarketPlacePraktuka.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace MarketPlacePraktuka.Pages.Salesman
{
    /// <summary>
    /// Логика взаимодействия для ProductPageSalesmen.xaml
    /// </summary>
    /// 
    public partial class ProductPageSalesmen : Page
    {


        List<CheckBox> checkBoxes = new List<CheckBox>();
        List<CheckBox> checkBoxes2 = new List<CheckBox>();
        public List<Product> Products
        {
            get { return (List<Product>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductsProperty =
            DependencyProperty.Register("Products", typeof(List<Product>), typeof(Product));

        


        public Product TempProduct
        {
            get { return (Product)GetValue(TempProductProperty); }
            set { SetValue(TempProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TempOroduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TempProductProperty =
            DependencyProperty.Register(nameof(TempProduct), typeof(Product), typeof(ProductPageSalesmen), new PropertyMetadata(null));

        public readonly IReadOnlyList<IFilter<Product>> Filters = new List<IFilter<Product>>()
        {
            new Filter<Product, Category>(product => product.Category),
            new Filter<Product, Status>(product => product.Status),
        }.AsReadOnly();

        public static List<PhotoProduct> photos = new List<PhotoProduct>();

        public byte[] Photo
        {
            get { return (byte[])GetValue(PhotoProperty); }
            set { SetValue(PhotoProperty, value); }
        }

        public static readonly DependencyProperty PhotoProperty =
            DependencyProperty.Register("Photo", typeof(byte[]), typeof(ProductPageSalesmen));

        public static int IndexPhoto = 0;




        public IEnumerable<CheckBox> this[int index]
        {
            get
            {
                IFilter<Product> filter = Filters[index];

                return filter.PossibleValues.Select(value =>
                {
                    var checkBox = new CheckBox()
                    {
                        Foreground = new SolidColorBrush(Colors.White),
                        FontSize = 20,
                        Content = value,
                        Margin = new Thickness(10),
                        IsChecked = false,
                    };

                    checkBox.Checked += delegate
                    {
                        filter.Add(value);
                        View.Refresh();
                    };

                    checkBox.Unchecked += delegate
                    {
                        filter.Remove(value);
                        View.Refresh();
                    };

                    return checkBox;
                });
            }
        }

        public ICollectionView View { get; }

        public ProductPageSalesmen()
        {
            App.DB.Product.LoadAsync();
            View = CollectionViewSource.GetDefaultView(App.DB.Product.Local);

            View.Filter = (value) => !Filters.Any() || Filters.All(filter => filter.IsAccepted(value as Product));
            View.Refresh();

            InitializeComponent();


            Categor.ItemsSource = App.DB.Category.Local.ToList();
            /*
                        foreach (var categoria in App.DB.Category)
                        {
                            var checkBox = new CheckBox
                            {
                                Foreground = new SolidColorBrush(Colors.White),
                                FontSize = 20,
                                Content = categoria.Name,
                                DataContext = categoria,
                                IsChecked = true
                            };
                            checkBox.Checked += CheckBox_Checked;
                            checkBox.Unchecked += CheckBox_Unchecked;
                            Categ.Children.Add(checkBox);
                            checkBoxes.Add(checkBox);
                        }
                        foreach (var status in App.DB.Status)
                        {
                            var checkBox2 = new CheckBox
                            {
                                Foreground = new SolidColorBrush(Colors.White),
                                FontSize = 20,
                                Content = status.Name,
                                DataContext = status,
                                IsChecked = true
                            };
                            checkBox2.Checked += CheckBox_Checked2;
                            checkBox2.Unchecked += CheckBox_Unchecked2;
                            Status.Children.Add(checkBox2);
                            checkBoxes.Add(checkBox2);
                        }
            */
        }

        private void RefreshItemsSource(Button addButton)
        {
            ListProduct.ItemsSource = App.DB.Product.Where(p => p.Salesman.ID_User == SaveSomeData.user.ID).ToList()
                                                                .Cast<object>()
                                                                .Append(addButton);
        }
        /* #region Категория
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
                 Products?.Remove(item);
             }
             ListProduct.ItemsSource = Products;
             ListProduct.Items.Refresh();
         }
         #endregion
         #region Status
         private void CheckBox_Checked2(object sender, RoutedEventArgs e)
         {
             var itemStatus = (sender as CheckBox).DataContext as Status;
             Products.AddRange(App.DB.Product.Where(p => p.Status.ID == itemStatus.ID));
             ListProduct.ItemsSource = Products;
             ListProduct.Items.Refresh();
         }

         private void CheckBox_Unchecked2(object sender, RoutedEventArgs e)
         {
             var itemStatus = (sender as CheckBox).DataContext as Status;
             foreach (var itemSt in App.DB.Product.Where(p => p.Status.ID == itemStatus.ID))
             {
                 Products?.Remove(itemSt);
             }
             ListProduct.ItemsSource = Products;
             ListProduct.Items.Refresh();
         }
         #endregion*/


        private void MinBut2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListProduct.SelectedItem = null;
        }

        private void ListProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemProduct = ListProduct.SelectedItem as Product;
            TempProduct = itemProduct;
            photos = itemProduct.PhotoProduct.ToList();
            if(photos.Count() == 0)
            {
                return;
            }
            Photo = photos[IndexPhoto].Photo;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TempProduct = new Product();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            if (TempProduct.ID == 0)
            {
                TempProduct.ID_Salesman = App.DB.Salesman.FirstOrDefault(s => s.User.ID == SaveSomeData.user.ID).ID;
                TempProduct.Removed = false;
                TempProduct.ID_Status = 3;
                App.DB.Product.Add(TempProduct);
            }
            App.DB.SaveChanges();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            App.DB.Product.Remove(TempProduct);
            App.DB.SaveChanges();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var product = App.DB.Product.Where(p => p.Name.Contains(Search.Text)).ToList();
            ListProduct.ItemsSource = product;
            View.Filter = (value) => !Filters.Any() || Filters.All(filter => filter.IsAccepted(value as Product));

            View.Refresh();
        }

        private void ImangeBtn_Click(object sender, RoutedEventArgs e)
        {
            byte[] imageGetInExplorer = ImageConverter.OpenFileDialogSave();
            App.DB.PhotoProduct.Add(new PhotoProduct
            {
                ID_Product = TempProduct.ID,
                Photo = imageGetInExplorer
            });
            App.DB.SaveChangesAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IndexPhoto--;
                Photo = photos[IndexPhoto].Photo;
            }
            catch
            {
                IndexPhoto = 0;
                MessageBox.Show("Ты ждолбаёёёб");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                IndexPhoto++ ;
                Photo = photos[IndexPhoto].Photo;
            }
            catch
            {
                IndexPhoto = 0;
                MessageBox.Show("Ты ждолбаёёёб");
            }
        }
    }
}
