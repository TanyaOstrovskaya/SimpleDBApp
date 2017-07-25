using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class FileEntry
    {
        public int id { get; private set; }
        public string fileName { get ; private set ; }
        public string fileContent { get ; private set ; }

        public FileEntry() { }

        public FileEntry (int id, string name, string content)
        {
            this.id = id;
            this.fileName = name;
            this.fileContent = content;
        }
    }
}
