using SupportActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Views;

namespace MenuTest
{
    public class MyActionBarDrawerToggle : SupportActionBarDrawerToggle
    {
        private AppCompatActivity mActivity;
        private int mOpenedResource;
        private int mClosedResource;

        public MyActionBarDrawerToggle(AppCompatActivity host, DrawerLayout drawerLayout, int openedResource, int closedResource)
            :base(host, drawerLayout,openedResource,closedResource)
        {
            mHostActivity = host;
			mOpenedResource = openDrawerDesc;
			mClosedResource = closeDrawerDesc;
        }


        public override void OnDrawerOpened(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;
            if (drawerType == 0)
            {
                base.OnDrawerOpened(drawerView);
                mHostActivity.SupportActionBar.SetTitle(mOpenedResource);
            }
        }

        public override void OnDrawerClosed(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;
            if (drawerType == 0)
            {
                base.OnDrawerClosed(drawerView);
                mHostActivity.SupportActionBar.SetTitle(mClosedResource);
            }
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            int drawerType = (int)drawerView.Tag;
            if (drawerType == 0)
            {
                base.OnDrawerSlide(drawerView, slideOffset);
            }
        }
    }
}