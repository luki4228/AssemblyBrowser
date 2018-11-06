using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public class AssemblyInfoProcessor
    {
        private Assembly asm;

        public AssemblyInfoProcessor(Assembly _asm)
        {
            asm = _asm;
        }

        public Assembly Assembly
        {
            get
            {
                return asm;
            }
            private set { }
        }

        public AssemblyResult GetStructeredInfo()
        {
            List<Type> asmTypes = new List<Type>(GetLoadableTypes());
            List<string> namespaces = new List<string>(asmTypes.Where(item => item.Namespace != null).Select(t => t.Namespace).Distinct());
            List<NamespaceResult> namespacesResult = new List<NamespaceResult>();
            foreach (string namesp in namespaces)
            {
                List<Type> namespaceTypes = new List<Type>(asmTypes.Where(t => t.Namespace == namesp && !t.Name.Contains("<>c") && !Regex.Match(t.Name, @"^[<]\w+[>]*", RegexOptions.IgnoreCase).Success));
                List<TypeResult> typeResults = new List<TypeResult>();
                foreach (Type type in namespaceTypes)
                {
                    TypeResult typeRes = new TypeResult(type);
                    typeResults.Add(typeRes);
                }
                NamespaceResult namespRes = new NamespaceResult(namesp, new List<TypeResult>(typeResults));
                namespacesResult.Add(namespRes);
            }
            return new AssemblyResult(new List<NamespaceResult>(namespacesResult));
        }

        private IEnumerable<Type> GetLoadableTypes()
        {
            if (Assembly == null) throw new ArgumentNullException(nameof(Assembly));
            try
            {
                return Assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}
