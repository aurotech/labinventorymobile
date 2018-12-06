using System;
using System.Collections.Generic;
using InnovationLabInventory.WebServices;
using Xamarin.Forms;
using InnovationLabInventory.Models;

namespace InnovationLabInventory.Views
{
    public partial class ManagerAssetView : ContentPage
    {
        CloudDataStore cloudStore = new CloudDataStore();
        bool Validate = false;
        public ManagerAssetView(ViewAsset Viewasset, ScanResponce scanResponce)
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

            // AssetEntry.Text = ScanResponce.assetId;

            if (Viewasset != null)
            {
                AssetEntry.Text = Viewasset.assetId;
                DescriptionEntry.Text = Viewasset.description;
                StatusEntry.Text = Viewasset.status;
                timeScannedEntry.Text = Viewasset.timeLastScanned;
                timeUpdatedEntry.Text = Viewasset.timeLastUpdated;
                commentsEntry.Text = Viewasset.comment;
                if (Viewasset.assignee != null)
                {
                    //String[] Stringbreak = Viewasset.assignee.Split('#');
                    AsigneeEntry.Text = Viewasset.assignee;
                    assign.Text = "Return Asset";
                    lstview.IsVisible = false;
                }
                else
                {

                }
            }

            if (scanResponce != null)
            {
                AssetEntry.Text = scanResponce.assetId;
                DescriptionEntry.Text = scanResponce.description;
                StatusEntry.Text = scanResponce.status;
                timeScannedEntry.Text = scanResponce.timeLastScanned;
                timeUpdatedEntry.Text = scanResponce.timeLastUpdated;
                commentsEntry.Text = scanResponce.comment;
                if (scanResponce.assignee != null)
                {
                    String[] Stringbreak = scanResponce.assignee.Split('#');
                    AsigneeEntry.Text = Stringbreak[1];
                    assign.Text = "Return Asset";
                    lstview.IsVisible = false;
                }
                else
                {

                }
            }



            lstview.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
            {
                if (e.SelectedItem == null)
                    return;
                Users item = (Users)e.SelectedItem;
                AsigneeEntry.Text = item.email;
                lstview.IsVisible = false;
                if (Validate = true)
                {
                    DateTime now = DateTime.Now.ToLocalTime();
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict["asset"] = AssetEntry.Text;
                    dict["assignee"] = AsigneeEntry.Text;
                    dict["time"] = now.ToString();
                    var isSuccess = await cloudStore.AssignAsset(dict);
                    if (isSuccess)
                    {
                        await DisplayAlert("Success", "Asset Assigned Successfully.", "Ok");
                        App.Current.MainPage = new MainViewAssetPage();

                        //AssetEntry.Text = string.Empty;
                        //DescriptionEntry.Text = string.Empty;
                    }
                    else
                    {
                        await DisplayAlert("Error", "Assignment Failed. Please try Again.", "Ok");
                        //AssetEntry.Text = string.Empty;
                        //DescriptionEntry.Text = string.Empty;
                    }
                    Validate = false;
                }

                ((ListView)sender).SelectedItem = null;
            };

            if (AsigneeEntry.Text == null)
            {
                AsigneeEntry.Focused += async (sender, e) =>
                {
                    try
                    {
                        var isSuccess = await cloudStore.GetUsers();
                        if (isSuccess != null)
                        {
                            lstview.ItemsSource = isSuccess;
                            lstview.IsVisible = true;
                        }
                        else
                        {
                            await DisplayAlert("Error", "No Assignee", "Cancel", "Ok");
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                };
            }
            else
            {
            }

        }

        async void Submit_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                StatusEntry.Text = LocationEntry.Text;
                if (AssetEntry.Text == null &&
                    StatusEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in missing fields", "OK");
                }
                else if (AssetEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in asset ID/ barcode.", "Ok");
                }
                else if (StatusEntry.Text == null)
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
                    dict["newLocation"] = StatusEntry.Text;
                    dict["time"] = now.ToString();
                    var isSuccess = await cloudStore.UpdateAsset(dict);
                    if (isSuccess)
                    {
                        locationstack.IsVisible = false;
                        await DisplayAlert("Success", "Location is updated.", "Ok");
                        App.Current.MainPage = new MainViewAssetPage();
                        //AssetEntry.Text = string.Empty;
                        //LocationEntry.Text = string.Empty;

                    }
                    else
                    {
                        await DisplayAlert("Error", "Updated failed. Please try again.", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        async void Update_Clicked(object sender, System.EventArgs e)
        {
            locationstack.IsVisible = true;
        }
        async void Assign_Clicked(object sender, System.EventArgs e)
        {
            if (assign.Text == "Return Asset")
            {
                try
                {
                    if (AssetEntry.Text == null)
                    {
                        await DisplayAlert("Error", "Please fill in asset ID.", "OK");
                    }
                    else
                    {
                        DateTime now = DateTime.Now.ToLocalTime();
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        dict["asset"] = AssetEntry.Text;
                        dict["time"] = now.ToString();
                        if (AssetEntry.Text != null)
                        {
                            await DisplayAlert("Return asset", "Are you sure?", "Cancel", "Ok");
                            var isSuccess = await cloudStore.ReturnAsset(dict);
                            if (isSuccess)
                            {
                                await DisplayAlert("Success", "Asset returned successfully.", "Ok");
                                App.Current.MainPage = new MainViewAssetPage();
                            }
                            else
                            {
                                await DisplayAlert("Error", "Return failed. Please try again.", "Ok");
                                AssetEntry.Text = string.Empty;
                            }
                        }
                        else
                        {
                            await DisplayAlert("No asset found.", "Cancel", "Ok");
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                try
                {
                    if (AssetEntry.Text == null &&
                        AsigneeEntry.Text == null)
                    {
                        await DisplayAlert("Error", "Please fill in missing fields.", "OK");
                    }
                    else if (AssetEntry.Text == null)
                    {
                        await DisplayAlert("Error", "Please fill in asset ID/barcode.", "OK");
                    }
                    else if (AsigneeEntry.Text == null)
                    {
                        //await DisplayAlert("Error", "Please Fill Assignee.", "OK");
                        try
                        {
                            List<Users> listUsers = await cloudStore.GetUsers();

                            if (listUsers != null)
                            {
                                List<Users> listManagers = await cloudStore.GetManagers();
                                for (int i = 0; i < listManagers.Count; i++)
                                {
                                    listUsers.Add(listManagers[i]);
                                }

                                lstview.ItemsSource = listUsers;
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    lstview.IsVisible = true;
                                    Validate = true;
                                });


                            }
                            else
                            {
                                await DisplayAlert("Error", "No assignee.", "Cancel", "Ok");
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        DateTime now = DateTime.Now.ToLocalTime();
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        dict["asset"] = AssetEntry.Text;
                        dict["assignee"] = AsigneeEntry.Text;
                        dict["time"] = now.ToString();

                        var isSuccess = await cloudStore.AssignAsset(dict);
                        if (isSuccess)
                        {
                            await DisplayAlert("Success", "Asset assigned successfully.", "Ok");
                            App.Current.MainPage = new MainViewAssetPage();

                            //AssetEntry.Text = string.Empty;
                            //DescriptionEntry.Text = string.Empty;
                        }
                        else
                        {
                            await DisplayAlert("Error", "Assignment failed. Please try again.", "Ok");
                            //AssetEntry.Text = string.Empty;
                            //DescriptionEntry.Text = string.Empty;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        async void Delete_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                if (AssetEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in asset ID/ barcode.", "OK");
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
                            await DisplayAlert("Error", "Delete failed.", "Ok");
                            //AssetEntry.Text = string.Empty;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Delete failed. Please try again.", "Ok");
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
                    await DisplayAlert("Error", "Please fill in missing fields.", "OK");
                }
                else if (AssetEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in asset ID/barcode.", "OK");
                }
                else if (DescriptionEntry.Text == null)
                {
                    await DisplayAlert("Error", "Please fill in description.", "OK");
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
                        await DisplayAlert("Success", "Description is updated.", "Ok");
                        App.Current.MainPage = new MainViewAssetPage();
                        //AssetEntry.Text = string.Empty;
                        //LocationEntry.Text = string.Empty;

                    }
                    else
                    {
                        await DisplayAlert("Error", "Update failed. Please try again.", "Ok");
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
                        await DisplayAlert("Success", "Comments are updated.", "Ok");
                        App.Current.MainPage = new MainViewAssetPage();
                        //AssetEntry.Text = string.Empty;
                        //LocationEntry.Text = string.Empty;

                    }
                    else
                    {
                        await DisplayAlert("Error", "Update failed. Please try again.", "Ok");
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



