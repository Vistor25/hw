using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace FileSystem
{
    public class FileSystemVisitor: IEnumerable
    {
        private List<string> dirs = new List<string>();

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
            catch(DirectoryNotFoundException e)
            {

            }
        
            dirs.AddRange(files);

            foreach (var directiry in directories)
            {
                
                dirs.Add(directiry);
                
                GetDerictories(directiry);
            }
            
        }

    

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<string> GetEnumerator()
        {
           foreach(var element in dirs)
            {
                yield return element;
            }
        }
    }
}
