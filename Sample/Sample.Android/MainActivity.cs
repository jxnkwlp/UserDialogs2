using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Passingwind.UserDialogs;

namespace Sample.Droid
{
    [Activity(Label = "UserDialogs", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            UserDialogs.Init(this);

            LoadApplication(new App());


            // Resource.Id.snackbar_text


            //var di = new Dialog(this);

            //di.SetContentView(new TextView(this)
            //{
            //    Text = "12121",
            //    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
            //});


            //di.Window.SetGravity(GravityFlags.Bottom);

            //di.Show();

             // ActionSheetOptions.DefaultAndroidStyleId = Resource.Style.dialog;

        }
    }
}