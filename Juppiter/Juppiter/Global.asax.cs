using Juppiter.Utilities.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Juppiter
{
    public class Global : System.Web.HttpApplication
    {
        public static DataTable dataTableFilterElements;
        public static DL.ServiceManager serviceManager;

        protected void Application_Start(object sender, EventArgs e)
        {
            serviceManager = new DL.ServiceManager();
            if(serviceManager.Initialize().Stato == DL.ItemEventoStato.OK)
            {

            }

            //serviceManager.CausaliManager.GetPrime20Causali().ToDataTable();

            dataTableFilterElements = new DataTable();
            dataTableFilterElements.Columns.Add(Utilities.Strings.Values.Codice, typeof(string));
            dataTableFilterElements.Columns.Add(Utilities.Strings.Values.Titolo, typeof(string));
            dataTableFilterElements.Columns.Add(Utilities.Strings.Values.Descrizione, typeof(string));
            dataTableFilterElements.Columns.Add(Utilities.Strings.Values.Image, typeof(string));
            dataTableFilterElements.Columns.Add(Utilities.Strings.Values.Page, typeof(string));

            foreach (FilterElement currentFilter in Utilities.Strings.Values.PopulateFilterElementsArray())
            {
                DataRow currentRow = dataTableFilterElements.NewRow();
                currentRow[Utilities.Strings.Values.Codice] = currentFilter.Codice;
                currentRow[Utilities.Strings.Values.Titolo] = currentFilter.Titolo;
                currentRow[Utilities.Strings.Values.Descrizione] = currentFilter.Descrizione;
                currentRow[Utilities.Strings.Values.Image] = currentFilter.Image;
                currentRow[Utilities.Strings.Values.Page] = currentFilter.Page;
                dataTableFilterElements.Rows.Add(currentRow);
            }


            //serviceManager.CausaliManager.GetPrime20Causali().ToDataTable();
        }   
    }
}