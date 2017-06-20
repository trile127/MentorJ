using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

namespace MentorJ_Android
{
    [Activity(Theme = "@style/AppFullScreenTheme")]
    public abstract class BaseActivity : AppCompatActivity
    {
        public Toolbar Toolbar { get; set; }

        protected abstract int LayoutResource { get; }

        protected int ActionBarIcon
        {
            set { Toolbar.SetNavigationIcon(value); }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(LayoutResource);
            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            if (Toolbar != null)
            {
                SetSupportActionBar(Toolbar);

                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }
        }
    }
}