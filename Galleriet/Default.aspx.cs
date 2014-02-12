using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Galleriet.Model;

namespace Galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void ChoseButton_Click(object sender, EventArgs e)
        {

        }

        public IEnumerable<System.IO.FileInfo> Repeater1_GetData()
        {
            return null;
        }
    }
}