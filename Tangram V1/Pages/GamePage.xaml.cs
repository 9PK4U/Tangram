using SkiaSharp;
using SkiaSharp.Views;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tangram.Data.DBData;
using Xamarin.Essentials;
//using Skia

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tangram.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private Tangram.GameCore.GameMap GameMap { get; set; }
        enum Status { Start, Init, Animation, Game, End };
        Status State{get;set;}

        private SKBitmap BGBitmap { get; set; }



        public GamePage(LevelItem level)
        {

            var wScreen = DeviceDisplay.MainDisplayInfo.Width;
            var hScreen = DeviceDisplay.MainDisplayInfo.Height;

            string resourceID = "Tangram.RESX.BackgroundGame.png";
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                BGBitmap = SKBitmap.Decode(stream);
                BGBitmap.Resize(new SKSizeI((int)wScreen, (int)hScreen), SKFilterQuality.High);
            }


            State = Status.Start;
            GameMap = new Tangram.GameCore.GameMap(level, new Size(wScreen, hScreen));

            InitializeComponent();
        }

        void OnTouch(object sender, SKTouchEventArgs e)
        {
            if (State != Status.Game)
            {
                if (State == Status.Start && e.ActionType == SKTouchAction.Pressed) State = Status.Init;
                e.Handled = true;
                canvasView.InvalidateSurface();
                return;
            }
            Point pos = e.Location.ToFormsPoint();

            if (e.ActionType == SKTouchAction.Pressed)
            {

                foreach (var item in GameMap.SuccsessionsID.Reverse())
                {
                    var fg = GameMap.Figures[item.Value];
                    if (fg.CheckCoordinates(pos))
                    {
                        GameMap.СurrentFigure = fg;
                        Debug.WriteLine($"Press ID FG:{fg.Id} AnPt:{fg.AnchorPoint}  DwPt:{fg.DrawPoint}" );
                        break;
                    }
                }

            }

            if (GameMap.СurrentFigure != null)
            {
                GameMap.СurrentFigure.TouchMove(pos);
            }

            if (e.ActionType == SKTouchAction.Released)
            {
                GameMap.СurrentFigure = null;
                bool isWin = GameMap.CheckStatus();

                if (isWin)
                {
                    State = Status.End;
                    DisplayAlert("Победа", "Вы правильно собрали картинку", "OK");
                    GameMap.UpdateLevelInfo();
                    Application.Current.MainPage.Navigation.PopModalAsync();
                }
            }

            e.Handled = true;
            canvasView.InvalidateSurface();
        }

        Random random = new Random();
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            canvas.DrawBitmap(BGBitmap, 0, 0);

            foreach (var item in GameMap.SuccsessionsID)
            {
                canvas.DrawBitmap(GameMap.Figures[item.Value].Bitmap, GameMap.Figures[item.Value].DrawPoint.ToSKPoint());
            }

            if (State == Status.Init)
            {
                Dictionary<int, Point> listNewPoint = new Dictionary<int, Point>();
                int c = 0;
                foreach (var item in GameMap.Figures)
                {
                    c = random.Next(4);
                    switch (c)
                    {
                        case 0:
                            listNewPoint[item.Key] = item.Value.DrawPoint.Offset(-item.Value.Width, -item.Value.Height);
                            c++;
                            break;
                        case 1:
                            listNewPoint[item.Key] = item.Value.DrawPoint.Offset(item.Value.Width, -item.Value.Height);
                            c++;
                            break;
                        case 2:
                            listNewPoint[item.Key] = item.Value.DrawPoint.Offset(item.Value.Width, item.Value.Height);
                            c++;
                            break;
                        case 3:
                            listNewPoint[item.Key] = item.Value.DrawPoint.Offset(-item.Value.Width, item.Value.Height);
                            c = 0;

                            break;
                        default:
                            break;
                    }
                }
                int count = 0;
                State = Status.Animation;
                Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                {
                    count++;
                    foreach (var item in GameMap.Figures)
                    {
                        item.Value.Move(item.Value.DrawPoint.Offset((listNewPoint[item.Key].X - item.Value.DrawPoint.X) / 30, (listNewPoint[item.Key].Y - item.Value.DrawPoint.Y) / 30));
                    }
                    canvasView.InvalidateSurface();
                    if (count < 30) return true;
                    State = Status.Game;
                    return false;
                });


            }


        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private void Start_Clicked(object sender, EventArgs e)
        {
            State = Status.Start;
            GameMap = new GameCore.GameMap(GameMap.Level, GameMap.SizeScreen);
            canvasView.InvalidateSurface();
        }
    }
    
}