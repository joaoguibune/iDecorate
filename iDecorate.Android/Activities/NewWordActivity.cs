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
using System.Collections;

namespace iDecorate.Android.Activities
{
    [Activity(Label = "Word")]
    public class NewWordActivity : Activity
    {
        private Spinner spinner;

        private List<TopicWordModel> words;
        private ListView ListViewWordData;
        private List<TopicModel> topics;
        private TopicModel topicSelected;

        private IClient<WordModel> _clientWord = new ClientWord();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            base.SetContentView(Resource.Layout.NewWord);

            var editTextWord = FindViewById<EditText>(Resource.Id.editTextWord);
            var editTextMeaning = FindViewById<EditText>(Resource.Id.editTextMeaning);
            var buttonAddWord = FindViewById<Button>(Resource.Id.buttonAddWord);

            words = Intent.GetStringExtra("Word") == null ? new List<TopicWordModel>() : JsonConvert.DeserializeObject<List<TopicWordModel>>(Intent.GetStringExtra("Word"));
            topics = Intent.GetStringExtra("Topic") == null ? new List<TopicModel>() : JsonConvert.DeserializeObject<List<TopicModel>>(Intent.GetStringExtra("Topic"));
                        
            ListViewWordData = FindViewById<ListView>(Resource.Id.ListViewWordData);

            var adapter = new ListViewWordAdapter(this, words);
            ListViewWordData.Adapter = adapter;

            spinner = FindViewById<Spinner>(Resource.Id.spinnerTopics);
            var spinneradapter = new CustomSpinnerAdapter(this, topics);
            spinner.Adapter = spinneradapter;
            spinner.ItemSelected += SpinnerItemSelected;
        }

        private void SpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            topicSelected = topics[e.Position];
        }
    }
}