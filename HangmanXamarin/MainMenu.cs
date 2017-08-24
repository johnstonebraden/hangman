using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace HangmanXamarin
{
    [Activity( MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    class MainMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.MainMenu);


            Button btnHighScores = FindViewById<Button>(Resource.Id.HighScores);
            Button btnStart = FindViewById<Button>(Resource.Id.btnPStart);

            btnStart.Click += BtnStart_Click;
            btnHighScores.Click += BtnHighScores_Click;
        }

        private void BtnHighScores_Click(object sender, EventArgs e)     //setting buttons to open up different activities
        {
            StartActivity(typeof(Scores));
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}