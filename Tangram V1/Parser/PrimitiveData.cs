using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xamarin.Forms;

namespace Parser
{
    [Serializable]
    public class PrimitiveData
    {
        public int BorderWidth { get; set; }
        public string FillColor { get; set; }
        public string BorderColor { get; set; }
        public string Type { get; set; }
        public Rect Bounds { get; set; }
        public List<Point> Points { get; set; }

        public PrimitiveData()
        {
        }
       
        
    }
}
