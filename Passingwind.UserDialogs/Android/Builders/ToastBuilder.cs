using System;
using Android.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Text.Style;
using Xamarin.Android;
using Android;

namespace Passingwind.UserDialogs.Platforms
{
    public static class ToastBuilder
    {
        static Toast _toast;

        public static void Show(Activity activity, ToastConfig config)
        {
            if (config.AndroidStyleId.HasValue)
                _toast = Toast.MakeText(activity, config.AndroidStyleId.Value, ToastLength.Short);
            else
                _toast = Toast.MakeText(activity, config.Message, ToastLength.Short);

            // set message 
            _toast.SetText(config.Message);

            // TODO
            // change time 
            if (config.Duration.Seconds >= 3)
            {
                _toast.Duration = ToastLength.Long;
            }

            // TODO change color 
            //var view = _toast.View;

            //if (view != null)
            //{
            //    TextView textView = view.FindViewById<TextView>(Android.Resource.Id.Message);

            //    if (config.BackgroundColor.HasValue)
            //        textView.SetBackgroundColor(config.BackgroundColor.Value.ToNativeColor());

            //    if (config.TextColor.HasValue)
            //        textView.SetTextColor(config.TextColor.Value.ToNativeColor());

            //    _toast.View = textView;
            //}



            if (config.Position == ToastPosition.Top)
            {
                _toast.SetGravity(GravityFlags.Top, 0, 0);
            }
            else if (config.Position == ToastPosition.Center)
            {
                _toast.SetGravity(GravityFlags.Center, 0, 0);
            }
            else if (config.Position == ToastPosition.Bottom)
            {
                _toast.SetGravity(GravityFlags.Bottom, 0, 0);
            }

            _toast.Show();
        }

        public static void Hide()
        {
            _toast?.Cancel();

        }





    }
}
