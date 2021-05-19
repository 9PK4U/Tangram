using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace Tangram.Data.DBData
{
    public class DataBase
    {
        SQLiteConnection database;

        public DataBase()
        {
            var path = Preferences.Get("PATH_DATABASE", "EMPTY");
            if (path == "EMPTY")
            {
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DataBase.db");
                Preferences.Set("PATH_DATABASE", path);
            }
            database = new SQLiteConnection(path);
            database.CreateTable<LevelItem>();
        }

        public List<LevelItem> GetLevelItems()
        {
            return database.Table<LevelItem>().ToList();
        }
        public int AddItem(LevelItem item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
        public void Remove()
        {
            database.DeleteAll<LevelItem>();
        }
        public bool FindItem(string name)
        {
            if (GetLevelItems().Find(x => x.Name == name)!=null)
            {
                return true;
            }
            return false;
        }
    }
}
