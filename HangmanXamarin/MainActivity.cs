using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.IO;
using SQLite;
using Android.Graphics;
using Android.Support.V7.App;
using Android.Views;
using Android.Database.Sqlite;
using System.Threading.Tasks;
using Android.Util;
using Android.Content.PM;

namespace HangmanXamarin
{
    [Activity(Label = "", MainLauncher = false, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        //(So the buttons are public
        Button KeyA;
        Button KeyB;
        Button KeyC;
        Button KeyD;
        Button KeyE;
        Button KeyF;
        Button KeyG;
        Button KeyH;
        Button KeyI;
        Button KeyJ;
        Button KeyK;
        Button KeyL;
        Button KeyM;
        Button KeyN;
        Button KeyO;
        Button KeyP;
        Button KeyQ;
        Button KeyR;
        Button KeyS;
        Button KeyT;
        Button KeyU;
        Button KeyV;
        Button KeyW;
        Button KeyX;
        Button KeyY;
        Button KeyZ; 
        //initialising variables
        int wordLength;
        int score = 0;
        int chances = 0;
        int CheckCount = 0;
        int primaryKey = 1;
        int letterInt = 0;          //letterint is refering to which letterindex in the array
        int cycleStage = 0;
        string name;
        string placeholders;
        string currentWORD;       
        string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbHangman.db3");     //setting path to database
        string[] placeholderArray = new string[] { };
        int[] gameCycle = { Resource.Drawable.State2, Resource.Drawable.State3, Resource.Drawable.State4, Resource.Drawable.State5, Resource.Drawable.State6, Resource.Drawable.State7 };   //image array for the gamecycle
        TextView txtScore;
        TextView txtWORD;
        ImageView hangMan;
        Button Menu;
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);

            //setting up the connection
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<HangmanWords>();
            db.CreateTable<HighScore>();

            var tb = db.Table<HangmanWords>();
            
            if(tb.Count() != 0)
            {
                //Toast.MakeText(this, "DB ALREADY EXISTS", ToastLength.Long).Show();
            }
            //setup a table
            else if(tb.Count() == 0)
            {

                string[] words = Resources.GetStringArray(Resource.Array.wordList);
                foreach (var word in words)
                {
                    //adds the words into the database
                    HangmanWords WORD = new HangmanWords(word,primaryKey);
                    db.Insert(WORD);
                    primaryKey++;
                }
                //Toast.MakeText(this, "DB MADE", ToastLength.Long).Show();
            }
            

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            hangMan = FindViewById<ImageView>(Resource.Id.ivHangMan);
            hangMan.SetImageResource(Resource.Drawable.State1);
            //setting all the buttons 
            KeyA = FindViewById<Button>(Resource.Id.KeyA);
            KeyB = FindViewById<Button>(Resource.Id.KeyB);
            KeyC = FindViewById<Button>(Resource.Id.KeyC);
            KeyD = FindViewById<Button>(Resource.Id.KeyD);
            KeyE = FindViewById<Button>(Resource.Id.KeyE);
            KeyF = FindViewById<Button>(Resource.Id.KeyF);
            KeyG = FindViewById<Button>(Resource.Id.KeyG);
            KeyH = FindViewById<Button>(Resource.Id.KeyH);
            KeyI = FindViewById<Button>(Resource.Id.KeyI);
            KeyJ = FindViewById<Button>(Resource.Id.KeyJ);
            KeyK = FindViewById<Button>(Resource.Id.KeyK);
            KeyL = FindViewById<Button>(Resource.Id.KeyL);
            KeyM = FindViewById<Button>(Resource.Id.KeyM);
            KeyN = FindViewById<Button>(Resource.Id.KeyN);
            KeyO = FindViewById<Button>(Resource.Id.KeyO);
            KeyP = FindViewById<Button>(Resource.Id.KeyP);
            KeyQ = FindViewById<Button>(Resource.Id.KeyQ);
            KeyR = FindViewById<Button>(Resource.Id.KeyR);
            KeyS = FindViewById<Button>(Resource.Id.KeyS);
            KeyT = FindViewById<Button>(Resource.Id.KeyT);
            KeyU = FindViewById<Button>(Resource.Id.KeyU);
            KeyV = FindViewById<Button>(Resource.Id.KeyV);
            KeyW = FindViewById<Button>(Resource.Id.KeyW);
            KeyX = FindViewById<Button>(Resource.Id.KeyX);
            KeyY = FindViewById<Button>(Resource.Id.KeyY);
            KeyZ = FindViewById<Button>(Resource.Id.KeyZ);
            Button NewWord = FindViewById<Button>(Resource.Id.btnNewWord);
            txtScore = FindViewById<TextView>(Resource.Id.txtScore);
            Menu = FindViewById<Button>(Resource.Id.btnMenu);
            Menu.Click += Menu_Click;

            
            KeyA.Click += myKeyPress;
            KeyB.Click += myKeyPress;
            KeyC.Click += myKeyPress;
            KeyD.Click += myKeyPress;
            KeyE.Click += myKeyPress;
            KeyF.Click += myKeyPress;
            KeyG.Click += myKeyPress;
            KeyH.Click += myKeyPress;
            KeyI.Click += myKeyPress;
            KeyJ.Click += myKeyPress;
            KeyK.Click += myKeyPress;
            KeyL.Click += myKeyPress;
            KeyM.Click += myKeyPress;
            KeyN.Click += myKeyPress;        
            KeyO.Click += myKeyPress;
            KeyP.Click += myKeyPress;
            KeyQ.Click += myKeyPress;
            KeyR.Click += myKeyPress;
            KeyS.Click += myKeyPress;
            KeyT.Click += myKeyPress;
            KeyU.Click += myKeyPress;
            KeyV.Click += myKeyPress;
            KeyW.Click += myKeyPress;
            KeyX.Click += myKeyPress;
            KeyY.Click += myKeyPress;
            KeyZ.Click += myKeyPress;

            //creating button array
            Button[] buttonArray = { KeyA, KeyB, KeyC, KeyD, KeyE, KeyF, KeyG, KeyH, KeyI, KeyJ, KeyK, KeyL, KeyM, KeyN, KeyO, KeyP, KeyQ, KeyR, KeyS, KeyT, KeyU, KeyV, KeyW, KeyX, KeyY, KeyZ };


            txtWORD = FindViewById<TextView>(Resource.Id.txtWord);
           //getting a random word from the database and getting he length of the word
            Random rnd = new Random();
            currentWORD = db.Get<HangmanWords>(rnd.Next(1, 43)).ToString();
            wordLength = currentWORD.Length;
            
            //char[] letterArray = currentWORD.ToCharArray();
            

            Array.Resize(ref placeholderArray, wordLength);

            for (int i = 0; i < placeholderArray.Length; i++)              //depending on the length of the random word the length of this array is set in Underscores
            {
                placeholderArray[i] = "_ ";
            }


            placeholders = string.Concat(placeholderArray);
            txtWORD.Text = placeholders;

            NewWord.Click += delegate        //if the player wants a new word this button will reset game and set a new word
            {
                Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(this);

                alertDialog.SetMessage("Are you sure? Your score will reset");
                alertDialog.SetPositiveButton("Yes!", delegate                         //if they say yes game will be reset
                {
                    cycleStage = 0;
                    chances = 0;
                    currentWORD = db.Get<HangmanWords>(rnd.Next(1, 43)).ToString();
                    wordLength = currentWORD.Length;
                    CheckCount = 0;
                    letterInt = 0;

                    Array.Resize(ref placeholderArray, wordLength);

                    for (int i = 0; i < placeholderArray.Length; i++)
                    {
                        placeholderArray[i] = "_ ";
                    }
                    hangMan.SetImageResource(gameCycle[cycleStage]);

                    placeholders = string.Concat(placeholderArray);
                    txtWORD.Text = placeholders;
                    foreach (var button in buttonArray)
                    {
                        button.SetTextColor(Color.Black);
                        button.Enabled = true;
                    }
                    alertDialog.Dispose();
                }); 
                alertDialog.SetNegativeButton("Cancel", delegate          //if thy say no nothing happens
                {
                    alertDialog.Dispose();
                });
                alertDialog.Show();
            };  
            

            

        }

        private void Menu_Click(object sender, EventArgs e)
        {
            Finish();
        }

        protected void myKeyPress(object sender, EventArgs e)      //checks to see if the key pressed is in the word,then the right outcome is shown (changing colour of text, adding letters to placeholder array)
        {
            Button[] buttonArray = { KeyA, KeyB, KeyC, KeyD, KeyE, KeyF, KeyG, KeyH, KeyI, KeyJ, KeyK, KeyL, KeyM, KeyN, KeyO, KeyP, KeyQ, KeyR, KeyS, KeyT, KeyU, KeyV, KeyW, KeyX, KeyY, KeyZ };
            CheckCount = 0;
            letterInt = 0;
            char[] letterArray = currentWORD.ToCharArray();

            Button button = sender as Button;
            var db = new SQLiteConnection(dbPath);

            foreach (var letter in letterArray)
            {                                                                                     //goes through each letter in the current work to check the letter guessed against letters from the word
                if(button.Text == letter.ToString() && cycleStage != 5)
                {
                    placeholderArray[letterInt] = button.Text;
                    //button.Enabled = false;
                    button.SetTextColor(Color.Green);
                }
                else if(button.Text != letter.ToString() && cycleStage != 5)
                {

                    chances++;
                    CheckCount++;
                }

                if (cycleStage == 5)
                {
                    Toast.MakeText(this, "Game over", ToastLength.Long).Show();
                    
                }
                else if (CheckCount == letterArray.Length)
                {
                    cycleStage++;
                    hangMan.SetImageResource(gameCycle[cycleStage]);
                    //button.Enabled = false;
                    button.SetTextColor(Color.Red);
                    hangMan.SetImageResource(gameCycle[cycleStage]);
                    
                }
                
                letterInt = letterInt + 1;
                
            }

            if (string.Concat(placeholderArray) == currentWORD)         //if the word is guessed, alert shown, score increased, new word
            {
                score = score + 100;
                txtScore.Text = "Score: " + score;

                Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(this);

                alertDialog.SetMessage("You guessed the word!");
                alertDialog.SetNeutralButton("Next Word!", delegate
                {
                    cycleStage = 0;
                    chances = 0;
                    Random rnd = new Random();
                    currentWORD = db.Get<HangmanWords>(rnd.Next(1, 43)).ToString();                       //sets a new word if the guess the word 
                    wordLength = currentWORD.Length;
                    CheckCount = 0;
                    letterInt = 0;

                    Array.Resize(ref placeholderArray, wordLength);

                    for (int i = 0; i < placeholderArray.Length; i++)
                    {
                        placeholderArray[i] = "_ ";
                    }
                    hangMan.SetImageResource(gameCycle[cycleStage]);

                    placeholders = string.Concat(placeholderArray);
                    txtWORD.Text = placeholders;
                    foreach (var b in buttonArray)
                    {
                        b.SetTextColor(Color.Black);
                        b.Enabled = true;
                    }
                    alertDialog.Dispose();
                });
                alertDialog.Show();

            }
            placeholders = string.Concat(placeholderArray);
            txtWORD.Text = placeholders;

            if (cycleStage == 5)
            {
                //creating an alert to get the users name to save with their score
                LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
                View mView = layoutInflaterAndroid.Inflate(Resource.Layout.user_input_dialog_box, null);
                Android.Support.V7.App.AlertDialog.Builder alertDialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertDialogbuilder.SetView(mView);
                alertDialogbuilder.SetTitle("Gameover");
                alertDialogbuilder.SetMessage("You ran out of attempts!");

                var userContent = mView.FindViewById<EditText>(Resource.Id.editText1);
                alertDialogbuilder.SetCancelable(false)
                 .SetPositiveButton("Save", delegate
                  {
                      name = userContent.Text;

                      //saving the score
                      HighScore HScore = new HighScore(name, score);
                      db.Insert(HScore);

                      score = 0;
                      StartActivity(typeof(MainActivity));
                      Finish();
                      alertDialogbuilder.Dispose();
                  });
                alertDialogbuilder.Show();
            }         
        } 
    }
}

