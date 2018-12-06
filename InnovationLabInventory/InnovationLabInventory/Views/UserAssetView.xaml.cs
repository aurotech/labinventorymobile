using System;
using System.Collections.Generic;
using InnovationLabInventory.WebServices;
using Xamarin.Forms;

namespace InnovationLabInventory.Views
{
    public partial class UserAssetView : ContentPage
    {
        CloudDataStore cloudStore = new CloudDataStore();
        public UserAssetView(ViewAsset Viewasset)
        {
            InitializeComponent();
            AssetEntry.Text = Viewasset.assetId;
            DescriptionEntry.Text = Viewasset.description;
            LocationEntry.Text = Viewasset.status;
            String[] Stringbreak = Viewasset.assignee.Split('#');
            AsigneeEntry.Text = Stringbreak[1];
        }
        async void Return_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                if (AssetEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please Fill Asset ID.", "OK");
                }
                else
                {
                    DateTime now = DateTime.Now.ToLocalTime();
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict["asset"] = AssetEntry.Text;
                    dict["time"] = now.ToString();
                    if (AssetEntry.Text != null)
                    {
                        await DisplayAlert("Return Asset", "Are you sure!", "Cancel","Ok");
                        var isSuccess = await cloudStore.ReturnAsset(dict);
                        if (isSuccess)
                        {
                            await DisplayAlert("Success", "Asset Returned Successfully", "Ok");
                            App.Current.MainPage = new UserViewAssetPage();
                        }
                        else
                        {
                            await DisplayAlert("Error", "Return failure, Please try Again", "Ok");
                            AssetEntry.Text = string.Empty;
                        }
                    }
                    else
                    {
                        await DisplayAlert("No Asset Found", "Cancel", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        void Back_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
           // App.Current.MainPage = new UserViewAssetPage();
        }
        void Home_Tapped(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new UserPage();
        }
    }
}
