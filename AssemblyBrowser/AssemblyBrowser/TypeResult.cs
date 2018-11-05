using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AssemblyBrowser
{
    public class TypeResult
    {
        private Type type;
        private string name;
        private List<string> methods;
        private List<string> fields;
        private List<string> properties;

        public TypeResult(Type t)
        {
            type = t;
            name = t.IsGenericType ? PrettyGenericName(t) : t.Name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<string> Methods
        {
            get { return GetMethodsSignature(); }
            set { methods = new List<string>(value); }
        }

        public List<string> Fields
        {
            get
            {
                List<string> res = new List<string>();
                foreach (FieldInfo field in type.GetFields())
                {
                    string typeName = field.FieldType.Name;
                    if (field.FieldType.IsGenericType)
                    {
                        typeName = PrettyGenericName(field.FieldType);
                    }
                    string fieldPlusType = String.Format("{0} - {1}", field.Name, typeName);
                    res.Add(fieldPlusType);
                }
                return res;
            }
            set { fields = new List<string>(value); }
        }

        public List<string> Properties
        {
            get
            {
                List<string> res = new List<string>();
                foreach (PropertyInfo property in type.GetProperties())
                {
                    string typeName = property.PropertyType.Name;
                    if (property.PropertyType.IsGenericType)
                    {
                        typeName = PrettyGenericName(property.PropertyType);
                    }
                    string propertyPlusType = string.Format("{0} - {1}", property.Name, typeName);
                    res.Add(propertyPlusType);
                }
                return res;
            }
            set { properties = new List<string>(value); }
        }

        private List<string> GetMethodsSignature()
        {
            List<MethodInfo> methods = new List<MethodInfo>(type.GetMethods().Where(m => !m.IsSpecialName));
            List<string> result = new List<string>();
            foreach (MethodInfo method in methods)
            {
                result.Add(MethodSignature(method));
            }
            return result;
        }

        private string MethodSignature(MethodInfo mi)
        {
            String[] param = mi.GetParameters()
                             .Select(p => String.Format("{0} {1}",
                             p.ParameterType.IsGenericType ? PrettyGenericName(p.ParameterType) : p.ParameterType.Name,
                             p.Name))
                             .ToArray();


            string signature = String.Format("{0} {1}({2})",
                mi.ReturnType.IsGenericType ? PrettyGenericName(mi.ReturnType) : mi.ReturnType.Name,
                mi.Name, String.Join(",", param));

            return signature;
        }

        private string PrettyGenericName(Type type)
        {
            if (type.GetGenericArguments().Length == 0)
            {
                return type.Name;
            }
            var genericArguments = type.GetGenericArguments();
            var typeDefeninition = type.Name;
            int indexOfSep = typeDefeninition.IndexOf("`");
            var unmangledName = typeDefeninition.Substring(0, indexOfSep > 0 ? indexOfSep : typeDefeninition.Length);
            return unmangledName + "<" + String.Join(",", genericArguments.Select(PrettyGenericName)) + ">";
        }

    }
}
