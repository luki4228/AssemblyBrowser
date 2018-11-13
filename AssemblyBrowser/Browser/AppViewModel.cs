using AssemblyBrowser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Browser
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private Assembly selectedAssembly;

        public ObservableCollection<Assembly> Assemblies { get; set; }
        public Assembly SelectedAssembly
        {
            get { return selectedAssembly; }
            set
            {
                selectedAssembly = value;
                OnPropertyChanged(nameof(SelectedAssembly));
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(OpenAssembly));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand((obj) =>
                    {
                        if (selectedAssembly != null)
                        {
                            Assemblies.Remove(selectedAssembly);
                        }
                    }));
            }
        }

        private IAssemblyProcessor assemblyProcessorService;
        public AppViewModel()
        {
            assemblyProcessorService = new AssemblyProcessor();
            Assemblies = new ObservableCollection<Assembly>();
        }

        private void OpenAssembly(object param)
        {
            Assembly choosenAsm = assemblyProcessorService.Process();
            if (choosenAsm != null)
            {
                if (!(Assemblies.Where(t => t.Name == choosenAsm.Name).Count() != 0))
                {
                    Assemblies.Add(choosenAsm);
                    SelectedAssembly = choosenAsm;
                }
                else
                {
                    MessageBox.Show("Assembly with the same name is already in the list!");
                }
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
