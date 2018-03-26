using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juppiter.Utilities.Items
{
    public class FilterElement
    {
        public string Codice;
        public string Titolo;
        public string Descrizione;
        public string Image;
        public string Page;

        public FilterElement(string Codice = "", string Titolo = "", string Descrizione = "", string Image = "", string Page ="")
        {
            this.Codice = Codice;
            this.Titolo = Titolo;
            this.Descrizione = Descrizione;
            this.Image = Image;
            this.Page = Page;
        }
    }
}