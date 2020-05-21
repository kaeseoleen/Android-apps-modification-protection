using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
            mainActivity.Data_Activity("open_app");
        }
        protected override void OnSleep()
        {
            mainActivity.Data_Activity("close_app");
        }
        protected override void OnResume()
        {
            mainActivity.Data_Activity("open_app");
        }
    }
}
