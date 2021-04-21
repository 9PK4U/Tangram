using System;
using System.Collections.Generic;
using Xamarin.Forms;


namespace Parser
{
    [Serializable]
    public class FigureData
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public int MajorFigureId { get; set; }
        public Point AnchorPoint { get; set; }
        public Point Drawpoint { get; set; }
        public int Angle { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ZIndex { get; set; }
        public List<int> AnchorFiguresId { get; set; }
        public List<PrimitiveData> PrimitivesData { get; set; }

        public FigureData()
        {
        }
        
    }
}
