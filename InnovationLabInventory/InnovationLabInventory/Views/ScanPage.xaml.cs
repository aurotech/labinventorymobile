using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using ZXing.Mobile;
using InnovationLabInventory.WebServices;
using InnovationLabInventory.Models;

namespace InnovationLabInventory.Views
{
    public partial class ScanPage : ContentPage
    {
        #region Global Variables
        ZXingScannerView QRCodeScanner;
        ZXingDefaultOverlay Scanneroverlay;
        CloudDataStore cloudStore = new CloudDataStore();
        bool IsScan = false;
        bool IsEnterManually = false;
        #endregion
        public ScanPage()
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



            #region BarCode
            QRCodeScanner = new ZXingScannerView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            QRCodeScanner.AutoFocus();
            QRCodeScanner.OnScanResult += ScanResult;

            Scanneroverlay = new ZXingDefaultOverlay
            {
                //TopText = "Scan BarCode.",
                //BackgroundColor = Color.Transparent
            };
            Grid gridScannerBody = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            gridScannerBody.Children.Add(QRCodeScanner);
            gridScannerBody.Children.Add(Scanneroverlay);
            #endregion

            ScannerStack.Children.Add(gridScannerBody);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsScan = true;
            IsEnterManually = false;
            if (Device.OS == TargetPlatform.iOS)
            {
                MobileBarcodeScanningOptions scanPageOptions = new MobileBarcodeScanningOptions()
                {
                    AutoRotate = false,
                    TryInverted = true,
                    TryHarder = true,
                };
            }

            QRCodeScanner.IsScanning = true;
        }
        protected override void OnDisappearing()
        {
            QRCodeScanner.IsScanning = false;

            base.OnDisappearing();
        }
        #region ScanResult Event
        void ScanResult(ZXing.Result result)
        {
            if (IsScan == true)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    DateTime now = DateTime.Now.ToLocalTime();
                    QRCodeScanner.IsScanning = false;
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict["asset"] = result.Text;
                    //string assetId = result.Text ;
                    dict["time"] = now.ToString();
                    var isSuccess = await cloudStore.ScanAsset(dict);
                    if (isSuccess)
                    {
                        await DisplayAlert("Alert", "Barcode scanned successfully.", "Ok");
                        ScanResponce responce = await cloudStore.GetScanUser(result.Text);
                        if (responce.assetType == "single")
                        {
                            await Navigation.PushModalAsync(new MainManagerAssetView(null, responce));
                        }
                        else
                        {
                            await Navigation.PushModalAsync(new MainBatchAssets(responce, null));
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Invalid barcode.", "Ok");
                        App.Current.MainPage = new MainScanPage();
                    }

                });
            }
            else
            {
                DisplayAlert("Error", "Invalid barcode.", "Ok");
                App.Current.MainPage = new MainScanPage();
            }
        }
        #endregion
        //void Scan_Clicked(object sender, System.EventArgs e)
        //{
        //    App.Current.MainPage = new ScannerPage();
        //}

        async void Accept_Clicked(object sender, System.EventArgs e)
        {
            IsScan = true;
            if (IsEnterManually == true && !string.IsNullOrEmpty(BarcodeEntry.Text))
            {
                DateTime now = DateTime.Now.ToLocalTime();
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict["asset"] = BarcodeEntry.Text;
                dict["time"] = now.ToString();
                //ScanResponce isSuccess = await cloudStore.ScanAsset(dict);
                var isSuccess = await cloudStore.ScanAsset(dict);
                if (isSuccess)
                {
                    await DisplayAlert("Alert", "Barcode scanned successfully.", "Ok");
                    ScanResponce responce = await cloudStore.GetScanUser(BarcodeEntry.Text);
                    if (responce.assetType == "single"){
                        await Navigation.PushModalAsync(new MainManagerAssetView(null,responce));
                    }
                    else{
                        await Navigation.PushModalAsync(new MainBatchAssets(responce,null));
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Invalid barcode.", "Ok");
                    App.Current.MainPage = new MainScanPage();
                }
            }
            else
            {
                DisplayAlert("Alert", "Invalid barcode.", "Ok");
            }
        }

        void Retake_Clicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new MainScanPage();
        }

        void EnterBarcode_Clicked(object sender, System.EventArgs e)
        {
            BarcodeEntry.Focus();
            IsEnterManually = true;
            BarcodeEntry.IsVisible = true;
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
}
