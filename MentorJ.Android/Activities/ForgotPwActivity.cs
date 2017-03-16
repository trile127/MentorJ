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
using SQLite;
using System.IO;
using MentorJ.Android;
using MentorJWcfService;

namespace MentorJ.Android.Activities
{
    [Activity(Label = "ForgotPwActivity")]
    public class ForgotPwActivity : Activity
    {
        Button btnsend;
        Button btnreturn;
        EditText txtEmail;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.ForgotPw);
            initialize();

        }
    
        private void initialize()
        {
            // Get our button from the layout resource,  
            // and attach an event to it  
            btnsend = FindViewById<Button>(Resource.Id.btnSend);
            btnreturn = FindViewById<Button>(Resource.Id.btnReturn);
            txtEmail = FindViewById<EditText>(Resource.Id.editEmail);

            btnsend.Click += btnsend_Click;
            btnreturn.Click += btnreturn_Click;
        }

        private void Btnreturn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
        }

        // insert method to check if email is registered
    }
}