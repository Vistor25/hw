using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSystem
{
    public class FileSystemVisitor
    {
        public List<string> dirs = new List<string>();

        public void GetDerictories(string directiryName)
        {
            string[] directories = new string[] { };
            string[] files = new string[] { };


            try
            {
                directories = Directory.GetDirectories(directiryName);
                files = Directory.GetFiles(directiryName);
            }
            catch(UnauthorizedAccessException e)
            {
                
            }

            dirs.AddRange(files);

            foreach (var directiry in directories)
            {
                
                dirs.Add(directiry);
                
                GetDerictories(directiry);
            }
            
        }
    }
}
