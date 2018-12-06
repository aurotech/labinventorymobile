using System;
using System.Collections.Generic;
using Xamarin.Forms;
using InnovationLabInventory.WebServices;
using InnovationLabInventory.Renderers;
using System.Linq;

namespace InnovationLabInventory.Views
{
    public partial class ViewAssetPage : ContentPage
    {
        CloudDataStore cloudStore = new CloudDataStore();
        List<ViewAsset> response;
        List<ViewAsset> response1;
        List<ViewAsset> finalResponse;
        public static int value = 0;

        public ViewAssetPage()
        {
            InitializeComponent();
            GetData();
            //List<ViewAsset> Searchlst = new List<ViewAsset>();
            //AssetIDEntry.TextChanged += (object sender, TextChangedEventArgs e) =>
            //{
            //    Searchlst.Clear();
            //    Searchlst = response.Where(i => i.assetId.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
            //    if (Searchlst.Count == 0)
            //    {
            //        Searchlst = response.Where(i => i.description.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
            //        if (Searchlst.Count == 0)
            //        {
            //            Searchlst = response.Where(i => i.status.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
            //        }
            //    }

            //    listView.ItemsSource = Searchlst;
            //};

            AssetIDEntry.TextChanged += (sender, e) => {
                Entry ssearchRef = sender as Entry;
                var str = ssearchRef.Text;
                listView.ItemsSource = finalResponse.Where(x => x.assetId.Contains(ssearchRef.Text) || x.description.Contains(ssearchRef.Text) || x.status.Contains(ssearchRef.Text) || x.assetType.Contains(ssearchRef.Text)).ToList();
            };

            listView.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                ViewAsset selected = listView.SelectedItem as ViewAsset;
                if(selected.assetType =="single"){
                    Navigation.PushModalAsync(new MainManagerAssetView(selected, null));  
                }
                else{
                    Navigation.PushModalAsync(new MainBatchAssets(null, selected));
                }

                //((ListView)sender).SelectedItem = null;
            };

            // Tap Gesture for Menu
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

            //listView.ItemTemplate = new DataTemplate(typeof(ListCell));
            //listView.SeparatorVisibility = SeparatorVisibility.None;

            //listView.ItemSelected += (sender, e) =>
            //{
            //    if (e.SelectedItem == null)
            //    {
            //        return;
            //    }

            //    Prototype selected = listView.SelectedItem as Prototype;
            //    Navigation.PushModalAsync(new MainPrototypesInfo(selected));
            //    ((ListView)sender).SelectedItem = null;
            //};
        }


        async void GetData()
        {
            try
            {
                response = await cloudStore.GetAssets();
                response1 = new List<ViewAsset>();
                foreach(var item in response)
                {
                    response1.Add(new ViewAsset(){assetId=item.assetId, description=item.description, status=item.status,assignee = item.assignee, timeLastScanned = item.timeLastScanned, timeLastUpdated=item.timeLastUpdated, comment=item.comment, assetType=item.assetType, quantity= item.status=="Removed"?0:item.assetType =="single"?1:item.quantity});
                }
                //List<ViewAsset> tempResSplit = new 
                for (int i = 0; i < response1.Count; i++)
                {
                    ViewAsset tempObj = new ViewAsset();
                    tempObj = response1[i];
                    //if(response1[i].assetType == "Removed"){
                    //    AssetType
                    //}
                    if (response1[i].assignee != null)
                    {
                        String[] Stringbreak = response1[i].assignee.Split('#');
                        tempObj.assignee = Stringbreak[1];
                    }
                    response1[i] = tempObj;
                }

                finalResponse = response1;
                listView.ItemsSource = response1;

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
            App.Current.MainPage = new MainPage();
        }

        //public class ListCell : ViewCell
        //{
        //    public ListCell()
        //    {
        //        var grid = new Grid()
        //        {
        //            HorizontalOptions = LayoutOptions.FillAndExpand,
        //            VerticalOptions = LayoutOptions.FillAndExpand,
        //            BackgroundColor = Color.FromHex("#fbd0a9")
        //        };
        //        //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        //        //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        //        //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        //        //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        //        //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

        //        var assetLabel = new Label()
        //        {
        //            VerticalOptions = LayoutOptions.CenterAndExpand,
        //            HorizontalOptions = LayoutOptions.Start,
        //            Margin = new Thickness(5, 0, 0, 0),
        //            TextColor = Color.FromHex("#301631"),
        //            FontSize = 15
        //        };
        //        assetLabel.SetBinding(Label.TextProperty, "assetId");

        //        BoxView vbox = new BoxView()
        //        {
        //            VerticalOptions = LayoutOptions.FillAndExpand,
        //            WidthRequest = 1,
        //            Color = Color.FromHex("#ffffff"),
        //            HorizontalOptions = LayoutOptions.End
        //        };
        //        var desLabel = new Label()
        //        {
        //            VerticalOptions = LayoutOptions.CenterAndExpand,
        //            HorizontalOptions = LayoutOptions.Start,
        //            //Margin = new Thickness(10, 0, 0, 0),

        //            TextColor = Color.FromHex("#301631"),
        //            FontSize = 15
        //        };
        //        desLabel.SetBinding(Label.TextProperty, "description");

        //        BoxView sbox = new BoxView()
        //        {
        //            VerticalOptions = LayoutOptions.FillAndExpand,
        //            WidthRequest = 1,
        //            Color = Color.FromHex("#ffffff"),
        //            HorizontalOptions = LayoutOptions.End
        //        };

        //        var statusLabel = new Label()
        //        {
        //            VerticalOptions = LayoutOptions.CenterAndExpand,
        //            HorizontalOptions = LayoutOptions.Start,
        //            //Margin = new Thickness(10, 0, 0, 0),
        //            TextColor = Color.FromHex("#301631"),
        //            FontSize = 15
        //        };
        //        statusLabel.SetBinding(Label.TextProperty, "status");
        //        grid.Children.Add(assetLabel, 0, 0);
        //        grid.Children.Add(vbox, 1, 0);
        //        grid.Children.Add(desLabel, 2, 0);
        //        grid.Children.Add(sbox, 3, 0);
        //        grid.Children.Add(statusLabel, 4, 0);
        //        BoxView box = new BoxView()
        //        {
        //            HorizontalOptions = LayoutOptions.FillAndExpand,
        //            HeightRequest = 1,
        //            Color = Color.FromHex("#ffffff"),
        //            VerticalOptions = LayoutOptions.End
        //        };
        //        StackLayout stack = new StackLayout()
        //        {
        //            HorizontalOptions = LayoutOptions.FillAndExpand,
        //            VerticalOptions = LayoutOptions.FillAndExpand,
        //            Spacing = 0
        //        };
        //        stack.Children.Add(grid);
        //        stack.Children.Add(box);
        //        View = stack;
        //        if (ViewAssetPage.value == 0)
        //        {
        //            grid.BackgroundColor = Color.FromHex("#ce958e");
        //            ViewAssetPage.value++;
        //        }

        //        else if (ViewAssetPage.value == 1)
        //        {
        //            grid.BackgroundColor = Color.FromHex("#f49480");
        //            ViewAssetPage.value = 0;
        //        }
        //        View = stack;
        //    }
        //}
    }
}
