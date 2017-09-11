using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace zzz.Droid
{
    [Activity(Label = "zzz", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {   //general setup
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            _addressText = FindViewById<TextView>(Resource.Id.address_text);
            _locationText = FindViewById<TextView>(Resource.Id.location_text);
            FindViewById<TextView>(Resource.Id.get_address_button).Click += AddressButton_OnClick;


            global::Xamarin.Forms.Forms.Init(this, bundle);
            InitializeLocationManager();
            LoadApplication(new App());
        }
        //User's location

        static readonly string TAG = "X:" + typeof(Activity1).Name;
            TextView _addressText;
            Location _currentLocation;
            LocationManager _locationManager;

        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + _locationProvider + ".");
        }

        [Activity(Label = "Get Location", MainLauncher = true, Icon = "@drawable/icon")]

        public class Activity1 : Activity, ILocationListener
        {
            // removed code for clarity

            public void OnLocationChanged(Location location) { }

            public void OnProviderDisabled(string provider) { }

            public void OnProviderEnabled(string provider) { }

            public void OnStatusChanged(string provider, Availability status, Bundle extras) { }
        }

        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }
        async void AddressButton_OnClick(object sender, EventArgs eventArgs)
        {
            if (_currentLocation == null)
            {
                _addressText.Text = "Can't determine the current address. Try again in a few minutes.";
                return;
            }

            Address address = await ReverseGeocodeCurrentLocation();
            DisplayAddress(address);
        }

        async Task<Address> ReverseGeocodeCurrentLocation()
        {
            Geocoder geocoder = new Geocoder(this);
            IList<Address> addressList =
                await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

            Address address = addressList.FirstOrDefault();
            return address;
        }

        void DisplayAddress(Address address)
        {
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
                _addressText.Text = deviceAddress.ToString();
            }
            else
            {
                _addressText.Text = "Unable to determine the address. Try again in a few minutes.";
            }
        }

        public async void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation == null)
            {
                _locationText.Text = "Unable to determine your location. Try again in a short while.";
            }
            else
            {
                _locationText.Text = string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude);
                Address address = await ReverseGeocodeCurrentLocation();
                DisplayAddress(address);
            }
        }

        static void Main(string[] args)
        {
            var client = new WebClient();
            var web = "https://maps.googleapis.com/maps/api/place/radarsearch/json?location={0},{1}&radius=800&type=supermarkets&key=AIzaSyCLWikvHTQytNmnw71Kj2mCH_CsTczMTtU";
            var lat = _currentLocation.Latitude;
            var lon = _currentLocation.Longitude;
            var url = string.Format(web, lat, lon);
            var text = client.DownloadString(url);
        
        }
    }
}

