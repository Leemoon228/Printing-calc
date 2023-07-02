using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_calc.Model
{
    internal class Format: INotifyPropertyChanged
    {
        private string? formatType;
        private string[] types = {"A0", "A1", "A2", "A3", "A4", "A5"};

        public string FormatType
        {
            get { return formatType; }
            set
            {
                formatType = value;
                OnPropertyChanged("FormatType");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
