using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Drawing;
//using System.Drawing;
using Xamarin.Forms;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tangram.GameCore;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Tangram.GameCore.Model;
using Tangram.GameCore.Model.Figures;
using Tangram.GameCore.Model.Primitives;
using Tangram.Data.DBData;
using Tangram.Data.LevelData;

namespace Tangram.GameCore
{

    public class GameMap
    {
        public LevelItem Level { get; set; }
        public Size SizeScreen { get; private set; }
        public Dictionary<int,Figure> Figures { get; set; }
        public SortedDictionary<int,int>  SuccsessionsID{ get; set; }
        public Figure СurrentFigure { get; set; }


        public GameMap(LevelItem level, Size size)
        {
            Level = level;
            SizeScreen = size;
            Figures = new Dictionary<int, Figure>();
            SuccsessionsID = new SortedDictionary<int, int>();

            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var levelData = File.ReadAllText(Path.Combine(folderPath, level.Source));
            var map = Parser.Parser.FromJson(levelData);
            var sizeMap = new Size(map.Width, map.Height);
            //sizeMap = new Size(709, 570);

            float _cW = (float)((float)(size.Width / sizeMap.Width) / 1.5);
            float _cH = (float)((float)(size.Height / sizeMap.Height) / 1.5);

            var _c = _cW < _cH ? _cW : _cH;

            
            sizeMap.Width *= _c;
            sizeMap.Height *= _c;
            //DEBUG
            //Figure _figure = new Figure(new List<Primitive>() {
            //    new GameCore.Model.Primitives.Rectangle(0,0,(int)sizeMap.Width, (int)sizeMap.Height,1,Color.FromRgba(255,0,0,10),Color.FromRgba(255,0,0,10))},
            //    new Point(0, 0), (int)sizeMap.Width, (int)sizeMap.Height, -1);
            //Figures[-1] = _figure;
            //DEBUG

            foreach (var item in map.FiguresData)
            {
                Figure figure = Figure.FromData(item,_c);
                // Figure figure = Figure.FromData(item, _cW < _cH ? _cW : _cH);
                Figures[figure.Id] = figure;
                SuccsessionsID[figure.ZIndex] = figure.Id;
            }

            Move(new Point(-map.DrawPoint.X * _c, -map.DrawPoint.Y*_c)); // 709 570

            Move(new Point(size.Width / 2 - sizeMap.Width / 2, size.Height / 2 - sizeMap.Height / 2));



        }
        public void Move(Point dpoint)
        {
            foreach (var item in Figures)
            {
                //DEBUG
                //if (item.Key == -1) continue;
                //DEBUG
                item.Value.Move(new Point (item.Value.DrawPoint.X + dpoint.X, item.Value.DrawPoint.Y + dpoint.Y));
            }
        }
        public Size GetSize()
        {
            double height = 0;
            double width = 0;
            foreach (var item in Figures)
            {
                double lastX = item.Value.DrawPoint.X + item.Value.Width;
                double lastY = item.Value.DrawPoint.Y + item.Value.Height;
                if (lastX > width) width = lastX;
                if (lastY > height) height = lastY;
            }
            return new Size(width, height);
        }


        public bool CheckStatus()
        {
            bool state = true;
            Debug.WriteLine("-------------------");
            foreach (var item in Figures)
            {
                var MID = item.Value.MajorFigureId;
                if (MID == 0)
                    continue;
                    var X = item.Value.DrawPoint.X + item.Value.AnchorPoint.X;
                    var Y = item.Value.DrawPoint.Y + item.Value.AnchorPoint.Y;


                    var X2 = Figures[MID].DrawPoint.X;
                    var Y2 = Figures[MID].DrawPoint.Y;

                var p1 = new Point(X, Y);
                var p2 = new Point(X2, Y2);

                var d = p1.Distance(p2);
                Debug.WriteLine($"ID:{item.Value.Id} MID:{MID} P1:{p1} P2:{p2} D:{d}");
                if (d > 75)
                    {
                    Debug.WriteLine("-------------------");
                    //return false;
                    state = false;
                    }
                else
                {
                    item.Value.Move(new Point(X2 - item.Value.AnchorPoint.X, Y2 - item.Value.AnchorPoint.Y));
                }
            }
            Debug.WriteLine("-------------------");
            return state;
        }
        public void UpdateLevelInfo()
        {
            Level.Passed = true;
            LevelController.UpdateLevel(Level);
        }
    }
}
