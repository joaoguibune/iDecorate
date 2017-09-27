using Android.App;
using Android.Widget;
using Android.OS;
using System.Json;
using System.Collections.Generic;
using Newtonsoft.Json;
using iDecorate.Android.Model;
using System.Threading.Tasks;
using System.Net;
using System;
using System.IO;
using Android.Views;
using System.Timers;
using Android.Graphics;
using iDecorate.Android.Business.Contract;
using iDecorate.Android.Adapters.Main;
using iDecorate.Android.Business.Client;
using iDecorate.Android.Activities;
using Android.Content;

namespace iDecorate.Android
{
    [Activity(Label = "iDecorate", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        ListView ListViewData;
        List<TopicWordModel> topicWords = new List<TopicWordModel>();
        List<TopicModel> topics = new List<TopicModel>();
        ProgressDialog progress;
        private IClient<TopicModel> _clientTopic = new ClientTopic();
        
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Loading...Please wait...");
            progress.SetCancelable(false);
            progress.Show();

            topics = (List<TopicModel>)await _clientTopic.GetList();

            var list = new List<TopicWordModel>();

            topics.ForEach(t =>
            {
                t.words.ForEach(w =>
                {
                    var tw = new TopicWordModel();
                    tw.topic_description = t.description;
                    tw.word_description = w.description;
                    tw.word_meaning = w.meaning;
                    topicWords.Add(tw);
                });
            });

            ListViewData = FindViewById<ListView>(Resource.Id.ListViewData);
            var buttonNewTopic = FindViewById<Button>(Resource.Id.buttonNewTopic);
            var buttonNewWord = FindViewById<Button>(Resource.Id.buttonNewWord);

            var adapter = new ListViewAdapter(this, topicWords);
            ListViewData.Adapter = adapter;

            progress.Dismiss();

            buttonNewTopic.Click += delegate
            {
                var newTopicActivity = new Intent(this, typeof(NewTopicActivity));
                newTopicActivity.PutExtra("Topic", JsonConvert.SerializeObject(topics));
                StartActivity(newTopicActivity);
            };

            buttonNewWord.Click += delegate
            {
                var newWordActivity = new Intent(this, typeof(NewWordActivity));
                newWordActivity.PutExtra("Word", JsonConvert.SerializeObject(topicWords));
                StartActivity(newWordActivity);
            };

            ////botão editar
            //btnEditar.Click += delegate
            //{
            //    //Aluno aluno = new Aluno()
            //    //{
            //    //    Id = int.Parse(txtNome.Tag.ToString()),
            //    //    Nome = txtNome.Text,
            //    //    Idade = int.Parse(txtIdade.Text),
            //    //    Email = txtEmail.Text
            //    //};
            //    //db.AtualizarAluno(aluno);
            //    //CarregarDados();
            //};
            ////botão deletar
            //btnDeletar.Click += delegate
            //{
            //    //Aluno aluno = new Aluno()
            //    //{
            //    //    Id = int.Parse(txtNome.Tag.ToString()),
            //    //    Nome = txtNome.Text,
            //    //    Idade = int.Parse(txtIdade.Text),
            //    //    Email = txtEmail.Text
            //    //};
            //    //db.DeletarAluno(aluno);
            //    //CarregarDados();
            //};
            //evento itemClick do ListView
            ListViewData.ItemClick += (s, e) =>
            {
                //for (int i = 0; i < lvDados.ChildCount; i++)
                //{
                //    if (e.Position == i)
                //        lvDados.GetChildAt(i).SetBackgroundColor(Color.Chocolate);
                //    else
                //        lvDados.GetChildAt(i).SetBackgroundColor(Color.Transparent);
                //}
                //vinculando dados do listview 
                //var lvtxtNome = e.View.FindViewById<TextView>(Resource.Id.txtvNome);
                //var lvtxtIdade = e.View.FindViewById<TextView>(Resource.Id.txtvIdade);
                //var lvtxtEmail = e.View.FindViewById<TextView>(Resource.Id.txtvEmail);
                //txtNome.Text = lvtxtNome.Text;
                //txtNome.Tag = e.Id;
                //txtIdade.Text = lvtxtIdade.Text;
                //txtEmail.Text = lvtxtEmail.Text;
            };
        }
    }
}

