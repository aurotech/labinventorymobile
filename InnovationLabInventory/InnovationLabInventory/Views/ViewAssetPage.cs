using System;

using Xamarin.Forms;

namespace InnovationLabInventory.Views
{
    public class ViewAssetPage : ContentPage
    {
        public ViewAssetPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

