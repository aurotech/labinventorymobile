using System;
using System.Collections.Generic;
using System.Linq;
using InnovationLabInventory.Views;
using Foundation;
using UIKit;

namespace InnovationLabInventory.iOS
{

    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        // global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();
            #if !DEBUG
            TestFlight.TakeOff("key");
            #endif
            #region For ZXing
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            #endregion

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            // Convert NSUrl to Uri
            var uri = new Uri(url.AbsoluteString);

            // Load redirectUrl page
            AuthenticationState.Authenticator.OnPageLoading(uri);

            return true;
        }
    }
}
