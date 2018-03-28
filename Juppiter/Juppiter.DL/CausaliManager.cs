using Juppiter.DL.Strings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juppiter.DL
{
    public class CausaliManager:BaseManager
    {
        private MongoClientSettings mongoClientSettings  = null;
        private MongoClient mongoClient = null;
        public CausaliManager(ServiceManager serviceManager) : base(serviceManager) {
            
        }

        private void Initialize()
        {
            mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(Properties.Settings.Default.connection));
            mongoClient = new MongoClient(mongoClientSettings);
        }

        public Entities.ResponseCollection<BsonDocument> GetPrime20Causali()
        {
            Entities.ResponseCollection<BsonDocument> response = new Entities.ResponseCollection<BsonDocument>();

            try
            {
                object dictionaryValue;
                if (dictionaryCausali.TryGetValue(DictionaryCausaliKey.Prime20Causali,out dictionaryValue))
                {
                    response.collection = (List<BsonDocument>)dictionaryValue;
                }
                else
                {
                    if(mongoClientSettings == null || mongoClient == null)
                    {
                        Initialize();
                    }

                    IMongoDatabase myDB = mongoClient.GetDatabase(DatabaseName.BAM);
                    IMongoCollection<BsonDocument> collection = myDB.GetCollection<BsonDocument>(CollectionsName.Movimenti);

                    response.collection = collection.Aggregate()
                        .Group(new BsonDocument { { DatabaseColumnsName._id, "$"+DatabaseColumnsName.SCAUSALE }, { DatabaseColumnsName.Count , new BsonDocument("$sum", 1) } })
                        .Sort(new BsonDocument { { DatabaseColumnsName.Count, -1 } })
                        .Limit(20)
                        .Lookup(CollectionsName.Causali, DatabaseColumnsName._id, DatabaseColumnsName.SCAUSALE, DatabaseColumnsName.Dettagli)
                        .Unwind(DatabaseColumnsName.Dettagli)
                        .Group(new BsonDocument { { DatabaseColumnsName._id, new BsonDocument {
                            { DatabaseColumnsName.Causale, "$" + DatabaseColumnsName.Dettagli + "." + DatabaseColumnsName.SCAUSALE }
                            , { DatabaseColumnsName.DescrizioneCausale ,"$" + DatabaseColumnsName.Dettagli + "." + DatabaseColumnsName.SDESCRIZIONECAUSALE } } }
                            , { DatabaseColumnsName.Count , new BsonDocument("$sum", 1) } })
                        .Project(new BsonDocument { { DatabaseColumnsName._id, 0 }, { DatabaseColumnsName.Causale, "$" + DatabaseColumnsName._id + "." + DatabaseColumnsName.Causale }
                            , { DatabaseColumnsName.DescrizioneCausale ,"$" + DatabaseColumnsName._id + "." + DatabaseColumnsName.DescrizioneCausale } })
                        .ToList();

                    dictionaryCausali.Add(DictionaryCausaliKey.Prime20Causali, response.collection);
                }
            }
            catch(Exception ex)
            {
                response.result.AddError(ex);
            }
            return response;
        }

        private Dictionary<DictionaryCausaliKey, object> dictionaryCausali = new Dictionary<DictionaryCausaliKey, object>();
        
        private enum DictionaryCausaliKey
        {
            Prime20Causali
        }
    }
}
