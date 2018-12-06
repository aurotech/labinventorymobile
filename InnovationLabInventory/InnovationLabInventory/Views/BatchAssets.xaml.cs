using System;
using System.Collections.Generic;
using InnovationLabInventory.WebServices;
using InnovationLabInventory.Models;
using Xamarin.Forms;

namespace InnovationLabInventory.Views
{
    public partial class BatchAssets : ContentPage
    {
        CloudDataStore cloudStore = new CloudDataStore();
        public BatchAssets(ScanResponce scanResponce, ViewAsset Viewasset)
        {
            InitializeComponent();
            if (scanResponce != null)
            {
                AssetEntry.Text = scanResponce.assetId;
                DescriptionEntry.Text = scanResponce.description;
                LocationEntry.Text = scanResponce.status;
                string quantity = scanResponce.quantity.ToString();
                QuantityEntry.Text = quantity;
                LastUpdated.Text = scanResponce.timeLastUpdated;
                LastScanned.Text = scanResponce.timeLastScanned;
                commentsEntry.Text = scanResponce.comment;
            }
            else
            {

            }
            if (Viewasset != null)
            {
                AssetEntry.Text = Viewasset.assetId;
                DescriptionEntry.Text = Viewasset.description;
                LocationEntry.Text = Viewasset.status;
                string quantity = Viewasset.quantity.ToString();
                QuantityEntry.Text = quantity;
                LastUpdated.Text = Viewasset.timeLastUpdated;
                LastScanned.Text = Viewasset.timeLastScanned;
                commentsEntry.Text = Viewasset.comment;
            }
            else
            {

            }

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

        async void Submit_Clicked(object sender, System.EventArgs e)
        {
            //string totalCount = Int32.Parse(TextBoxD1.Text); QuantityEntry.Text 

            try
            {
                if (AssetEntry.Text == null &&
                    AddQuantity.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in missing fields.", "OK");
                }
                else if (AssetEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please Fill in asset ID/barcode.", "OK");
                }
                else if (AddQuantity.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in quantity.", "OK");
                    //LocationEntry.IsVisible = false;
                    //statusFrameEntry.IsVisible = true;
                }
                else
                {
                    DateTime now = DateTime.Now.ToLocalTime();
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict["asset"] = AssetEntry.Text;
                    dict["amount"] = AddQuantity.Text;
                    dict["time"] = now.ToString();
                    var isSuccess = false;
                    if (addOrsub.Text == "+")
                    {
                        isSuccess = await cloudStore.AddAsset(dict);
                        if (isSuccess)
                        {
                            await DisplayAlert("Success", "Asset added successfully.", "Ok");
                            App.Current.MainPage = new MainViewAssetPage();
                            //AssetEntry.Text = string.Empty;
                            //LocationEntry.Text = string.Empty;

                        }
                        else
                        {
                            await DisplayAlert("Error", "Add failed. Please try again.", "Ok");
                        }
                    }
                    else
                    {
                        isSuccess = await cloudStore.SubtractAsset(dict);
                        if (isSuccess)
                        {
                            await DisplayAlert("Success", "Asset subtracted successfully.", "Ok");
                            App.Current.MainPage = new MainViewAssetPage();
                            //AssetEntry.Text = string.Empty;
                            //LocationEntry.Text = string.Empty;

                        }
                        else
                        {
                            await DisplayAlert("Error", "Subtract failed. Please try again.", "Ok");
                       
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        void Add_Clicked(object sender, System.EventArgs e)
        {
            addQtyRefStack.IsVisible = true;
            addOrsub.Text = "+";

        }
        void Subtract_Clicked(object sender, System.EventArgs e)
        {
            addQtyRefStack.IsVisible = true;
            addOrsub.Text = "-";

            //try
            //{
            //    if (AssetEntry.Text == null &&
            //        QuantityEntry.Text == null)
            //    {
            //        await DisplayAlert("Error", "Please Fill Missing Fields", "OK");
            //    }
            //    else if (AssetEntry.Text == null)
            //    {
            //        await DisplayAlert("Error", "Please Fill Asset ID/ Barcode.", "OK");
            //    }
            //    else if (QuantityEntry.Text == null)
            //    {
            //        await DisplayAlert("Error", "Please Fill Quantity.", "OK");
            //        //LocationEntry.IsVisible = false;
            //        //statusFrameEntry.IsVisible = true;
            //    }
            //    else
            //    {
            //        DateTime now = DateTime.Now.ToLocalTime();
            //        Dictionary<string, string> dict = new Dictionary<string, string>();
            //        dict["asset"] = AssetEntry.Text;
            //        dict["amount"] = QuantityEntry.Text;
            //        dict["time"] = now.ToString();
            //        var isSuccess = await cloudStore.SubtractAsset(dict);
            //        if (isSuccess)
            //        {
            //            await DisplayAlert("Success", "Asset Subtracted Successfully", "Ok");
            //            App.Current.MainPage = new MainViewAssetPage();
            //            //AssetEntry.Text = string.Empty;
            //            //LocationEntry.Text = string.Empty;

            //        }
            //        else
            //        {
            //            await DisplayAlert("Error", "Subtract failure, Please try Again", "Ok");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }
        async void Delete_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                if (AssetEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in asset ID/barcode.", "OK");
                }
                else
                {
                    DateTime now = DateTime.Now.ToLocalTime();
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict["asset"] = AssetEntry.Text;
                    dict["time"] = now.ToString();
                    if (AssetEntry.Text != null)
                    {
                        await DisplayAlert("Delete Asset", "Are you sure?", "Cancel", "Ok");
                        var isSuccess = await cloudStore.DeleteAsset(dict);
                        if (isSuccess)
                        {
                            await DisplayAlert("Success", "Asset deleted successfully.", "Ok");
                            //AssetEntry.Text = string.Empty;
                            App.Current.MainPage = new MainViewAssetPage();
                        }
                        else
                        {
                            await DisplayAlert("Delete Failed", "Cancel", "Ok");
                            //AssetEntry.Text = string.Empty;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Delete failed. Please try again", "Ok");
                        //AssetEntry.Text = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        async void UpdateDes_Clicked(object sender, System.EventArgs e)
        {
            descstack.IsVisible = true;
        }
        async void DescUpdate_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                DescriptionEntry.Text = DescEntry.Text;
                if (AssetEntry.Text == null &&
                    DescriptionEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in missing fields.", "Ok");
                }
                else if (AssetEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in asset ID/barcode.", "Ok");
                }
                else if (DescriptionEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in location.", "Ok");
                    //LocationEntry.IsVisible = false;
                    //statusFrameEntry.IsVisible = true;
                }
                else
                {
                    DateTime now = DateTime.Now.ToLocalTime();
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict["asset"] = AssetEntry.Text;
                    dict["newDescription"] = DescriptionEntry.Text;
                    dict["time"] = now.ToString();
                    var isSuccess = await cloudStore.EditDescription(dict);
                    if (isSuccess)
                    {
                        descstack.IsVisible = false;
                        await DisplayAlert("Success", "Updated the description.", "Ok");
                        App.Current.MainPage = new MainViewAssetPage();
                        //AssetEntry.Text = string.Empty;
                        //LocationEntry.Text = string.Empty;

                    }
                    else
                    {
                        await DisplayAlert("Error", "Update failed, Please try again.", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
        void UpdateComments_Clicked(object sender, System.EventArgs e)
        {
            commentstack.IsVisible = true;
        }
        async void CommentUpdate_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                commentsEntry.Text = CommentEntry.Text;
                if (AssetEntry.Text == null &&
                    commentsEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in missing fields.", "Ok");
                }
                else if (AssetEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in asset ID/barcode.", "Ok");
                }
                else if (commentsEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in comments.", "Ok");
                    //LocationEntry.IsVisible = false;
                    //statusFrameEntry.IsVisible = true;
                }
                else
                {
                    DateTime now = DateTime.Now.ToLocalTime();
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict["asset"] = AssetEntry.Text;
                    dict["newComment"] = commentsEntry.Text;
                    dict["time"] = now.ToString();
                    var isSuccess = await cloudStore.EditComment(dict);
                    if (isSuccess)
                    {
                        commentstack.IsVisible = false;
                        await DisplayAlert("Success", "Updated the comments.", "Ok");
                        App.Current.MainPage = new MainViewAssetPage();
                        //AssetEntry.Text = string.Empty;
                        //LocationEntry.Text = string.Empty;

                    }
                    else
                    {
                        await DisplayAlert("Error", "Update failed, Please try again.", "Ok");
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
            //App.Current.MainPage = new MainPage();
        }
        void Home_Tapped(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}
