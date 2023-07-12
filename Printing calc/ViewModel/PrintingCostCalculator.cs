using Printing_calc.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Printing_calc.ViewModel
{
    public class PrintingCostCalculatorViewModel : INotifyPropertyChanged
    {
        private int numberOfPages;
        private decimal currentPricePerPage;
        private decimal totalCost;
        private ProductType selectedType;
        private Format selectedFormat;
        public ObservableCollection<ProductType> ProductTypes { get; set; }
        public ObservableCollection<Format> ProductFormats { get; set; }
        private string[] notCapableThings;

        public PrintingCostCalculatorViewModel()
        {
            ProductTypes = new ObservableCollection<ProductType> //TODO бд с сохраненными типами
            {
                new ProductType { Name = ProductType.types[1], BaseCost = 2M},
                new ProductType { Name = ProductType.types[2], BaseCost = 3.5M},
                new ProductType { Name = ProductType.types[3], BaseCost = 4M}
            };
            SelectedType = ProductTypes[0];

            ProductFormats = new ObservableCollection<Format>
            { 
                new Format { CurrentFormat = "A4", Cost=10},
                new Format { CurrentFormat = "A5", Cost=7},
                new Format { CurrentFormat = "A6", Cost=5}
            };
            SelectedFormat = ProductFormats[0];
            //CreateDataBase();



        }

        private void GetData()
        {

        }

/*        public static void CreateDataBase()
        {
            using (var connection = new SqliteConnection("Data Source=scoresdata.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "CREATE TABLE ProductTypes(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, BaseCost TEXT NOT NULL, PerPage INTEGER)";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE Formats(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, BaseCost TEXT NOT NULL, PerPage INTEGER)";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE Colors(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, BaseCost TEXT NOT NULL, PerPage INTEGER)";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }*/


        public ProductType SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }
        public Format SelectedFormat
        {
            get { return selectedFormat; }
            set
            {
                selectedFormat = value;
                OnPropertyChanged("SelectedFormat");
            }
        }

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
            get { return currentPricePerPage; }
            set
            {
                currentPricePerPage = value;
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



        public void CalculatePrintingCost()
        {
            TotalCost = NumberOfPages * PricePerPage;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
