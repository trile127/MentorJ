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
using System.IO;
using SQLite;
using HelloWorld.Android.Models;
using static Android.Views.View;

namespace HelloWorld.Android
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText txtUsername;
        EditText txtEmail;
        EditText txtPassword;
        Button btncreate, btn_reg_back;
        public static String userSessionPref = "userPrefs";
        public static String User_Email = "userEmail";
        public static String User_Password = "userPassword";
        ISharedPreferences session;
        String SESSION_EMAIL, SESSION_PASS;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);
            initialize();
            session = GetSharedPreferences(userSessionPref, FileCreationMode.Private);
        }

        public void initialize()
        {
            btncreate = FindViewById<Button>(Resource.Id.btn_reg_create);
            btn_reg_back = FindViewById<Button>(Resource.Id.btn_reg_back);
            btn_reg_back.Click += Btn_reg_back_Click;
            txtUsername = FindViewById<EditText>(Resource.Id.txt_reg_username);
            txtEmail= FindViewById<EditText>(Resource.Id.txt_reg_email);
            txtPassword = FindViewById<EditText>(Resource.Id.txt_reg_password);
            btncreate.Click += Btncreate_Click;

        }

        public void checkCredentials()
        {
            //ISharedPreferences preferences = GetSharedPreferences(userSessionPref, )

        }

        private void Btn_reg_back_Click(object sender, EventArgs e)
        {
            Intent n = new Intent(this, typeof(MainActivity));
            StartActivity(n);
            Finish();
        }

        private void Btncreate_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");

                var db = new SQLiteConnection(dpPath);
                var data = db.Table<LoginTable>();
                if ( data == null )
                {
                    db.CreateTable<LoginTable>();
                    LoginTable tbl = new LoginTable();
                    tbl.username = txtUsername.Text;
                    tbl.email = txtEmail.Text;
                    tbl.password = txtPassword.Text;
                    db.Insert(tbl);
                    Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
                }
                
                else
                {
                    var data1 = data.Where(x => (x.username == txtUsername.Text || x.email == txtEmail.Text) && x.password == txtPassword.Text).FirstOrDefault(); //Linq Query 
                    if ( data1 == null)
                    {
                        LoginTable tbl = new LoginTable();
                        tbl.username = txtUsername.Text;
                        tbl.email = txtEmail.Text;
                        tbl.password = txtPassword.Text;
                        db.Insert(tbl);
                        Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
                    } 
                    else
                    {
                        Toast.MakeText(this, "The same user credentials have already been taken!,", ToastLength.Long).Show();
                    }

                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}