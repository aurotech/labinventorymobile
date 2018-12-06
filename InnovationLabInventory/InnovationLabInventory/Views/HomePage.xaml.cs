using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace InnovationLabInventory.Views
{
    public partial class HomePage : ContentPage 
    {
        public HomePage()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                try
                {
                    MasterDetailPage ParentPage = (MasterDetailPage)this.Parent;
                    ParentPage.IsPresented = (ParentPage.IsPresented == false) ? true : false;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            };
            img.GestureRecognizers.Add(tapGestureRecognizer);
        }


        //async void UserAsset_Clicked(object sender, System.EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new MainUserPage());
        //}
       async void ViewAsset_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new MainViewAssetPage());
        }

        async void ScanAsset_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new MainScanPage());
        }

        async void CreateAsset_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new MainCreateAssetPage());

        }

        void Back_Tapped(object sender, System.EventArgs e)
        {
            //Navigation.PopModalAsync();
            App.Current.MainPage = new MainPage();
        }
        void Home_Tapped(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}
