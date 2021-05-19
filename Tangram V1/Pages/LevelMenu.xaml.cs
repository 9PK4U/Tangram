using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Tangram.Data.LevelData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tangram.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelMenu : ContentPage
    {


        public LevelMenu()
        {
            InitializeComponent();

            LoadBitmapCollection();

        }


        void  LoadBitmapCollection()
        {



            flexLayout.Children.Clear();


            var levels = LevelController.LoadLevelCollection();
            int count = 1;
            for (int i = 0; i < levels.Count; i++)
            {
                ContentView viev = new ContentView
                {
                    Content = new Button()
                    {
                        Text = $"{count++}",
                        CornerRadius = 30,
                        FontSize = 32,
                        BackgroundColor = levels[i].Passed ? Color.FromRgba(30, 200, 30, 200) : Color.FromRgba(122, 122, 122, 122),
                        Command = new Xamarin.Forms.Command<int>((int it) => {
                            var gamePage = new GamePage(levels[it]);
                            Navigation.PushModalAsync(gamePage);
                            gamePage.Disappearing += (sender, e) =>
                            {
                                LoadBitmapCollection();
                            };
                        }),
                        CommandParameter = count - 2
                    },
                    WidthRequest = 120,
                    HeightRequest = 120
                };
                flexLayout.Children.Add(viev);
            }


        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            LevelController.RemoveDB();
            LoadBitmapCollection();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            LevelController.Update();
            LoadBitmapCollection();
        }
    }

}