using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_calc.Model
{
    internal class Type
    {

        private string? printingType;
        private string[] types = { "A0", "A1", "A2", "A3", "A4", "A5" };

        public string PrintingType
        {
            get { return printingType; }
            set
            {
                printingType = value;
                OnPropertyChanged("PrintingType");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
