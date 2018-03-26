using Juppiter.Utilities.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juppiter.Utilities.Strings
{
    public class Values
    {
        public static string Codice = "Codice";
        public static string Titolo = "Titolo";
        public static string Descrizione = "Descrizione";
        public static string Image = "Image";
        public static string Page = "Page";

        public static FilterElement[] PopulateFilterElementsArray()
        {
            List<FilterElement> list = new List<FilterElement>();
            FilterElement current;

            current = new FilterElement(
                "ContoCorrente",
                "Stato Conto Corrente",
                "Selezione del Segno dei Conti Corrente di cui si vogliono visualizzare i movimenti.",
                String.Empty,
                "./Pages/Filter/PageFilterContoCorrente.aspx"
            );
            list.Add(current);

            current = new FilterElement(
                "Filiale",
                "Filiale",
                "Selezione delle filiali di cui si vogliono visualizzare i movimenti.",
                String.Empty,
                "./Pages/Filter/PageFilterFiliale.aspx"
            );
            list.Add(current);


            current = new FilterElement(
                "Data",
                "Data",
                "Selezione del periodo della data dove si vogliono visualizzare i movimenti.",
                String.Empty,
                "./Pages/Filter/PageFilterData.aspx"
            );
            list.Add(current);

            current = new FilterElement(
                "Segno",
                "Segno",
                "Selezione del Segno dei Movimenti di cui si vogliono visualizzare i movimenti.",
                String.Empty,
                "./Pages/Filter/PageFilterSegnoMovimenti.aspx"
            );
            list.Add(current);


            current = new FilterElement(
                "Causale",
                "Causale",
                "Selezione della Causale di cui si vogliono visualizzare i movimenti.",
                String.Empty,
                "./Pages/Filter/PageFilterCausale.aspx"
            );
            list.Add(current);

            return list.ToArray();
        }
    }
}