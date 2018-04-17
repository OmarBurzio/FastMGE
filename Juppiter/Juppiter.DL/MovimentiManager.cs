﻿using Juppiter.DL.Entities;
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

        static void OutFuncDate(DateTime dataDa,DateTime dataA, out ResponseCollection<BsonDocument> response)
        {
            response = new ResponseCollection<BsonDocument>();
            try
            {
                string dataDaString = String.Format("{0}-{1}-{2}", dataDa.Year.ToString("D2"), dataDa.Month.ToString("D2"), dataDa.Day.ToString("D2"));
                string dataAString = String.Format("{0}-{1}-{2}", dataA.Year.ToString("D2"), dataA.Month.ToString("D2"), dataA.Day.ToString("D2"));
                var builder = Builders<BsonDocument>.Filter;

                List<FilterDefinition<BsonDocument>> listFilter = new List<FilterDefinition<BsonDocument>>();
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
                    response.collection = collection.Aggregate().Match(builder.Or(listFilter.ToArray())).
                             Project(new BsonDocument { { DatabaseColumnsName._id, 0 }, { DatabaseColumnsName._idMovimento, 1 } }).ToList();
                }
            }
            catch(Exception ex)
            {
                response.result.AddError(ex);
            }
        }

        private delegate void OutAction<T1, T2, T3>(T1 dataDa, T2 dataA, out T3 listResult);

        static void OutFunc(string key, Dictionary<string, string> dictionary, out ResponseCollection<BsonDocument> response)
        {           
            response = new ResponseCollection<BsonDocument>();
            try
            {
                var builder = Builders<BsonDocument>.Filter;
                if (key == SelectedFilterDataTable_Types.Causale)
                {
                    List<FilterDefinition<BsonDocument>> listFilter = new List<FilterDefinition<BsonDocument>>();
                    foreach (string currentKey in dictionary.Keys)
                    {
                        listFilter.Add(builder.Eq(DatabaseColumnsName.SCAUSALE, currentKey));
                    }
                    if (listFilter.Count > 0)
                    {
                        IMongoDatabase myDB = mongoClient.GetDatabase(DatabaseName.BAM);
                        IMongoCollection<BsonDocument> collection = myDB.GetCollection<BsonDocument>(CollectionsName.Causali_Movimenti);
                        response.collection = collection.Aggregate().Match(builder.Or(listFilter.ToArray())).
                            Project(new BsonDocument { { DatabaseColumnsName._id, 0 }, { DatabaseColumnsName._idMovimento, 1 } }).ToList();
                    }
                }
                else if (key == SelectedFilterDataTable_Types.Filiale)
                {
                    List<FilterDefinition<BsonDocument>> listFilter = new List<FilterDefinition<BsonDocument>>();
                    foreach (string currentKey in dictionary.Keys)
                    {
                        listFilter.Add(builder.Eq(DatabaseColumnsName.NFILIALE, Convert.ToInt32(currentKey)));
                    }
                    if (listFilter.Count > 0)
                    {
                        IMongoDatabase myDB = mongoClient.GetDatabase(DatabaseName.BAM);
                        IMongoCollection<BsonDocument> collection = myDB.GetCollection<BsonDocument>(CollectionsName.Filiali_Movimenti);

                        response.collection = collection.Aggregate().Match(builder.Or(listFilter.ToArray())).
                            Project(new BsonDocument { { DatabaseColumnsName._id, 0 }, { DatabaseColumnsName._idMovimento, 1} }).ToList();                       
                    }
                }
                else if (key == SelectedFilterDataTable_Types.Segno)
                {
                    List<FilterDefinition<BsonDocument>> listFilter = new List<FilterDefinition<BsonDocument>>();
                    foreach (string g in dictionary.Values)
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

                    IMongoDatabase myDB = mongoClient.GetDatabase(DatabaseName.BAM);
                    IMongoCollection<BsonDocument> collection = myDB.GetCollection<BsonDocument>(CollectionsName.Segno_Movimenti);

                    response.collection = collection.Aggregate().Match(builder.Or(listFilter)).
                            Project(new BsonDocument { { DatabaseColumnsName._id, 0 }, { DatabaseColumnsName._idMovimento, 1 } }).ToList();
                }
                else if (key == SelectedFilterDataTable_Types.StatoConto)
                {
                    FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;
                    foreach (string g in dictionary.Values)
                    {
                        switch (g)
                        {
                            case "Aperto":
                                filter = builder.Eq(DatabaseColumnsName.SSTATORAPPORTO, DatabaseColumnsName.stato.Aperto);
                                break;
                            case "Estinto":
                                filter = builder.Eq(DatabaseColumnsName.SSTATORAPPORTO, DatabaseColumnsName.stato.Estinto);
                                break;
                        }
                        filter = builder.Eq(DatabaseColumnsName.SSTATORAPPORTO, g);
                    }

                    IMongoDatabase myDB = mongoClient.GetDatabase(DatabaseName.BAM);
                    IMongoCollection<BsonDocument> collection = myDB.GetCollection<BsonDocument>(CollectionsName.Stato_Movimenti);

                    response.collection = collection.Aggregate().Match(builder.Or(filter)).
                            Project(new BsonDocument { { DatabaseColumnsName._id, 0 }, { DatabaseColumnsName._idMovimento, 1 } }).ToList();
                }
                else if (key == SelectedFilterDataTable_Types.Data)
                {
                    string value;
                    dictionary.TryGetValue(key, out value);

                    string[] dates = value.Split((" - ".ToCharArray()));

                    DateTime dataDa, dataA;
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

                 //   List<Action> listAction = new List<Action>();
                    Dictionary<int, ResponseCollection<BsonDocument>> dictionaryResponses = new Dictionary<int, ResponseCollection<BsonDocument>>();

                    for (int year = dataDa.Year; year <= dataA.Year; year++)
                    {
                        ResponseCollection<BsonDocument> currentResponse = new ResponseCollection<BsonDocument>();
                        dictionaryResponses.Add(year, currentResponse);

                        dictionaryResponses.TryGetValue(year, out currentResponse);

                        if (year == dataDa.Year && year == dataA.Year)
                        {
                            OutFuncDate(dataDa, dataA, out currentResponse);
                            // listAction.Add(new Action(() => { OutFuncDate(dataDa, dataA, out currentResponse); }));
                        }
                        else if (year == dataDa.Year)
                        {
                            OutFuncDate(dataDa, DateTime.Parse("31/12/" + year.ToString()), out currentResponse);
                            //listAction.Add(new Action(() => { OutFuncDate(dataDa, DateTime.Parse("31/12/" + year.ToString()), out currentResponse); }));
                        }
                        else if (year == dataA.Year)
                        {
                            OutFuncDate(DateTime.Parse("01/01/" + year.ToString()), dataA, out currentResponse);
                            //listAction.Add(new Action(() => { OutFuncDate(DateTime.Parse("01/01/" + year.ToString()), dataA, out currentResponse); }));
                        }
                        else
                        {
                            OutFuncDate(DateTime.Parse("01/01/" + year.ToString()), DateTime.Parse("31/12/" + year.ToString()), out currentResponse);
                            //    listAction.Add(new Action(() => { OutFuncDate(DateTime.Parse("01/01/" + year.ToString()), DateTime.Parse("31/12/" + year.ToString()), out currentResponse); }));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                response.result.AddError(ex);
            }
        }

        public ResponseEntity<BsonDocument> GetNumeroMovimentiFiltrati(Dictionary<string,Dictionary<string,string>> filterDictionary)
        {           
            ResponseEntity<BsonDocument> response = new ResponseEntity<BsonDocument>();
            try
            {
                Dictionary<string, string> currentDictionary;

            //    List<Action> listAction = new List<Action>();

                Dictionary<string, ResponseCollection<BsonDocument>> dictionaryList = new Dictionary<string, ResponseCollection<BsonDocument>>();

                ResponseCollection<BsonDocument> currentResponse = new ResponseCollection<BsonDocument>();
                ResponseCollection<BsonDocument> ResponseQuerys = new ResponseCollection<BsonDocument>();                 
                foreach (string currentKey in filterDictionary.Keys)
                {
                    dictionaryList.Add(currentKey, currentResponse);
                    filterDictionary.TryGetValue(currentKey,out currentDictionary);

                    dictionaryList.TryGetValue(currentKey, out currentResponse);

                    OutFunc(currentKey, currentDictionary, out currentResponse);
                    if (ResponseQuerys.collection.Count == 0)
                    {
                        ResponseQuerys.collection = currentResponse.collection;
                    }
                    else
                    {
                        ResponseQuerys.collection = ResponseQuerys.collection.Intersect(currentResponse.collection).ToList();
                    }
                    // listAction.Add(new Action(() => { OutFunc(currentKey, currentDictionary, out currentResponse); }));                    
                }
                response.entity = new BsonDocument("Numero Movimenti con i filtri selezionati",currentResponse.collection.Count());                
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
