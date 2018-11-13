using AssemblyBrowser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser
{
    public class AssemblyProcessor : IAssemblyProcessor
    {
        public Assembly Process()
        {
            IService service = new Service();
            string asmPath = service.OpenFileDialog();
            if (asmPath != null)
            {
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);
                System.Reflection.Assembly asm = System.Reflection.Assembly.ReflectionOnlyLoadFrom(asmPath);
                AssemblyInfoProcessor asmProcessor = new AssemblyInfoProcessor(asm);
                AssemblyResult asmRes = asmProcessor.GetStructeredInfo();
                ObservableCollection<NameSpace> namespaces = new ObservableCollection<NameSpace>();
                foreach (NamespaceResult namesp in asmRes.Namespaces)
                {
                    ObservableCollection<DataType> types = new ObservableCollection<DataType>();
                    foreach (TypeResult typeRes in namesp.DataTypeResult)
                    {
                        DataType type = new DataType
                        {
                            Name = typeRes.Name,
                            TypeInfo = new ObservableCollection<string>(typeRes.Fields.Concat(typeRes.Properties).Concat(typeRes.Methods))
                        };
                        types.Add(type);
                    }
                    NameSpace nmsp = new NameSpace { Name = namesp.Name, Types = types };
                    namespaces.Add(nmsp);
                }
                Assembly result = new Assembly { Name = asm.GetName().Name, Namespaces = namespaces };
                return result;
            }
            return null;
        }

        static System.Reflection.Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
        }
    }
}
