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

        public event Action Start;
        public event Action Finish;
        public event Action DirectoryFound;
        public event Action FileFound;

        private Predicate<string> _filter; 
        public FileSystemVisitor() { }
        public FileSystemVisitor(Predicate<string> filter)
        {
            _filter = filter;
        }
        private void GetDerictories(string directiryName)
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
            foreach (var file in files)
            {
                OnFileFound();
            }
        
            dirs.AddRange(files);

            foreach (var directiry in directories)
            {
                OnDirectoryFound();
                dirs.Add(directiry);
                
                GetDerictories(directiry);
            }
            
        }

        public void GetAllFilesAndDirectories(string directoryname)
        {
            OnStart();
            GetDerictories(directoryname);
            OnFinish();
        }

        public void Filter()
        {
            dirs.Where(item => _filter(item));
        }
        protected virtual void OnStart() => Start?.Invoke();
        protected virtual void OnFinish() => Finish?.Invoke();
        protected virtual void OnDirectoryFound() => DirectoryFound?.Invoke();
        protected virtual void OnFileFound() => FileFound?.Invoke();
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
