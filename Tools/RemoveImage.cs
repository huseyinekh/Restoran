using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restoran.Tools
{
    public  static class RemoveImage
    {
        public static bool RemoveImg( string root, string imageLink)
        {
            var path = Path.Combine( root,  "assets/image/" +imageLink);

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }

    }
}
