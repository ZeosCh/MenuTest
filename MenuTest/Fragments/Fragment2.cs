using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using MenuTest.Resources.datacenter;
using MenuTest.Resources.Model;

namespace MenuTest.Resources.Fragments
{
    public class Fragment2 : Android.Support.V4.App.Fragment
    {
        ListView lstData;
        List<Station> lstSource = new List<Station>();
        Database db;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        private void LoadData()
        {
            lstSource = db.selectTableStation();
            var adapter = new ListViewAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view= inflater.Inflate(Resource.Layout.Fragment2, container, false);

            db = new Database();
            db.CreateDatabase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            lstData = FindViewById<ListView>(Resource.Id.listView);

            var edtName = FindViewById<EditText>(Resource.Id.edtName);
            var edtAddress = FindViewById<EditText>(Resource.Id.edtAddress);
            var edtCapacity = FindViewById<EditText>(Resource.Id.edtCapacity);
            var edtAvailability = FindViewById<EditText>(Resource.Id.edtAvailability);
            var edtState = FindViewById<EditText>(Resource.Id.edtState);

            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            LoadData();

            //Event
            btnAdd.Click += delegate
            {
                Station station = new Station()
                {
                    Name = edtName.Text,
                    Address = edtAddress.Text,
                    Capacity = int.Parse(edtCapacity.Text),
                    Availability = int.Parse(edtAvailability.Text),
                    State = edtState.Text
                };
                db.InsertIntoTableStation(station);
                LoadData();
            };

            btnEdit.Click += delegate
            {
                Station station = new Station()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    Address = edtAddress.Text,
                    Capacity = int.Parse(edtCapacity.Text),
                    Availability = int.Parse(edtAvailability.Text),
                    State = edtState.Text
                };
                db.UpdateTableStation(station);
                LoadData();
            };

            btnDelete.Click += delegate
            {
                Station station = new Station()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    Address = edtAddress.Text,
                    Capacity = int.Parse(edtCapacity.Text),
                    Availability = int.Parse(edtAvailability.Text),
                    State = edtState.Text
                };
                db.DeleteTableStation(station);
                LoadData();
            };

            lstData.ItemClick += (s, e) =>
            {

                //Binding Data
                var txtName = e.View.FindViewById<TextView>(Resource.Id.textView1);
                var txtAddress = e.View.FindViewById<TextView>(Resource.Id.textView2);
                var txtCapacity = e.View.FindViewById<TextView>(Resource.Id.textView3);
                var txtAvailability = e.View.FindViewById<TextView>(Resource.Id.textView4);
                var txtState = e.View.FindViewById<TextView>(Resource.Id.textView5);

                edtName.Text = txtName.Text;
                edtName.Tag = e.Id;

                edtAddress.Text = txtAddress.Text;
                edtCapacity.Text = txtCapacity.Text;
                edtAvailability.Text = txtAvailability.Text;
                edtState.Text = txtState.Text;

            };

            return view ;
        }
    }
}