using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using System.Timers;
using Plugin.Geolocator;
using Android.OS;

namespace PathFinder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        Timer time = new Timer();
        double lat = 0;
        double lon = 0;
        private Database db;
        int st = 0;
        List<Point> pointsList = new List<Point>();
        public MapPage()
        {
            InitializeComponent();
            //TODO odkomentiri za release.
            //DisplayAlert("Namig", "Pozdravljeni, prva točka ima zanimiv dizajn, našli pa jo boste v centru mesta ob reki Paki.", "Začni");
            db = new Database();

            Timer_elapsed();
            time.Enabled = true;
            time.Interval = 1000;
            time.Elapsed += OnTimedEvent;
            time.Start();
        }


        async void OnButtonClicked(object sender, EventArgs args)
        {
            Timer_elapsed();
        }

        private async void PointList()
        {
            pointsList = await db.GetPointsAsync();
            Point point = pointsList[st];

            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);

            double distance = Location.CalculateDistance(location.Latitude, location.Longitude, point.coordX, point.coordY, DistanceUnits.Kilometers);
            
            
            Console.WriteLine(location + "DELA");
            lat = location.Latitude;
            lon = location.Longitude;

            bool vis = false;
            distance *= 1000;
            Console.WriteLine(distance + "   DISTANCE:");
            //TODO spod nastavi d <= 25(m), ce je kaj druga je sam za testirat.
            if (distance <= 25 || pointsList[st].discovered == 1)
            {
                vis = true;

                Pin pin = new Pin
                {
                    Label = point.name,
                    Type = PinType.Place,
                    Position = new Position(point.coordX, point.coordY),
                    IsVisible = vis
                };


                pin.Clicked += (sender, e) => {
                    Point selectedPin = (Point)pin.Tag;
                    DisplayAlert("Namig", selectedPin.hint, "Zapri");
                    Console.WriteLine(pin.Tag + "DELA");
                };

                pin.Tag = point;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    map.Pins.Add(pin);
                });

                App.achList.Add(point);
                App.globalID = 1;

                st++;
            }

        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Timer_elapsed();
        }
       
        private async void Timer_elapsed()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(1)));

           


            PointList();
        }
    }
}