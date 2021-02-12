using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agoratopia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var date = DateTime.Today.ToShortDateString();
            var hour = DateTime.Now.Hour;
            var timeOfDay = "";

            if (hour < 12)
                timeOfDay = "morning";
            else if (hour < 18)
                timeOfDay = "afternoon";
            else
                timeOfDay = "evening";

            DateTest.Text = "Good " + timeOfDay + "! Today is:\n " + date + "\n\n\n\n\n\n\n\n";
        }

        private void SubmitReport(object sender, EventArgs e)
        {
            var exposure = ExposureBool.SelectedItem;
            var stress = StressScale.SelectedItem;
            var anxiety = BearableAnxiety.SelectedItem;
            var date = DateTime.Today.ToShortDateString();

            DisplayAlert("Test Message", exposure + "\n" + stress + "\n" + anxiety + "\nRecorded on: " + date, "OK *thumbs up*");
        }
    }
}