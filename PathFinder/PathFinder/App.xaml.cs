using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PathFinder
{
    public partial class App : Application
    {
        public static string DatabaseFilename = "database.db";
        public static int selectedID = 0;
        public App()
        {
            InitializeComponent();

            MainPage = new TabbedPage1();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
