using Parser;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tangram.GameCore.Model.Primitives
{


    abstract public class Primitive
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Color FillColor { get; protected set; }
        public Color BorderColor { get; protected set; }
        public SKBitmap Bitmap { get; protected set; }



    }


    public class Rectangle : Primitive
    {

        public Rectangle(int x, int y, int width, int height, int widthBorder, Color borderColor, Color color)
        {
            BorderColor = borderColor;
            FillColor = color;
            X = x;
            Y = y;
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = borderColor.ToSKColor(),
                StrokeWidth = widthBorder

            };
            paint.IsAntialias = true;
            SKBitmap sKBitmap = new SKBitmap(width, height);
            using (SKCanvas bitmapCanvas = new SKCanvas(sKBitmap))
            {
                bitmapCanvas.DrawRect(0, 0, width, height, paint);
                paint.Color = color.ToSKColor();
                bitmapCanvas.DrawRect(widthBorder, widthBorder, width - widthBorder * 2, height - widthBorder * 2, paint);
            }
            Bitmap = sKBitmap;
        }


    }




    public class Elipse : Primitive
    {
        public Elipse(int x, int y, int width, int height, int widthBorder, Color borderColor, Color color)
        {
            BorderColor = borderColor;
            FillColor = color;
            X = x;
            Y = y;
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = borderColor.ToSKColor(),
                StrokeWidth = widthBorder


            };
            paint.IsAntialias = true;
            SKBitmap sKBitmap = new SKBitmap(width, height);
            using (SKCanvas bitmapCanvas = new SKCanvas(sKBitmap))
            {
                bitmapCanvas.Clear();
                bitmapCanvas.DrawOval(width / 2, height / 2, width / 2, height / 2, paint);
                paint.Color = color.ToSKColor();
                bitmapCanvas.DrawOval(width / 2, height / 2, width / 2 - widthBorder, height / 2 - widthBorder, paint);
            }
            Bitmap = sKBitmap;
        }
    }
    public class Line : Primitive
    {
        public Line(SKRect rect, SKPoint startPoint,SKPoint endPoint, int width, Color color)
        {
            BorderColor = color;
            FillColor = color;
            X = (int)rect.Location.X;
            Y = (int)rect.Location.Y;
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = color.ToSKColor(),
                StrokeWidth = width

            };
            paint.IsAntialias = true;

            paint.StrokeCap = SKStrokeCap.Round;
            SKBitmap sKBitmap = new SKBitmap((int)rect.Width, (int)rect.Height);
            using (SKCanvas bitmapCanvas = new SKCanvas(sKBitmap))
            {

                bitmapCanvas.DrawLine(startPoint, endPoint, paint);
            }
            Bitmap = sKBitmap;
        }


    }
    public class Polygon : Primitive
    {

        public Polygon(SKRect rect, List<Point> points, int widthBorder, Color borderColor, Color color)
        {
            BorderColor = borderColor;
            FillColor = color;
            X = (int)rect.Location.X;
            Y = (int)rect.Location.Y;
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = color.ToSKColor(),
                //Color = borderColor.ToSKColor(),
                StrokeWidth = widthBorder

            };
            paint.IsAntialias = true;

            SKBitmap sKBitmap = new SKBitmap((int)rect.Width, (int)rect.Height);
            using (SKCanvas bitmapCanvas = new SKCanvas(sKBitmap))
            {
                SKPath path = new SKPath();

                // Define the first contour
                path.MoveTo((float)points[0].X - X, (float)points[0].Y - Y);
                path.LineTo((float)points[1].X - X, (float)points[1].Y - Y);
                path.LineTo((float)points[2].X - X, (float)points[2].Y - Y);


                
                path.Close();
                bitmapCanvas.DrawPath(path, paint);
                paint.Style = SKPaintStyle.Stroke;
                paint.Color = borderColor.ToSKColor();
                bitmapCanvas.DrawPath(path, paint);
            }
            Bitmap = sKBitmap;
        }
    }
}
