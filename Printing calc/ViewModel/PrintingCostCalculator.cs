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
using static System.Formats.Asn1.AsnWriter;
using System.Windows.Documents;
using System.Xml.Linq;
using System.Windows.Media;
using System.Printing;

namespace Printing_calc.ViewModel
{
    public class PrintingCostCalculatorViewModel : INotifyPropertyChanged
    {
        private int numberOfPages;
        private decimal currentPricePerPage;
        private decimal totalCost;
        private ProductType selectedType;
        private Format selectedFormat;

        private List<ProductType> ProductTypes = new List<ProductType>();
        private List<Format> ProductFormats = new List<Format>();
        private List<ProductColour> ProductColors = new List<ProductColour>();
        public ObservableCollection<ProductType> CurrentProductTypes { get; set; }
        public ObservableCollection<Format> CurrentProductFormats { get; set; }
        private Dictionary<string, bool> CapableThings = new Dictionary<string, bool>();
        private List<List<string>> ListsNotCapable = new List<List<string>>();

        public PrintingCostCalculatorViewModel()
        {
            CurrentProductTypes = new ObservableCollection<ProductType>();
            CurrentProductFormats = new ObservableCollection<Format>();



            GetData();
            CurrentProductTypes = new ObservableCollection<ProductType>(ProductTypes);
            //SelectedType = CurrentProductTypes[0];

            //UpdateNotCapable();
            //SelectedFormat = CurrentProductFormats[0];




        }

        private void GetData()
        {
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                string sqlExpression = "SELECT * FROM ProductTypes ORDER BY Name DESC";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    ProductTypes.Clear();
                    if (reader.HasRows) // если есть данные
                    {

                        while (reader.Read())   // построчно считываем данные
                        {
                            var id = Convert.ToInt32(reader.GetValue(0));
                            var name = (string)reader.GetValue(1);
                            var baseCost = (string)reader.GetValue(2);
                            var perPage = Convert.ToBoolean(reader.GetValue(3));
                            var notCapable = ((string)reader.GetValue(4)).Split('|').ToList();
                            ProductTypes.Add(new ProductType { Id = id, Name = name, BaseCost = decimal.Parse(baseCost), PerPage = perPage, NotCapable = notCapable });
                            CapableThings.Add(name, true);
                        }
                    }
                }


                sqlExpression = "SELECT * FROM Formats ORDER BY Name DESC";
                command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    ProductFormats.Clear();
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var id = Convert.ToInt32(reader.GetValue(0));
                            var name = (string)reader.GetValue(1);
                            var baseCost = (string)reader.GetValue(2);
                            var perPage = Convert.ToBoolean(reader.GetValue(3));
                            var bothSides = Convert.ToBoolean(reader.GetValue(4));
                            var twoSideAdditionalCost = (string)reader.GetValue(5);
                            var notCapable = ((string)reader.GetValue(6)).Split('|').ToList();
                            ProductFormats.Add(new Format { Id = id, Name = name, BaseCost = decimal.Parse(baseCost), PerPage = perPage, BothSides = bothSides, SecondSide=decimal.Parse(twoSideAdditionalCost), NotCapable = notCapable });
                            CapableThings.Add(name, true);
                        }
                    }
                }



                connection.Close();
            }
        }

        public void UpdateNotCapable(string changed)
        {

            if (selectedType != null && !ListsNotCapable.Contains(selectedType.NotCapable))
                ListsNotCapable.Add(selectedType.NotCapable);


            if (selectedFormat!= null && !ListsNotCapable.Contains(selectedFormat.NotCapable))
                ListsNotCapable.Add(selectedFormat.NotCapable);

            if (changed == "SelectedType")
                UpdateFormat();

            if (changed == "SelectedType")
                UpdateColors();



            /*            if (!CurrentProductFormats.Contains(SelectedFormat))
                            SelectedFormat = CurrentProductFormats[0];*/

            /*List<Format> toRemove = new List<Format>();
            foreach(Format i in CurrentProductFormats)
            {
                if (NotCapableThings.Contains(i.Name))
                    toRemove.Add(i);
            }
            foreach (Format i in toRemove)
                CurrentProductFormats.Remove(i);
            if (!CurrentProductFormats.Contains(SelectedFormat))
                SelectedFormat = CurrentProductFormats[0];

            foreach(string a in SelectedFormat.NotCapable) // Adding not capable things from format
                NotCapableThings.Add(a);*/
        }

        void UpdateFormat() {
            CurrentProductFormats.Clear();
            foreach (Format newFormat in ProductFormats)
            {
                bool flag = true;
                foreach (List<string> ListNotCapableStrings in ListsNotCapable)
                    foreach (string NotCapableThing in ListNotCapableStrings)
                        if (NotCapableThing == newFormat.Name)
                            flag = false;
                if (flag)
                    CurrentProductFormats.Add(newFormat);
            }
        }

        void UpdateColors() { 
        
        }



        public ProductType SelectedType
        {
            get { return selectedType; }
            set
            {
                if (SelectedType!=null) 
                    ListsNotCapable.Remove(SelectedType.NotCapable);
                selectedType = value;
                UpdateNotCapable("SelectedType");
                OnPropertyChanged("SelectedType");
            }
        }
        public Format SelectedFormat
        {
            get { return selectedFormat; }
            set
            {
                if (SelectedFormat!=null)
                    ListsNotCapable.Remove(SelectedFormat.NotCapable);
                selectedFormat = value;
                UpdateNotCapable("SelectedFormat");
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
