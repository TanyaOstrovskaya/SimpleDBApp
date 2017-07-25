using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    interface IDataBaseConnect
    {
        List<FileEntry> GetAllFilesInformation();
        List<string> GetFileNamesList();
        int InsertNewFile(string name, string content);
        bool DeleteFileByName(string name);
        bool UpdateFileContent(string name, string newContent);
        string ReadFileContent(string name);
    }
}
