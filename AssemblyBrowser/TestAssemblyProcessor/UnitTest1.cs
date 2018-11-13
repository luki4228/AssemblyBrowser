using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyBrowser;
using System.Reflection;
using System.Linq;

namespace TestAssemblyProcessor
{
    [TestClass]
    public class UnitTest1
    {
        private AssemblyResult asmResult;
        public string NameTest { get; set; }
        private Type testClassType;

        [TestInitialize]
        public void Initialize()
        {
            Assembly asm = Assembly.ReflectionOnlyLoadFrom("./TestAssemblyProcessor.dll");
            AssemblyInfoProcessor asmInfoProcessor = new AssemblyInfoProcessor(asm);
            asmResult = asmInfoProcessor.GetStructeredInfo();
            testClassType = typeof(UnitTest1);
        }

        [TestMethod]
        public void TestNamespacesIsNotNull()
        {
            Assert.IsNotNull(asmResult.Namespaces);
        }

        [TestMethod]
        public void TestNamespacesCount()
        {
            Assert.IsTrue(asmResult.Namespaces.Count > 0);
        }

        [TestMethod]
        public void TestNamespaceName()
        {
            Assert.AreEqual(asmResult.Namespaces[0].Name, nameof(TestAssemblyProcessor));
        }

        [TestMethod]
        public void TestNamespacesTypesCount()
        {
            foreach (NamespaceResult nmp in asmResult.Namespaces)
            {
                Assert.IsTrue(nmp.DataTypeResult.Count > 0);
            }
        }

        [TestMethod]
        public void TestTypeFieldsCount()
        {
            int actualCount = asmResult.Namespaces[0].DataTypeResult[0].Fields.Count;
            int expectedCount = testClassType.GetFields().Length;
            Assert.AreEqual(actualCount, expectedCount);
        }

        [TestMethod]
        public void TestTypePropertiesCount()
        {
            int actualCount = asmResult.Namespaces[0].DataTypeResult[0].Properties.Count;
            int expectedCount = testClassType.GetProperties().Length;

            Assert.AreEqual(actualCount, expectedCount);
        }

        [TestMethod]
        public void TestTypeMethodsCount()
        {
            int actualCount = asmResult.Namespaces[0].DataTypeResult[0].Methods.Count;
            int expectedCount = testClassType.GetMethods().Where(item => !item.IsSpecialName).Count();
            Assert.AreEqual(actualCount, expectedCount);
        }
    }
}
