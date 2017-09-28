using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using iDecorate.Android.Adapters.Main;
using iDecorate.Android.Util;
using Android.Graphics;
using iDecorate.Android.Domain.Client;
using iDecorate.Android.Domain.Model;
using iDecorate.Android.Domain.Contract;
using System;

namespace iDecorate.Android.Activities
{
    [Activity(Label = "Topic")]
    public class NewTopicActivity : Activity
    {
        private IClient<TopicModel> _clientTopic = new Client<TopicModel>("TopicMobile");
        private List<TopicModel> topics;
        private TopicModel topicSelected = new TopicModel();

        private ListView listViewTopicData;
        private ListViewTopicAdapter adapterListViewTopic;
        private EditText editTextTopic;
        private Button buttonAdd;
        private ProgressCustom progress;
        private Button buttonDeleteTopic;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            base.SetContentView(Resource.Layout.NewTopic);

            progress = new ProgressCustom(this, true);

            topics = Intent.GetStringExtra("Topic") == null ? new List<TopicModel>() : JsonConvert.DeserializeObject<List<TopicModel>>(Intent.GetStringExtra("Topic"));

            RegiterEvents();

            progress.Dismiss();
        }

        private void RegiterEvents()
        {
            editTextTopic = FindViewById<EditText>(Resource.Id.VIEW002_EditTextTopic);
            buttonAdd = FindViewById<Button>(Resource.Id.VIEW002_ButtonAddTopic);
            listViewTopicData = FindViewById<ListView>(Resource.Id.VIEW002_ListViewTopicData);
            buttonDeleteTopic = FindViewById<Button>(Resource.Id.VIEW002_ButtonDeleteTopic);
            adapterListViewTopic = new ListViewTopicAdapter(this, topics);

            listViewTopicData.Adapter = adapterListViewTopic;

            buttonAdd.Click += async (s, e) =>
            {
                if (editTextTopic.Text.Length.Equals(0))
                    editTextTopic.SetError("Topic is required.", null);
                else
                {
                    if (topicSelected.id.Equals(Guid.Empty))
                        topics = (List<TopicModel>)await _clientTopic.Post(new TopicModel { description = editTextTopic.Text });
                    else
                    {
                        topicSelected.description = editTextTopic.Text;
                        topics = (List<TopicModel>)await _clientTopic.Put(topicSelected);
                    }

                    var newTopicActivity = new Intent(this, typeof(NewTopicActivity));
                    newTopicActivity.PutExtra("Topic", JsonConvert.SerializeObject(topics));
                    StartActivity(newTopicActivity);
                }
            };

            listViewTopicData.ItemClick += (s, e) =>
            {
                for (int i = 0; i < listViewTopicData.ChildCount; i++)
                {
                    buttonDeleteTopic = listViewTopicData.GetChildAt(i).FindViewById<Button>(Resource.Id.VIEW002_ButtonDeleteTopic);

                    buttonDeleteTopic.Click += async (s2, e2) =>
                    {
                        topics = (List<TopicModel>)await _clientTopic.Delete(topicSelected.id.ToString());

                        var newTopicActivity = new Intent(this, typeof(NewTopicActivity));
                        newTopicActivity.PutExtra("Topic", JsonConvert.SerializeObject(topics));
                        StartActivity(newTopicActivity);
                    };

                    if (e.Position == i)
                    {
                        if (topicSelected.id.Equals(topics[i].id))
                        {
                            listViewTopicData.GetChildAt(i).SetBackgroundColor(Color.Transparent);
                            buttonDeleteTopic.Visibility = ViewStates.Invisible;
                            editTextTopic.Text = string.Empty;
                            buttonAdd.Text = "Add";
                            topicSelected = new TopicModel();

                        }
                        else
                        {
                            listViewTopicData.GetChildAt(i).SetBackgroundColor(Color.Chocolate);
                            buttonDeleteTopic.Visibility = ViewStates.Visible;
                            topicSelected = topics[i];
                            editTextTopic.Text = topicSelected.description;
                            buttonAdd.Text = "Edit";
                        }
                    }
                    else
                    {
                        listViewTopicData.GetChildAt(i).SetBackgroundColor(Color.Transparent);
                        buttonDeleteTopic.Visibility = ViewStates.Invisible;
                    }
                }
            };
        }
    }
}