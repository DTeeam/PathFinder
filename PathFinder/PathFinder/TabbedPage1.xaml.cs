using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PathFinder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        public TabbedPage1()
        {
            InitializeComponent();
            NavigationPage MapPage = new NavigationPage(new MapPage());
            MapPage.Title = "Zemljevid";

            NavigationPage AchievmentsPage = new NavigationPage(new AchievmentsPage());
            AchievmentsPage.Title = "Dosežki";

            Children.Add(MapPage);
            Children.Add(AchievmentsPage);
        }
    }
}