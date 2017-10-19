using FileSysClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //DirectoryInfo dirPrograms = new DirectoryInfo(@"c:\progrm files");
            //foreach(var directory in dirPrograms.EnumerateDirectories())
            //{
            //    Console.WriteLine(directory.FullName);
            //}
            //Console.ReadLine();
            //string dirname = Console.ReadLine();
            FileSystemVisitor fl = new FileSystemVisitor(@"C:\Python27\Tools");
            fl.Start += () => Console.WriteLine("Started");
            fl.Finish += () => Console.WriteLine("Finished");
           


            foreach (var name in fl)
            {
                Console.WriteLine(name);
            }
            Console.ReadLine();
        }
    }
}
