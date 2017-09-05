using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Psoniapp_maps
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //InitializeComponent();
            btnMap.Clicked += (_, e) => Navigation.PushAsync(new GetLocationPage());
        }
    }
}
