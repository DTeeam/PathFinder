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

    }
}