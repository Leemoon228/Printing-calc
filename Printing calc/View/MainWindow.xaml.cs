using Printing_calc.ViewModel;
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

namespace Printing_calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PrintingCostCalculatorViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new PrintingCostCalculatorViewModel();
            DataContext = viewModel;
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NumberOfPages = Int32.Parse(Empty_check(txtNumberOfPages.Text));
            viewModel.PricePerPage = Decimal.Parse(Empty_check(txtPricePerPage.Text));
            viewModel.CalculatePrintingCost();
        }

        private static string Empty_check(string input)
        {
            string str = new string(input.Where(c => char.IsDigit(c)).ToArray());
            if (str=="")
                return "0";
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }
    }
}
