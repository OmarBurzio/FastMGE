using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juppiter.DL.Entities
{
    public class ResponseDataTable
    {
        public ItemEvento result;
        public DataTable dataTable;

        public ResponseDataTable()
        {
            result = new ItemEvento();
            dataTable = new DataTable();
        }

        public ResponseDataTable(ItemEvento result)
        {
            this.result = result;
            dataTable = new DataTable();
        }
    }
}
