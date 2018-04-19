using Juppiter.DL.Entities;
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
    public class MovimentiManager : BaseManager
    {
        private MongoClientSettings mongoClientSettings;
        private static MongoClient mongoClient;
        public MovimentiManager(ServiceManager serviceManager) : base(serviceManager)
        {
            mongoClientSettings =  MongoClientSettings.FromUrl(new MongoUrl(Properties.Settings.Default.connection));
            mongoClient = new MongoClient(mongoClientSettings);
        }

        private void Initialize()
        {
           
        }

        private delegate void OutActionDate<T1,T2,T3>(DateTime dataDa, DateTime dataA, out T3 listResult);

        static void OutFuncDate(DateTime dataDa,DateTime dataA, List<FilterDefinition<BsonDocument>> listFilter, out int count)
        {
            count = 0;
            ResponseCollection<BsonDocument> response = new ResponseCollection<BsonDocument>();
            try
            {               
                string dataDaString = String.Format("{0}-{1}-{2}", dataDa.Year.ToString("D2"), dataDa.Month.ToString("D2"), dataDa.Day.ToString("D2"));
                string dataAString = String.Format("{0}-{1}-{2}", dataA.Year.ToString("D2"), dataA.Month.ToString("D2"), dataA.Day.ToString("D2"));
                var builder = Builders<BsonDocument>.Filter;
                if (dataDa.Year == 1)
                {
                    if (listFilter.Count > 0)
                    {
                        count = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            IMongoDatabase myDB = mongoClient.GetDatabase(DatabaseName.BAM);
                            IMongoCollection<BsonDocument> collection = myDB.GetCollection<BsonDocument>(CollectionsName.Elab_Movimenti[i].ToString());
                            //var pippo = collection.Aggregate().Match(builder.And(listFilter.ToArray())).
                            //    Project(new BsonDocument { { DatabaseColumnsName._id, 0 }, { DatabaseColumnsName._idMovimento, 1 } });
                            response.collection = collection.Aggregate().Match(builder.And(listFilter.ToArray())).
                               Group(new BsonDocument { { DatabaseColumnsName._id, 0 }, { DatabaseColumnsName.Count, new BsonDocument { { "$sum", 1 } } } }).ToList();
                            count += response.collection[0].GetValue(DatabaseColumnsName.Count).ToInt32();
                        }
                    }
                }
                else
                {
                    
                    if (dataDa > DateTime.Parse("01/01/" + dataDa.Year.ToString()))
                    {
                        listFilter.Add(builder.Gte(DatabaseColumnsName.DDATA, dataDaString));
                    }
                    if (dataA < DateTime.Parse("31/12/" + dataA.Year.ToString()))
                    {
                        listFilter.Add(builder.Lte(DatabaseColumnsName.DDATA, dataAString));
                    }
                    if (listFilter.Count > 0)
                    {
                        IMongoDatabase myDB = mongoClient.GetDatabase(DatabaseName.BAM);
                        IMongoCollection<BsonDocument> collection = myDB.GetCollection<BsonDocument>(CollectionsName.Elab_Movimenti + dataDa.Year.ToString());
                        response.collection = collection.Aggregate().Match(builder.And(listFilter.ToArray())).
                               Group(new BsonDocument { { DatabaseColumnsName._id, 0 }, { DatabaseColumnsName.Count, new BsonDocument { { "$sum", 1 } } } }).ToList();
                        count += response.collection[0].GetValue(DatabaseColumnsName.Count).ToInt32();
                    }
                }
            }
            catch(Exception ex)
            {
                response.result.AddError(ex);
            }
        }

        private delegate void OutAction<T1, T2, T3>(T1 dataDa, T2 dataA, out T3 listResult);

        static void OutFunc(Dictionary<string,Dictionary<string, string>> dictionary, out int count)
        {
            count = 0;
            ResponseCollection<BsonDocument> response = new ResponseCollection<BsonDocument>();
            Dictionary<string, string> currentDictionary;
            var builder = Builders<BsonDocument>.Filter;
            DateTime dataDa = new DateTime(), dataA = new DateTime();
            List<FilterDefinition<BsonDocument>> listFilter = new List<FilterDefinition<BsonDocument>>();
            //List<FilterDefinition<BsonDocument>> listFilterSpec = new List<FilterDefinition<BsonDocument>>();
            try
            {
                foreach (string key in dictionary.Keys)
                {
                    dictionary.TryGetValue(key, out currentDictionary);
                    if (key == SelectedFilterDataTable_Types.Causale)
                    {
                        //listFilterSpec.Clear();
                        List<int> listCausali = new List<int>();
                        foreach (string currentKey in currentDictionary.Keys)
                        {
                            listCausali.Add(Convert.ToInt32(currentKey));
                            //listFilterSpec.Add(builder.Eq(DatabaseColumnsName.SCAUSALE, Convert.ToInt32(currentKey)));
                        }
                        //listFilter.Add(builder.Or(listFilterSpec.ToArray()));
                        listFilter.Add(builder.In(DatabaseColumnsName.SCAUSALE, listCausali.ToArray()));
                    }
                    else if (key == SelectedFilterDataTable_Types.Filiale)
                    {
                        //listFilterSpec.Clear();
                        List<int> listFiliali = new List<int>();
                        foreach (string currentKey in currentDictionary.Keys)
                        {
                            listFiliali.Add(Convert.ToInt32(currentKey));

                            //listFilterSpec.Add(builder.Eq(DatabaseColumnsName.NFILIALE, Convert.ToInt32(currentKey)));
                        }
                        //listFilter.Add(builder.And(builder.Or(listFilterSpec.ToArray())));
                        listFilter.Add(builder.In(DatabaseColumnsName.NFILIALE, listFiliali.ToArray()));
                    }
                    else if (key == SelectedFilterDataTable_Types.Segno)
                    {
                        foreach (string g in currentDictionary.Values)
                        {
                            switch (g)
                            {
                                case "Entrata":
                                    listFilter.Add(builder.Eq(DatabaseColumnsName.SSEGNO, DatabaseColumnsName.segno.Entrata));
                                    break;
                                case "Uscita":
                                    listFilter.Add(builder.Eq(DatabaseColumnsName.SSEGNO, DatabaseColumnsName.segno.Uscita));
                                    break;
                            }
                        }
                    }
                    else if (key == SelectedFilterDataTable_Types.StatoConto)
                    {
                        foreach (string g in dictionary.Keys)
                        {
                            switch (g)
                            {
                                case "Aperto":
                                    listFilter.Add(builder.Eq(DatabaseColumnsName.SSTATORAPPORTO, DatabaseColumnsName.stato.Aperto));
                                    break;
                                case "Estinto":
                                    listFilter.Add(builder.Eq(DatabaseColumnsName.SSTATORAPPORTO, DatabaseColumnsName.stato.Estinto));
                                    break;
                            }
                        }
                    }
                    else if (key == SelectedFilterDataTable_Types.Data)
                    {
                        string value;
                        currentDictionary.TryGetValue(key, out value);

                        string[] dates = value.Split((" - ".ToCharArray()));

                        
                        if (dates.Length > 1)
                        {
                            dataDa = DateTime.Parse(dates[0]);
                            dataA = DateTime.Parse(dates[3]);
                        }
                        else
                        {
                            dataDa = DateTime.Parse(dates[0]);
                            dataA = DateTime.Parse(dates[0]);
                        }
                                               
                        Dictionary<int, ResponseCollection<BsonDocument>> dictionaryResponses = new Dictionary<int, ResponseCollection<BsonDocument>>();

                        for (int year = dataDa.Year; year <= dataA.Year; year++)
                        {
                            ResponseCollection<BsonDocument> currentResponse = new ResponseCollection<BsonDocument>();
                            dictionaryResponses.Add(year, currentResponse);

                            dictionaryResponses.TryGetValue(year, out currentResponse);

                            if (year == dataDa.Year && year == dataA.Year)
                            {
                                //OutFuncDate(dataDa, dataA, out currentResponse);                                
                            }
                            else if (year == dataDa.Year)
                            {
                                dataA = DateTime.Parse("31/12/" + year.ToString());
                            }
                            else if (year == dataA.Year)
                            {
                                dataDa = DateTime.Parse("01/01/" + year.ToString());                                
                            }
                            else
                            {
                                dataDa = DateTime.Parse("01/01/" + year.ToString());
                                dataA = DateTime.Parse("31/12/" + year.ToString());                                
                            }
                        }
                    }                   
                }
                OutFuncDate(dataDa, dataA, listFilter, out count);
            }
            catch (Exception ex)
            {
                response.result.AddError(ex);
            }
        }

        public ResponseEntity<BsonDocument> GetNumeroMovimentiFiltrati(Dictionary<string,Dictionary<string,string>> filterDictionary)
        {           
            ResponseEntity<BsonDocument> response = new ResponseEntity<BsonDocument>();
            try
            {
                //Dictionary<string, string> currentDictionary;
                //    Dictionary<string, string> Dictionary = new Dictionary<string, string>();

                //List<Action> listAction = new List<Action>();

                //Dictionary<string, ResponseCollection<BsonDocument>> dictionaryList = new Dictionary<string, ResponseCollection<BsonDocument>>();

                int count = 0;
                //ResponseCollection<BsonDocument> ResponseQuerys = new ResponseCollection<BsonDocument>();                 
                //foreach (string currentKey in filterDictionary.Keys)
                //{
                //    dictionaryList.Add(currentKey, currentResponse);
                //    filterDictionary.TryGetValue(currentKey,out currentDictionary);
                //    Dictionary = currentDictionary;
                //    dictionaryList.TryGetValue(currentKey, out currentResponse);
                                       
                //    //if (ResponseQuerys.collection.Count == 0)
                //    //{
                //    //    ResponseQuerys.collection = currentResponse.collection;
                //    //}
                //    //else
                //    //{
                //    //    ResponseQuerys.collection = ResponseQuerys.collection.Intersect(currentResponse.collection).ToList();
                //    //}
                //    // listAction.Add(new Action(() => { OutFunc(currentKey, currentDictionary, out currentResponse); }));                    
                //}
                OutFunc(filterDictionary, out count);
                response.entity = new BsonDocument("Numero Movimenti con i filtri selezionati", count);                
              //  Parallel.Invoke(listAction.ToArray());              
            }
            catch(Exception ex)
            {
                response.result.AddError(ex);
            }
            return response;
        }      
    }
}
