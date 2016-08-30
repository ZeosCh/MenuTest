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
using Android.Util;
using MenuTest.Resources.Model;

namespace MenuTest.Resources
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtName { get; set; }

        public TextView txtAddress { get; set; }

        public TextView txtCapacity { get; set; }

        public TextView txtAvailability { get; set; }

        public TextView txtState { get; set; }
    }

    public class ListViewAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Station> lstStation;
        public ListViewAdapter(Activity activity, List<Station> lstStation)
        {
            this.activity = activity;
            this.lstStation = lstStation;
        }

        public override int Count
        {
            get
            {
                return lstStation.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstStation[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //Maybe needs some change! We're placing Fragments
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view_dataTemplate, parent, false);

            var txtName = view.FindViewById<TextView>(Resource.Id.textView1);
            var txtAddress = view.FindViewById<TextView>(Resource.Id.textView2);
            var txtCapacity = view.FindViewById<TextView>(Resource.Id.textView3);
            var txtAvailability = view.FindViewById<TextView>(Resource.Id.textView4);
            var txtState = view.FindViewById<TextView>(Resource.Id.textView5);

            txtName.Text = lstStation[position].Name;
            txtAddress.Text = lstStation[position].Address;
            txtCapacity.Text = "" + lstStation[position].Capacity;
            txtAvailability.Text = "" + lstStation[position].Availability;
            txtState.Text = "" + lstStation[position].State;

            return view;
        }
    }
}