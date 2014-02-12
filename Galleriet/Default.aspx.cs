using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Galleriet.Model;
using System.IO;

namespace Galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var fileContent = FileUpload.FileContent;
                var fileName = FileUpload.FileName;

                Gallery gallery = new Gallery();
                gallery.SaveImage(fileContent, fileName);
            }
        }

        public IEnumerable<System.String> Repeater1_GetData()
        {
            Gallery G = new Gallery();
            return G.GetImageNames();
        }
    }
}