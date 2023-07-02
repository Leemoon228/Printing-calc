using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_calc.ViewModel
{
    public class PrintingCostCalculatorViewModel : INotifyPropertyChanged
    {
        private int numberOfPages;
        private decimal pricePerPage;
        private decimal totalCost;

        public int NumberOfPages
        {
            get { return numberOfPages; }
            set
            {
                numberOfPages = value;
                OnPropertyChanged("NumberOfPages");
            }
        }

        public decimal PricePerPage
        {
            get { return pricePerPage; }
            set
            {
                pricePerPage = value;
                OnPropertyChanged("PricePerPage");
            }
        }

        public decimal TotalCost
        {
            get { return totalCost; }
            set
            {
                totalCost = value;
                OnPropertyChanged("TotalCost");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CalculatePrintingCost()
        {
            TotalCost = NumberOfPages * PricePerPage;
        }
    }
}
