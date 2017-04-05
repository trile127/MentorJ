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
using Android.Support.V7.Widget;

namespace MentorJ_Android
{

    [Activity(Label = "GroupMainActivity", MainLauncher = false, Theme ="@style/Theme.AppCompat.Light.DarkActionBar")]
    class GroupMainActivity : Activity
    {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        GroupList mGroupList;
        GroupListAdapter mAdapter;
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            mGroupList = new GroupList(); 

            SetContentView(Resource.Layout.GroupsPage);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            mAdapter = new GroupListAdapter(mGroupList);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecyclerView.SetAdapter(mAdapter);

        }

        //don't need, but keeping for information
        private void MAdapter_ItemClick(object sender, int e)
        {
            int groupNum = e + 1;
            Toast.MakeText(this, "Click for more info!", ToastLength.Short).Show();
        }
    }
}