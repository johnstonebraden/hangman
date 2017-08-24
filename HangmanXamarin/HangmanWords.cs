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

namespace HangmanXamarin
{
    class HangmanWords  
    {
        [PrimaryKey, AutoIncrement]
        public int WordID { get; set; }
        public string Word { get; set; }
        

        public HangmanWords(string word, int word_id)
        {
            Word = word;
            WordID = word_id;
        }

        public HangmanWords(){}


        public override string ToString()
        {
            return Word;
        }
    }
}