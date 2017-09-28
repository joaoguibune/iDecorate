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
        private ListView ListViewData;
        private Button buttonNewTopic;
        private Button buttonNewWord;
        private ListViewAdapter adapterListView;
        private List<TopicWordModel> topicWords = new List<TopicWordModel>();
        private List<TopicModel> topics = new List<TopicModel>();
        private ProgressCustom progress;
        private IClient<TopicModel> _clientTopic = new Client<TopicModel>("Topic");

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            progress = new ProgressCustom(this);

            progress.Show();

            topics = (List<TopicModel>)await _clientTopic.GetList();

            

            progress.Dismiss();

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            ListViewData = FindViewById<ListView>(Resource.Id.ListViewData);
            buttonNewTopic = FindViewById<Button>(Resource.Id.buttonNewTopic);
            buttonNewWord = FindViewById<Button>(Resource.Id.buttonNewWord);

            //adapterListView = new ListViewAdapter(this, topicWords);

            //ListViewData.Adapter = adapterListView;

            buttonNewTopic.Click += delegate
            {
                var newTopicActivity = new Intent(this, typeof(NewTopicActivity));
                newTopicActivity.PutExtra("Topic", JsonConvert.SerializeObject(topics));
                StartActivity(newTopicActivity);
            };

            buttonNewWord.Click += delegate
            {
                var newWordActivity = new Intent(this, typeof(NewWordActivity));
                newWordActivity.PutExtra("Topic", JsonConvert.SerializeObject(topics));
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

