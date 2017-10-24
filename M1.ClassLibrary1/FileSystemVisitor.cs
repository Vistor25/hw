using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace FileSysClassLibrary
{
    public class FileSystemVisitor: IEnumerable
    {
        private DirectoryInfo _rootDirectory;
        private bool Run = true;
        public event Action Start;
        public event Action Finish;
        public event EventHandler<DirectoryEventArgs> DirectoryFound;
        public event EventHandler<FileEventArgs> FileFound;
        public event EventHandler<DirectoryEventArgs> FilteredDirectoryFound;
        public event EventHandler<FileEventArgs> FilteredFileFound;

        private Predicate<DirectoryInfo> _directoryFilter;
        private Predicate<FileInfo> _fileFilter;
        public FileSystemVisitor(string rootFolder)
        {
            _rootDirectory = new DirectoryInfo(rootFolder);
        }
        public FileSystemVisitor(string rootFolder, Predicate<FileInfo> fileFilter, Predicate<DirectoryInfo> directoryFilter):this(rootFolder)
        {
            _fileFilter = fileFilter;
            _directoryFilter = directoryFilter;
        }
        private IEnumerable<string> GetDerictories(DirectoryInfo rootDirectory)
        {
            var directories = new List<DirectoryInfo>();
            try
            {
                directories = rootDirectory.EnumerateDirectories()?.ToList();
            }
            catch (UnauthorizedAccessException)
            {

            }

            foreach (var file in rootDirectory.EnumerateFiles())
            {
                OnFileFound(file);
                if (_fileFilter != null && _fileFilter(file))
                {
                    OnFilteredFileFound(file);
                }
                yield return file.Name;
                if (!Run)
                {
                    yield break;
                }

            }

            foreach (var directory in directories)
            {
                if (!Run)
                {
                    yield break;
                }

                OnDirectoryFound(directory);
                if (_directoryFilter != null && _directoryFilter(directory))
                {
                    OnFilteredDirectoryFound(directory);
                }

                yield return directory.FullName;

                if (!Run)
                {
                    yield break;
                }

                foreach (var item in GetDerictories(directory))
                {
                    yield return item;
                }

            }

            if (rootDirectory == _rootDirectory)
            {
                OnFinish();
            }

        }


        protected virtual void OnStart() => Start?.Invoke();
        protected virtual void OnFinish() => Finish?.Invoke();
        protected virtual void OnDirectoryFound(DirectoryInfo directory) => DirectoryEvent(DirectoryFound, directory);
        protected virtual void OnFileFound(FileInfo file) => FileEvent(FileFound, file);
        protected virtual void OnFilteredDirectoryFound(DirectoryInfo directory) => DirectoryEvent(FilteredDirectoryFound, directory);
        protected virtual void OnFilteredFileFound(FileInfo file) => FileEvent(FilteredFileFound, file);

        private void FileEvent(EventHandler<FileEventArgs> fileEvent, FileInfo file)
        {
            if (fileEvent != null)
            {
                FileEventArgs args = new FileEventArgs
                {
                    File = file,
                    Run = true
                };
                fileEvent?.Invoke(this, args);
                Run = args.Run;
            }
            
        }
        private void DirectoryEvent(EventHandler<DirectoryEventArgs> directoryEvent, DirectoryInfo directory)
        {
            if (directoryEvent != null)
            {
                DirectoryEventArgs args = new DirectoryEventArgs
                {
                    Directory = directory,
                    Run = true
                };
                directoryEvent?.Invoke(this, args);
                Run = args.Run;
            }

        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            OnStart();
            return GetDerictories(_rootDirectory).GetEnumerator();          
        }

        
    }
}
