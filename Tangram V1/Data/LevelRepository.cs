using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Tangram.Data.DBData;
using Tangram.Data.LevelData;
using Xamarin.Essentials;

namespace Tangram.Data
{

    public class LevelRepository
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DataBase db = new DataBase();
        public LevelRepository()
        {

        }
        public void AddLevel(LevelItem level, string lvldata)
        {
            string filename = $"{level.Name}.lvldt";
            level.Source = filename;
            if (String.IsNullOrEmpty(filename)) return;

            File.WriteAllText(Path.Combine(folderPath, filename), lvldata);
            db.AddItem(level);
        }
        public List<LevelItem> GetLevels()
        {
            return db.GetLevelItems().ToList(); 
        }

        public void UpdateDataBase()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress("176.99.11.108", 27017);
            var client = new MongoClient(settings);
            IMongoDatabase database = client.GetDatabase("GameData");

            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Maps");

            var documents = collection.Find(new BsonDocument())
                 .Project(Builders<BsonDocument>.Projection.Exclude("_id"))
                 .ToList();

            foreach (var item in documents)
            {
                //levels.Add(new Level() { Source = item.ToJson() });
                if (!db.FindItem(item["Title"].AsString))
                {
                    AddLevel(LevelItem.FromBson(item), item.ToJson());
                }

            }
        }
        public void UpdateLevel(LevelItem item)
        {
            db.AddItem(item);
        }
        public void RemoveDB()
        {
            db.Remove();
        }
    }
}
