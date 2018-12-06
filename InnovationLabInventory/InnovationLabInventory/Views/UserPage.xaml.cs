using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InnovationLabInventory.Views
{
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            InitializeComponent();
            //var tapGestureRecognizer = new TapGestureRecognizer();
            //tapGestureRecognizer.Tapped += (s, e) =>
            //{
            //    try
            //    {
            //        MasterDetailPage ParentPage = (MasterDetailPage)this.Parent;
            //        ParentPage.IsPresented = (ParentPage.IsPresented == false) ? true : false;
            //    }
            //    catch (Exception ex)
            //    {
            //        var msg = ex.Message;
            //    }
            //};
            //img.GestureRecognizers.Add(tapGestureRecognizer);
        }
        async void ViewAsset_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new UserViewAssetPage());
        }

        void Logout_Clicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new LoginPage();

        }

        void Back_Tapped(object sender, System.EventArgs e)
        {
            //Navigation.PopModalAsync();
            App.Current.MainPage = new UserPage();

        }
        void Home_Tapped(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new UserPage();
        }
    }
}
