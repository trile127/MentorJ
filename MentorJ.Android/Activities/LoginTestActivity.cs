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
using System.ServiceModel;
using MentorJWcfService;
using MentorJ.Android.Utilities;
using System.Threading.Tasks;

namespace MentorJ.Android
{
    [Activity(Label = "LoginTestActivity")]
    public class LoginTestActivity : Activity
    {
        public static readonly EndpointAddress EndPoint = new EndpointAddress("http://192.168.1.129:9608/MentorJService.svc");

        EditText txtUserName;
        EditText txtPassword;
        Button btnLogin, btnForgotPass, btnRegister;
        private MentorJInfoServiceClient _client;
        string msg;
        tblUserInfo newUser;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);
            initialize();
            msg = null;
            InitializeMentorJInfoServiceClient();
            // Create your application here
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


        public void initialize()
        {
            // Get our button from the layout resource,  
            // and attach an event to it  
            btnLogin = FindViewById<Button>(Resource.Id.buttonLogin);
            btnRegister = FindViewById<Button>(Resource.Id.textRegister);

            btnForgotPass = FindViewById<Button>(Resource.Id.textForgotPassword);

            txtUserName = FindViewById<EditText>(Resource.Id.editUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.editPassword);


            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;
            btnForgotPass.Click += BtnForgotPass_Click;
        }

        private async Task delayTask()
        {
            await Task.Delay(500);
        }

        private async void BtnLogin_Click(object sender, EventArgs eventArgs)
        {
            //_getUserInfoTextView.Text = "Waiting for WCF...";
            
            _client.ValidateLogin_UserInfoAsync(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            //Figure out a better way to wait and break out
            while (msg == null || msg != "Login Successful!")
            {
                await delayTask();
                if (msg != null && msg != "Login Successful!")
                {
                    break;
                    //Error, 
                }
            }

            if (msg == "Login Successful!")
            {
                //Set user preferences

                Intent n = new Intent(this, typeof(MyProfileActivity));
                StartActivity(n);
                Finish();
            }
            else
            {
                Toast.MakeText(this, "Login Failed", ToastLength.Long).Show();  //Add error message on UI code saying why it failed. IE: "Username or password incorrect"
            }
            

        }

        private void BtnRegister_Click(object sender, EventArgs eventArgs)
        {
            
            Intent n = new Intent(this, typeof(RegisterActivity));
            StartActivity(n);
            Finish();

        }

        private void BtnForgotPass_Click(object sender, EventArgs eventArgs)
        {

            //Intent n = new Intent(this, typeof(ForgotPasswordActivity));
            //StartActivity(n);
            //Finish();
        }

        private void ClientOnValidateLogin_UserInfoCompleted(object sender, ValidateLogin_UserInfoCompletedEventArgs validateLoginCompletedEventArgs)
        {
            string msg = null;
            

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
                newUser = validateLoginCompletedEventArgs.Result;
                newUser.LastLoginDate = DateTime.Now;
                newUser.LastActiveDate = DateTime.Now;
                _client.UpdateRecord_UserInfoAsync(newUser);
                msg = "Login Successful!";

            }
            RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
        }
    }
}