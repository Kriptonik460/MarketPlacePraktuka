using MarketPlacePraktuka.Models;
using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace MarketPlacePraktuka.Pages.Salesmen
{
    /// <summary>
    /// Логика взаимодействия для SettingPageSaleaman.xaml
    /// </summary>
    public partial class SettingPageSaleaman : Page
    {
        
        public SettingPageSaleaman()
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
        }
       

        public List<Address> addresses
        {
            get { return (List<Address>)GetValue(addressesProperty); }
            set { SetValue(addressesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty addressesProperty =
            DependencyProperty.Register("addresses", typeof(List<Address>), typeof(Address));



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

        //<Canvas m:MapLayer.Position="55.80172776728095, 49.17723998396573" x:Name="PointMap"
        //            m:MapLayer.PositionOrigin="BottomCenter" Width="30" Height="30">

        //        <materialDesign:PackIcon Kind = "MapMarker" Width="30" Height="30" Foreground="Red"/>
        //    </Canvas>

        private ICommand _navigateToAddressCommand;
        public ICommand NavigateToAddressCommand => _navigateToAddressCommand ?? (_navigateToAddressCommand = new RelayCommand<Address>(NavigateToPoint));
        private void NavigateToPoint(Address address)
        {
            PanelAdress.Visibility = Visibility.Visible;
            Address = address;

            NameAdress.Text = Address.Name;
            LatPoint.Text = Address.lat.ToString();
            LotPoint.Text = Address.lot.ToString();

            var latitude = (double)address.lat;
            var longitude = (double)address.lot;
            Location newLocation = new Location(latitude, longitude);
            Map.SetView(newLocation, 16);
            Map.Center = newLocation;
        }

        private void AddAdress_Click(object sender, RoutedEventArgs e)
        {
            PanelAdress.Visibility = Visibility.Visible;
            Address = new Address();
        }
         
        public ObservableCollection<List<Address>> Adresses { get; private set; } = new ObservableCollection<List<Address>>();
        private async void SaveAdress_Click(object sender, RoutedEventArgs e)
        {
            if (Address.ID == 0)
            {
                Address.Name = NameAdress.Text;
                Address.lat = Convert.ToDecimal(LatPoint.Text);
                Address.lot = Convert.ToDecimal(LotPoint.Text);
                App.DB.Address.Add(Address);
            }
            await App.DB.SaveChangesAsync();
            NavigationService.Refresh();
            PanelAdress.Visibility = Visibility.Collapsed;
            PointAdress.ItemsSource = Adresses; 
        }

        private async void DeleAdress_Click(object sender, RoutedEventArgs e)
        {
            App.DB.Address.Remove(Address);
            await App.DB.SaveChangesAsync();
            PointAdress.ItemsSource = App.DB.Address.ToList();
            PanelAdress.Visibility = Visibility.Collapsed;
        }
    }
}
