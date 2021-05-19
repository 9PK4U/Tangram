using MongoDB.Bson;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tangram.Data.DBData
{
    [Table("Levels")]
    public class LevelItem
    {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }

            public string Name { get; set; }
            public string Source { get; set; }
            public bool Passed { get; set; }
            public string Complexity { get; set; }
            public string Time { get; set; }

        public static LevelItem FromBson(BsonDocument bson)
        {
            LevelItem item = new LevelItem();
            item.Name = bson["Title"].AsString;

            return item;
        }

        public override string ToString()
        {
            return $"Name: {Name} Sourse: {Source}";
        }
    }
}
