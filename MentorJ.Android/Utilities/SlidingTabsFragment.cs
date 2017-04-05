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
using MentorJWcfService;
using Newtonsoft.Json;

namespace MentorJ_Android
{
    
    public class SlidingTabsFragment : Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;
        private Bundle bundle;
        private tblUserProfile userProfile;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //FragmentSample holds scrolling tab layout
            String stringData = Arguments.GetString("MyDataTag");
            userProfile = JsonConvert.DeserializeObject<tblUserProfile>(stringData);
            return inflater.Inflate(Resource.Layout.FragmentSample, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new SamplePagerAdapter();
            mSlidingTabScrollView.ViewPager = mViewPager;

        }



        public class SamplePagerAdapter : PagerAdapter
        {
            List<string> items = new List<string>();
            private Bundle bundle;
            public SamplePagerAdapter() : base()
            {

                items.Add("Profile");
                items.Add("Forum");
                items.Add("Groups");
                items.Add("Part");
                items.Add("12");
                items.Add("Hooray");
            }

            public override int Count
            {
                get { return items.Count; }
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public void setArguments(Bundle savedInstanceState)
            {
                bundle = savedInstanceState;
            }


            //Based on Position, make an if statement
            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.PagerItem, container, false);
                container.AddView(view);

                if ( position == 0 )
                {
                    TextView txtTitle = view.FindViewById<TextView>(Resource.Id.item_title);
                    int pos = position + 1;
                    txtTitle.Text = pos.ToString();

                }

                else if (position == 1)
                {


                }

                return view;
            }

            public string GetHeaderTitle(int position)
            {
                return items[position];
            }

          
            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView((View)obj);
            }
        }
    }
}