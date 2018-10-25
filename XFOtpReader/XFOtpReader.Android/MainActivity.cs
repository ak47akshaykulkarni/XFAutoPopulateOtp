using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;
using XFOtpReader.Droid.BroadcastReceivers;

namespace XFOtpReader.Droid
{
    [Activity(Label = "XFOtpReader", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        SmsReceivedBroadcastReceiver _receiver;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            _receiver = new SmsReceivedBroadcastReceiver();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(_receiver, new IntentFilter("android.provider.Telephony.SMS_RECEIVED"));
        }

        protected override void OnPause()
        {
            UnregisterReceiver(_receiver);
            base.OnPause();
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

