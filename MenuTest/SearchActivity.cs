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
using Android.Support.V7.App;
using Android.Support.V4.View;

namespace MenuTest
{
    [Activity(Label = "SearchActivity",Theme ="@style/Theme.AppCompat.Light")]
    public class SearchActivity : AppCompatActivity
    {

        private SearchView _searchView;
        private ListView _listView;
        private ArrayAdapter _adapter;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Search);

            var products = new[]
            {
                "Breaking Bad","Games Of Thrones","Breakout Kings","Bad Meets Evil","White Colar","NCIS","House"
            };

            _listView = FindViewById<ListView>(Resource.Id.listView);
            _adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, products);
            _listView.Adapter = _adapter;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_bar,menu);
            var item = menu.FindItem(Resource.Id.search_bar);
            var searchView = MenuItemCompat.GetActionView(item);
            _searchView = searchView.JavaCast<SearchView>();

            _searchView.QueryTextChange += (s, e) => _adapter.Filter.InvokeFilter(e.NewText);

            _searchView.QueryTextSubmit += (s, e) =>
            {
                Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
                e.Handled = true;
            };
            return true;
        }

    }
}