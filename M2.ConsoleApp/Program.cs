using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tas2;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string a = Console.ReadLine();
                Console.WriteLine(ParseNumber.Parse(a));
            }
            //string a = "(54)";
            //out int b = 0;
            //Int32.TryParse(a,b);
        }
    }
}
