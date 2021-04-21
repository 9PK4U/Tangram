using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tangram.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneralMenu : ContentPage
    {
        public GeneralMenu()
        {
            InitializeComponent();
        }

        private void Exit_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Exit");
        }
        private void Start_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LevelMenu());
        }
    }
}