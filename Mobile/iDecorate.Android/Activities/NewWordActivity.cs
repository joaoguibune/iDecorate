using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using iDecorate.Android.Adapters.Main;
using iDecorate.Android.Util;
using iDecorate.Android.Domain.Model;
using iDecorate.Android.Domain.Client;
using iDecorate.Android.Domain.Contract;
using Android.Graphics;
using Android.Views;
using System;

namespace iDecorate.Android.Activities
{
    [Activity(Label = "Word")]
    public class NewWordActivity : Activity
    {
        private Spinner spinnerTopic;
        private CustomSpinnerAdapter spinnerAdapter;
        private ListView listViewWordData;
        private ListViewWordAdapter adapterListViewWord;
        private ProgressCustom progress;
        private EditText editTextWord;
        private EditText editTextMeaning;
        private Button buttonAddWord;

        private List<TopicWordModel> words = new List<TopicWordModel>();
        private List<TopicModel> topics = new List<TopicModel>();
        private TopicWordModel wordSelected = new TopicWordModel();
        private TopicModel topicSelected = new TopicModel();

        private IClient<TopicModel> _clientTopic = new Client<TopicModel>("Topic");
        private IClient<WordModel> _clientWord = new Client<WordModel>("Word");
        private Button buttonDeleteWord;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            base.SetContentView(Resource.Layout.NewWord);

            progress = new ProgressCustom(this, true);

            //topics = Intent.GetStringExtra("Topic") == null ?
            //    (List<TopicModel>)await _clientTopic.GetList() :
            //    JsonConvert.DeserializeObject<List<TopicModel>>(Intent.GetStringExtra("Topic"));

            topics = (List<TopicModel>)await _clientTopic.GetList();

            topics.ForEach(topic =>
            {
                topic.words.ForEach(word =>
                {
                    words.Add(new TopicWordModel
                    {
                        word_id = word.id,
                        topic_id = topic.id,
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
            spinnerTopic = FindViewById<Spinner>(Resource.Id.VIEW003_SpinnerTopics);

            adapterListViewWord = new ListViewWordAdapter(this, words);
            listViewWordData.Adapter = adapterListViewWord;

            spinnerAdapter = new CustomSpinnerAdapter(this, topics);
            spinnerTopic.Adapter = spinnerAdapter;
            spinnerTopic.ItemSelected += (s, e) =>
            {
                topicSelected = topics[e.Position];
            };

            buttonAddWord.Click += async (s, e) =>
            {
                var isSuccess = false;
                if (editTextWord.Text.Length.Equals(0) || editTextMeaning.Text.Length.Equals(0))
                    editTextWord.SetError("All the field is required.", null);
                else
                {
                    if (wordSelected.word_id.Equals(Guid.Empty))
                        isSuccess = await _clientWord.Post(new WordModel
                        {
                            topic_id = topicSelected.id,
                            description = editTextWord.Text,
                            meaning = editTextMeaning.Text
                        });
                    else
                    {
                        isSuccess = await _clientWord.Put(new WordModel
                        {
                            id = wordSelected.word_id,
                            topic_id = topicSelected.id,
                            description = editTextWord.Text,
                            meaning = editTextMeaning.Text
                        });
                    }

                    if (isSuccess)
                    {
                        Intent intent = this.Intent;
                        Finish();
                        StartActivity(intent);
                    }
                }
            };

            listViewWordData.ItemSelected += (s, e) =>
            {
                var isSuccess = false;

                for (int i = 0; i < listViewWordData.ChildCount; i++)
                {
                    buttonDeleteWord = listViewWordData.GetChildAt(i).FindViewById<Button>(Resource.Id.VIEW003_ButtonDeleteWord);

                    if (e.Position == i)
                    {
                        if (wordSelected.word_id.Equals(words[i].word_id))
                        {
                            listViewWordData.GetChildAt(i).SetBackgroundColor(Color.Transparent);
                            buttonDeleteWord.Visibility = ViewStates.Invisible;
                            editTextWord.Text = string.Empty;
                            editTextMeaning.Text = string.Empty;
                            buttonAddWord.Text = "Add";
                            wordSelected = new TopicWordModel();

                        }
                        else
                        {
                            listViewWordData.GetChildAt(i).SetBackgroundColor(Color.Chocolate);
                            buttonDeleteWord.Visibility = ViewStates.Visible;
                            wordSelected = words[i];
                            editTextWord.Text = wordSelected.word_description;
                            editTextMeaning.Text = wordSelected.word_meaning;
                            spinnerTopic.SetSelection(getIndex(wordSelected.topic_description));
                            buttonAddWord.Text = "Edit";

                            buttonDeleteWord.Click += async (s2, e2) =>
                            {
                                isSuccess = await _clientTopic.Delete(wordSelected.word_id.ToString());

                                if (isSuccess)
                                {
                                    Intent intent = this.Intent;
                                    Finish();
                                    StartActivity(intent);
                                }
                            };
                        }
                    }
                    else
                    {
                        listViewWordData.GetChildAt(i).SetBackgroundColor(Color.Transparent);
                        buttonDeleteWord.Visibility = ViewStates.Invisible;
                    }
                }
            };
        }

        private int getIndex(String myString)
        {
            for (int i = 0; i < spinnerTopic.ChildCount; i++)
                if (spinnerTopic.GetItemAtPosition(i).ToString().ToUpper().Equals(myString.ToUpper()))
                    return i;

            return -1;
        }
    }
}