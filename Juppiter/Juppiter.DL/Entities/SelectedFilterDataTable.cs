using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Juppiter.DL.Entities
{
    public static class SelectedFilterDataTable_Types
    {
        public static string Data = "Data";
        public static string Causale = "Causale";
        public static string Filiale = "Filiale";
        public static string Segno = "Segno Movimento";
        public static string StatoConto = "Stato Conto Corrente";
    }

    public static class SelectedFilterDataTable_Columns
    {
        public static string Tipo = "Tipo";
        public static string Filtro = "Filtro";
    }

    public class SelectedFilterDataTable
    {
        private DataTable dataTable;

        public SelectedFilterDataTable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(SelectedFilterDataTable_Columns.Tipo, typeof(string));
            dataTable.Columns.Add(SelectedFilterDataTable_Columns.Filtro, typeof(string));
        }


        /// <summary>
        /// Funzione per aggiungere un nuovo filtro alla dataTable dei filtri selezionati. 
        /// In caso il tipo fosse una data controlla se è già stato impostato un filtro sulla data, in caso negativo la aggiunge mentre in caso affermativo la sovrascive.
        /// In caso di altri tipi di dato semplicemente aggiunge il nuovo filtro.
        /// </summary>
        /// <param name="Tipo">Tipo del filtro</param>
        /// <param name="Valore">Valore del filtro impostato</param>
        /// <returns></returns>
        public bool AddFiltro(string Tipo, string Valore)
        {
            try
            {
                if (Tipo == SelectedFilterDataTable_Types.Data)
                {
                    DataRow[] dateRow = dataTable.Select(SelectedFilterDataTable_Columns.Tipo + " = '" + SelectedFilterDataTable_Types.Data + "'");
                    if (dateRow.Length != 0)
                    {
                        dataTable.Rows.Remove(dateRow[0]);
                    }
                }
                if (Tipo == SelectedFilterDataTable_Types.Segno)
                {
                    DataRow[] dateRow = dataTable.Select(SelectedFilterDataTable_Columns.Tipo + " = '" + SelectedFilterDataTable_Types.Segno + "'");
                    if (dateRow.Length != 0)
                    {
                        dataTable.Rows.Remove(dateRow[0]);
                    }
                }
                if (Tipo == SelectedFilterDataTable_Types.StatoConto)
                {
                    DataRow[] dateRow = dataTable.Select(SelectedFilterDataTable_Columns.Tipo + " = '" + SelectedFilterDataTable_Types.StatoConto + "'");
                    if (dateRow.Length != 0)
                    {
                        dataTable.Rows.Remove(dateRow[0]);
                    }
                }

                DataRow workrow = dataTable.NewRow();
                workrow[SelectedFilterDataTable_Columns.Tipo] = Tipo;
                workrow[SelectedFilterDataTable_Columns.Filtro] = Valore;
                dataTable.Rows.Add(workrow);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Error -- DataTableFilter -- AddFiltro -- " + ex.Message);
                return false;
            }

            return true;
        }

        public bool RemoveFiltro(string Tipo, string Valore)
        {
            try
            {
                if (Tipo == SelectedFilterDataTable_Types.Data)
                {
                    DataRow[] dateRow = dataTable.Select(SelectedFilterDataTable_Columns.Tipo + " = '" + SelectedFilterDataTable_Types.Data + "'");
                    if (dateRow.Length != 0)
                    {
                        dataTable.Rows.Remove(dateRow[0]);
                    }
                }

                DataRow workrow = dataTable.NewRow();
                workrow[SelectedFilterDataTable_Columns.Tipo] = Tipo;
                workrow[SelectedFilterDataTable_Columns.Filtro] = Valore;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Error -- DataTableFilter -- RemoveFiltro -- " + ex.Message);
                return false;
            }

            return true;
        }

        public DataTable getDataTable()
        {
            return dataTable;
        }

    }
}