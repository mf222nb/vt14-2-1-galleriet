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
        private Gallery _gallery;

        public Gallery Gallery 
        { 
            get { return _gallery ?? (_gallery = new Gallery()); }
        }

        //Returnerar true eller false beroende på om ett meddelande finns eller inte
        private bool HasMessage 
        { 
            get
            {
                return Session["uploadMessage"] != null;
            } 
        }

        //Skapar en Session och tar bort den när den är skapad
        private string UploadMessage 
        { 
            get
            {
                var message = Session["uploadMessage"] as string;
                Session.Remove("uploadMessage");
                return message;
                
            }
            set
            {
                Session["uploadMessage"] = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            var image = Request.QueryString["name"];
            if (image != null)
            {
                FullSiezeImage.Visible = true;
            }
            

            if (HasMessage)
            {
                SuccessLiteral.Text = String.Format("{0}", UploadMessage); 
                SuccessPanel.Visible = true;
            }
            
            FullSiezeImage.ImageUrl = "~/Content/Images/" + image;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var fileContent = FileUpload.FileContent;
                var fileName = FileUpload.FileName;

                var savedImage = Gallery.SaveImage(fileContent, fileName);
                UploadMessage = String.Format("Uppladdningen av {0} lyckades", savedImage);
                Response.Redirect("Default.aspx?name=" + savedImage);
            }
        }

        public IEnumerable<System.String> Repeater1_GetData()
        {
            return Gallery.GetImageNames();
        }
    }
}