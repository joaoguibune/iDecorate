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
using iDecorate.Android.Business.Client;
using iDecorate.Android.Business.Contract;
using iDecorate.Android.Model;
using Newtonsoft.Json;
using iDecorate.Android.Adapters.Main;

namespace iDecorate.Android.Activities
{
    [Activity(Label = "Word")]
    public class NewWordActivity : Activity
    {

        List<TopicWordModel> words;

        ListView ListViewWordData;

        private IClient<WordModel> _clientWord = new ClientWord();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            base.SetContentView(Resource.Layout.NewWord);

            var editTextWord = FindViewById<EditText>(Resource.Id.editTextWord);
            var editTextMeaning = FindViewById<EditText>(Resource.Id.editTextMeaning);
            var buttonAddWord = FindViewById<Button>(Resource.Id.buttonAddWord);

            words = Intent.GetStringExtra("Word") == null ? new List<TopicWordModel>() : JsonConvert.DeserializeObject<List<TopicWordModel>>(Intent.GetStringExtra("Word"));

            ListViewWordData = FindViewById<ListView>(Resource.Id.ListViewWordData);

            var adapter = new ListViewWordAdapter(this, words);
            ListViewWordData.Adapter = adapter;
        }
    }
}