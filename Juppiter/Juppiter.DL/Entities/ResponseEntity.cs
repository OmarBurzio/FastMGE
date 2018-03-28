using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juppiter.DL.Entities
{
    public class ResponseEntity<T>
        where T : BsonDocument, new()
    {
        public ItemEvento result;
        public T entity;

        public ResponseEntity()
        {
            result = new ItemEvento();
            entity = new T();
        }
    }
}
