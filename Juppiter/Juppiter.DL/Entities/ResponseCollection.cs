﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juppiter.DL.Entities
{
    public class ResponseCollection<T>
        where T : BsonDocument
    {
        public ItemEvento result;
        public List<T> collection;

        public ResponseCollection()
        {
            result = new ItemEvento();
            collection = new List<T>();
        }


        public ResponseDataTable ToDataTable()
        {
            ResponseDataTable response = new ResponseDataTable(result);
            try
            {
                if (result.Stato == ItemEventoStato.OK)
                {
                    List<string> columnsName = collection[0].Names.ToList();
                    foreach (string currentColumn in columnsName) {
                        response.dataTable.Columns.Add(currentColumn,typeof(string));
                    }
                    foreach (BsonDocument current in collection)
                    {
                        DataRow newRow = response.dataTable.NewRow();

                        foreach (string currentColumn in columnsName)
                        {
                            newRow[currentColumn] = current.GetValue(currentColumn).ToString(); 
                        }
                        response.dataTable.Rows.Add(newRow);
                    }
                }
                else
                {
                    response.result = result;
                }

            }
            catch(Exception ex)
            {
                response.result.AddError(ex);
            }
            return response;
        }
    }
}