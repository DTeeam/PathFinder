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

        private Database db;
        List<points> pointsList = new List<points>();
        public MapPage()
        {
            InitializeComponent();
            db = new Database();

            pointList();

            timer_elapsed();
            time.Enabled = true;
            time.Interval = 500;
            time.Elapsed += OnTimedEvent;
            time.Start();

            
        }


        async void OnButtonClicked(object sender, EventArgs args)
        {
            findMe();
        }

        private async void pointList()
        {
            pointsList = await db.GetPointsAsync();
            foreach(points point in pointsList)
            {
                Pin pin = new Pin
                {
                    Label = point.name,
                    Address = point.description,
                    Type = PinType.Place,
                    Position = new Position(point.coordX, point.coordY)
                };
                map.Pins.Add(pin);
            }

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

    }
}