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
using iDecorate.Android.Business.Contract;
using iDecorate.Android.Model;
using iDecorate.Android.Business.Client;
using Newtonsoft.Json;
using iDecorate.Android.Adapters.Main;
using Android.Text;
using iDecorate.Android.Util;

namespace iDecorate.Android.Activities
{
    [Activity(Label = "Topic")]
    public class NewTopicActivity : Activity
    {
        private IClient<TopicModel> _clientTopic = new Client<TopicModel>("Topic");
        private List<TopicModel> topics;

        private ListView ListViewTopicData;
        private ListViewTopicAdapter adapterListViewTopic;
        private EditText editTextTopic;
        private Button buttonAdd;
        private ProgressCustom progress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            base.SetContentView(Resource.Layout.NewTopic);
            
            progress = new ProgressCustom(this);

            progress.Show();

            topics = Intent.GetStringExtra("Topic") == null ? new List<TopicModel>() : JsonConvert.DeserializeObject<List<TopicModel>>(Intent.GetStringExtra("Topic"));

            RegiterEvents();
            
            progress.Dismiss();

        }

        private void RegiterEvents()
        {
            editTextTopic = FindViewById<EditText>(Resource.Id.editTextTopic);
            buttonAdd = FindViewById<Button>(Resource.Id.buttonAddTopic);
            ListViewTopicData = FindViewById<ListView>(Resource.Id.ListViewTopicData);
            adapterListViewTopic = new ListViewTopicAdapter(this, topics);

            ListViewTopicData.Adapter = adapterListViewTopic;

            buttonAdd.Click += async delegate
            {
                if (editTextTopic.Text.Length.Equals(0))
                    editTextTopic.SetError("Topic is required.", null);
                else
                {
                    if (await _clientTopic.Post(new TopicModel { description = editTextTopic.Text }))
                        StartActivity(new Intent(this, typeof(NewTopicActivity)));
                }
            };
        }
    }
}