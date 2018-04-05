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
        static string selectedFilterType;

        static Dictionary<string, Dictionary<string,string>> dictionarySelectedRows;

        static List<BsonDocument> ListaCausaliScelti = new List<BsonDocument>();
                
        static SelectedFilterDataTable dataTableSelectedItems;

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                dataTableSelectedItems = new SelectedFilterDataTable();
                dictionarySelectedRows = new Dictionary<string, Dictionary<string,string>>();
                foreach (var collezione in Global.serviceManager.CollectionManager.GetCollection().collection.ToList())
                {
                    selectColl.Items.Add(collezione["name"].ToString());
                }
            }            
            LViewFilter.DataSource = Global.dataTableFilterElements;
            LViewFilter.DataBind();            
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
            DivButton.Visible = true;
            if (((Button)sender).ToolTip.Contains("Causale"))
            {
                //((Button)sender).Attributes.Remove("class");
                //((Button)sender).Attributes["class"] = "btnSelected";
                //string pippo = ((Button)sender).CssClass;
                //((Button)sender).CssClass = "btnSelected";
                //LViewFilter.DataBind();
                ((Button)sender).BackColor = System.Drawing.Color.LightBlue;
                DivSegno.Visible = false;
                DivStato.Visible = false;
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
                DivSegno.Visible = false;
                DivStato.Visible = false;
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
                DivSegno.Visible = false;
                DivStato.Visible = false;
                selectedFilterType = SelectedFilterDataTable_Types.Data;
            }
            else if (((Button)sender).Text.Contains("Segno"))
            {
                DivData.Visible = false;
                DivFiltro.Visible = false;
                DivSegno.Visible = true;
                DivStato.Visible = false;
                selectedFilterType = SelectedFilterDataTable_Types.Segno;
            }
            else if (((Button)sender).Text.Contains("Conto"))
            {
                DivData.Visible = false;
                DivFiltro.Visible = false;
                DivSegno.Visible = false;
                DivStato.Visible = true;
                selectedFilterType = SelectedFilterDataTable_Types.StatoConto;
            }
            ButtonSelezione.Visible = true;
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
            else if (selectedFilterType == SelectedFilterDataTable_Types.StatoConto)
            {
                string value;
                if (currentValue.TryGetValue(SelectedFilterDataTable_Types.StatoConto, out value))
                {
                    value = RadioStato.SelectedValue.ToString();
                    currentValue[SelectedFilterDataTable_Types.StatoConto] =value;
                }
                else
                {
                    value = RadioStato.SelectedValue.ToString();
                    currentValue.Add(SelectedFilterDataTable_Types.StatoConto,value);
                }
                dictionarySelectedRows[selectedFilterType] = currentValue;
                dataTableSelectedItems.AddFiltro(selectedFilterType, value);
            }
            else if (selectedFilterType == SelectedFilterDataTable_Types.Segno)
            {
                string value;
                if (currentValue.TryGetValue(SelectedFilterDataTable_Types.Segno, out value))
                {
                    value = RadioSegno.SelectedValue.ToString();
                    currentValue[SelectedFilterDataTable_Types.Segno] = value;
                }
                else
                {
                    value = RadioSegno.SelectedValue.ToString();
                    currentValue.Add(SelectedFilterDataTable_Types.Segno, value);
                }
                dictionarySelectedRows[selectedFilterType] = currentValue;
                dataTableSelectedItems.AddFiltro(selectedFilterType, value);               
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
                            row.Cells[2].Text = HttpUtility.HtmlDecode(row.Cells[2].Text);

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
        protected void GridViewFilterScelti_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gdr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gdr.RowIndex;
            if(e.CommandName == "Deseleziona")
            {
                GridViewRow row =  GridViewFilterScelti.Rows[rowIndex];
                row.Cells[1].Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
                row.Cells[2].Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                if(row.Cells[1].Text == SelectedFilterDataTable_Types.Data)
                {
                    dictionarySelectedRows.Remove(SelectedFilterDataTable_Types.Data);
                }
                else 
                {
                    Dictionary<string, string> CurrentDictionary;
                    if(dictionarySelectedRows.TryGetValue(row.Cells[1].Text, out CurrentDictionary))
                    {
                        CurrentDictionary.Remove(row.Cells[2].Text.Split((" - ").ToCharArray())[0]);
                        dictionarySelectedRows[row.Cells[1].Text] = CurrentDictionary;
                    }
                }
                dataTableSelectedItems.RemoveFiltro(row.Cells[1].Text, row.Cells[2].Text);

                GridViewFilterScelti.DataSource = dataTableSelectedItems.getDataTable();
                GridViewFilterScelti.DataBind();
            }
        }

        protected void ButtonAnnulla_Click(object sender, EventArgs e)
        {
            if(((Button)sender).CommandArgument.Contains("Filtri"))
            {
                GridViewFilterScelti.DataBind();
                dataTableSelectedItems = new SelectedFilterDataTable();
                ListaCausaliScelti.Clear();
                dictionarySelectedRows.Clear();
            }
            else
            {
                GridViewDocumenti.DataBind();
                LabelImportati.Visible = false;
            }
        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                LabelImportati.Attributes.Add("style", "color:Green;");
                LabelImportati.Text = "Primi cinque Documenti della collezione importata: "+"</br>";
                ResponseDataTable response = Global.serviceManager.CollectionManager.GetPrimi20Documenti(selectColl.Value.ToString()).ToDataTable();
                if (response.result.Stato == DL.ItemEventoStato.OK)
                {                    
                    GridViewDocumenti.DataSource = response.dataTable;
                    GridViewDocumenti.DataBind();
                }
            }
            catch(Exception ex)
            {
                LabelImportati.Attributes.Add("style", "color:Red;");
                LabelImportati.Text = ex.ToString();
            }
        }
    }
}