using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test
{
    [TestClass]
    public class FrameworkTest
    {
        [DataRow("Object extension methods")]
        [DataTestMethod]
        public void TestNamingConvention(string aaa)
        {
            Dictionary<string, string[]> pairs = new Dictionary<string, string[]>();

            Debug.WriteLine(string.Empty);
            Debug.WriteLine($"### {aaa}:");
            Debug.WriteLine($"|cannot|must|");
            Debug.WriteLine($"|--|--|");

            foreach (var type in typeof(IValidatable).Assembly.GetTypes()
                                                              .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(ValidationAttribute)))
                                                              .OrderBy(x => x.Name))
            {
                string name;

                name = type.Name.Replace("Cannot", string.Empty).Replace("Must", string.Empty);

                if (!pairs.ContainsKey(name))
                {
                    pairs.Add(name, new string[2]);
                }

                if (type.Name.StartsWith("Cannot"))
                {
                    pairs[name][0] = type.Name;
                }
                else if (type.Name.StartsWith("Must"))
                {
                    pairs[name][1] = type.Name;
                }
            }

            foreach (var values in pairs.Values)
            {
                Debug.WriteLine($"|{values[0]}|{values[1]}|");
            }
        }
    }
}