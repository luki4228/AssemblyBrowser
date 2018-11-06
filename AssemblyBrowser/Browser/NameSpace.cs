﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace Browser
{
    public class NameSpace : INotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<DataType> types;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ObservableCollection<DataType> Types
        {
            get
            {
                return types;
            }
            set
            {
                types = new ObservableCollection<DataType>(value);
                OnPropertyChanged(nameof(Types));
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
