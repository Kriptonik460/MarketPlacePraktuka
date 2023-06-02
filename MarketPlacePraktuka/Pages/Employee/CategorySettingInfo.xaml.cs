using MarketPlacePraktuka.Models;
using MarketPlacePraktuka.Pages.Salesmen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для CategorySettingInfo.xaml
    /// </summary>
    public partial class CategorySettingInfo : Page
    {
        public CategorySettingInfo()
        {
            InitializeComponent();
            DataContext = this;

            App.DB.Product.Load();
            RefreshProductList();
        }



        public Category category1
        {
            get { return (Category)GetValue(category1Property); }
            set { SetValue(category1Property, value); }
        }

        // Using a DependencyProperty as the backing store for category1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty category1Property =
            DependencyProperty.Register("category1", typeof(Category), typeof(Category));



        private void RefreshProductList() =>
            CategoryList.ItemsSource = App.DB.Category.ToList();
        public ObservableCollection<List<Category>> Categorys { get; private set; } = new ObservableCollection<List<Category>>();
        private void SaveCat_Click(object sender, RoutedEventArgs e)
        {
            category1 = new Category();
            if (category1.ID == 0)
            {
                category1.Name = NameText.Text;
                App.DB.Category.Add(category1);
            }
            App.DB.SaveChanges();
            RefreshProductList();

        }

        private void NewCat_Click(object sender, RoutedEventArgs e)
        {
            StackAdd.Visibility = Visibility.Visible;
            NewCat.Visibility = Visibility.Collapsed;
        }
        private ICommand _changeCommand;
        public ICommand ChangeCommand => _changeCommand ?? (_changeCommand = new RelayCommand<Category>((Category category) =>
        {
            StackAdd.Visibility = Visibility.Visible;
            NewCat.Visibility = Visibility.Collapsed;
            NameText.Text = category.Name;
          

            RefreshProductList();
        }));

        public ICommand _delCommand;
        public ICommand DelCommand => _delCommand ?? (_delCommand = new RelayCommand<Category>((Category category) =>
        {
           App.DB.Category.Remove(category);
            App.DB.SaveChanges();

            RefreshProductList();
        }));

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            StackAdd.Visibility = Visibility.Collapsed;
            NewCat.Visibility = Visibility.Visible;
        }
    }
}
