using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using InnovationLabInventory.WebServices;
using Xamarin.Forms;


namespace InnovationLabInventory.Views
{
    public partial class CreateAssetPage : ContentPage
    {
        CloudDataStore cloudStore = new CloudDataStore();
        public CreateAssetPage()
        {
            InitializeComponent();
            List<AssetType> lst = new List<AssetType>();
            lst.Add(new AssetType()
            {
                assetType = "single"
            });

            lst.Add(new AssetType()
            {
                assetType = "batch"
            });
            lstview.ItemsSource = lst;

            AssetTypeEntry.Focused += (sender, e) =>
           {
               try
               {
                   lstview.ItemsSource = lst;
                   lstview.IsVisible = true;
               }
               catch (Exception)
               {

               }
           };
            lstview.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
           {
               if (e.SelectedItem == null)
                   return;
               AssetType item = (AssetType)e.SelectedItem;
               AssetTypeEntry.Text = item.assetType;
               lstview.IsVisible = false;
               if (item.assetType == "batch")
               {
                   QuantityFrameEntry.IsVisible = true;
               }
               else
               {
                   QuantityFrameEntry.IsVisible = false;
               }
               ((ListView)sender).SelectedItem = null;
           };
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
        async void CreateAssets_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                if (AssetIDTextField.Text == null &&
                    AssetTypeEntry.Text == null &&
                    DescriptionTextField.Text == null &&
                    LocationTextField.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in missing fields.", "Ok");
                }
                else if (AssetIDTextField.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in asset ID/barcode.", "Ok");
                }
                else if (AssetTypeEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in asset type.", "Ok");
                }
                else if (DescriptionTextField.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in description.", "Ok");
                }
                else if (LocationTextField.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in location.", "Ok");
                }
                //else if (QuantityEntry.Text == null)
                //{
                //    await DisplayAlert("Error", "Please Fill Quantity.", "OK");
                //}
                else
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict["assetId"] = AssetIDTextField.Text;
                    dict["assetType"] = AssetTypeEntry.Text;
                    if (AssetTypeEntry.Text == "batch")
                    {
                        dict["description"] = DescriptionTextField.Text;
                        dict["status"] = LocationTextField.Text;
                        dict["quantity"] = QuantityEntry.Text;
                        var isSuccess = await cloudStore.AddCreateAsset(dict);
                        if (isSuccess)
                        {
                            await DisplayAlert("Success", "Asset created successfully.", "Ok");
                            App.Current.MainPage = new MainPage();
                        }
                    }
                    else if (AssetTypeEntry.Text == "single")
                    {
                        dict["description"] = DescriptionTextField.Text;
                        dict["status"] = LocationTextField.Text;
                        var isSuccess = await cloudStore.AddCreateAsset(dict);
                        if (isSuccess)
                        {
                            await DisplayAlert("Success", "Asset created successfully.", "Ok");
                            App.Current.MainPage = new MainPage();
                        }
                    }

                    else
                    {
                        await DisplayAlert("Error", "Asset creation failed. Please try again.", "Ok");
                        AssetIDTextField.Text = string.Empty;
                        AssetTypeEntry.Text = string.Empty;
                        DescriptionTextField.Text = string.Empty;
                        LocationTextField.Text = string.Empty;
                        QuantityEntry.Text = string.Empty;
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        async void GenarateID_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var isSuccess = await cloudStore.GenarateAssetID();

                if (isSuccess!=null)
                {
                    AssetIDTextField.Text = isSuccess;   
                    //await DisplayAlert("Success", "Asset ID genarated successfully.", "Ok");
                    //App.Current.MainPage = new MainCreateAssetPage();
                }
                else
                {
                }
            }
            catch(Exception ex)
            {
                var mes = ex.Message;
            }
        }

        void Back_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        void Home_Tapped(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
    public class AssetType
    {
        public string assetType { get; set; }
    }
}
