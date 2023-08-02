using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_calc.Model
{
    public class ProductType : INotifyPropertyChanged
    {

        private string name;
        public static string[] types = { "Листовая печать", "Визитки", "Брошюры", "Плакаты", "Папки", "Наклейка", "Флаер", "Методички" };
/*        private Format[] notCapableFormats = { };*/
        private decimal baseCost = 1;
        private Boolean perPage = false;
        private List<string> notCapable;

        public int Id;

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
            get { return baseCost; }
            set
            {
                baseCost = value;
                OnPropertyChanged("BaseCost");
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

        public List<string> NotCapable
        {
            get { return notCapable; }
            set
            {
                notCapable = value;
                OnPropertyChanged("NotCapable");
            }
        }


        /*        public Format[] NotCapableFormats
                {
                    get { return notCapableFormats; }
                    set
                    {
                        notCapableFormats = value;
                        OnPropertyChanged("Types");
                    }
                }*/


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
