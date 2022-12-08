using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Utlity
{
    public static class FileSystem
    {
        public static string Backslash = ('\'').ToString();

        public static string GetSource()
        {
            return Environment.CurrentDirectory;
        }
    }
}
