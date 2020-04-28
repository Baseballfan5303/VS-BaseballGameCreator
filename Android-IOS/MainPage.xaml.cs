using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_BaseballGameCreator
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void startNewGame_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SetRosters());
        }
        private async void VisitGithub_Click(object sender, EventArgs e)
        {
            String website = @"https://github.com/Baseballfan5303/VS-BaseballGameCreator";
            var uri = new Uri(website);

            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}
