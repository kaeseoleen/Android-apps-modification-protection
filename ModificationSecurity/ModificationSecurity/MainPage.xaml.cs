using System;
using Xamarin.Forms;

namespace ModificationSecurity
{
    public partial class MainPage : ContentPage 
    {
        MainActivity mainActivity = new MainActivity();
        XOR xor = new XOR();
        public int tempScore;
        public MainPage()
        {
            InitializeComponent();
            ScoreText.Text = mainActivity.Get_trueScore(); 
        }
        private async void AddScoreButton_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdsPage());
            //Добавление баллов 
            string temp = mainActivity.Get_trueScore();
            //XOR-Дешифрование 
            tempScore = int.Parse(xor.DecryptXOR(temp));
            tempScore += 1;
            //XOR-шифрование
            mainActivity.Update_trueScore(xor.EncryptXOR(tempScore.ToString())); 
            ScoreText.Text = tempScore.ToString();
            tempScore = 0;
        }   
    }
}