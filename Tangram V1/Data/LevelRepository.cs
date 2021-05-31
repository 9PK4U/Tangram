using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangram.Data.DBData;
using Tangram.Data.LevelData;
using Xamarin.Essentials;

namespace Tangram.Data
{

    public class LevelRepository
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DataBase db = new DataBase();
        MongoClient client;
        public LevelRepository()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress("176.99.11.108", 27017);
            client = new MongoClient(settings);
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
            
            IMongoDatabase database = client.GetDatabase("GameData");

            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Maps");

            var documents = collection.Find(new BsonDocument())
                 .Project(Builders<BsonDocument>.Projection.Exclude("_id"))
                 .ToList();

            foreach (var item in documents)
            {
                if (!db.FindItem(item["Title"].AsString))
                {
                    AddLevel(LevelItem.FromBson(item), item.ToJson());
                }

            }

            //МАКСУ
            //collection.UpdateOneAsync(
            //new BsonDocument("Update", "Levels"),
            //new BsonDocument("$set", new BsonDocument("DataLevelUpdate", DateTime.Now.ToString())));


            IMongoCollection<BsonDocument> collectionConf = database.GetCollection<BsonDocument>("Configurations");
            var document = collectionConf.Find(new BsonDocument()).FirstOrDefault();
            var dateUpdateDB = document.GetValue("DataLevelUpdate", null);
            Preferences.Set("DATE_UPDATE_LEVEL", dateUpdateDB.ToString());
        }
        public void UpdateLevel(LevelItem item)
        {
            db.AddItem(item);

        }
        public void RemoveDB() //DEBUG
        {
            db.Remove();
        }
        public bool CheckUpdateLevel()
        {
            try
            {
                IMongoDatabase database = client.GetDatabase("GameData");
                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Configurations");
                var document = collection.Find(new BsonDocument()).FirstOrDefault();
                var dateUpdateDB = document.GetValue("DataLevelUpdate", null);
                var dateUpdateClient = Preferences.Get("DATE_UPDATE_LEVEL", "EMPTY");
                if (dateUpdateClient == "EMPTY")
                {
                    return true;
                }
                else
                {
                    if (dateUpdateClient == dateUpdateDB)
                    {
                        return false;
                    }
                }


                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
