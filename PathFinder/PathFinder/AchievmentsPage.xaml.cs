using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;
using System.Collections;
using System.Collections.Specialized;

namespace PathFinder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AchievmentsPage : ContentPage
    {
        Database db = new Database();

        ObservableCollection<string> pointsList = new ObservableCollection<string>();
        public ObservableCollection<string> Items { get { return pointsList; } }


        public AchievmentsPage()
        {
            InitializeComponent();
            lol();

        }

        public async void FillAchievments()
        {
            this.pointsList.Add("test");
            this.pointsList = new ObservableCollection<string>();
            this.litViewAchievment.ItemsSource = null;

            foreach(points p in App.achList)
            {
                this.pointsList.Add(p.description);
            }
            this.litViewAchievment.ItemsSource = pointsList;
        }

        public async void lol()
        {

            pointsList.Add("A");
            pointsList.Add("B");
            pointsList.Add("C");
            litViewAchievment.ItemsSource = pointsList;

        }
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            lol();
        }
        private void ListView_Refreshing(object sender, EventArgs e)
        {
            litViewAchievment.ItemsSource = pointsList;
            litViewAchievment.EndRefresh();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.globalID != 0)
            {
                FillAchievments();
                App.globalID = 0;
            }
        }

        private void litViewAchievment_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            int id = 0;
            foreach(points p in App.achList)
            {
                if(p.description == litViewAchievment.SelectedItem.ToString())
                    break;

                id++;
            }
            Console.WriteLine((App.achList.IndexOf(litViewAchievment.SelectedItem) + 1) + " DELA");
            Console.WriteLine(litViewAchievment.SelectedItem + " !DELA");
        }
    }
}