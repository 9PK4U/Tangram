using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser;

namespace Parser
{
    public abstract class Parser
    {
     
        public static LevelData FromJson(string levelData)
        {
            return JsonSerializer.Deserialize<LevelData>(levelData);
        }
    }
}
