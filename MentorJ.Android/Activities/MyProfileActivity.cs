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
using Newtonsoft.Json;
using SQLite;
using System.IO;

namespace MentorJ.Android
{
    [Activity(Label = "MyProfileActivity")]
    public class MyProfileActivity : Activity
    {
        //private Button btn_reg_logout;
        public static readonly EndpointAddress EndPoint = new EndpointAddress("http://192.168.1.129:9608/MentorJService.svc");
        private MentorJProfileServiceClient _client;
        string msg;
        tblUserProfile loggedOnUser;
        tblUserProfile userProfile;
        tblUserProfile newProfile;
        public static String userSessionPref = "userPrefs";
        ISharedPreferences session;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ProfilePage);
            InitializeMentorJProfileServiceClient();
            session = GetSharedPreferences(userSessionPref, FileCreationMode.Private);
            ProfileInfo();
            


            Bundle dataBundle = new Bundle();
            dataBundle.PutString("UserProfile", JsonConvert.SerializeObject(loggedOnUser));

            loggedOnUser = JsonConvert.DeserializeObject<tblUserProfile>(dataBundle.GetString("UserProfile"));

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            SlidingTabsFragment fragment = new SlidingTabsFragment();
            fragment.Arguments = dataBundle;
            //SlidingTabsFragmentProfile fragment2 = new SlidingTabsFragmentProfile();

            //transaction.Replace(Resource.Id.sample_content_fragment, fragment);
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

        private void InitializeMentorJProfileServiceClient()
        {
            BasicHttpBinding binding = CreateBasicHttp();
            _client = new MentorJProfileServiceClient(binding, EndPoint);
            _client.ReadRecord_UserProfileCompleted += ClientOnReadRecord_UserProfileCompleted;
            _client.AddUpdateRecord_UserProfileCompleted += ClientOnAddUpdateRecord_UserProfileCompleted;
        }

        private async void ProfileInfo()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);

            //Check if database exists
            if (tblUserProfile.TableExists<tblUserProfile>(db))
            {
                loggedOnUser = await getUserProfile();
            }
            else
            {
                createuserProfileDatabase();
            }
        }

        private void createuserProfileDatabase()
        {
            //Check if user profile created
            if (session.GetString("UserProfile", "") == null)
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                string test = tblUserProfile.createDatabase(dpPath);
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<tblUserInfo>();
                newProfile.UserID = session.GetLong("userid", -1); ;
                newProfile.About = null;
                newProfile.PictureURL = null;
                newProfile.PictureData = null;
                newProfile.SmallPictureURL = null;
                newProfile.BigPictureURL = null;
                newProfile.WebProfileLink = null;
                newProfile.WebPicturesOfLink = null;
                newProfile.Modified = DateTime.Now;
                _client.InsertRecord_UserProfileAsync(newProfile);
                string success = tblUserProfile.insertUpdateData(newProfile, dpPath); //Insert userInfo into SQLITE on phone
                ISharedPreferencesEditor session_editor = session.Edit();
                session_editor.PutString("UserProfile", JsonConvert.SerializeObject(userProfile));
                session_editor.Commit();
                Toast.MakeText(this, "Create Database: " + test + "\nInsertUpdateData: " + success, ToastLength.Short).Show();
                loggedOnUser = newProfile;
            }

            //User profile created. Login
            else
            {
                loggedOnUser = JsonConvert.DeserializeObject<tblUserProfile>(session.GetString("UserProfile", ""));
            }

        }


        private async Task<tblUserProfile> getUserProfile()
        {

            long id = session.GetLong("userid", -1);
            if (session.GetLong("userid", -1) > 0)
            {
                _client.ReadRecord_UserProfileAsync(session.GetLong("userid", -1));
                //Figure out a better way to wait and break out
                while (msg == null || msg != "Read UserProfile Successful!")
                {
                    await delayTask();
                    if (msg != null && msg != "Read UserProfile Successful!")
                    {
                        break;  //Error
                    }
                }
                ISharedPreferencesEditor session_editor = session.Edit();
                session_editor.PutString("UserProfile", JsonConvert.SerializeObject(userProfile));
                session_editor.Commit();
                return userProfile;
            }
            else
            {
                if (Intent.Extras.ContainsKey("UserInfo"))
                {
                    var resultData = Intent.GetStringExtra("UserInfo");
                    tblUserInfo userInfo = JsonConvert.DeserializeObject<tblUserInfo>(resultData);
                    _client.ReadRecord_UserProfileAsync(userInfo.UserID);
                    //Figure out a better way to wait and break out
                    while (msg == null || msg != "Read UserProfile Successful!")
                    {
                        await delayTask();
                        if (msg != null && msg != "Read UserProfile Successful!")
                        {
                            break;  //Error
                        }
                    }
                    ISharedPreferencesEditor session_editor = session.Edit();
                    session_editor.PutString("UserProfile", JsonConvert.SerializeObject(userProfile));
                    session_editor.Commit();
                }
                return userProfile;
            }
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

        private void ClientOnAddUpdateRecord_UserProfileCompleted(object sender, AddUpdateRecord_UserProfileCompletedEventArgs AddUpdateRecord_UserProfileCompletedEventArgs)
        {
            if (AddUpdateRecord_UserProfileCompletedEventArgs.Error != null)
            {
                msg = AddUpdateRecord_UserProfileCompletedEventArgs.Error.Message;
            }
            else if (AddUpdateRecord_UserProfileCompletedEventArgs.Cancelled)
            {
                msg = "Request was cancelled.";
            }
            else
            {
                if (AddUpdateRecord_UserProfileCompletedEventArgs.Result)
                {
                    msg = "Add Updated UserProfile Successful!";
                }
                
            }
            RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
        }

    }
}