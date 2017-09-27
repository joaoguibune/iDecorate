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
    public class ListViewAdapter : BaseAdapter
    {
        private readonly Activity context;
        private readonly List<TopicWordModel> topics;
        public ListViewAdapter(Activity _context, List<TopicWordModel> topics)
        {
            this.context = _context;
            this.topics = topics;
        }
        public override int Count
        {
            get
            {
                return topics.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return 0; // return alunos[position].id;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.ListViewLayout, parent, false);
            var textViewWord = view.FindViewById<TextView>(Resource.Id.textViewWord);
            var textViewMeaning = view.FindViewById<TextView>(Resource.Id.textViewMeaning);
            var textViewTopic = view.FindViewById<TextView>(Resource.Id.textViewTopic);
            textViewWord.Text = topics[position].word_description;
            textViewMeaning.Text = "" + topics[position].word_meaning;
            textViewTopic.Text = topics[position].topic_description;
            return view;
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
    }
}