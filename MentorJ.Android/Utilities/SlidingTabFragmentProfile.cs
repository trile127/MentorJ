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
using Android.Support.V4.View;
using Android.App;

namespace MentorJ_Android
{

    public class SlidingTabsFragmentProfile : Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;

        //public global::Android.Support.V4.App.FragmentManager SupportFragmentManager { get; private set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //FragmentSample holds scrolling tab layout
            return inflater.Inflate(Resource.Layout.FragmentSample, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);

            mViewPager.Adapter = new SamplePagerAdapter2();

            mSlidingTabScrollView.ViewPager = mViewPager;

        }

    }
}