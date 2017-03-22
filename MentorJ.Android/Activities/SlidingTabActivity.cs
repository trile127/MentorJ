using Android.Views;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using System.Collections.Generic;
using Android.App;
using Android.Widget;


namespace MentorJ.Android
{
    [Activity(Label = "Sliding Tab Layout", MainLauncher = true, Icon = "@drawable/xs")]
    public class MainActivity : FragmentActivity
    {
        private ViewPager mViewPager;
        private SlidingTabScrollView mScrollView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SlidingTabActivity);
            mScrollView = FindViewById<SlidingTabScrollView>(Resource.Id.slidingTabs);
            mViewPager = FindViewById<ViewPager>(Resource.Id.viewPager);

            mViewPager.Adapter = new SamplePagerAdapter(SupportFragmentManager);
            mScrollView.ViewPager = mViewPager;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }

    public class SamplePagerAdapter : FragmentPagerAdapter
    {
        private List<Android.Support.V4.App.Fragment> mFragmentHolder;

        public SamplePagerAdapter(Android.Support.V4.App.FragmentManager fragManager) : base(fragManager)
        {
            mFragmentHolder = new List<Android.Support.V4.App.Fragment>();
            mFragmentHolder.Add(new Fragment1());
            mFragmentHolder.Add(new Fragment2());
            mFragmentHolder.Add(new Fragment3());
        }

        public override int Count
        {
            get { return mFragmentHolder.Count; }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return mFragmentHolder[position];
        }
    }

    public class Fragment1 : Android.Support.V4.App.Fragment
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

    public class Fragment2 : Android.Support.V4.App.Fragment
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

    public class Fragment3 : Android.Support.V4.App.Fragment
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

