using MarketPlacePraktuka.Models;
using MarketPlacePraktuka.Pages.Salesmen;
using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для ClientPageMap.xaml
    /// </summary>
    public partial class ClientPageMap : Page
    {

        public static Address address1;
        public ClientPageMap()
        {
            InitializeComponent();
            App.DB.Address.Load();
            Map.CredentialsProvider = new ApplicationIdCredentialsProvider("jmnQMChUewby61QPp8yX~UybthbuvuTzuf1YdRN_Orw~Ar6qcJOEb8zAFDtoD9ruug36AYHRHXqrPeIbXHFYT0K50oPfxXpGmbzDcrNx-YrO");
            var addresses = App.DB.Address.ToList();
            PointAdress.ItemsSource = App.DB.Address.ToList();

            addresses.ForEach(a =>
            {
                var canvas = new Canvas();
                canvas.SetValue(MapLayer.PositionProperty, new Location((double)a.lat, (double)a.lot));
                canvas.SetValue(MapLayer.PositionOriginProperty, PositionOrigin.BottomCenter);
                canvas.Width = canvas.Height = 30;
                var pointIcon = new PackIcon()
                {
                    Kind = PackIconKind.MapMarker,
                    Width = 30,
                    Height = 30,
                    Foreground = Brushes.Red
                };
                canvas.Children.Add(pointIcon);
                Map.Children.Add(canvas);
            });

            var firstAddress = addresses.First();
            var initialLocation = new Location((double)firstAddress.lat, (double)firstAddress.lot);
            //Map.Center = initialLocation;
            Map.SetView(initialLocation, 16);



            NameCard.Text = SaveSomeData.client.Surname;
            DayCarg.Text = Convert.ToString(SaveSomeData.client.Month);
            YearCarg.Text = Convert.ToString(SaveSomeData.client.Year);
            string text = SaveSomeData.client.NumberOfCreditCard;
            string newText = "";
            for (int i = 0; i < text.Length; i++)
            {
                if ((i + 1) % 4 == 0) // если текущий символ - это каждый четвертый символ
                    newText += text[i] + " "; // добавляем к новому тексту пробел после текущего символа
                else
                    newText += text[i]; // иначе добавляем только текущий символ к новому тексту
            }

            TextBlock1.Text = newText; 
            SurnameTxt.Text = SaveSomeData.client.Surname;
            NameTxt.Text = SaveSomeData.client.Name;
            PatTxt.Text = SaveSomeData.client.Patronymic;
        }


    



        public Address Address
        {
            get { return (Address)GetValue(addressProperty); }
            set { SetValue(addressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for address.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty addressProperty =
            DependencyProperty.Register("address", typeof(Address), typeof(Address));



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var search = Search.Text.Trim();
            var SearchList = App.DB.Address.Where(p => p.Name.Contains(search)).ToList();
            PointAdress.ItemsSource = SearchList;
        }

       

        private ICommand _navigateToAddressCommand;
        public ICommand NavigateToAddressCommand => _navigateToAddressCommand ?? (_navigateToAddressCommand = new RelayCommand<Address>(NavigateToPoint));
        private void NavigateToPoint(Address address)
        {

            Address = address;
            var latitude = (double)address.lat;
            var longitude = (double)address.lot;
            Location newLocation = new Location(latitude, longitude);
            Map.SetView(newLocation, 16);
            Map.Center = newLocation;
            address1 = address;
            AdressOrder.Text = address.Name;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (address1 == null)
            {
                MessageBox.Show("Вы не выбрали точку выдачи");
            }
            else
            {
                var neworder = new Order()
                {
                ID_Address = address1.ID,
                ID_Client = SaveSomeData.client.ID
                };
                App.DB.Order.Add(neworder);
               
                foreach (var  item in App.DB.ProductList.Where(s => s.ID_Basket == SaveSomeData.basket.ID).ToList())
                {
                    App.DB.ProductListOrder.Add(new ProductListOrder()
                    {
                        ID_Order = neworder.ID,
                        ID_Product = item.ID_Product,
                        Count = item.Count
                    });
                    App.DB.ProductList.Remove(item);
                }
                SaveSomeData.basket.Status = false;
                App.DB.SaveChanges();
                MessageBox.Show("Заказ оформлен");
                MainPageClientWok.Instance.FrameUser.Navigate(new ClientProductPage());
            }
        }
    }
}
