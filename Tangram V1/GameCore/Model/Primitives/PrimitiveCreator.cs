using Parser;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tangram.GameCore.Model.Primitives
{
    class PrimitiveCreator
    {
        public static Primitive CreateFromData(PrimitiveData data, float scale = 1)
        {
            try
            {
                switch (data.Type)
                {
                    case "Line":
                        return CreateLineFromData(data, scale);
                    case "Rectangle":
                        return CreateRectangleFromData(data, scale);
                    case "Ellipse":
                        return CreateEllipseFromData(data, scale);
                    case "Triangle":
                        return CreatePolygonFromData(data, scale);
                    case "Polygon":
                        return CreatePolygonFromData(data, scale);
                    default:
                        throw new Exception($"\"{data.Type}\" is invalid type primitive");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PrimitiveCreator:CreateFromData: " + ex.Message);
            }
        }


        private static Elipse CreateEllipseFromData(PrimitiveData data, float scale = 1)
        {
            try
            {

                var h = data.Bounds.Height * scale;
                var w = data.Bounds.Width * scale;

                var x = data.Bounds.X * scale;
                var y = data.Bounds.Y * scale;

                return new Elipse((int)x, (int)y, (int)w, (int)h, (int)(data.BorderWidth * scale),
                                                SKColor.Parse(data.BorderColor).ToFormsColor(),
                                                SKColor.Parse(data.FillColor).ToFormsColor());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private static Polygon CreatePolygonFromData(PrimitiveData data, float scale = 1)
        {
            try
            {

                var h = data.Bounds.Height * scale;
                var w = data.Bounds.Width * scale;

                var x = data.Bounds.X * scale;
                var y = data.Bounds.Y * scale;

                SKRect rect = SKRect.Create(new SKPoint((float)x, (float)y), new SKSize((float)w, (float)h));

                //System.Text.Json.JsonElement Points = (System.Text.Json.JsonElement)data.Parameters["Points"];

                List<Point> points = data.Points;
                //List<Point> points = System.Text.Json.JsonSerializer.Deserialize<List<Point>>(Points.ToString());
                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = new Point(points[i].X *scale, points[i].Y * scale);
                }
               
                return new Polygon(rect, points, (int)(data.BorderWidth * scale),
                                                SKColor.Parse(data.BorderColor).ToFormsColor(),
                                                SKColor.Parse(data.FillColor).ToFormsColor());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private static Line CreateLineFromData(PrimitiveData data, float scale = 1)
        {
            try
            {
                var h = data.Bounds.Height * scale;
                var w = data.Bounds.Width * scale;

                var x = data.Bounds.X * scale;
                var y = data.Bounds.Y * scale;


                //System.Text.Json.JsonElement StartPoint = (System.Text.Json.JsonElement)data.Parameters["StartPoint"];
                //System.Text.Json.JsonElement EndPoint = (System.Text.Json.JsonElement)data.Parameters["EndPoint"];

                var startX = data.Points[0].X * scale;
                var startY = data.Points[0].Y * scale;
                var endX = data.Points[1].X * scale;
                var endY = data.Points[1].Y * scale;

                SKPoint startPoint = new SKPoint((float)(data.Bounds.Right * scale - startX), (float)(data.Bounds.Bottom * scale - startY));
                SKPoint endPoint = new SKPoint((float)(data.Bounds.Right * scale - endX), (float)(data.Bounds.Bottom * scale - endY));

                SKRect rect = SKRect.Create(new SKPoint((float)x, (float)y), new SKSize((float)w, (float)h));



                return new Line(rect, startPoint, endPoint, (int)(data.BorderWidth * scale),
                //SKColor.Parse(data.BorderColor).ToFormsColor());
                Color.Black);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        private static Rectangle CreateRectangleFromData(PrimitiveData data, float scale = 1)
        {
            try
            {

                var h = data.Bounds.Height * scale;
                var w = data.Bounds.Width * scale;

                var x = data.Bounds.X * scale;
                var y = data.Bounds.Y * scale;

                return new Rectangle((int)x, (int)y, (int)w, (int)h, (int)(data.BorderWidth * scale),
                                                SKColor.Parse(data.BorderColor).ToFormsColor(),
                                                SKColor.Parse(data.FillColor).ToFormsColor());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


}

