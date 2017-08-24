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
using SQLite;
using Android.Content.PM;

namespace HangmanXamarin
{ 
        [Activity(Label = "HangmanXamarin", MainLauncher = false, Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar", ScreenOrientation = ScreenOrientation.Portrait)]
        public class Scores : Activity
        {
            string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbHangman.db3");

            
            protected override void OnCreate(Bundle bundle)
            {               
                base.OnCreate(bundle);
                SetContentView(Resource.Layout.HighScores);
                var db = new SQLiteConnection(dbPath);

                var table = db.Table<HighScore>();
                
                TextView txtHighscores = FindViewById<TextView>(Resource.Id.txtHighscores);
            //loading in the scores from database
            foreach (var item in table)
            {
                HighScore score = new HighScore(item.scoreID, item.Score);                                 //FIXME      does not order the highscores
                txtHighscores.Text += score + "\n ";
            }

                Button btnMenu = FindViewById<Button>(Resource.Id.btnBackToMenu);


                btnMenu.Click += BtnMenu_Click;
            }

            private void BtnMenu_Click(object sender, EventArgs e)
            {
                StartActivity(typeof(MainMenu));
            }
        }    
}