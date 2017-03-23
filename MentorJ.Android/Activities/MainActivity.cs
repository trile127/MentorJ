using System;
using Android.App;
using Android.Widget;
using Android.OS;
using System.ServiceModel;
using MentorJ;
using Android;
using System.Xml;
using Android.Content;
using MentorJ.Android;

using MentorJ.Android.Utilities;
using MentorJWcfService;

namespace MentorJ.Android
{
    [Activity(Label = "MentorJ.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity

    {
        public static readonly EndpointAddress EndPoint = new EndpointAddress("http://192.168.1.129:9608/MentorJService.svc");


        private MentorJInfoServiceClient _client;
        private Button _getUserInfoButton;
        private TextView _getUserInfoTextView;
        private Button _sayHelloWorldButton;
        private TextView _sayHelloWorldTextView;
        private Button _loginButton;
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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            InitializeMentorJInfoServiceClient();

            // This button will invoke the GetHelloWorldData - the method that takes a C# object as a parameter.
            _getUserInfoButton = FindViewById<Button>(Resource.Id.getUserInfoButton);
            _getUserInfoButton.Click += GetUserInfoButtonOnClick;
            _getUserInfoTextView = FindViewById<TextView>(Resource.Id.getUserInfoTextView);

            // This button will invoke SayHelloWorld - this method takes a simple string as a parameter.
            //_sayHelloWorldButton = FindViewById<Button>(Resource.Id.sayHelloWorldButton);
            //_sayHelloWorldButton.Click += SayHelloWorldButtonOnClick;
            //_sayHelloWorldTextView = FindViewById<TextView>(Resource.Id.sayHelloWorldTextView);

            _loginButton = FindViewById<Button>(Resource.Id.loginButton);
            _loginButton.Click += LoginButtonOnClick;



            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.MaxCharactersFromEntities = 1024;
        }

        private void InitializeMentorJInfoServiceClient()
        {
            BasicHttpBinding binding = CreateBasicHttp();

            _client = new MentorJInfoServiceClient(binding, EndPoint);
             //_client.SayHelloToCompleted += ClientOnSayHelloToCompleted;
            _client.ReadRecord_UserInfoCompleted += ClientOnReadRecordCompleted;


        }

        private void GetUserInfoButtonOnClick(object sender, EventArgs eventArgs)
        {
            _getUserInfoTextView.Text = "Waiting for WCF...";

           _client.ReadRecord_UserInfoAsync(1);

        }

        //private void SayHelloWorldButtonOnClick(object sender, EventArgs eventArgs)
        //{
        //    _sayHelloWorldTextView.Text = "Waiting for WCF...";
        //    _client.SayHelloToAsync("Kilroy");
        //}

        private void LoginButtonOnClick(object sender, EventArgs eventArgs)
        {
            //Intent n = new Intent(this, typeof(LoginActivity));
            //StartActivity(n);
            //Finish();
            Intent n = new Intent(this, typeof(SlidingTabActivity));
            StartActivity(n);
            Finish();
        }

        private void ClientOnReadRecordCompleted(object sender, ReadRecord_UserInfoCompletedEventArgs readRecordCompletedEventArgs)
        {
            string msg = null;
            tblUserInfo newUser = null;

            if (readRecordCompletedEventArgs.Error != null)
            {
                msg = readRecordCompletedEventArgs.Error.Message;
            }
            else if (readRecordCompletedEventArgs.Cancelled)
            {
                msg = "Request was cancelled.";
            }
            else
            {
                newUser = readRecordCompletedEventArgs.Result;
                Serializer.Clone<tblUserInfo>(readRecordCompletedEventArgs.Result, newUser);
            }
            RunOnUiThread(() => _getUserInfoTextView.Text = newUser.UserName);
        }

        //private void ClientOnSayHelloToCompleted(object sender, SayHelloToCompletedEventArgs sayHelloToCompletedEventArgs)
        //{
        //    string msg = null;

        //    if (sayHelloToCompletedEventArgs.Error != null)
        //    {
        //        msg = sayHelloToCompletedEventArgs.Error.Message;
        //    }
        //    else if (sayHelloToCompletedEventArgs.Cancelled)
        //    {
        //        msg = "Request was cancelled.";
        //    }
        //    else
        //    {
        //        msg = sayHelloToCompletedEventArgs.Result;
        //    }
        //    RunOnUiThread(() => _sayHelloWorldTextView.Text = msg);
        //}
    }
}

