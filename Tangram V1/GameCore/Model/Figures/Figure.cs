using Parser;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tangram.GameCore.Model.Primitives;
using Xamarin.Forms;

namespace Tangram.GameCore.Model.Figures
{
    public class Figure
    {
        public string Title { get; private set; }
        public int Angle { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int ZIndex { get; private set; }
        public Point DrawPoint { get; private set; }
        public Point CursorPoint { get; private set; }


        public int Id { get; set; }
        public int MajorFigureId { get; set; }
        public Point AnchorPoint { get; set; }
        public List<int> AnchorFiguresId { get; set; }

        public SKBitmap Bitmap { get; private set; }

        public Figure(List<Primitive> primitives, Point point, int width, int height, int zIndex) 
        {
            DrawPoint = point;
            Width = width;
            Height = height;
            ZIndex = zIndex;

            InitBitmap(primitives);
        }
        private void InitBitmap(List<Primitive> primitives)
        {
            Bitmap = new SKBitmap(Width, Height);


            //DEBUG

            //SKPaint paint = new SKPaint
            //{
            //    Style = SKPaintStyle.Fill,
            //    Color = Color.FromRgba(100, 100, 100, 50).ToSKColor()

            //};

            //using (SKCanvas canvas = new SKCanvas(Bitmap))
            //{
            //    canvas.DrawRect(0, 0, Width, Height, paint);
            //}
            //DEBUG
            using (SKCanvas canvas = new SKCanvas(Bitmap))
            {
                foreach (var fgr in primitives)
                {
                    canvas.DrawBitmap(fgr.Bitmap, new SKPoint(fgr.X,fgr.Y));
                }
            }
        }
        public bool CheckCoordinates(Point point)
        {
            if (point.X > DrawPoint.X && point.X < DrawPoint.X + Width)
                if (point.Y > DrawPoint.Y && point.Y < DrawPoint.Y + Height)
                {
                    if (Bitmap.GetPixel((int)(point.X - DrawPoint.X), (int)(point.Y - DrawPoint.Y)) != SKColor.Empty)
                    {
                        CursorPoint = new Point (((int)(point.X - DrawPoint.X)), (point.Y - DrawPoint.Y));
                        return true;
                    }
                        
                }
            return false;
        }

        public void Move(Point point) 
        {
            DrawPoint = CursorPoint = point;
            //CursorPoint = new Point(((int)(point.X - DrawPoint.X)), (point.Y - DrawPoint.Y));
        }
        public void TouchMove(Point point)
        {
            DrawPoint = new Point(point.X - CursorPoint.X, point.Y - CursorPoint.Y);
            CursorPoint = new Point(((int)(point.X - DrawPoint.X)), (point.Y - DrawPoint.Y));
        }
        public static Figure FromData(FigureData figureData, float scale = 1)
        {
            try 
            {
                List<Primitive> primitives = new List<Primitive>();
                foreach (var prim in figureData.PrimitivesData)
                {
                    primitives.Add(PrimitiveCreator.CreateFromData(prim, scale));
                }
                return new
                    Figure(primitives, new Point(figureData.Drawpoint.X * scale, figureData.Drawpoint.Y * scale), (int)(figureData.Width * scale), (int)(figureData.Height * scale), figureData.ZIndex)
                { 
                    Id = figureData.Id,
                    MajorFigureId = figureData.MajorFigureId,
                    AnchorPoint = new Point(figureData.AnchorPoint.X*scale, figureData.AnchorPoint.Y * scale),
                    AnchorFiguresId = figureData.AnchorFiguresId
                };

    }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
