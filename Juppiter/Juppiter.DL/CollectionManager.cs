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
    public class CollectionManager : BaseManager
    {
        private MongoClientSettings mongoClientSettings = null;
        private MongoClient mongoClient = null;
        public CollectionManager(ServiceManager serviceManager) : base(serviceManager)
        {
        }

        private void Initialize()
        {
            mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(Properties.Settings.Default.connection));
            mongoClient = new MongoClient(mongoClientSettings);
        }

        public Entities.ResponseCollection<BsonDocument> GetCollection()
        {
            Entities.ResponseCollection<BsonDocument> response = new Entities.ResponseCollection<BsonDocument>();
            try
            {
                object dictionaryValue;
                if (dictionaryCollection.TryGetValue(DictionaryCollezioniKey.Collezioni, out dictionaryValue))
                {
                    response.collection = (List<BsonDocument>)dictionaryValue;
                }
                else
                {
                    if (mongoClientSettings == null || mongoClient == null)
                    {
                        Initialize();
                    }
                    IMongoDatabase myDB = mongoClient.GetDatabase(DatabaseName.BAM_Affinati);
                    List<BsonDocument> collection = myDB.ListCollections().ToList();

                    response.collection = collection;
                    dictionaryCollection.Add(DictionaryCollezioniKey.Collezioni, response.collection);
                }
            }
            catch (Exception ex)
            {
                response.result.AddError(ex);
            }
            return response;
        }
        public Dictionary<DictionaryCollezioniKey, object> dictionaryCollection= new Dictionary<DictionaryCollezioniKey, object>();
        public Entities.ResponseCollection<BsonDocument> GetPrimi20Documenti(string collezione)
        {
            Entities.ResponseCollection<BsonDocument> response = new Entities.ResponseCollection<BsonDocument>();
            try
            {
                if (mongoClientSettings == null || mongoClient == null)
                {
                    Initialize();
                }
                    IMongoDatabase myDB = mongoClient.GetDatabase(DatabaseName.BAM_Affinati);
                    IMongoCollection<BsonDocument> collection = myDB.GetCollection<BsonDocument>(collezione);

                    response.collection = collection.Aggregate()
                        .Limit(5)
                        .Project(new BsonDocument { { DatabaseColumnsName._id, 0 }, {DatabaseColumnsName.DDATA,1 },  { DatabaseColumnsName.SFILIALE, 1 }, { DatabaseColumnsName.SSEGNO, 1 },{DatabaseColumnsName.SCAUSALE,1 },{ DatabaseColumnsName.SDESCRIZIONECAUSALE, 1 }, { DatabaseColumnsName.SRAPPORTO, 1 } }).ToList();

                if (response.collection.Count == 0)
                {
                    throw new Exception("Empty list");
                }                  
                
            }
            catch (Exception ex)
            {
                response.result.AddError(ex);
            }
            return response;
        }
        public enum DictionaryCollezioniKey
        {
            Collezioni
        }       
    }
}
