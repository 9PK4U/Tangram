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

        public enum Tag { Animals, Anime, Technic, Tangram};
        Tag tagLevels;
        public LevelMenu(Tag tag)
        {
            tagLevels = tag;
            InitializeComponent();

            LoadBitmapCollection(tag);

        }


        void  LoadBitmapCollection(Tag tag)
        {



            flexLayout.Children.Clear();


            var levels = LevelController.LoadLevelCollection();
            bool PredicateTag(Data.DBData.LevelItem item)
            {
                return item.Tag != tag.ToString();
            }
            levels.RemoveAll(PredicateTag);

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
                                LoadBitmapCollection(tag);
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
            LoadBitmapCollection(tagLevels);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            LevelController.Update();
            LoadBitmapCollection(tagLevels);
        }
        private void HomeButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }

}