using System;
using InnovationLabInventory.Models;
using Xamarin.Forms;

namespace InnovationLabInventory.Views
{
    public class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            Master = new MenuPage();
            Detail = new HomePage();
        }
    }

    public class MainViewAssetPage : MasterDetailPage
    {
        public MainViewAssetPage()
        {
            Master = new MenuPage();
            Detail = new ViewAssetPage();
        }
    }

    public class MainCreateAssetPage : MasterDetailPage
    {
        public MainCreateAssetPage()
        {
            Master = new MenuPage();
            Detail = new CreateAssetPage();
        }
    }

    public class MainScanPage : MasterDetailPage
    {
        public MainScanPage()
        {
            Master = new MenuPage();
            Detail = new ScanPage();
        }
    }

    public class MainManagerAssetView : MasterDetailPage
    {
        public MainManagerAssetView(ViewAsset Viewasset, ScanResponce scanResponce)
        {
            Master = new MenuPage();
            {
                Detail = new ManagerAssetView(Viewasset, scanResponce);
            }
        }
    }
    public class MainBatchAssets : MasterDetailPage
    {
        public MainBatchAssets(ScanResponce scanResponce, ViewAsset Viewasset)
        {
            Master = new MenuPage();
            {
                Detail = new BatchAssets(scanResponce, Viewasset);
            }
        }
    }

    //public class MainUserPage : MasterDetailPage
    //{
    //    public MainUserPage()
    //    {
    //        Master = new MenuPage();
    //        Detail = new UserPage();
    //    }
    //}

    //public class MainUserAssetView : MasterDetailPage
    //{
    //    public MainUserAssetView()
    //    {
    //        Master = new MenuPage();
    //        Detail = new UserAssetView();
    //    }
    //}

    //public class MainUserViewAssetPage : MasterDetailPage
    //{
    //    public MainUserViewAssetPage()
    //    {
    //        Master = new MenuPage();
    //        Detail = new UserViewAssetPage();
    //    }
    //}
}
