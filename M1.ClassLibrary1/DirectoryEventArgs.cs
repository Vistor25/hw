using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSysClassLibrary
{
    public class DirectoryEventArgs: EventArgs
    {
        public bool CanRun { get; set; }
        public DirectoryInfo Directory { get; set; }
    }
}
