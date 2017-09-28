using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;
using iDecorate.Android.Domain.Model;

namespace iDecorate.Android.Adapters.Main
{
    public class CustomSpinnerAdapter : BaseAdapter
    {
        readonly Activity _context;
        private List<TopicModel> _items;
        public CustomSpinnerAdapter(Activity context, List<TopicModel> listOfItems)
        {
            _context = context;
            _items = listOfItems;
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            var view = (convertView ?? _context.LayoutInflater.Inflate(global::Android.Resource.Layout.SimpleSpinnerDropDownItem,
                parent,
                false));
            var name = view.FindViewById<TextView>(global::Android.Resource.Id.Text1);
            name.Text = item.description;
            return view;
        }

        public TopicModel GetItemAtPosition(int position)
        {
            return _items[position];
        }
    }
}