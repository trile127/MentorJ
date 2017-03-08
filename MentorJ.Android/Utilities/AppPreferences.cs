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
using Android.Preferences;

namespace MentorJ.Android.Utilities
{
    public class AppPreferences
{
    private ISharedPreferences mSharedPrefs;
    private ISharedPreferencesEditor mPrefsEditor;
    private Context mContext;

    private static String PREFERENCE_ACCESS_KEY = "PREFERENCE_ACCESS_KEY";

    public AppPreferences (Context context)
    {
        this.mContext = context;
        mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
        mPrefsEditor = mSharedPrefs.Edit ();            
    }

    public void saveAccessKey(string key){
        mPrefsEditor.PutString(PREFERENCE_ACCESS_KEY, key);
        mPrefsEditor.Commit();
    }

    public string getAccessKey(){
        return mSharedPrefs.GetString(PREFERENCE_ACCESS_KEY, "");
    }
}
}