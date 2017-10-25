using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSysClassLibrary;

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
            FileSystemVisitor fl = new FileSystemVisitor(@"C:\Intel");
            fl.Start += () => Console.WriteLine("Started");
            fl.Finish += () => Console.WriteLine("Finished");
            fl.DirectoryFound += asasas;
            fl.FileFound += a;



            foreach (var name in fl)
            {
                Console.WriteLine(name);
            }
            Console.ReadLine();

            void asasas(object sender, DirectoryEventArgs arg)
            {
                if (arg.Directory.Name == "Logs")
                    arg.CanRun = false;
            }

            void a(object sender, FileEventArgs arg)
            {
                if (arg.File.Name == "cacerts")
                    arg.CanRun = false;
            }
        }
    }
}
