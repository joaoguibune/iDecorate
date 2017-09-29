using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Newtonsoft.Json;
using iDecorate.Android.Adapters.Main;
using iDecorate.Android.Activities;
using Android.Content;
using iDecorate.Android.Util;
using iDecorate.Android.Domain.Model;
using iDecorate.Android.Domain.Contract;
using iDecorate.Android.Domain.Client;

namespace iDecorate.Android
{
    [Activity(Label = "iDecorate", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        private IClient<TopicModel> _clientTopic = new Client<TopicModel>("TopicMobile");
        private List<TopicModel> topics = new List<TopicModel>();

        private Button buttonNewTopic;
        private Button buttonNewWord;
        private Button buttonPratice;
        private ProgressCustom progress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            progress = new ProgressCustom(this, true);

            RegisterEvents();

            progress.Dismiss();
        }

        private void RegisterEvents()
        {
            buttonNewTopic = FindViewById<Button>(Resource.Id.VIEW001_ButtonNewTopic);
            buttonNewWord = FindViewById<Button>(Resource.Id.VIEW001_ButtonNewWord);
            buttonPratice = FindViewById<Button>(Resource.Id.VIEW001_ButtonPratice);

            buttonNewTopic.Click += (s, e) =>
            {
                var newTopicActivity = new Intent(this, typeof(NewTopicActivity));
                newTopicActivity.PutExtra("Topic", JsonConvert.SerializeObject(topics));
                StartActivity(newTopicActivity);
            };

            buttonNewWord.Click += (s, e) =>
            {
                var newWordActivity = new Intent(this, typeof(NewWordActivity));
                newWordActivity.PutExtra("Topic", JsonConvert.SerializeObject(topics));
                StartActivity(newWordActivity);
            };

            buttonPratice.Click += (s, e) =>
            {

            };
        }
    }
}

