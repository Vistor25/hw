using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSysClassLibrary
{
    public class FileSystemVisitor : IEnumerable
    {
        private readonly Predicate<DirectoryInfo> _directoryFilter;
        private readonly Predicate<FileInfo> _fileFilter;
        private readonly DirectoryInfo _rootDirectory;
        private bool CanRun = true;

        public FileSystemVisitor(string rootFolder)
        {
            _rootDirectory = new DirectoryInfo(rootFolder);
        }

        public FileSystemVisitor(string rootFolder, Predicate<FileInfo> fileFilter,
            Predicate<DirectoryInfo> directoryFilter) : this(rootFolder)
        {
            _fileFilter = fileFilter;
            _directoryFilter = directoryFilter;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            OnStart();
            return GetDerictories(_rootDirectory).GetEnumerator();
        }

        public event Action Start;
        public event Action Finish;
        public event EventHandler<DirectoryEventArgs> DirectoryFound;
        public event EventHandler<FileEventArgs> FileFound;
        public event EventHandler<DirectoryEventArgs> FilteredDirectoryFound;
        public event EventHandler<FileEventArgs> FilteredFileFound;

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
                    OnFilteredFileFound(file);
                yield return file.Name;
                if (!CanRun)
                    yield break;
            }

            foreach (var directory in directories)
            {
                if (!CanRun)
                    yield break;

                OnDirectoryFound(directory);
                if (_directoryFilter != null && _directoryFilter(directory))
                    OnFilteredDirectoryFound(directory);

                yield return directory.FullName;

                if (!CanRun)
                    yield break;

                foreach (var item in GetDerictories(directory))
                    yield return item;
            }

            if (rootDirectory == _rootDirectory)
                OnFinish();
        }


        protected virtual void OnStart() => Start?.Invoke();
        protected virtual void OnFinish() => Finish?.Invoke();
        protected virtual void OnDirectoryFound(DirectoryInfo directory) => DirectoryEvent(DirectoryFound, directory);
        protected virtual void OnFileFound(FileInfo file) => FileEvent(FileFound, file);

        protected virtual void OnFilteredDirectoryFound(DirectoryInfo directory)
            => DirectoryEvent(FilteredDirectoryFound, directory);

        protected virtual void OnFilteredFileFound(FileInfo file) => FileEvent(FilteredFileFound, file);

        private void FileEvent(EventHandler<FileEventArgs> fileEvent, FileInfo file)
        {
            if (fileEvent != null)
            {
                var args = new FileEventArgs
                {
                    File = file,
                    CanRun = true
                };
                fileEvent?.Invoke(this, args);
                CanRun = args.CanRun;
            }
        }

        private void DirectoryEvent(EventHandler<DirectoryEventArgs> directoryEvent, DirectoryInfo directory)
        {
            if (directoryEvent != null)
            {
                var args = new DirectoryEventArgs
                {
                    Directory = directory,
                    CanRun = true
                };
                directoryEvent?.Invoke(this, args);
                CanRun = args.CanRun;
            }
        }
    }
}