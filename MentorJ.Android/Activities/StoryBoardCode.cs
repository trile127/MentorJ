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
using MentorJ.Android;

namespace MentorJ.Android
{
    [Activity(Label = "StoryBoardCode")]
    public class StoryBoardCode : Activity
    {
        private Button btn_reg_logout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Storyboard);
            btn_reg_logout = FindViewById<Button>(Resource.Id.btn_reg_logout);
            btn_reg_logout.Click += Btn_reg_logout_Click;
        }

        private void Btn_reg_logout_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}