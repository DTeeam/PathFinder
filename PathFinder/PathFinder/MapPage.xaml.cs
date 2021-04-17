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
        List<points> pointsList = new List<points>();
        public MapPage()
        {
            InitializeComponent();
            db = new Database();

            //
            //pointList();

            timer_elapsed();
            time.Enabled = true;
            time.Interval = 500;
            time.Elapsed += OnTimedEvent;
            time.Start();
        }


        async void OnButtonClicked(object sender, EventArgs args)
        {
            findMe();
            pointList();
            
        }

        private async void pointList()
        {
            userXY();
            bool vis = false;


            pointsList = await db.GetPointsAsync();
            //foreach(points point in pointsList)
            //{
                userXY();               
                const double r = 6371e3;
                double numa = lat * Math.PI / 180;
                double numb = pointsList[st].coordX * Math.PI / 180;
                double numc = (pointsList[st].coordX - lat) * Math.PI / 180;
                double numd = (pointsList[st].coordY - lon) * Math.PI / 180;

                double a = Math.Sin(numc / 2) * Math.Sin(numc / 2) +
                    Math.Cos(numa) * Math.Cos(numb) *
                    Math.Sin(numd / 2) * Math.Sin(numd / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                double d = r * c;

                Console.WriteLine("    DISTANCE:    " + d );
                //TODO spod nastavi d <= 75(m), 1005 je sam za testirat.
                if (d <= 1005 || pointsList[st].discovered == 1)
                {
                    vis = true;

                    Pin pin = new Pin
                    {
                        Label = pointsList[st].name,
                        Address = pointsList[st].description,
                        Type = PinType.Place,
                        Position = new Position(pointsList[st].coordX, pointsList[st].coordY),
                        IsVisible = vis
                    };
                    map.Pins.Add(pin);
                    st++;
                }
            //}


        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            timer_elapsed();
        }
       
        private async void timer_elapsed()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(1)));
        }

        private async void findMe()
        {
            var locator = CrossGeolocator.Current;
            Plugin.Geolocator.Abstractions.Position position = new Plugin.Geolocator.Abstractions.Position();

            position = await locator.GetPositionAsync();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                            Distance.FromKilometers(1)));
            
            
        }

        private async void userXY()
        {
            var locator = CrossGeolocator.Current;
            Plugin.Geolocator.Abstractions.Position position = new Plugin.Geolocator.Abstractions.Position();

            position = await locator.GetPositionAsync();

            lat = position.Latitude;
            lon = position.Longitude;

        }

    }
}