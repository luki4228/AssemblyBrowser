using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public class AssemblyResult
    {
        private List<NamespaceResult> namespaces;

        public AssemblyResult(List<NamespaceResult> namespacesResults)
        {
            namespaces = namespacesResults;
        }

        public List<NamespaceResult> Namespaces
        {
            get { return namespaces; }
            set { namespaces = new List<NamespaceResult>(value); }
        }
    }
}
