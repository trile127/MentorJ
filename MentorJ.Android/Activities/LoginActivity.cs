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
using System.ServiceModel;
using MentorJ.Android.Utilities;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MentorJ.Android
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {

        public static readonly EndpointAddress EndPoint = new EndpointAddress("http://192.168.1.129:9608/MentorJService.svc");
        EditText txtEmail;
        EditText txtPassword;
        Button btnCreate;
        Button btnSign;
        Button btnforgotPW;
        public static String userSessionPref = "userPrefs";
        public static String User_Name = "userName";
        public static String User_Email = "userEmail";
        public static String User_Password = "userPassword";
        ISharedPreferences session;
        String SESSION_NAME, SESSION_EMAIL, SESSION_PASS;
        public static long SESSION_USERID;

        private MentorJInfoServiceClient _client;
        string msg;
        tblUserInfo user;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Login);
            Initialize();
            InitializeMentorJInfoServiceClient();
            checkCredentials();
            session = GetSharedPreferences(userSessionPref, FileCreationMode.Private);


        }

        private void Initialize()
        {
            // Get our button from the layout resource,  
            // and attach an event to it  
            btnSign = FindViewById<Button>(Resource.Id.btnLogin);
            btnCreate = FindViewById<Button>(Resource.Id.btnRegister);
            txtEmail = FindViewById<EditText>(Resource.Id.editEmail);
            txtPassword = FindViewById<EditText>(Resource.Id.editPassword);
            btnforgotPW = FindViewById<Button>(Resource.Id.btnForgotPw);
            btnSign.Click += Btnsign_Click;
            btnCreate.Click += Btncreate_Click;
            btnforgotPW.Click += BtnforgotPW_Click;

        }

        private static BasicHttpBinding CreateBasicHttp()
        {
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Name = "basicHttpBinding",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };
            TimeSpan timeout = new TimeSpan(0, 0, 30);
            binding.SendTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.ReceiveTimeout = timeout;
            return binding;
        }

        private void InitializeMentorJInfoServiceClient()
        {
            BasicHttpBinding binding = CreateBasicHttp();
            _client = new MentorJInfoServiceClient(binding, EndPoint);
            _client.ValidateLogin_UserInfoCompleted += ClientOnValidateLogin_UserInfoCompleted;
        }

        private async Task delayTask()
        {
            await Task.Delay(500);
        }

        private void Btncreate_Click(object sender, EventArgs e)
        {
            Intent n = new Intent(this, typeof(RegisterActivity));
            StartActivity(n);
            Finish();
        }

        private void BtnforgotPW_Click(object sender, EventArgs e)
        {

            Intent n = new Intent(this, typeof(ForgotPwActivity));
            StartActivity(n);
            Finish();
        }

        private async void Btnsign_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                if ( tblUserInfo.TableExists<tblUserInfo>(db) )
                {
                    var data = db.Table<tblUserInfo>(); //Call Table  
                    var query = data.Where(x => (x.Email == txtEmail.Text) && x.Password == txtPassword.Text).FirstOrDefault(); //Linq Query  
                    if (query != null)
                    {
                        _client.ValidateLogin_UserInfoAsync(txtEmail.Text.Trim(), txtPassword.Text.Trim());
                        while (msg == null || msg != "Login Successful!")
                        {
                            await delayTask();
                            if (msg != null && msg != "Login Successful!")
                            {
                                break;  //Error
                            }
                        }
                        if (msg == "Login Successful!")
                        {
                            //Set user preferences

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
                            n.PutExtra("UserInfo", JsonConvert.SerializeObject(user));
                            StartActivity(n);
                            Finish();
                        }

                        //Database exists but UserInfo not in it. Check with webserver
                        else
                        {
                            _client.ValidateLogin_UserInfoAsync(txtEmail.Text.Trim(), txtPassword.Text.Trim());
                            while (msg == null || msg != "Login Successful!")
                            {
                                await delayTask();
                                if (msg != null && msg != "Login Successful!")
                                {
                                    break;  //Error
                                }
                            }
                            if (msg == "Login Successful!")
                            {
                                
                                var dataTbl = db.Table<tblUserInfo>();
                                string success = tblUserInfo.insertUpdateData(user, dpPath); //Insert userInfo into SQLITE on phone
                                Toast.MakeText(this, "InsertUpdateData: " + success, ToastLength.Short).Show();
                                SESSION_NAME = user.UserName;
                                SESSION_EMAIL = user.Email;
                                SESSION_PASS = user.Password;
                                SESSION_USERID = user.UserID;
                                ISharedPreferencesEditor session_editor = session.Edit();
                                session_editor.PutString("username", SESSION_NAME);
                                session_editor.PutString("email", SESSION_EMAIL);
                                session_editor.PutString("pass", SESSION_PASS);
                                session_editor.PutLong("userid", SESSION_USERID);
                                session_editor.PutString("UserInfo", JsonConvert.SerializeObject(user));
                                session_editor.Commit();
                                Intent n = new Intent(this, typeof(MyProfileActivity));
                                n.PutExtra("UserInfo", JsonConvert.SerializeObject(user));
                                StartActivity(n);
                                Finish();
                            }
                            else
                            {
                                Toast.MakeText(this, "Login Failed", ToastLength.Long).Show();  //Add error message on UI code saying why it failed. IE: "Username or password incorrect"
                            }

                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Email or Password invalid", ToastLength.Short).Show();
                    }
                }
                else
                {

                    _client.ValidateLogin_UserInfoAsync(txtEmail.Text.Trim(), txtPassword.Text.Trim()); //When done, will give tblUserInfo user object the record information
                    //Figure out a better way to wait and break out
                    while (msg == null || msg != "Login Successful!")
                    {
                        await delayTask();
                        if (msg != null && msg != "Login Successful!")
                        {
                            break;  //Error 
                        }
                    }
                    if (msg == "Login Successful!")
                    {
                        //No Database, so create one
                        string test = tblUserInfo.createDatabase(dpPath);
                        var dataTbl = db.Table<tblUserInfo>();
                        string success = tblUserInfo.insertUpdateData(user, dpPath); //Insert userInfo into SQLITE on phone
                        Toast.MakeText(this, "Create Database: " + test + "\nInsertUpdateData: " + success, ToastLength.Short).Show();
                        SESSION_NAME = user.UserName;
                        SESSION_EMAIL = user.Email;
                        SESSION_PASS = user.Password;
                        SESSION_USERID = user.UserID;
                        ISharedPreferencesEditor session_editor = session.Edit();
                        session_editor.PutString("username", SESSION_NAME);
                        session_editor.PutString("email", SESSION_EMAIL);
                        session_editor.PutString("pass", SESSION_PASS);
                        session_editor.PutLong("userid", SESSION_USERID);
                        session_editor.PutString("UserInfo", JsonConvert.SerializeObject(user));
                        session_editor.Commit();
                        Intent n = new Intent(this, typeof(MyProfileActivity));
                        n.PutExtra("UserInfo", JsonConvert.SerializeObject(user));
                        StartActivity(n);
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, "Login Failed", ToastLength.Short).Show();
                    }

                }
                
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        public async void checkCredentials()
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
                _client.ValidateLogin_UserInfoAsync(email, pass);

                //Figure out a better way to wait and break out
                while (msg == null || msg != "Login Successful!")
                {
                    await delayTask();
                    if (msg != null && msg != "Login Successful!")
                    {
                        break;  //Error
                    }
                }

                if (msg == "Login Successful!")
                {
                    //Set user preferences
                    msg = null;
                    Intent intent = new Intent(this, typeof(MyProfileActivity));
                    intent.PutExtra("UserInfo", JsonConvert.SerializeObject(user));
                    RunOnUiThread(() => Toast.MakeText(this, "Successful Login!!,", ToastLength.Short).Show() );
                    Intent n = new Intent(this, typeof(MyProfileActivity));
                    n.PutExtra("UserInfo", JsonConvert.SerializeObject(user));
                    StartActivity(n);
                    Finish();
                }
                else
                {
                    msg = null;
                    Toast.MakeText(this, "Login Failed", ToastLength.Long).Show();  //Add error message on UI code saying why it failed. IE: "Username or password incorrect"
                }
            }

        }

        private void ClientOnValidateLogin_UserInfoCompleted(object sender, ValidateLogin_UserInfoCompletedEventArgs validateLoginCompletedEventArgs)
        {
            if (validateLoginCompletedEventArgs.Error != null)
            {
                msg = validateLoginCompletedEventArgs.Error.Message;
            }
            else if (validateLoginCompletedEventArgs.Cancelled)
            {
                msg = "Request was cancelled.";
            }
            else
            {
                user = validateLoginCompletedEventArgs.Result;
                user.LastLoginDate = DateTime.Now;
                user.LastActiveDate = DateTime.Now;
                _client.UpdateRecord_UserInfoAsync(user);
                msg = "Login Successful!";
            }
            RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
        }
    }
}