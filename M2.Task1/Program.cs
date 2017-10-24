using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    string a = Console.ReadLine();
                    Console.WriteLine(FirstSymbol.GetFirst(a));
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Please, type something");

                }
            }
        }
    }
}
