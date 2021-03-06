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

using SupportFragment = Android.Support.V4.App.Fragment;

namespace MenuTest.Resources.Fragments
{
    public class Fragment1 : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle bundle)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment1, container, false);
            return view;
        }

        public void switch_fragment(View view)
        {
            SupportFragment newFragment = null;
            switch (view.FindViewById())
            {
                case Resource.id.button1:
                    newFragment = new Fragment2();
                    break;
                case Resource.Id.button2:
                    newFragment = new Fragment3();
                    break;
            }
            SetFragment(newFragment);
        }
    }
}