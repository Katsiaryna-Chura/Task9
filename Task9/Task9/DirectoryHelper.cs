using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task9
{
    class DirectoryHelper
    {
        DirectoryInfo dirInfo;

        public DirectoryHelper(string path)
        {
            dirInfo = new DirectoryInfo(path);
        }

        public List<FileInfo> GetAllDllFromDir()
        {
            return dirInfo.GetFiles("*.dll").ToList();
        }
    }
}
