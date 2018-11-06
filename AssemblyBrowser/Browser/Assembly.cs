using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Browser
{
    public class Assembly : INotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<NameSpace> namespaces;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ObservableCollection<NameSpace> Namespaces
        {
            get { return namespaces; }
            set
            {
                namespaces = new ObservableCollection<NameSpace>(value);
                OnPropertyChanged(nameof(Namespaces));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
