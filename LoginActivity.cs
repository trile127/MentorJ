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

namespace MentorJ.Android
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        EditText txtEmail;
        EditText txtPassword;
        Button btncreate;
        Button btnsign;
        Button btnforgotpw;
        public static String userSessionPref = "userPrefs";
        public static String User_Name = "userName";
        public static String User_Email = "userEmail";
        public static String User_Password = "userPassword";
        ISharedPreferences session;
        String SESSION_NAME, SESSION_EMAIL, SESSION_PASS;
        long SESSION_USERID;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Login);
            checkCredentials();
            initialize();
            session = GetSharedPreferences(userSessionPref, FileCreationMode.Private);
        }

        private void initialize()
        {
            // Get our button from the layout resource,  
            // and attach an event to it  
            btnsign = FindViewById<Button>(Resource.Id.btnLogin);
            btncreate = FindViewById<Button>(Resource.Id.btnRegister);
            txtEmail = FindViewById<EditText>(Resource.Id.editEmail);
            txtPassword = FindViewById<EditText>(Resource.Id.editPassword);
            btnforgotpw = FindViewById<EditText>(Resource.Id.btnForgotPw);


            btnsign.Click += btnsign_Click;
            btncreate.Click += btncreate_Click;
            btnforgotpw.Click += btnforgotpw_Click;
            

            //CreateDB();
        }
        private void Btncreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }

        private void Btnforgotpw_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ForgotPwActivity));
        }

        private void Btnsign_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<tblUserInfo>(); //Call Table  
                var query = data.Where(x => (x.Email == txtEmail.Text) && x.Password == txtPassword.Text).FirstOrDefault(); //Linq Query  
                if (query != null)
                {
                    //if you want you can toast 
                    Toast.MakeText(this, "Login Success", ToastLength.Short).Show();
                    SESSION_NAME = query.UserName;
                    SESSION_EMAIL = query.Email;
                    SESSION_PASS = query.Password;
                    SESSION_USERID = query.UserID;
                    ISharedPreferencesEditor session_editor = session.Edit();
                    session_editor.PutString("username", SESSION_NAME);
                    session_editor.PutString("email", SESSION_EMAIL);
                    session_editor.PutString("pass", SESSION_PASS);
                    session_editor.PutLong("userid", SESSION_USERID);
                    session_editor.Commit();
                    Intent n = new Intent(this, typeof(MyProfileActivity));
                    StartActivity(n);
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Email or Password invalid", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
        //public string CreateDB()
        //{
        //    var output = "";
        //    output += "Creating Database if it doesnt exists";
        //    if ( File.Exists(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3")) == false )
        //    {
        //        string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
        //        var db = new SQLiteConnection(dpPath);
        //        output += "\n Database Created....";
        //        Toast.MakeText(this, "Database Created!,", ToastLength.Short).Show();
        //        return output;
        //    } //Create New Database  
        //    else
        //    {
        //        output = "Database already exists.";
        //        Toast.MakeText(this, "User Login Database already exists!,", ToastLength.Short).Show();
        //        return output;
        //    }
        //}

        // method to check existing credentials
        public void checkCredentials()
        {
            ISharedPreferences preferences = GetSharedPreferences(userSessionPref, FileCreationMode.Private);
            String email = preferences.GetString("email", "");
            String username = preferences.GetString("username", "");
            Toast.MakeText(this, "Username: " + username + "\nEmail: " + email, ToastLength.Short).Show();
            String pass = preferences.GetString("pass", "");
            long userid = preferences.GetLong("userid", -1);
            if (!username.Equals("") && !email.Equals("") && !pass.Equals("") && userid != -1)
            {
                //Check with webserver HERE


                Toast.MakeText(this, "Successful Login!!,", ToastLength.Short).Show();
                Intent n = new Intent(this, typeof(MyProfileActivity));
                StartActivity(n);
                Finish();
            }

        }
    }
}