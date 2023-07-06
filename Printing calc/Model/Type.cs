using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_calc.Model
{
    internal class Type : INotifyPropertyChanged
    {

        private string? printingType;
        private string[] types = { "Листовая печать", "Визитки", "Брошюры", "Визитки", "Плакаты", "Папки", "Наклейки" };
        private Format[] capableFormats = {  };

        public string SelectedType
        {
            get { return SelectedType; }
            set
            {
                SelectedType = value;
                OnPropertyChanged("PrintingType");
            }
        }

        public string[] Types
        {
            get { return types; }
            set
            { 
                types = value;
                OnPropertyChanged("Types");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
