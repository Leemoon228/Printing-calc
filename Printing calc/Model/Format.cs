using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_calc.Model
{
    public class Format : INotifyPropertyChanged
    {
        private string? name;
        public static string[] possibleFormats = { "A0", "A1", "A2", "A3", "A4", "A5" };
        private decimal cost = 1;
        private decimal secondSide = 1;
        public int Id;
        private Boolean perPage = false;
        private Boolean bothSides = false;

        private List<string> notCapable;


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public decimal BaseCost
        {
            get { return cost; }
            set
            {
                cost = value;
                OnPropertyChanged("BaseCost");
            }
        }

        public decimal SecondSide
        {
            get { return secondSide; }
            set
            {
                secondSide = value;
                OnPropertyChanged("SecondSide");
            }
        }


        public Boolean PerPage
        {
            get { return perPage; }
            set
            {
                perPage = value;
                OnPropertyChanged("PerPage");
            }
        }
        public Boolean BothSides
        {
            get { return bothSides; }
            set
            {
                bothSides = value;
                OnPropertyChanged("BothSides");
            }
        }
        public List<string> NotCapable
        {
            get { return notCapable; }
            set
            {
                notCapable = value;
                OnPropertyChanged("NotCapable");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
