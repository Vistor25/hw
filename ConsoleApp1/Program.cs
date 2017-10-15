using FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirname = Console.ReadLine();
            FileSystemVisitor fl = new FileSystemVisitor();
            fl.Start += () => Console.WriteLine("Started");
            fl.Finish += () => Console.WriteLine("Finished");
            fl.FileFound += () => Console.WriteLine("File was founded");
            fl.DirectoryFound += () => Console.WriteLine("Directory was founded");
            fl.GetAllFilesAndDirectories(dirname);
            
            
            foreach(var name in fl)
            {
                Console.WriteLine(name);
            }
            Console.ReadLine();
        }
    }
}
