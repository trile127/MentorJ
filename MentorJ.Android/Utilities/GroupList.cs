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
using Android.Support.v7.Widget;
using Android.Support.v7.AppCompat;
using Android.Support.v7.RecyclerView;
using Anrdoid.Support.v7.CardView;


namespace MentorJ.Android
{
    public class Group
    {
        public int mGroupID { get; set; }
        public string mGroupName { get; set; }
    }
    public class GroupList
    {
        static Group[] listGroups =
        {
            new Group() {mGroupID = Resource.Drawable.imgDog, mGroupName="Group 1: Dogs" },
            new Group() {mGroupID = Resource.Drawable.imgDog, mGroupName="Group 2: Cats" },
            new Group() {mGroupID = Resource.Drawable.imgDog, mGroupName="Group 3: Bunnies" }
        };

        private Group[] groups;

        public GroupList()
        {
            this.groups = listGroups;
        }

        public int numGroups
        {
            get
            {
                return groups.Length;
            }
        }

        public Group this[int i]
        {
            get { return groups[i]; }
        }
    }   
    public class GroupViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }
        public TextView GroupName { get; set; }

        public GroupViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            Image = itemView.FindViewBydId<ImageView>(Resource.Id.imageView);
            GroupName = itemView.FindViewById<TextView>(Resource.Id.textView);
            itemView.Click += (sender, e) => listener(base.Position);
        }
    }

    public class GroupListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public GroupList mGroupList;

        public GroupListAdapter(GroupList groupList)
        {
            mGroupList = groupList;
        }
        public override int ItemCount
        {
            get
            {
                return mPhotoAlbum.numGroups;
            }
        }
        
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            GroupViewHolder vh = holder as GroupViewHolder;
            vh.Image.SetImageResource(mGroupList[position].mGroupID);
            vh.GroupName.Text = mGroupList[position].mGroupName;
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.GroupCard, parent, false);
            GroupViewHolder vh = new GroupViewHolder(itemView, OnClick);
            return vh;
        }

        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj); 
        }
    }
}