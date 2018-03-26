using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Juppiter.Pages_Analytics
{
    public partial class ProgettazioneAnalisi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LViewFilter.DataSource = Global.dataTableFilterElements;
            LViewFilter.DataBind();
        }
        protected void ImageButton_Show(object sender, ImageClickEventArgs e)
        {
            if (((ImageButton)sender).ImageUrl.Contains("minus"))
            {
                ((ImageButton)sender).ImageUrl = Properties.Resources.plus.ToString();
                if (((ImageButton)sender).CommandArgument == Properties.Settings.Default.ContenutoImportazione)
                {
                    
                }
            }else
            {
                ((ImageButton)sender).ImageUrl = Properties.Resources.minus.ToString();
                if (((ImageButton)sender).CommandArgument == Properties.Settings.Default.ContenutoImportazione)
                {
                    Control myControl1 = FindControl(Properties.Settings.Default.ContenutoImportazione);
                    myControl1.Visible = false;
                }
            }

        }        
    }
}