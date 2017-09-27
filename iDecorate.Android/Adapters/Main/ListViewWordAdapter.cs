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
using iDecorate.Android.Model;

namespace iDecorate.Android.Adapters.Main
{
    public class ListViewWordAdapter : BaseAdapter
    {
        private readonly Activity context;
        private readonly List<TopicWordModel> words;
        public ListViewWordAdapter(Activity _context, List<TopicWordModel> words)
        {
            this.context = _context;
            this.words = words;
        }
        public override int Count
        {
            get
            {
                return words.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return 0; // return alunos[position].id;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.ListViewWordLayout, parent, false);
            var textViewWordDescription = view.FindViewById<TextView>(Resource.Id.textViewWordDescription);
            var textViewWordMeaning = view.FindViewById<TextView>(Resource.Id.textViewWordMeaning);
            textViewWordDescription.Text = words[position].word_description;
            textViewWordMeaning.Text = words[position].word_meaning;
            return view;
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
    }
}