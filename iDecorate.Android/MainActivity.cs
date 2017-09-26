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

namespace iDecorate.Android
{
    [Activity(Label = "iDecorate", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        ListView lvDados;
        List<TopicWordModel> listaAlunos = new List<TopicWordModel>();
        ProgressDialog progress;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            string url = "http://idecorate.azurewebsites.net/api/Topic";

            // Fetch the weather information asynchronously, 
            // parse the results, then update the screen:

            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Loading... Please wait...");
            progress.SetCancelable(false);
            progress.Show();

            JsonValue json = await FetchWeatherAsync(url);

            var obj = JsonConvert.DeserializeObject<List<TopicModel>>(json.ToString());

            var list = new List<TopicWordModel>();

            obj.ForEach(t =>
            {
                t.words.ForEach(w =>
                {
                    var tw = new TopicWordModel();
                    tw.topic_description = t.description;
                    tw.word_description = w.description;
                    tw.word_meaning = w.meaning;
                    listaAlunos.Add(tw);
                });
            });

            lvDados = FindViewById<ListView>(Resource.Id.lvDados);
            //var txtNome = FindViewById<EditText>(Resource.Id.txtNome);
            //var txtIdade = FindViewById<EditText>(Resource.Id.txtIdade);
            //var txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            //var btnIncluir = FindViewById<Button>(Resource.Id.btnIncluir);
            //var btnEditar = FindViewById<Button>(Resource.Id.btnEditar);
            //var btnDeletar = FindViewById<Button>(Resource.Id.btnDeletar);

            var adapter = new ListViewAdapter(this, listaAlunos);
            lvDados.Adapter = adapter;

            progress.Dismiss();

            ////botão Incluir
            //btnIncluir.Click += delegate
            //{
            //    //TopicModel aluno = new TopicModel()
            //    //{
            //    //    Nome = txtNome.Text,
            //    //    Idade = int.Parse(txtIdade.Text),
            //    //    Email = txtEmail.Text
            //    //};
            //    //db.InserirAluno(aluno);
            //    //CarregarDados();
            //};
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
            ////evento itemClick do ListView
            //lvDados.ItemClick += (s, e) =>
            //{
            //    //for (int i = 0; i < lvDados.ChildCount; i++)
            //    //{
            //    //    if (e.Position == i)
            //    //        lvDados.GetChildAt(i).SetBackgroundColor(Color.Chocolate);
            //    //    else
            //    //        lvDados.GetChildAt(i).SetBackgroundColor(Color.Transparent);
            //    //}
            //    //vinculando dados do listview 
            //    var lvtxtNome = e.View.FindViewById<TextView>(Resource.Id.txtvNome);
            //    var lvtxtIdade = e.View.FindViewById<TextView>(Resource.Id.txtvIdade);
            //    var lvtxtEmail = e.View.FindViewById<TextView>(Resource.Id.txtvEmail);
            //    txtNome.Text = lvtxtNome.Text;
            //    txtNome.Tag = e.Id;
            //    txtIdade.Text = lvtxtIdade.Text;
            //    txtEmail.Text = lvtxtEmail.Text;
            //};
        }

        // Gets weather data from the passed URL.
        private async Task<JsonValue> FetchWeatherAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));

                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }
    }

    public class ListViewAdapter : BaseAdapter
    {
        private readonly Activity context;
        private readonly List<TopicWordModel> alunos;
        public ListViewAdapter(Activity _context, List<TopicWordModel> _alunos)
        {
            this.context = _context;
            this.alunos = _alunos;
        }
        public override int Count
        {
            get
            {
                return alunos.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return 0; // return alunos[position].id;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.ListViewLayout, parent, false);
            var lvtxtNome = view.FindViewById<TextView>(Resource.Id.txtvNome);
            var lvtxtIdade = view.FindViewById<TextView>(Resource.Id.txtvIdade);
            var lvtxtEmail = view.FindViewById<TextView>(Resource.Id.txtvEmail);
            lvtxtNome.Text = alunos[position].word_description;
            lvtxtIdade.Text = "" + alunos[position].word_meaning;
            lvtxtEmail.Text = alunos[position].topic_description;
            return view;
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
    }
}

