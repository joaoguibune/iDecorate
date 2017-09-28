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

namespace iDecorate.Android.Util
{
    public class ProgressCustom
    {
        private ProgressDialog progress;

        public ProgressCustom(Activity element, string message = "Loading the content, please wait...")
        {
            progress = new ProgressDialog(element);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage(message);
            progress.SetCancelable(false);
        }

        public void Show()
        {
            progress.Show();
        }

        public void Dismiss()
        {
            progress.Dismiss();
        }
    }
}