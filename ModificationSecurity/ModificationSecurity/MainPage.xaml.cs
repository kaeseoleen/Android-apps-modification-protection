using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ModificationSecurity
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void AddScoreButton_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdsPage());

        }
        private void ExitAppButton_Click(object sender, EventArgs e)
        {

        }
    }
}
