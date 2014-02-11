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
        private static Regex ApprovedExenstions;
        private static Regex SantizePath;
        private static string PhysicalUploadedImagePath;

        static Gallery()
        {
            ApprovedExenstions = new Regex("^.*/.(gif|jpg|png)$", RegexOptions.IgnoreCase);
            
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SantizePath = new Regex(string.Format("[0]", Regex.Escape(invalidChars)));

            PhysicalUploadedImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Content/Images");
        }

        public IEnumerable<string> GetImageNames()
        {
            var di = new DirectoryInfo(PhysicalUploadedImagePath);
            throw new NotImplementedException();
        }

        public bool ImageExists(string name)
        {
            if (!File.Exists(name))
            {
                return false;
            }
            else
	        {
                return true;
	        }
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