using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public class NamespaceResult
    {
        private string namespaceName;
        private List<TypeResult> dataTypes;

        public NamespaceResult(string name, List<TypeResult> types)
        {
            namespaceName = name;
            dataTypes = new List<TypeResult>(types);
        }

        public string Name => namespaceName;

        public List<TypeResult> DataTypeResult
        {
            get
            {
                return dataTypes;
            }
            set
            {
                dataTypes = new List<TypeResult>(value);
            }
        }
    }
}
