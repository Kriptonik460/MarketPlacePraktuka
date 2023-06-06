using MarketPlacePraktuka.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace MarketPlacePraktuka.Pages.Salesmen
{
    /// <summary>
    /// Логика взаимодействия для DiagrammaSalesmenForProduct.xaml
    /// </summary>
    public partial class DiagrammaSalesmenForProduct : Page
    {

        public LiveCharts.SeriesCollection SeriesCollection { get; set; }

        public DiagrammaSalesmenForProduct() 
        {
            InitializeComponent();
           
            var salesman = App.DB.Salesman.ToList().First(p => p.ID_User == SaveSomeData.user.ID);

            SeriesCollection = new LiveCharts.SeriesCollection();
            foreach (var item in App.DB.Product.Where(e => e.ID_Salesman == salesman.ID))
            {
                var seria = new ColumnSeries
                {
                    Title = item.ID + " " + item.Name.ToString(),
                    Values = new ChartValues<double> { (double)item.ProductListOrder.Sum(r => r.Count) }
                };
                SeriesCollection.Add(seria);
            }

            var Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            Func<double, string> Formatter = value => value.ToString("N");

            // привязываем данные к элементам управления в разметке
            this.DataContext = this;


        }
          

    }
}
