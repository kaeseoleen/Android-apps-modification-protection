using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace ModificationSecurity
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdsPage : ContentPage
	{
		public AdsPage ()
		{
			InitializeComponent ();
		}
        private async void AdEndedButton_Click(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}