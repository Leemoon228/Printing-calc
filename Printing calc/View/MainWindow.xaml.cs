using Printing_calc.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            string editedText = txtPricePerPage.Text.Replace(".", ",");
            if (editedText != "")

                viewModel.PricePerPage = Decimal.Parse(editedText);
            else
                viewModel.PricePerPage = 0;



            viewModel.CalculatePrintingCost();
        }

        private static string Empty_check(string input)
        {
            string str = new string(input.Where(c => char.IsDigit(c)).ToArray());
            if (str=="")
                return "0";
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        private void Add_ProductType_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Add_Format_Click(object sender, RoutedEventArgs e)
        {

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void FloatNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^\\d*[,.]?\\d*$");
            e.Handled = !regex.IsMatch(e.Text); // I dont know why, but somehow that deals exact what i need (float number in textbox)
        }

        private void UsersPriceUsage(object sender, RoutedEventArgs e)
        {
            foreach (UIElement item in EachCostStack.Children)
            {
                item.IsEnabled = true;
            }
        }
        private void UsersPriceNotUsage(object sender, RoutedEventArgs e)
        {
            foreach (UIElement item in EachCostStack.Children)
            {
                if (item is CheckBox)
                    continue;
                item.IsEnabled = false;
                if (item is TextBox textBoxItem)
                    if (viewModel.SelectedFormat != null && viewModel.SelectedType != null)
                        textBoxItem.Text = (viewModel.SelectedFormat.BaseCost + viewModel.SelectedType.BaseCost).ToString();
                    else
                        textBoxItem.Text = "";
            }
        }
    }
}
