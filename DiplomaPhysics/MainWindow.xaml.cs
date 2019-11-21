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

namespace DiplomaPhysics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int AmplifierNumbers;
        private double Intensity;
        private double StrengthIndicator;
        private double NoisePower;
        private double AreaLength;
        private int NumberOfPieces;
        public MainWindow()
        {
            
            InitializeComponent();
            AmplifiersNumberTextBox.Text = "4";
            PiecesNumberTextBox.Text = "10";
            AreaLengthTextBox.Text = "150";
            StrengthIndicatorTextBox.Text = "5.3";
            IntensityTextBox.Text = "11.3";
            NoisePowerTextBox.Text = "1";

        }

        private void PastingHandlerInt(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsValidInt(text)) e.CancelCommand();
            }
            else e.CancelCommand();
        }
         private void PastingHandlerDouble(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsValidInt(text)) e.CancelCommand();
            }
            else e.CancelCommand();
        }

        private void TextBox_PreviewTextInputInt(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidInt(((TextBox)sender).Text + e.Text);
        }

        public static bool IsValidInt(string str)
        {
            int i;
            return int.TryParse(str, out i) && i <= 9999999;
        } 
        
        private void TextBox_PreviewTextInputDouble(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidDouble(((TextBox)sender).Text + e.Text);
        }

        public static bool IsValidDouble(string str)
        {
            double i;
            return double.TryParse(str, out i) && i <= 99999999;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AmplifierNumbers = int.Parse(AmplifiersNumberTextBox.Text);
                Intensity = double.Parse(IntensityTextBox.Text);
                StrengthIndicator = double.Parse(StrengthIndicatorTextBox.Text);
                NoisePower = double.Parse(NoisePowerTextBox.Text);
                AreaLength = double.Parse(AreaLengthTextBox.Text);
                NumberOfPieces = int.Parse(PiecesNumberTextBox.Text);
            }
            catch (Exception)
            {
                ValidationMessage.Visibility = Visibility.Visible;
            }

            if (AmplifierNumbers == 0 || Intensity == 0 || StrengthIndicator == 0 || NoisePower == 0 || AreaLength == 0 || NumberOfPieces == 0)
            {
                ValidationMessage.Visibility = Visibility.Visible;
            }
            else
            {
                var logic = new Logic(AmplifierNumbers, Intensity, StrengthIndicator, NoisePower, AreaLength, NumberOfPieces);
                var chartWindow = new Chart(logic);
                chartWindow.Show();
                Close();

            }

        }
    }
}
