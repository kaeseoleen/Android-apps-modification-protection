using System;
using Xamarin.Forms;
namespace ModificationSecurity
{
    public partial class MainPage : ContentPage 
    {
        MainActivity mainActivity = new MainActivity();
        public int N;
        public MainPage()
        {
            InitializeComponent();            
        }
        private async void N_Add_Button_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdsPage());
            //XOR-Дешифрование  
            N = int.Parse(mainActivity.MG_Decrypt(mainActivity.Get_NScore())) + 1;
            ScoreText.Text = N.ToString();
            //XOR-шифрование      
            mainActivity.Update_NScore(mainActivity.MG_Encrypt(N.ToString()));     
            N = 0;
        }   
    }
}