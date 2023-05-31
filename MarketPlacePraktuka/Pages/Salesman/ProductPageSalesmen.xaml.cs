using MarketPlacePraktuka.HeplClasses;
using MarketPlacePraktuka.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            DependencyProperty.Register(nameof(TempProduct), typeof(Product), typeof(ProductPageSalesmen));

        public readonly IReadOnlyList<IFilter<Product>> Filters = new List<IFilter<Product>>()
        {
            new Filter<Product, Category>(product => product.Category),
            new Filter<Product, Status>(product => product.Status),
        }.AsReadOnly();

        public readonly IReadOnlyList<Searching<Product>> Searchings = new List<Searching<Product>>()
        {
            new Searching<Product>(product => product.Name)
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

        private string _searchingText = "";
        private RelayCommand<Product> _selectionCommand;

        public RelayCommand<Product> SelectionCommand =>
            _selectionCommand ?? (_selectionCommand = new RelayCommand<Product>(product => TempProduct = product));

        public string SearchingText
        {
            get => _searchingText;
            set
            {
                _searchingText = value;
                View.Refresh();
            }
        }

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

            View.Filter = (value) => (!Filters.Any() || Filters.All(filter => filter.IsAccepted(value as Product))) &&
                                     (!Searchings.Any() || Searchings.All(searching => searching.IsAccepted(value as Product, SearchingText)));
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

            TempProduct = null;
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
            App.DB.SaveChangesAsync();

        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            App.DB.Product.Remove(TempProduct);
            App.DB.SaveChangesAsync();
            TempProduct = null;
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
            View.Refresh();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IndexPhoto--;
                PhotoImg.Source = ImageConverter.ConvertToImageSource(photos[IndexPhoto].Photo);
                RightBtn.Visibility = Visibility.Visible;
            }
            catch
            {
                LeftBtn.Visibility = Visibility.Collapsed;
                
               
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                IndexPhoto++;
                PhotoImg.Source = ImageConverter.ConvertToImageSource(photos[IndexPhoto].Photo);
                LeftBtn.Visibility = Visibility.Visible;
            }
            catch
            {
                RightBtn.Visibility = Visibility.Collapsed;
               
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {


                LeftBtn.Visibility = Visibility.Visible;
                RightBtn.Visibility = Visibility.Visible;
                IndexPhoto = 0;
                if (!((sender as Button).DataContext is Product itemProduct))
                    return;
                TempProduct = itemProduct;
            if (App.DB.PhotoProduct.FirstOrDefault(d => d.ID_Product == TempProduct.ID) != null)
            {

                photos = itemProduct.PhotoProduct.ToList();
                if (photos.Count() == 0)
                {
                    return;
                }
                PhotoImg.Source = ImageConverter.ConvertToImageSource(photos[0].Photo);
            }
            else
            {
                LeftBtn.Visibility = Visibility.Collapsed;
                RightBtn.Visibility = Visibility.Collapsed;
                PhotoImg.Source = new BitmapImage { UriSource = new Uri(@"C:\Users\user\source\repos\MarketPlacePraktuka\MarketPlacePraktuka\Source\free-icon-no-photo-5540531.png") };
            }

        }
        

        private void ImangeDelBtn_Click(object sender, RoutedEventArgs e)
        {

            App.DB.PhotoProduct.Remove(photos[IndexPhoto]);
            App.DB.SaveChangesAsync();

        }
    }

    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) != false;

        public void Execute(object parameter) => _execute?.Invoke((T)parameter);
    }
}
