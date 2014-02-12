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

        static Gallery()
        {
            ApprovedExtensions = new Regex("^.*/.(gif|jpg|png)$", RegexOptions.IgnoreCase);
            
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));

            PhysicalUploadedImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Content/Images");
        }

        public IEnumerable<string> GetImageNames()
        {
            var files = new DirectoryInfo(PhysicalUploadedImagePath).GetFiles();
            List<string> images = new List<string>(50);
            for (int i = 0; i < files.Length; i++)
            {
                images.Add(files[i].ToString());
            }
            images.Select(fileName => ApprovedExtensions.IsMatch(fileName));
            images.TrimExcess();
            images.Sort();

            return images.AsReadOnly();
        }

        public bool ImageExists(string name)
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
            throw new NotImplementedException();
        }
    }
}