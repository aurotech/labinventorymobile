//using System;
//using Xamarin.Forms;
//using ZXing.Net.Mobile.Forms;
//using ZXing.Mobile;
//using InnovationLabInventory.WebServices;

//namespace InnovationLabInventory.Views
//{
//    public class ScannerPage : ContentPage
//    {
//        #region Global Variables
//        ZXingScannerView QRCodeScanner;
//        ZXingDefaultOverlay Scanneroverlay;
//        CloudDataStore cloudStore = new CloudDataStore();
//        #endregion
//        public ScannerPage()
//        {
//            #region for Header Stack
//            Label lblTitle = new Label()
//            {
//                Text = "Scanner",
//                TextColor = Color.White,
//                FontAttributes = FontAttributes.Bold,
//                FontSize = 20,
//                HorizontalOptions = LayoutOptions.StartAndExpand,
//                VerticalOptions = LayoutOptions.CenterAndExpand,
//                Margin = new Thickness(10, 0, 0, 0)
//            };
//            Label lblskip = new Label()
//            {
//                Text = "Skip",
//                FontSize = 20,
//                FontAttributes = FontAttributes.Bold,
//                TextColor = Color.White,
//                HorizontalOptions = LayoutOptions.EndAndExpand,
//                VerticalOptions = LayoutOptions.CenterAndExpand,
//                Margin = new Thickness(0, 0, 10, 0)
//            };
//            StackLayout stackSkip = new StackLayout()
//            {
//                Children = { lblskip },
//                BackgroundColor = Color.FromHex("#301631"),
//                HorizontalOptions = LayoutOptions.End,
//                VerticalOptions = LayoutOptions.FillAndExpand
//            };
//            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
//            tapGestureRecognizer.Tapped += lblskipTapped;
//            stackSkip.GestureRecognizers.Add(tapGestureRecognizer);
//            StackLayout stackHeader = new StackLayout()
//            {
//                Children = { lblTitle, stackSkip },
//                Orientation = StackOrientation.Horizontal,
//                BackgroundColor = Color.FromHex("#301631"),
//                HeightRequest = 70,
//                HorizontalOptions = LayoutOptions.FillAndExpand,
//                VerticalOptions = LayoutOptions.Start
//            };
//            #endregion

//            StackLayout stackfooter = new StackLayout()
//            {
//                BackgroundColor = Color.FromHex("#301631"),
//                HeightRequest = 70,
//                HorizontalOptions = LayoutOptions.FillAndExpand,
//                VerticalOptions = LayoutOptions.Start
//            };

//            #region BarCode
//            QRCodeScanner = new ZXingScannerView()
//            {
//                HorizontalOptions = LayoutOptions.FillAndExpand,
//                VerticalOptions = LayoutOptions.FillAndExpand,
//            };
//            QRCodeScanner.AutoFocus();
//            QRCodeScanner.OnScanResult += ScanResult;

//            Scanneroverlay = new ZXingDefaultOverlay
//            {
//                TopText = "Scan BarCode.",
//                BackgroundColor = Color.Transparent
//            };
//            Grid gridScannerBody = new Grid
//            {
//                VerticalOptions = LayoutOptions.FillAndExpand,
//                HorizontalOptions = LayoutOptions.FillAndExpand,
//            };
//            gridScannerBody.Children.Add(QRCodeScanner);
//            gridScannerBody.Children.Add(Scanneroverlay);
//            #endregion
//            #region Main Stack
//            StackLayout stackMain = new StackLayout()
//            {
//                VerticalOptions = LayoutOptions.FillAndExpand,
//                HorizontalOptions = LayoutOptions.FillAndExpand,
//                Children = { stackHeader, gridScannerBody, stackfooter },
//                Spacing = 0
//            };
//            #endregion

//            Content = stackMain;
//        }
//        protected override void OnAppearing()
//        {
//            base.OnAppearing();

//            if (Device.OS == TargetPlatform.iOS)
//            {
//                MobileBarcodeScanningOptions scanPageOptions = new MobileBarcodeScanningOptions()
//                {
//                    AutoRotate = false,
//                    TryInverted = true,
//                    TryHarder = true,
//                };
//            }

//            QRCodeScanner.IsScanning = true;
//        }
//        protected override void OnDisappearing()
//        {
//            QRCodeScanner.IsScanning = false;

//            base.OnDisappearing();
//        }

//        #region lblskipTapped event
//        void lblskipTapped(object sender, EventArgs e)
//        {
//            Navigation.PushModalAsync(new ScanPage());
//        }
//        #endregion

//        #region ScanResult Event
//        void ScanResult(ZXing.Result result)
//        {
//            Device.BeginInvokeOnMainThread(async () =>
//            {
//                QRCodeScanner.IsScanning = false;
//                string assetId = result.Text;

//                var isSuccess = await cloudStore.GetBarcode(assetId);
//                if (isSuccess != null)
//                {
//                    await DisplayAlert("Alert", isSuccess.ToString(), "OK");
//                    await Navigation.PushModalAsync(new ScanPage());
//                }
//                else
//                {
//                    await DisplayAlert("Error", "Invalid BarCode.", "Ok");
//                    App.Current.MainPage = new ScanPage();
//                }

//            });
//        }
//        #endregion
//    }
//}
