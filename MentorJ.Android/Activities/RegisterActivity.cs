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
using AndroidApp.Models;
using MentorJWcfService;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AndroidApp
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        private MentorJInfoServiceClient _client;
        
        EditText txtUsername;
        EditText txtEmail;
        EditText txtPassword;
        EditText txtFirstName;
        EditText txtMiddleName;
        EditText txtLastName;
        EditText txtConfirmPassword;
        EditText txtStreetAddress;
        EditText txtCity;
        EditText txtState;
        EditText txtCountry;
        EditText txtPhoneNumber;

        Button btnContinue, btnContinue2;

        tblUserInfo newUser;
        public static String userSessionPref = "userPrefs";
        String SESSION_NAME, SESSION_EMAIL, SESSION_PASS;
        public static long SESSION_USERID;
        ISharedPreferences session;
        long getUserID = -1;
        bool isNameTaken, isEmailTaken;
        int checkUserName = -1, checkEmail = -1;
        public static readonly EndpointAddress EndPoint = new EndpointAddress("http://192.168.1.129:9608/MentorJService.svc");


        //Insert Code Here

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterBasicInfo);
            Initialize();
            InitializeMentorJServiceClient();
            session = GetSharedPreferences(userSessionPref, FileCreationMode.Private);
        }

        private void Initialize()
        {
            btnContinue = FindViewById<Button>(Resource.Id.btnContinue);
            btnContinue.Click += btnContinue_Click;
            btnContinue2 = FindViewById<Button>(Resource.Id.btnContinue2);
            btnContinue.Click += btnContinue2_Click;

            //btn_reg_back = FindViewById<Button>(Resource.Id.btn_reg_back);
            //btn_reg_back.Click += Btn_reg_back_Click;

            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtEmail= FindViewById<EditText>(Resource.Id.txtEmail);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            txtConfirmPassword = FindViewById<EditText>(Resource.Id.txtConfirmPassword);
            txtFirstName = FindViewById<EditText>(Resource.Id.txtFirstName);
            txtMiddleName = FindViewById<EditText>(Resource.Id.txtMiddleName);
            txtLastName = FindViewById<EditText>(Resource.Id.txtLastName);
            txtStreetAddress = FindViewById<EditText>(Resource.Id.txtStreetAddress);
            txtCity = FindViewById<EditText>(Resource.Id.txtCity);
            txtState = FindViewById<EditText>(Resource.Id.txtState);
            txtCountry = FindViewById<EditText>(Resource.Id.txtCountry);
            txtPhoneNumber = FindViewById<EditText>(Resource.Id.txtPhoneNumber);
            

            //Insert Code Here for buttons and textviews
        }

        private async Task delayTask()
        {
            await Task.Delay(500);
        }

        private void InitializeMentorJServiceClient()
        {
            BasicHttpBinding binding = CreateBasicHttp();

            _client = new MentorJInfoServiceClient(binding, EndPoint);
            //_client.SayHelloToCompleted += ClientOnSayHelloToCompleted;
            _client.InsertRecord_UserInfoCompleted += ClientOnInsertRecordCompleted;
            _client.isUserNameTaken_UserInfoCompleted += ClientOnisUserNameTakenCompleted;
            _client.isEmailTaken_UserInfoCompleted += ClientOnisEmailTakenCompleted;
            _client.assignUserID_UserInfoCompleted += ClientOnisassignUserIDCompleted;


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

        private void btnContinue_Click(object sender, EventArgs e)
        {

            try
            {

                newUser = new tblUserInfo();
                //Put code here
                //Go to next page, but check required fields FIRST
                //Also check if username and email are already TAKEN
                

                if (txtUsername.Text.Trim() != "")  //if something is in it
                {
                    
                    newUser.UserName = txtUsername.Text.Trim();
                    
                }

                newUser.Email = txtEmail.Text;
                newUser.Password = txtPassword.Text;
                newUser.First_Name = "Tri";
                newUser.Middle_Name = "Xuan";
                newUser.Last_Name = "Le";

                //All good?
                SetContentView(Resource.Layout.RegisterBasicInfo);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }


        private async void btnContinue2_Click(object sender, EventArgs e)
        {
            
            try
            {
                _client.assignUserID_UserInfoAsync();
                while (getUserID == -1)
                {
                    await delayTask();
                }
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                string test = tblUserInfo.createDatabase(dpPath);
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<tblUserInfo>();
                newUser.UserID = getUserID;
                newUser.UserName = txtUsername.Text.Trim();
                newUser.Email = txtEmail.Text;
                newUser.Password = txtPassword.Text;
                newUser.First_Name = txtFirstName.Text.Trim();
                newUser.Middle_Name = txtMiddleName.Text.Trim();
                newUser.Last_Name = txtLastName.Text.Trim();
                newUser.Sex = "Male";
                newUser.Birthday = DateTime.Now;
                newUser.Age = 24;
                newUser.Street_Address = txtStreetAddress.Text.Trim();
                newUser.City = txtCity.Text.Trim();
                newUser.State = txtState.Text.Trim();
                newUser.ZipCode = "22312";
                newUser.Country = txtCountry.Text.Trim();
                newUser.PhoneNumber = txtPhoneNumber.Text.Trim();
                newUser.isPremium = true;
                newUser.isMentor = true;
                newUser.isAdmin = false;
                newUser.LastUpdatedDate = DateTime.Now;
                newUser.LastLoginDate = DateTime.Now;
                newUser.LastActiveDate = DateTime.Now;
                newUser.AccountCreationDate = DateTime.Now;
                newUser.FailedLoginAttempts = 0;
                newUser.LastFailedLoginDate = DateTime.Now;
                newUser.AccountLocked = false;
                
                _client.isUserNameTaken_UserInfoAsync(newUser);  //check if username in use
                
                _client.isEmailTaken_UserInfoAsync(newUser); //check if email is in use
                while (checkEmail == -1 && checkUserName == -1) // wait for request to change flags
                {
                    await delayTask();
                }
                if (!isNameTaken && !isEmailTaken)    //if name and email are unused, insert into web SQL Database
                {
                    _client.InsertRecord_UserInfoAsync(newUser);
                    string success = tblUserInfo.insertUpdateData(newUser, dpPath); //Insert userInfo into SQLITE on phone
                    Toast.MakeText(this, "Create Database: " + test + "\nInsertUpdateData: " + success, ToastLength.Short).Show();
                    checkEmail = -1; // reset FLAGS, request done.
                    checkUserName = -1;
                    getUserID = -1;

                    SESSION_NAME = newUser.UserName;
                    SESSION_EMAIL = newUser.Email;
                    SESSION_PASS = newUser.Password;
                    SESSION_USERID = newUser.UserID;
                    ISharedPreferencesEditor session_editor = session.Edit();
                    session_editor.PutString("username", SESSION_NAME);
                    session_editor.PutString("email", SESSION_EMAIL);
                    session_editor.PutString("pass", SESSION_PASS);
                    session_editor.PutLong("userid", SESSION_USERID);
                    session_editor.PutString("UserInfo", Newtonsoft.Json.JsonConvert.SerializeObject(newUser));
                    session_editor.Commit();
                    Intent n = new Intent(this, typeof(MyProfileActivity));
                    n.PutExtra("UserInfo", Newtonsoft.Json.JsonConvert.SerializeObject(newUser));
                    StartActivity(n);
                    Finish();
                }
                else
                {
                    checkEmail = -1; // reset FLAGS, request done.
                    checkUserName = -1;
                    getUserID = -1;
                    Toast.MakeText(this, "The same user credentials have already been taken!,", ToastLength.Long).Show();
                }

                //}

                //else
                //{
                //    var data1 = data.Where(x => (x.UserName == txtUsername.Text || x.Email == txtEmail.Text) && x.Password == txtPassword.Text).FirstOrDefault(); //Linq Query 
                //    if ( data1 == null)
                //    {
                //        tblUserInfo newUser = new tblUserInfo();

                //        newUser.UserName = txtUsername.Text;
                //        newUser.Email = txtEmail.Text;
                //        newUser.Password = txtPassword.Text;
                //        db.Insert(newUser);
                //        Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
                //    } 
                //    else
                //    {
                //        Toast.MakeText(this, "The same user credentials have already been taken!,", ToastLength.Long).Show();
                //    }

                //}
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }



        private void ClientOnInsertRecordCompleted(object sender, InsertRecord_UserInfoCompletedEventArgs insertRecordCompletedEventArgs)
        {
            string msg = null;
            bool insertion = false;

            if (insertRecordCompletedEventArgs.Error != null)
            {
                msg = insertRecordCompletedEventArgs.Error.Message;
            }
            else if (insertRecordCompletedEventArgs.Cancelled)
            {
                msg = "Request was cancelled.";
            }
            else
            {
                insertion = insertRecordCompletedEventArgs.Result;
                msg = "Profile Created! Inserted: " + insertion;
            }
            RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Long).Show());
        }

        private void ClientOnisUserNameTakenCompleted(object sender, isUserNameTaken_UserInfoCompletedEventArgs isUserNameTakenCompletedEventArgs)
        {
            string msg = null;

            if (isUserNameTakenCompletedEventArgs.Error != null)
            {
                msg = isUserNameTakenCompletedEventArgs.Error.Message;
                Toast.MakeText(this, msg, ToastLength.Short).Show();
            }
            else if (isUserNameTakenCompletedEventArgs.Cancelled)
            {
                msg = "Request was cancelled.";
                Toast.MakeText(this, msg, ToastLength.Short).Show();
            }
            else
            {
                isNameTaken = isUserNameTakenCompletedEventArgs.Result;
                if (isNameTaken)
                {
                    msg = "UserName Collision: " + isNameTaken;

                }
                else
                {
                    msg = "No UserName Collision: " + isNameTaken;
                }
               
            }
            checkUserName = 1;
            RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Long).Show());
        }
        private void ClientOnisassignUserIDCompleted(object sender, assignUserID_UserInfoCompletedEventArgs isassignUserIDCompletedEventArgs)
        {
            string msg = null;

            if (isassignUserIDCompletedEventArgs.Error != null)
            {
                msg = isassignUserIDCompletedEventArgs.Error.Message;
                Toast.MakeText(this, msg, ToastLength.Short).Show();
            }
            else if (isassignUserIDCompletedEventArgs.Cancelled)
            {
                msg = "Request was cancelled.";
                Toast.MakeText(this, msg, ToastLength.Short).Show();
            }
            else
            {
                getUserID = isassignUserIDCompletedEventArgs.Result;
                msg = "Generated new UserID: " + getUserID;

            }

            RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Long).Show());
        }


        private void ClientOnisEmailTakenCompleted(object sender, isEmailTaken_UserInfoCompletedEventArgs isEmailTakenCompletedEventArgs)
        {
            string msg = null;

            if (isEmailTakenCompletedEventArgs.Error != null)
            {
                msg = isEmailTakenCompletedEventArgs.Error.Message;
                Toast.MakeText(this, msg, ToastLength.Short).Show();
            }
            else if (isEmailTakenCompletedEventArgs.Cancelled)
            {
                msg = "Request was cancelled.";
                Toast.MakeText(this, msg, ToastLength.Short).Show();
            }
            else
            {
                isEmailTaken = isEmailTakenCompletedEventArgs.Result;
                if (isEmailTaken)
                {
                    msg = "Email Collision: " + isEmailTaken;

                }
                else
                {
                    msg = "No Email Collision: " + isEmailTaken;
                }
            }
            checkEmail = 1;
            RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Long).Show());
        }
    }
}