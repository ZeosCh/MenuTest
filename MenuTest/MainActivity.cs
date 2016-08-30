using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.Collections.Generic;
using MenuTest.Resources.Fragments;
using Android.Content.Res;
using Android.Support.V4.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace MenuTest
{
    [Activity(Label = "MenuTest", MainLauncher = true, Icon = "@drawable/icon",Theme ="@style/MyTheme", ConfigurationChanges= Android.Content.PM.ConfigChanges.ScreenSize|Android.Content.PM.ConfigChanges.Orientation)]
    public class MainActivity : AppCompatActivity
    {
        private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ListView mRightDrawer;
        private ArrayAdapter mLeftAdapter;
        private ArrayAdapter mRightAdapter;
        private List<string> mLeftDataSet;
        private List<string> mRightDataSet;
        
        private SupportFragment mCurrentFRagment;
        private Fragment1 mFragment1;
        private Fragment2 mFragment2;
        private Fragment3 mFragment3;
        private Fragment4 mFragment4;
        private Fragment5 mFragment5;
        private Stack<SupportFragment> mStackFragment;

        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            mRightDrawer = FindViewById<ListView>(Resource.Id.right_drawer);

            mFragment1 = new Fragment1();
            mFragment2 = new Fragment2();
            mFragment3 = new Fragment3();
            mFragment4 = new Fragment4();
            mFragment5 = new Fragment5();

            mStackFragment = new Stack<SupportFragment>();
            mLeftDrawer.Tag =0;
            mRightDrawer.Tag = 1;

            SetSupportActionBar(mToolbar);

            mLeftDataSet = new List<string>();
            mLeftDataSet.Add("Home");
            mLeftDataSet.Add("Search");
            mLeftDataSet.Add("Search Map");
            mLeftAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;

            mRightDataSet = new List<string>();
            mRightDataSet.Add("About");
            mRightDataSet.Add("Contact Us");
            mRightAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, mRightDataSet);
            mRightDrawer.Adapter = mRightAdapter;


            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, mFragment5, "Fragment5");
            trans.Hide(mFragment5);
            trans.Add(Resource.Id.fragmentContainer, mFragment4, "Fragment4");
            trans.Hide(mFragment4);
            trans.Add(Resource.Id.fragmentContainer, mFragment3, "Fragment3");
            trans.Hide(mFragment3);
            trans.Add(Resource.Id.fragmentContainer, mFragment2, "Fragment2");
            trans.Hide(mFragment2);
            trans.Add(Resource.Id.fragmentContainer, mFragment1, "Fragment1");
            trans.Commit();

            mCurrentFRagment = mFragment1;

            mDrawerLayout.AddDrawerListener(mDrawerToggle);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            mDrawerToggle.SyncState();

            if (bundle != null)
            {
                if (bundle.GetString("DrawerState") == "Opened")
                {
                    SupportActionBar.SetTitle(Resource.String.openDrawer);
                }
                else
                {
                    SupportActionBar.SetTitle(Resource.String.closeDrawer);
                }
            }
            else
            {
                SupportActionBar.SetTitle(Resource.String.closeDrawer);
            }

        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu,menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (mDrawerToggle.OnOptionsItemSelected(item))
            {
                if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
                {
                    mDrawerLayout.CloseDrawer(mRightDrawer);
                }
                return true;
            }

            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    mDrawerLayout.CloseDrawer(mRightDrawer);
                    mDrawerToggle.OnOptionsItemSelected(item);
                    return true;
                case Resource.Id.info:
                    if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
                    {
                        mDrawerLayout.CloseDrawer(mRightDrawer);
                    }
                    else
                    {
                        mDrawerLayout.CloseDrawer(mLeftDrawer);
                        mDrawerLayout.OpenDrawer(mRightDrawer);
                    }
                    return true;
                
                case Resource.Id.action_fragment4:
                        ShowFragment(mFragment4);
                        return true;
                case Resource.Id.action_fragment5:
                        ShowFragment(mFragment5);
                        return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
            }
            else
            {
                outState.PutString("DrawerState", "Closed");
            }
            base.OnSaveInstanceState(outState);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
        }

        protected void ShowFragment(SupportFragment fragment)
        {
            if (fragment.IsVisible)
            { return; }
            var trans = SupportFragmentManager.BeginTransaction();
            trans.SetCustomAnimations(Resource.Animation.slide_in, Resource.Animation.slide_out, Resource.Animation.slide_in, Resource.Animation.slide_out);

            trans.Hide(mCurrentFRagment);
            trans.Show(fragment);
            trans.AddToBackStack(null);
            trans.Commit();

            mStackFragment.Push(mCurrentFRagment);
            mCurrentFRagment = fragment;
        }

        public override void OnBackPressed()
        {
            if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                SupportFragmentManager.PopBackStack();
                mCurrentFRagment = mStackFragment.Pop();
            }
            else
            {
                base.OnBackPressed();
            }
        }
    }
}

