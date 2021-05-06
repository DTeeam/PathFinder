using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;

namespace PathFinder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AchievmentsPage : ContentPage
    {
        Database db = new Database();

        List<points> pointsList = new List<points>();
        public AchievmentsPage()
        {
            InitializeComponent();
        }

        public async void FillAchievments(int id)
        {

            pointsList = await db.GetPointsAsync();

            litViewAchievment.ItemsSource = pointsList[id].description;
            litViewAchievment.HeightRequest = (40 * pointsList.Count());
        }
    }
}