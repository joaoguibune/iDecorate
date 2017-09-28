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
using iDecorate.Android.Util;

namespace iDecorate.Android.Activities
{
    [Activity(Label = "Word")]
    public class NewWordActivity : Activity
    {
        private Spinner spinner;
        private CustomSpinnerAdapter spinnerAdapter;
        private ListView listViewWordData;
        private ListViewWordAdapter adapterListViewWord;
        private ProgressCustom progress;
        private EditText editTextWord;
        private EditText editTextMeaning;
        private Button buttonAddWord;

        private List<TopicWordModel> words = new List<TopicWordModel>();
        private List<TopicModel> topics = new List<TopicModel>();
        private TopicModel topicSelected;

        private IClient<WordModel> _clientWord = new Client<WordModel>("Word");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            base.SetContentView(Resource.Layout.NewWord);

            progress = new ProgressCustom(this);

            progress.Show();

            topics = Intent.GetStringExtra("Topic") == null ? new List<TopicModel>() : JsonConvert.DeserializeObject<List<TopicModel>>(Intent.GetStringExtra("Topic"));

            topics.ForEach(topic =>
            {
                topic.words.ForEach(word =>
                {
                    words.Add(new TopicWordModel
                    {
                        topic_description = topic.description,
                        word_description = word.description,
                        word_meaning = word.meaning
                    });
                });
            });

            RegisterEvents();

            progress.Dismiss();
        }

        private void RegisterEvents()
        {

            editTextWord = FindViewById<EditText>(Resource.Id.editTextWord);
            editTextMeaning = FindViewById<EditText>(Resource.Id.editTextMeaning);
            buttonAddWord = FindViewById<Button>(Resource.Id.buttonAddWord);
            listViewWordData = FindViewById<ListView>(Resource.Id.ListViewWordData);
            spinner = FindViewById<Spinner>(Resource.Id.spinnerTopics);

            adapterListViewWord = new ListViewWordAdapter(this, words);
            listViewWordData.Adapter = adapterListViewWord;

            spinnerAdapter = new CustomSpinnerAdapter(this, topics);
            spinner.Adapter = spinnerAdapter;
            spinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
            {
                topicSelected = topics[e.Position];
            };
        }
    }
}