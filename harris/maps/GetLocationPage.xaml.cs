using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Psoniapp_maps
{
    public partial class GetLocationPage : ContentPage
    {
        public GetLocationPage()
        {
            //InitializeComponent();
            
                //btnlocation.Clicked += (_, e) =>
                if (true){
                    Navigation.PushAsync(new GetLocationPage());
                }
        }
    }
}
