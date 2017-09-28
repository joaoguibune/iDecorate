using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using iDecorate.Android.Adapters.Main;
using iDecorate.Android.Util;
using iDecorate.Android.Domain.Model;
using iDecorate.Android.Domain.Contract;
using iDecorate.Android.Domain.Client;

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

            progress = new ProgressCustom(this, true);

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
            editTextWord = FindViewById<EditText>(Resource.Id.VIEW003_EditTextWord);
            editTextMeaning = FindViewById<EditText>(Resource.Id.VIEW003_EditTextMeaning);
            buttonAddWord = FindViewById<Button>(Resource.Id.VIEW003_ButtonAddWord);
            listViewWordData = FindViewById<ListView>(Resource.Id.VIEW003_ListViewWordData);
            spinner = FindViewById<Spinner>(Resource.Id.VIEW003_SpinnerTopics);

            adapterListViewWord = new ListViewWordAdapter(this, words);
            listViewWordData.Adapter = adapterListViewWord;

            spinnerAdapter = new CustomSpinnerAdapter(this, topics);
            spinner.Adapter = spinnerAdapter;
            spinner.ItemSelected += (sender, e) =>
            {
                topicSelected = topics[e.Position];
            };
        }
    }
}