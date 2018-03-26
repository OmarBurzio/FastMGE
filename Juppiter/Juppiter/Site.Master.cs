using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Juppiter
{
    public partial class Site : System.Web.UI.MasterPage
    {
        private static class treeViewValues
        {
            public static string AcquisizioneDati = "AcquisizioneDati";
            public static string DataQuality = "DataQuality";
            public static string Analystics = "Analytics";
            public static string RealTime= "RealTime";
            public static string Knowledge = "Knowledge";
            public static string Knowledge_Descrittivo = "Knowledge_Descrittivo";
            public static string Knowledge_Prescrittivo = "Knowledge_Precrittivo";
            public static string Knowledge_Predittivo = "Knowledge_Predittivo";
            public static string Dashboard = "Dashboard";
            public static string GestioneTicket = "GestioneTicket";
            public static string Strumenti = "Strumenti";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TreeViewNavigationBar_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode selectedNode = TreeViewNavigationBar.SelectedNode;
            if(selectedNode.Value == treeViewValues.Knowledge)
            {
                if ((bool)selectedNode.Expanded)
                {
                    selectedNode.Collapse();
                }
                else
                {
                    selectedNode.Expand();
                }
                selectedNode.Selected = false;
            }
        }
    }
}