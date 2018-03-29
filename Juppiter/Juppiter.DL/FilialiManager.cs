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
    public class FilialiManager : BaseManager
    {
        private MongoClientSettings mongoClientSettings = null;
        private MongoClient mongoClient = null;
        public FilialiManager(ServiceManager serviceManager) : base(serviceManager)
        {

        }
        private void Initialize()
        {
            mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(Properties.Settings.Default.connection));
            mongoClient = new MongoClient(mongoClientSettings);
        }

        public Entities.ResponseCollection<BsonDocument> GetFiliali()
        {
            Entities.ResponseCollection<BsonDocument> response = new Entities.ResponseCollection<BsonDocument>();
            try
            {
                object dictionaryValue;
                if (dictionaryFiliali.TryGetValue(DictionaryFilialiliKey.Filiali, out dictionaryValue))
                {
                    response.collection = (List<BsonDocument>)dictionaryValue;
                }
                else
                {
                    if (mongoClientSettings == null || mongoClient == null)
                    {
                        Initialize();
                    }
                    IMongoDatabase myDB = mongoClient.GetDatabase(DatabaseName.BAM);
                    IMongoCollection<BsonDocument> collection = myDB.GetCollection<BsonDocument>(CollectionsName.Filiali_Movimenti);

                   response.collection = collection.Aggregate()
                        .Group(new BsonDocument { { DatabaseColumnsName._id, "$" + DatabaseColumnsName.FILIALE}})
                        .Project(new BsonDocument { { DatabaseColumnsName._id, 0 }, { DatabaseColumnsName.FILIALE, "$" + DatabaseColumnsName._id}, {"Sede Filiale",""} })
                        .Sort(new BsonDocument { { DatabaseColumnsName.FILIALE, 1 } })
                        .ToList();                   
                    dictionaryFiliali.Add(DictionaryFilialiliKey.Filiali, response.collection);
                }
            }
            catch (Exception ex)
            {
                response.result.AddError(ex);
            }
            return response;
        }
        public Dictionary<DictionaryFilialiliKey, object> dictionaryFiliali = new Dictionary<DictionaryFilialiliKey, object>();

        public enum DictionaryFilialiliKey
        {
            Filiali
        }
    }
}
