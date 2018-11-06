using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Browser
{
    public class DataType : INotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<string> typeInfo;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ObservableCollection<string> TypeInfo
        {
            get
            {
                return typeInfo;
            }
            set
            {
                typeInfo = new ObservableCollection<string>(value);
                OnPropertyChanged(nameof(Properties));
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
