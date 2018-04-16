using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juppiter.DL.Strings
{
    public static class DatabaseColumnsName
    {
        public static string _id = "_id";
        public static string _idMovimento = "id_Movimento";
        public static string SCAUSALE = "SCAUSALE";
        public static string SDESCRIZIONECAUSALE = "SDESCRIZIONECAUSALE";
        public static string Causale = "Causale";
        public static string DescrizioneCausale = "DescrizioneCausale";
        public static string Dettagli = "Dettagli";
        public static string Count = "Count";
        public static string SSEGNO = "SSEGNO";
        public static string Segno = "Segno";
        public static string NFILIALE = "NFILIALE";
        public static string SFILIALE = "SFILIALE";
        public static string Sfiliale = "Sede Filiale";
        public static string DDATA = "DDATA";
        public static string SRAPPORTO = "SRAPPORTO";
        public static string SSTATORAPPORTO = "SSTATORAPPORTO";

        public static class stato
        {
            public static string Estinto = "E";
            public static string Aperto = "A";
        }
        public static class segno
        {
            public static string Entrata = "A";
            public static string Uscita = "D";
        }
    }
}
