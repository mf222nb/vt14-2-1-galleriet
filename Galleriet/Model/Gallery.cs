using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Galleriet.Model
{
    public class Gallery
    {
        private static Regex ApprovedExtensions;
        private static Regex SantizePath;
        private static string PhysicalUploadedImagePath;
        private static string PhysicalUploadedThumbnailPath;

        static Gallery()
        {
            ApprovedExtensions = new Regex("^.*/.(gif|jpg|png)$", RegexOptions.IgnoreCase);

            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));

            PhysicalUploadedImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\Images");
            PhysicalUploadedThumbnailPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\Images\Thumbnails");
        }

        //Metod som skapar en lista och lägger till alla bilder som finns i en viss katalog till den listan
        public IEnumerable<string> GetImageNames()
        {
            var fileInfos = new DirectoryInfo(PhysicalUploadedThumbnailPath).GetFiles();
            List<string> imageNames = new List<string>();
            foreach (var fileInfo in fileInfos)
            {
                if (ApprovedExtensions.IsMatch(fileInfo.Extension))
                {
                    imageNames.Add(fileInfo.Name);
                }
            }
            imageNames.TrimExcess();
            imageNames.Sort();

            return imageNames.AsEnumerable();
        }

        //Tittar om filen existerar eller inte
        public static bool ImageExists(string name)
        {
            return File.Exists(String.Format("{0}/{1}", PhysicalUploadedImagePath, name));
        }

        //Kollar om bilden är av typen Gif, Jpeg eller Png
        private bool IsValidImage(Image image)
        {
            return image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                   image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid ||
                   image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid;
        }

        //Metod som kollar om bilder redan finns och då lägger jag på en siffra i slutet av namnet och sparar ner både bilden
        //och gör den til len tumnagelbild, om det är av otilåten typ så kastas ett undantag
        public string SaveImage(Stream stream, string fileName)
        {
            SantizePath.Replace(fileName, String.Empty);
            if (ApprovedExtensions.IsMatch(fileName))
            {
                throw new ArgumentException("Du kan bara spara filer bilder med formaten jpg, png och gif");
            }

            if (ImageExists(fileName))
            {
                var extension = Path.GetExtension(fileName);
                var imgName = Path.GetFileNameWithoutExtension(fileName);

                int i = 0;
                do
                {
                    fileName = String.Format("{0}{1}{2}", imgName, i, extension);
                    i++;
                } while (ImageExists(fileName));
            }
            
            var image = System.Drawing.Image.FromStream(stream);
            if (IsValidImage(image))
            {
                image.Save(Path.Combine(PhysicalUploadedImagePath, fileName));
            }
            var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
            thumbnail.Save(Path.Combine(PhysicalUploadedThumbnailPath, fileName));

            return fileName;
        }
    }
}