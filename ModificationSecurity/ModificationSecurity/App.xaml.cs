using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;
using System.Text;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ModificationSecurity
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }
        MainActivity mainActivity = new MainActivity();
        protected override void OnStart()
        {
            mainActivity.SQLite_Activity("open_app");
        }
        protected override void OnSleep()
        {
            mainActivity.SQLite_Activity("close_app");
        }
        protected override void OnResume()
        {
            mainActivity.SQLite_Activity("open_app");
        }
    }
}
