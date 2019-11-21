using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using LiveCharts;
using LiveCharts.Wpf;

namespace DiplomaPhysics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Chart : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private readonly Logic _logic;

        public Chart(Logic logic)
        {
            InitializeComponent();
            _logic = logic;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Сигнал",
                    Values = new ChartValues<double>(_logic.CalcBer(_logic.Amplifiers, _logic.Noises))
                }
            };

            var labels = new List<string>();
            for(var i = 1; i <= _logic.Amplifiers.Length; i++)
            {
                labels.Add("Посилювач " + i);
            }

            Labels = labels.ToArray();
            //YFormatter = value => value.ToString("C");

            DataContext = this;
        }

    }
}
