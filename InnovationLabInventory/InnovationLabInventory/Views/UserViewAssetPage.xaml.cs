using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InnovationLabInventory.Models;
using Xamarin.Forms;
using InnovationLabInventory.WebServices;

namespace InnovationLabInventory.Views
{
    public partial class UserViewAssetPage : ContentPage
    {
        CloudDataStore cloudStore = new CloudDataStore();
        List<ViewAsset> response;
        public UserViewAssetPage()
        {
            InitializeComponent();
            GetData();
            List<ViewAsset> Searchlst = new List<ViewAsset>();
            listview.ItemsSource = response;
            AssetIDEntry.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                Searchlst.Clear();
                Searchlst = response.Where(i => i.assetId.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
                if (Searchlst.Count == 0)
                {
                    Searchlst = response.Where(i => i.description.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
                    if (Searchlst.Count == 0)
                    {
                        Searchlst = response.Where(i => i.status.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
                    }
                }
                listview.ItemsSource = Searchlst;
            };
            //listview.ItemTemplate = new DataTemplate(typeof(ListCell));
            //listview.SeparatorVisibility = SeparatorVisibility.None;
            listview.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                ViewAsset selected = listview.SelectedItem as ViewAsset;
                Navigation.PushModalAsync(new UserAssetView(selected));
                ((ListView)sender).SelectedItem = null;
            };
        }
        async void GetData()
        {
            try
            {
                response = await cloudStore.GetAssets();
                listview.ItemsSource = response;
            }
            catch (Exception ex)
            {
            }
        }
        void Back_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        void Home_Tapped(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new UserPage();
        }
    }
}
