using System;
using System.Text;
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
            //Add score with XOR
            string temp = mainActivity.Get_trueScore();
            tempScore = int.Parse(xor.Decode(temp));
            tempScore += 1;
            mainActivity.Update_trueScore(xor.Encode(tempScore.ToString())); 
            ScoreText.Text = tempScore.ToString();
            tempScore = 0;
        }
    }
}