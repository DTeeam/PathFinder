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
    public partial class DescriptionPage : ContentPage
    {
        public DescriptionPage(int id)
        {
            InitializeComponent();
            fillPage(id);
        }
        public async void fillPage(int id)
        {
            labelTitle.Text = App.achList[id].name;
            labelDesc.Text = App.achList[id].description;
            imagaImg.Source = App.achList[id].image;
        }
    }
}