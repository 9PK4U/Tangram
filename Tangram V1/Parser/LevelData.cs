using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Parser
{
    [Serializable]
    public class LevelData
    {
        public string Title { get; set; }
        public List<FigureData> FiguresData { get; set; }
        public Point DrawPoint { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public LevelData()
        {
        }
        
    }
}
