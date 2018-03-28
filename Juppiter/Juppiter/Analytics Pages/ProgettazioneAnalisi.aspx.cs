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
        MongoClientSettings settings;
        MongoClient mclient;
        static List<BsonDocument> ListaCausali;
        static List<BsonDocument> ListaCausaliScelti = new List<BsonDocument>();

        static DataTable dataTableGeneralItems;
        static DataTable dataTableSelectedItems;
        protected void Page_Load(object sender, EventArgs e)
        {
            LViewFilter.DataSource = Global.dataTableFilterElements;
            LViewFilter.DataBind();

            settings = MongoClientSettings.FromUrl(new MongoUrl(Properties.Settings.Default.connection));
            mclient = new MongoClient(settings);
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
            var myDB = mclient.GetDatabase(Properties.Settings.Default.DbBAM);
            if (((Button)sender).ToolTip.Contains("Causale"))
            {
                var collection = myDB.GetCollection<BsonDocument>(Properties.Settings.Default.CollectionCausali);
                var updoneresult1 = collection.Aggregate().Project(Builders<BsonDocument>.Projection.Exclude("_id").Include("SCAUSALE").Include("SDESCRIZIONECAUSALE")).Sort(Builders<BsonDocument>.Sort.Ascending("SCAUSALE")).Limit(4).ToList();
                ListaCausali = updoneresult1.Distinct().ToList();

                dataTableGeneralItems = new DataTable();
                dataTableGeneralItems.Columns.Add("CAUSALE", typeof(int));
                dataTableGeneralItems.Columns.Add("DESCRIZIONE CAUSALE", typeof(string));
                for (int i = 0; i < ListaCausali.Count; i++)
                {
                    DataRow currentRow = dataTableGeneralItems.NewRow();
                    currentRow["CAUSALE"] = ListaCausali[i]["SCAUSALE"].ToString();
                    currentRow["DESCRIZIONE CAUSALE"] = ListaCausali[i]["SDESCRIZIONECAUSALE"].ToString();                   
                    dataTableGeneralItems.Rows.Add(currentRow);
                }
                DivFiltro.Visible = true;               
                GridViewFilter.DataSource = dataTableGeneralItems;
                GridViewFilter.DataBind();
            }
            else if (((Button)sender).ToolTip.Contains("Filiale"))
            {
                //var collection = myDB.GetCollection<BsonDocument>(Properties.Settings.Default.CollectionFiliali);
                //var updoneresult1 = collection.Aggregate().Project(Builders<BsonDocument>.Projection.Exclude("_id").Include("NFILIALE").Include("SDESCRIZIONECAUSALE")).Sort(Builders<BsonDocument>.Sort.Ascending("SCAUSALE")).Limit(20).ToList();
                //ListaCausali = updoneresult1.Distinct().ToList();
            }
        }

        protected void ButtonSelezione_Click(object sender, EventArgs e)
        {
            dataTableSelectedItems = new DataTable();
            dataTableSelectedItems.Columns.Add("CAUSALE", typeof(int));
            dataTableSelectedItems.Columns.Add("DESCRIZIONE CAUSALE", typeof(string));
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