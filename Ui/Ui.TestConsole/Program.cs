using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ui.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TestPropertyChanged();
            Console.ReadLine();
        }

        private static void TestPropertyChanged()
        {
            var test = new TestClass();
            test.PropertyChanged += (s, e) =>
            {
                Console.WriteLine($"Property {e.PropertyName} has changed. New value is [{test.SomeProperty}].");
            };
            test.SomeProperty = "Hello World";
            test.SomeProperty = "Hello World";
            test.SomeProperty = "Hello World again!";
        }
    }
}
