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

namespace iDecorate.Android.Activities
{
    [Activity(Label = "Topic")]
    public class NewTopicActivity : Activity
    {
        private IClient<TopicModel> _clientTopic = new ClientTopic();
        List<TopicModel> topics;

        ListView ListViewTopicData;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            base.SetContentView(Resource.Layout.NewTopic);

            var editTextTopic = FindViewById<EditText>(Resource.Id.editTextTopic);
            var buttonAdd = FindViewById<Button>(Resource.Id.buttonAddTopic);

            topics = Intent.GetStringExtra("Topic") == null ? new List<TopicModel>() : JsonConvert.DeserializeObject<List<TopicModel>>(Intent.GetStringExtra("Topic"));

            ListViewTopicData = FindViewById<ListView>(Resource.Id.ListViewTopicData);

            var adapter = new ListViewTopicAdapter(this, topics);
            ListViewTopicData.Adapter = adapter;

            buttonAdd.Click += async delegate
            {
                if (editTextTopic.Text.Length.Equals(0))
                    editTextTopic.SetError("Topic is required.", null);
                else
                {
                    if (await _clientTopic.Post(new TopicModel { description = editTextTopic.Text }))
                        OnBackPressed();
                }
            };

        }
    }
}