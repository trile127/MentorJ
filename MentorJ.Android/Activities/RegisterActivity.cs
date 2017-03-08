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
using MentorJ.Android.Models;
using MentorJWcfService;
using System.ServiceModel;
using System.Threading.Tasks;

namespace MentorJ.Android
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        private MentorJServiceClient _client;
        EditText txtUsername;
        EditText txtEmail;
        EditText txtPassword;
        Button btncreate, btn_reg_back;
        public static String userSessionPref = "userPrefs";
        public static String User_Email = "userEmail";
        public static String User_Password = "userPassword";
        ISharedPreferences session;
        String SESSION_EMAIL, SESSION_PASS;
        bool isNameTaken, isEmailTaken;
        int checkUserName = -1, checkEmail = -1;
        long getUserID = -1;
        public static readonly EndpointAddress EndPoint = new EndpointAddress("http://192.168.1.129:9608/MentorJService.svc");
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);
            Initialize();
            InitializeMentorJServiceClient();
            session = GetSharedPreferences(userSessionPref, FileCreationMode.Private);
        }

        private void Initialize()
        {
            btncreate = FindViewById<Button>(Resource.Id.btn_reg_create);
            btn_reg_back = FindViewById<Button>(Resource.Id.btn_reg_back);
            btn_reg_back.Click += Btn_reg_back_Click;
            txtUsername = FindViewById<EditText>(Resource.Id.txt_reg_username);
            txtEmail= FindViewById<EditText>(Resource.Id.txt_reg_email);
            txtPassword = FindViewById<EditText>(Resource.Id.txt_reg_password);
            btncreate.Click += Btncreate_Click;

        }

        private async Task delayTask()
        {
            await Task.Delay(500);
        }

        private void InitializeMentorJServiceClient()
        {
            BasicHttpBinding binding = CreateBasicHttp();

            _client = new MentorJServiceClient(binding, EndPoint);
            //_client.SayHelloToCompleted += ClientOnSayHelloToCompleted;
            _client.InsertRecordCompleted += ClientOnInsertRecordCompleted;
            _client.isUserNameTakenCompleted += ClientOnisUserNameTakenCompleted;
            _client.isEmailTakenCompleted += ClientOnisEmailTakenCompleted;
            _client.assignUserIDCompleted += ClientOnisassignUserIDCompleted;


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


        private async void Btncreate_Click(object sender, EventArgs e)
        {
            
            try
            {
                _client.assignUserIDAsync();
                while (getUserID == -1)
                {
                    await delayTask();
                }
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                string test = tblUserInfo.createDatabase(dpPath);
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<tblUserInfo>();
                //if ( data == null )
                //{
                //db.CreateTable<tblUserInfo>();
                tblUserInfo newUser = new tblUserInfo();
                newUser.UserID = getUserID;
                newUser.UserName = txtUsername.Text;
                newUser.Email = txtEmail.Text;
                newUser.Password = txtPassword.Text;
                newUser.First_Name = "Tri";
                newUser.Middle_Name = "Xuan";
                newUser.Last_Name = "Le";
                newUser.Sex = "Male";
                newUser.Birthday = DateTime.Now;
                newUser.Age = 24;
                newUser.Street_Address = "6447";
                newUser.City = "Aex";
                newUser.State = "Vir";
                newUser.ZipCode = "22312";
                newUser.Country = "USA";
                newUser.PhoneNumber = "703";
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
                
                _client.isUserNameTakenAsync(newUser);  //check if username in use
                
                _client.isEmailTakenAsync(newUser); //check if email is in use
                while (checkEmail == -1 && checkUserName == -1) // wait for request to change flags
                {
                    await delayTask();
                }
                if (!isNameTaken && !isEmailTaken)    //if name and email are unused, insert into web SQL Database
                {
                    _client.InsertRecordAsync(newUser);
                }
                else
                {
                    Toast.MakeText(this, "The same user credentials have already been taken!,", ToastLength.Long).Show();
                }
                string success = tblUserInfo.insertUpdateData(newUser, dpPath); //Insert userInfo into SQLITE on phone
                Toast.MakeText(this, "Create Database: " + test + "\nInsertUpdateData: " + success, ToastLength.Short).Show();

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
                checkEmail = -1; // reset FLAGS, request done.
                checkUserName = -1;
                getUserID = -1;
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }



        private void ClientOnInsertRecordCompleted(object sender, InsertRecordCompletedEventArgs insertRecordCompletedEventArgs)
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

        private void ClientOnisUserNameTakenCompleted(object sender, isUserNameTakenCompletedEventArgs isUserNameTakenCompletedEventArgs)
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
        private void ClientOnisassignUserIDCompleted(object sender, assignUserIDCompletedEventArgs isassignUserIDCompletedEventArgs)
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


        private void ClientOnisEmailTakenCompleted(object sender, isEmailTakenCompletedEventArgs isEmailTakenCompletedEventArgs)
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