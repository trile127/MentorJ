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
using MentorJWcfService;
using System.ServiceModel;
using System.Threading.Tasks;

namespace MentorJ.Android
{
    [Activity(Label = "MyProfileActivity")]
    public class MyProfileActivity : Activity
    {
        //private Button btn_reg_logout;
        public static readonly EndpointAddress EndPoint = new EndpointAddress("http://192.168.1.129:9608/MentorJService.svc");
        private MentorJProfileServiceClient _client;
        string msg;
        tblUserProfile userProfile;

        public static String userSessionPref = "userPrefs";
        ISharedPreferences session;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ProfilePage);
            InitializeMentorJInfoServiceClient();
            session = GetSharedPreferences(userSessionPref, FileCreationMode.Private);
            if (session.GetLong("userid", -1) > 0)
            {
                _client.ReadRecord_UserProfileAsync(session.GetLong("userid", -1));
            }

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            SlidingTabsFragment fragment = new SlidingTabsFragment();
            //SlidingTabsFragmentProfile fragment2 = new SlidingTabsFragmentProfile();

            transaction.Replace(Resource.Id.sample_content_fragment, fragment);
            transaction.Commit();





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
            _client = new MentorJProfileServiceClient(binding, EndPoint);
            _client.ReadRecord_UserProfileCompleted += ClientOnReadRecord_UserProfileCompleted;
        }

        private async Task delayTask()
        {
            await Task.Delay(500);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_bar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void ClientOnReadRecord_UserProfileCompleted(object sender, ReadRecord_UserProfileCompletedEventArgs ReadRecord_UserProfileCompletedEventArgs)
        {
            if (ReadRecord_UserProfileCompletedEventArgs.Error != null)
            {
                msg = ReadRecord_UserProfileCompletedEventArgs.Error.Message;
            }
            else if (ReadRecord_UserProfileCompletedEventArgs.Cancelled)
            {
                msg = "Request was cancelled.";
            }
            else
            {
                userProfile = ReadRecord_UserProfileCompletedEventArgs.Result;
                msg = "Read UserProfile Successful!";
            }
            RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
        }

    }
}