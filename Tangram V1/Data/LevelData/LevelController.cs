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
        static public bool CheckUpdate()
        {
            try
            {
                return levelRepository.CheckUpdateLevel();
            }
            catch (Exception)
            {

                return false;
            }
        }
    }

}
