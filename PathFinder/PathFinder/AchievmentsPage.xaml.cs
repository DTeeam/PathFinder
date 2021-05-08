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
        DescriptionPage desc;
        ObservableCollection<string> pointsList = new ObservableCollection<string>();
        public ObservableCollection<string> Items { get { return pointsList; } }


        public AchievmentsPage()
        {
            InitializeComponent();
        }

        public async void FillAchievments()
        {
            this.pointsList = new ObservableCollection<string>();
            this.litViewAchievment.ItemsSource = null;

            foreach(Point p in App.achList)
            {
                this.pointsList.Add(p.description);
            }
            this.litViewAchievment.ItemsSource = pointsList;
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            FillAchievments();
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

        private async void litViewAchievment_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            int id = 0;
            foreach(Point p in App.achList)
            {
                if(p.description == litViewAchievment.SelectedItem.ToString())
                    break;

                id++;
            }
            App.globalID = id;

            await Navigation.PushAsync(new DescriptionPage(id));
            
        }
    }
}