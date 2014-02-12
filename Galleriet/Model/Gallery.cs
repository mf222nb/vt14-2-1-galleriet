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

        public IEnumerable<string> GetImageNames()
        {
            var files = new DirectoryInfo(PhysicalUploadedThumbnailPath).GetFiles();
            List<string> images = new List<string>(50);
            for (int i = 0; i < files.Length; i++)
            {
                images.Add(files[i].ToString());
            }
            images.Select(fileName => ApprovedExtensions.IsMatch(fileName));
            images.TrimExcess();
            images.Sort();

            return images.AsEnumerable();
        }

        public static bool ImageExists(string name)
        {
            return File.Exists(string.Format("{0}/{1}", PhysicalUploadedImagePath, name));
        }

        private bool IsValidImage(Image image)
        {
            return image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                   image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid ||
                   image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid;
        }

        public string SaveImage(Stream stream, string fileName)
        {
            SantizePath.Replace(fileName, String.Empty);
            if (!ApprovedExtensions.IsMatch(fileName))
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
            try
            {
                var image = System.Drawing.Image.FromStream(stream); // stream -> ström med bild
                if (IsValidImage(image))
                {
                    image.Save(Path.Combine(PhysicalUploadedImagePath, fileName));
                }
                var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
                thumbnail.Save(Path.Combine(PhysicalUploadedThumbnailPath, fileName)); // path -> fullständig fysisk filnamn inklusive sökväg
            }
            catch (Exception)
            {
                
                throw;
            }
            
            return fileName;
        }
    }
}