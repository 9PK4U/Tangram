using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Tangram.Data.LevelData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tangram.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneralMenu : ContentPage
    {
        
        public GeneralMenu()
        {

            DependencyService.Get<IAudio>().PlayBackMusic();
            InitializeComponent();




            CheckUpdate();



        }
        private async void ShowUpdateAlert()
        {
            bool result = await App.Current.MainPage.DisplayAlert("Доступны новые уровни", "Вы хотите их загрузить?", "Да", "Нет");
            if (result == true)
            {
                Indicator.IsRunning = true;
                LevelController.Update();
                await App.Current.MainPage.DisplayAlert("Успех","Уровни успешно загруженны", "Ок");
                Indicator.IsRunning = false;
            }
        }
        private async void CheckUpdate()
        {
            var isUpdate = await Task<bool>.Run(() => LevelController.CheckUpdate());
            if (isUpdate == true)
            {
                ShowUpdateAlert();
            }
        }
        private void Exit_Clicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }
        private void Start_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CategoryMenu());
        }
    }
}