using System;

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
    public class JsonObjects
    {
        public class Rootobject
        {
            public Geometry geometry { get; set; }
            public string id { get; set; }
            public string place_id { get; set; }
            public string reference { get; set; }
        }

        public class Geometry
        {
            public Location location { get; set; }
        }

        public class Location
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }
    }

}

