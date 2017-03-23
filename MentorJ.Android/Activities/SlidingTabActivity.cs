using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.App;
using Android.Support.V4.View;

namespace MentorJ.Android
{
    [Activity(Label = "SlidingTabActivity", MainLauncher = true, Icon = "@drawable/xs")]
    public class SlidingTabActivity : Activity
    {
        private ViewPager mViewPager;
        private SlidingTabScrollView mScrollView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.FragmentSample);
            mScrollView = FindViewById<SlidingTabScrollView>(Resource.Id.slidingTabs);
            mViewPager = FindViewById<ViewPager>(Resource.Id.viewPager);

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            SlidingTabsFragment fragment = new SlidingTabsFragment();
            transaction.Replace(Resource.Id.sample_content_fragment, fragment);
            transaction.Commit();

            mViewPager.Adapter = new SamplePagerAdapter();
            mScrollView.ViewPager = mViewPager;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }


}

