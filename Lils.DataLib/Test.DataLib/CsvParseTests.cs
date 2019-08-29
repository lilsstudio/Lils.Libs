using System;
using Lils.DataLib.Csv;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.DataLib
{
    [TestClass]
    public class CsvParseTests
    {
        class FruitState
        {
            public string Name { get; set; }
            public string Price { get; set; }
            public string Inventory { get; set; }
        }

        [TestMethod]
        public void BaseTest()
        {
            var csvString =
@"
Name,Price,Inventory
Apple,5,100
Berry,10,30
Banana,6,50
";

            var fruits = CsvConvert.DeserializeCollection<FruitState>(csvString);
            foreach (var fruit in fruits)
            {
                Console.WriteLine($"{fruit.Name}\t{fruit.Price}\t{fruit.Inventory}");
            }
        }
    }
}
