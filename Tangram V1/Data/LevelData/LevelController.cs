using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tangram.Data.DBData;

namespace Tangram.Data.LevelData
{
    class LevelController
    {
        static public LevelRepository levelRepository = new LevelRepository();
        static public List<LevelItem> LoadLevelCollection()
        {
            //List<Level> levels = new List<Level>();


            //MongoClientSettings settings = new MongoClientSettings();
            //settings.Server = new MongoServerAddress("176.99.11.108", 27017);
            //var client = new MongoClient(settings);
            //IMongoDatabase database = client.GetDatabase("GameData");

            //IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Maps");

            //var documents = collection.Find(new BsonDocument())
            //     .Project(Builders<BsonDocument>.Projection.Exclude("_id"))
            //     .ToList();

            //foreach (var item in documents)
            //{
            //    //levels.Add(new Level() { Source = item.ToJson() });
            //    LevelRepository levelRepository = new LevelRepository();
            //    levelRepository.GetLevels()
            //}

            var levels = levelRepository.GetLevels();
            foreach (var item in levels)
            {
                Debug.WriteLine(item.ToString());
            }

            return levelRepository.GetLevels();


        }
        static public void UpdateLevel(LevelItem item)
        {
            levelRepository.UpdateLevel(item);
        }
        static public void RemoveDB()
        {
            levelRepository.RemoveDB();
        }
        static public void Update()
        {
            levelRepository.UpdateDataBase();
        }
    }

}
