using System;
using Xamarin.Forms;

namespace ModificationSecurity
{
    public partial class MainPage : ContentPage 
    {
        MainActivity mainActivity = new MainActivity();
        XOR xor = new XOR();
        public int N;
        public MainPage()
        {
            InitializeComponent();            
        }
        //Добавление баллов 
        private async void N_Add_Button_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdsPage());            
            //XOR-Дешифрование             
            N = int.Parse(xor.X_Decrypt(mainActivity.Get_NScore()));
            N += 1;
            ScoreText.Text = N.ToString();
            //XOR-шифрование
            mainActivity.Update_NScore(xor.X_Encrypt(N.ToString()));             
            N = 0;
        }   
    }
}