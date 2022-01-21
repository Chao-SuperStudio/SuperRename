using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperRename.Core.Utils
{
    public static class FileUtils
    {

        public static bool IsProperFilename(string testName)
        {
            string InvalidFileNameChars = new string(System.IO.Path.GetInvalidFileNameChars());
            Regex regFixFileName = new Regex("[" + Regex.Escape(InvalidFileNameChars) + "]");
            return !regFixFileName.IsMatch(testName);
        }

        public static bool IsProperPath(string path)
        {
            if (path.IndexOf("\\") <= 0) return false;
            string[] paths = path.Split('\\');
            for (int i = 1; i < paths.Length; i++)
            {
                if (!IsProperFilename(paths[i])) return false;
            }
            return true;
        }

        public static string changeFileExt(string origin, string ext)
        {
            if (origin == null || ext == null || !IsProperFilename(origin) || !IsProperFilename(ext)) return origin;
            if (ext.Length <= 0) return origin;
            if (origin.Length <= 0) return "." + ext;
            if (origin.LastIndexOf(".", StringComparison.OrdinalIgnoreCase) < 0)
            {
                return origin + "." + ext;
            }
            else
            {
                string[] o = origin.Split('.');
                o[o.Length - 1] = ext;
                return String.Join(".", o);
            }
        }
    }
}
