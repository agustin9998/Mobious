using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinnessLogicProject
{
    public static class FileManager
    {
        public static string ReplaceForbiddenCharacters(string name)
        {
            return name.Replace(":", "").Replace(",", "").Replace(".","").Replace("- ","").Replace("?","").Replace("/","");
        }

        public static string ReplaceDoubleSlash(string path)
        {
            return path.Replace(@"\\", @"\");
        }

        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}
