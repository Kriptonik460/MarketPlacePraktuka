using MarketPlacePraktuka.Models;
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

namespace MarketPlacePraktuka.Pages.Salesmen
{
    /// <summary>
    /// Логика взаимодействия для PageInfoSalesmen.xaml
    /// </summary>
    public partial class PageInfoSalesmen : Page
    {
        
        public PageInfoSalesmen()
        {
            InitializeComponent();
            App.DB.Salesman.Load();
            salesman = App.DB.Salesman.ToList().First(p => p.ID_User == SaveSomeData.user.ID);
            NameComp.Text = salesman.NameCompany.ToString();
            Discr.Text = salesman.Description.ToString();
            DatePick.Text = salesman.DateOnMarketplace.ToString();
            Log.Text = salesman.User.Login.ToString();
            pass.Text = salesman.User.Password.ToString();
            SaveChangeInBase.Visibility = Visibility.Collapsed;

        }




        public Salesman salesman
        {
            get { return (Salesman)GetValue(salesmanProperty); }
            set { SetValue(salesmanProperty, value); }
        }

        // Using a DependencyProperty as the backing store for salesman.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty salesmanProperty =
            DependencyProperty.Register("salesman", typeof(Salesman), typeof(PageInfoSalesmen));







        private void SaveChangeInBase_Click(object sender, RoutedEventArgs e)
        {
            salesman.NameCompany = NameComp.Text;
            salesman.Description = Discr.Text;
            salesman.DateOnMarketplace = DatePick.SelectedDate;
            App.DB.SaveChangesAsync();
           
            SaveChangeInBase.Visibility = Visibility.Collapsed;
        }

       

      

        private void PassCorrect_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (pass.Text == PassCorrect.Text)
            {
                SaveChangeInBase.Visibility = Visibility.Visible;
            }
            else
            {
                SaveChangeInBase.Visibility = Visibility.Collapsed;
            }
        }
    }
}
