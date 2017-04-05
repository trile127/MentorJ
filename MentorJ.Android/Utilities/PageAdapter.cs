using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.App;
using Android.Support.V4.View;
using Java.Lang;

namespace MentorJ_Android
{
    public class SamplePagerAdapter2 : PagerAdapter
    {
        private List<Fragment> mFragmentHolder;
        int fragmentCount;

        public SamplePagerAdapter2() : base()
        {
            mFragmentHolder = new List<Fragment>();
            mFragmentHolder.Add(new Fragment1());
            mFragmentHolder.Add(new Fragment2());
            mFragmentHolder.Add(new Fragment3());
            fragmentCount = mFragmentHolder.Count;

        }

        public override int Count
        {
            get
            {
                return fragmentCount;
            }
        }



        public Fragment GetItem(int position)
        {
            return mFragmentHolder[position];
        }

        public string GetHeaderTitle(int position)
        {
            return mFragmentHolder[position].ToString();
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == @object;
        }
    }

    public class Fragment1 : Fragment
    {
        private TextView mTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //var view = inflater.Inflate(Resource.Layout.ProfilePage, container, false);

            //mTextView = view.FindViewById<TextView>(Resource.Id.txtMyProfile);
            //mTextView.Text = "Fragment 1 Class";

            View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.PagerItem, container, false);
            container.AddView(view);

         
            TextView txtTitle = view.FindViewById<TextView>(Resource.Id.item_title);
            txtTitle.Text = "Fragment 1 Class";
            
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Fragment 1";
        }
    }

    public class Fragment2 : Fragment
    {
        private EditText mTxt;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //var view = inflater.Inflate(Resource.Layout.GroupsPage, container, false);

            //mTxt = view.FindViewById<EditText>(Resource.Id.editText1);
            //mTxt.Text = "Fragment 2 Class :)";

            View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.PagerItem, container, false);
            container.AddView(view);


            TextView txtTitle = view.FindViewById<TextView>(Resource.Id.editText1);
            txtTitle.Text = "Fragment 2 Class :";
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Fragment 2";
        }
    }

    public class Fragment3 : Fragment
    {
        private Button mButton;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //var view = inflater.Inflate(Resource.Layout.Forum, container, false);

            //mButton = view.FindViewById<Button>(Resource.Id.btnSample);

            View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.PagerItem, container, false);
            container.AddView(view);


            //mButton = view.FindViewById<Button>(Resource.Id.btnSample);
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Fragment 3";
        }
    }
}