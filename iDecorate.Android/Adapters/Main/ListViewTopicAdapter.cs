﻿using System;
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
    public class ListViewTopicAdapter : BaseAdapter
    {
        private readonly Activity context;
        private readonly List<TopicModel> topics;
        public ListViewTopicAdapter(Activity _context, List<TopicModel> topics)
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
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.ListViewTopicLayout, parent, false);
            var textViewDescription = view.FindViewById<TextView>(Resource.Id.textViewDescription);
            textViewDescription.Text = topics[position].description;
            return view;
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
    }
}