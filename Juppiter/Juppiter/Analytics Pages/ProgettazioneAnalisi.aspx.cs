using Juppiter.DL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Juppiter.Analytics_Pages
{
    public partial class ProgettazioneAnalisi : System.Web.UI.Page
    {
        static List<BsonDocument> ListaCausaliScelti = new List<BsonDocument>();

        static DataTable dataTableSelectedItems;
        protected void Page_Load(object sender, EventArgs e)
        {
            LViewFilter.DataSource = Global.dataTableFilterElements;
            LViewFilter.DataBind();

            //settings = MongoClientSettings.FromUrl(new MongoUrl(Properties.Settings.Default.connection));
            //mclient = new MongoClient(settings);
        }
        protected void ImageButton_Show(object sender, ImageClickEventArgs e)
        {
            if (((ImageButton)sender).ImageUrl.Contains("minus"))
            {
                ((ImageButton)sender).ImageUrl = "~/Immagini/plus.png";
                if (((ImageButton)sender).CommandArgument == Properties.Settings.Default.ContenutoImportazione)
                    ContentImportazioneDati.Visible = false;
                else if (((ImageButton)sender).CommandArgument == Properties.Settings.Default.ContenutoImpostazioneFiltri)
                    ContentImpostazioneFiltri.Visible = false;
                else if (((ImageButton)sender).CommandArgument == Properties.Settings.Default.ContenutoEsecuzioneAnalisi)
                    ContentEsecuzioneAnalisi.Visible = false;
            }
            else
            {
                ((ImageButton)sender).ImageUrl = "~/Immagini/minus.png";
                if (((ImageButton)sender).CommandArgument == Properties.Settings.Default.ContenutoImportazione)
                    ContentImportazioneDati.Visible = true;
                else if (((ImageButton)sender).CommandArgument == Properties.Settings.Default.ContenutoImpostazioneFiltri)
                    ContentImpostazioneFiltri.Visible = true;
                else if (((ImageButton)sender).CommandArgument == Properties.Settings.Default.ContenutoEsecuzioneAnalisi)
                    ContentEsecuzioneAnalisi.Visible = true;
            }
        }
        protected void ButtonSelectFilter_Click(object sender, EventArgs e)
        {
            //var myDB = mclient.GetDatabase(Properties.Settings.Default.DbBAM);
            if (((Button)sender).ToolTip.Contains("Causale"))
            {
                ResponseDataTable responseDataTable = Global.serviceManager.CausaliManager.GetPrime20Causali().ToDataTable();
                if (responseDataTable.result.Stato == DL.ItemEventoStato.OK)
                {
                    DivFiltro.Visible = true;
                    GridViewFilter.DataSource = responseDataTable.dataTable;
                    GridViewFilter.DataBind();
                }
            }
            else if (((Button)sender).ToolTip.Contains("Filiale"))
            {                
                //var updoneresult1 = collection.Aggregate().Project(Builders<BsonDocument>.Projection.Exclude("_id").Include("NFILIALE").Include("SDESCRIZIONECAUSALE")).Sort(Builders<BsonDocument>.Sort.Ascending("SCAUSALE")).Limit(20).ToList();
               
            }
            else if (((Button)sender).ToolTip.Contains("Corrente"))
            {

            }
        }

        protected void ButtonSelezione_Click(object sender, EventArgs e)
        {
            if (dataTableSelectedItems == null)
            {
                dataTableSelectedItems = new DataTable();
                dataTableSelectedItems.Columns.Add("CAUSALE", typeof(int));
                dataTableSelectedItems.Columns.Add("DESCRIZIONE CAUSALE", typeof(string));
            }
            foreach (GridViewRow row in GridViewFilter.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckRow = (row.Cells[0].FindControl("CheckboxFiltro") as CheckBox);
                    if (CheckRow.Checked)
                    {
                        DataRow currentRow = dataTableSelectedItems.NewRow();
                        currentRow["CAUSALE"] = row.Cells[1].Text;
                        if(row.Cells[2].Text == "&nbsp;")
                        {
                            row.Cells[2].Text = "";
                        }                        
                        currentRow["DESCRIZIONE CAUSALE"] = row.Cells[2].Text;                        
                        dataTableSelectedItems.Rows.Add(currentRow);
                    }
                }                   
            }
            GridViewFilterScelti.DataSource = dataTableSelectedItems;
            GridViewFilterScelti.DataBind();
        }
    }
}