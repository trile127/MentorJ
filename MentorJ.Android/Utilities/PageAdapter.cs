using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace MentorJ.Android
{
    public class SamplePagerAdapter : FragmentPagerAdapter
    {
        private List<Fragment> mFragmentHolder;

        public SamplePagerAdapter(FragmentManager fragManager) : base(fragManager)
        {
            mFragmentHolder = new List<Fragment>();
            mFragmentHolder.Add(new Fragment1());
            mFragmentHolder.Add(new Fragment2());
            mFragmentHolder.Add(new Fragment3());
        }

        public override int Count
        {
            get { return mFragmentHolder.Count; }
        }

        public override Fragment GetItem(int position)
        {
            return mFragmentHolder[position];
        }
    }

    public class Fragment1 : Fragment
    {
        private TextView mTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.ProfilePage, container, false);

            mTextView = view.FindViewById<TextView>(Resource.Id.txtMyProfile);
            mTextView.Text = "Fragment 1 Class";
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
            var view = inflater.Inflate(Resource.Layout.GroupsPage, container, false);

            mTxt = view.FindViewById<EditText>(Resource.Id.editText1);
            mTxt.Text = "Fragment 2 Class :)";
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
            var view = inflater.Inflate(Resource.Layout.Forum, container, false);

            mButton = view.FindViewById<Button>(Resource.Id.btnSample);
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Fragment 3";
        }
    }
}