using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace zzz
{
    public partial class MapPage : ContentPage
    {
        //get current location

        public MapPage()
        {
            InitializeComponent();
            //Initialize the map
            var map = new Map(
                MapSpan.FromCenterAndRadius(
                        new Position(37, -122), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;



            //Map types

            map.MapType = MapType.Street;
            btnHybridView.Clicked += (sender, e) =>
            {
                map.MapType = MapType.Hybrid;
            };
            btnSatteliteView.Clicked += (sender, e) =>
            {
                map.MapType = MapType.Satellite;
            };
            btnStreetView.Clicked += (sender, e) =>
            {
                map.MapType = MapType.Street;
            };

        }
    }
}
