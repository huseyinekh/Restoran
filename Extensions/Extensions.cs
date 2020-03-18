using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Restoran.Extensions
{
    public static class Extension
    {
        public static bool IsImage(this IFormFile image)
        {
            return image.ContentType == "image/jpg" ||
                    image.ContentType == "image/png" ||
                    image.ContentType == "image/jpeg" ||
                    image.ContentType == "image/gif" ||
                    image.ContentType == "image/tif";



        }

        public static async Task<string> Save(this Byte[] file, string root, string MainFolder, string Subfolder)
        {


            var NewName = Guid.NewGuid().ToString() + ".png";
            var fullpath = Path.Combine(root, MainFolder, Subfolder, NewName);


            var stream = new MemoryStream(file);

            IFormFile Formfile = new FormFile(stream, 0, file.Length,  NewName, "fileName");

            using (var   data = new FileStream(fullpath, FileMode.Create))
            {
                await Formfile.CopyToAsync(data);
            }
            return NewName;
        }



        public static async Task<string> SaveImg(this Image file, string root, string MainFolder, string Subfolder)
        {


            var NewName = Guid.NewGuid().ToString() + ".png";
            var fullpath = Path.Combine(root, MainFolder, Subfolder, NewName);




            var imageToByte = ImageToByteArray(file);

             var stream = new MemoryStream(imageToByte);
            IFormFile Formfile = new FormFile(stream, 0, imageToByte.Length, NewName, "fileName");
            using (var data = new FileStream(fullpath, FileMode.Create))
            {
                await Formfile.CopyToAsync(data);
            }
            return NewName;
        }
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        public static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
