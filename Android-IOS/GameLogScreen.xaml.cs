using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_BaseballGameCreator
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameLogScreen : ContentPage
    {
        public GameLogScreen()
        {
            InitializeComponent();

            Finally.Text += BaseballGameScreen.getFINALGAMELOG().ToString();
        }

        private async void saveGameLog_Click(object sender, EventArgs e)
        {
            var filename = System.IO.Path.Combine("storage/emulated/0/Download/", "BaseballGC-GameLog-" + BaseballGameScreen.getAwayTeamNameForGL() + "_vs._" + BaseballGameScreen.getHomeTeamNameForGL() + "_" + extraNotesX.Text + ".txt");

            using (var streamWriter = new StreamWriter(filename))
            {
                streamWriter.WriteLine(extraNotesX.Text + "\n" + Finally.Text);
            }

            await Navigation.PushAsync(new MainPage());
        }
    }
}