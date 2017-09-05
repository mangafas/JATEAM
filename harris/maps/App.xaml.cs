using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Psoniapp_maps
{
    public partial class GetLocation : Application
    {
        public GetLocation()
        {
            //InitializeComponent();

            MainPage = new Psoniapp_maps.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
