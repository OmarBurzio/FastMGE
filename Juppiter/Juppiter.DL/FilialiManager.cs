using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juppiter.DL
{
    class FilialiManager : BaseManager
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


    }
}
