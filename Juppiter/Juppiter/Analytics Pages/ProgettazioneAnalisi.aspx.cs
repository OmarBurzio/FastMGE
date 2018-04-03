using Juppiter.DL.Entities;
using Juppiter.Utilities.Items;
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
        static string selectedFilterType;

        static Dictionary<string, Dictionary<string,string>> dictionarySelectedRows;

        static List<BsonDocument> ListaCausaliScelti = new List<BsonDocument>();

        //static DataTable dataTableSelectedItems;

        static SelectedFilterDataTable dataTableSelectedItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dataTableSelectedItems = new SelectedFilterDataTable();
                dictionarySelectedRows = new Dictionary<string, Dictionary<string,string>>();
            }

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
                DivData.Visible = false;
                ResponseDataTable responseDataTable = Global.serviceManager.CausaliManager.GetPrime20Causali().ToDataTable();
                if (responseDataTable.result.Stato == DL.ItemEventoStato.OK)
                {
                    DivFiltro.Visible = true;
                    GridViewFilter.DataSource = responseDataTable.dataTable;
                    GridViewFilter.DataBind();
                }
                selectedFilterType = SelectedFilterDataTable_Types.Causale;
            }
            else if (((Button)sender).ToolTip.Contains("filiali"))
            {
                DivData.Visible = false;
                ResponseDataTable response = Global.serviceManager.FilialiManager.GetFiliali().ToDataTable();
                if (response.result.Stato == DL.ItemEventoStato.OK)
                {
                    DivFiltro.Visible = true;
                    GridViewFilter.DataSource = response.dataTable;
                    GridViewFilter.DataBind();
                }
                selectedFilterType = SelectedFilterDataTable_Types.Filiale;
            }
            else if (((Button)sender).ToolTip.Contains("data"))
            {
                DivData.Visible = true;
                DivFiltro.Visible = false;
                selectedFilterType = SelectedFilterDataTable_Types.Data;
            }

        }

        protected void ButtonSelezione_Click(object sender, EventArgs e)
        {
            #region Old
            //if (dataTableSelectedItems == null)
            //{
            //    dataTableSelectedItems = new DataTable();
            //    for( int c = 1; c< GridViewFilter.HeaderRow.Cells.Count; c++)
            //    {
            //        dataTableSelectedItems.Columns.Add(GridViewFilter.HeaderRow.Cells[c].Text);
            //    }
            //}
            //foreach (GridViewRow row in GridViewFilter.Rows)
            //{                
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        CheckBox CheckRow = (row.Cells[0].FindControl("CheckboxFiltro") as CheckBox);
            //        if (CheckRow.Checked)
            //        {
            //            DataRow currentRow = dataTableSelectedItems.NewRow();
            //            currentRow[0] = row.Cells[1].Text;
            //            if (row.Cells[2].Text == "&nbsp;")
            //            {
            //                row.Cells[2].Text = "";
            //            }
            //            currentRow[1] = row.Cells[2].Text;

            //            DataRow outRow;
            //            if (!dictionarySelectedRows.TryGetValue(currentRow[0].ToString(), out outRow))
            //            {
            //                dataTableSelectedItems.Rows.Add(currentRow);
            //                dictionarySelectedRows.Add(currentRow[0].ToString(), currentRow);
            //            }
            //        }
            //    }                   
            //}

            #endregion Old

            Dictionary<string, string> currentValue;
            if (!dictionarySelectedRows.TryGetValue(selectedFilterType, out currentValue))
            {
                currentValue = new Dictionary<string, string>();
                dictionarySelectedRows.Add(selectedFilterType, currentValue);
            }

            if (selectedFilterType == SelectedFilterDataTable_Types.Data)
            {
                string value;
                if (CheckDataA.Checked)
                {
                    if (CalendarDataA.SelectedDate > CalendarDataDa.SelectedDate)
                    {
                        if (currentValue.TryGetValue(SelectedFilterDataTable_Types.Data, out value))
                        {
                            value = String.Format("{0} - {1}", CalendarDataDa.SelectedDate.ToShortDateString()
                                , CalendarDataA.SelectedDate.ToShortDateString());
                            currentValue[SelectedFilterDataTable_Types.Data] = value;
                        }
                        else
                        {
                            value = String.Format("{0} - {1}", CalendarDataDa.SelectedDate.ToShortDateString()
                                , CalendarDataA.SelectedDate.ToShortDateString());
                            currentValue.Add(SelectedFilterDataTable_Types.Data, value);
                        }
                        dictionarySelectedRows[selectedFilterType] = currentValue;
                        dataTableSelectedItems.AddFiltro(selectedFilterType, value);
                    }
                }
                else
                {
                    if (currentValue.TryGetValue(SelectedFilterDataTable_Types.Data, out value))
                    {
                        currentValue[SelectedFilterDataTable_Types.Data] = CalendarDataDa.SelectedDate.ToShortDateString();
                    }
                    else
                    {
                        currentValue.Add(SelectedFilterDataTable_Types.Data, CalendarDataDa.SelectedDate.ToShortDateString());
                    }
                    dictionarySelectedRows[selectedFilterType] = currentValue;
                    dataTableSelectedItems.AddFiltro(selectedFilterType, CalendarDataDa.SelectedDate.ToShortDateString());

                }
            }
            else
            {
                foreach (GridViewRow row in GridViewFilter.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox CheckRow = (row.Cells[0].FindControl("CheckboxFiltro") as CheckBox);
                        if (CheckRow.Checked)
                        {
                            if (row.Cells[2].Text == "&nbsp;")
                            {
                                row.Cells[2].Text = "";
                            }
                            


                            string value;
                            if (!currentValue.TryGetValue(row.Cells[1].Text, out value))
                            {
                                if (String.IsNullOrWhiteSpace(row.Cells[2].Text))
                                {
                                    value = row.Cells[1].Text;
                                }
                                else
                                {
                                    value = row.Cells[1].Text + " - " + row.Cells[2].Text;
                                }
                                dataTableSelectedItems.AddFiltro(selectedFilterType, value);
                                currentValue.Add(row.Cells[1].Text, value);

                                dictionarySelectedRows[selectedFilterType] = currentValue;
                            }
                        }
                    }
                }
            }

            GridViewFilterScelti.DataSource = dataTableSelectedItems.getDataTable();
            GridViewFilterScelti.DataBind();
        }

        protected void CheckDataA_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                CalendarDataA.Enabled = true;
            }
            else
            {
                CalendarDataA.Enabled = false;
            }
        }

        protected void ButtonScegliData_Click(object sender, EventArgs e)
        {
            if(CheckDataA.Checked)
            {
                if(CalendarDataDa.SelectedDate < CalendarDataA.SelectedDate)
                {

                }
            }
        }

        protected void ImageButtonDeselectFilter_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}