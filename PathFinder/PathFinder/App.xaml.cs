using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PathFinder
{
    public partial class App : Application
    {
        public static string DatabaseFilename = "database.db";
        public static int globalID = 0;
        public static List<Point> achList = new List<Point>();

        public App()
        {
            InitializeComponent();

            MainPage = new TabbedPage1();
            //MainPage = new AchievmentsPage();
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
