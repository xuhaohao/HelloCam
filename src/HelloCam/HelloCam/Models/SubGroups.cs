using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCam.Models
{
    public class SubGroups : INotifyPropertyChanged
    {
        
        public string Id { get; set; }

        private string _name;
        public string Name {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public int Identity { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        virtual internal protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
