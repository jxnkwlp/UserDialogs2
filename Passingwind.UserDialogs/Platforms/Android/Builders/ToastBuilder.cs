using System;
using Android.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Text.Style;

namespace Passingwind.UserDialogs.Platforms
{
    public class ToastBuilder
    {
        static Toast _toast;

        public static void Show(Activity activity, ToastConfig config)
        {
            if (_toast == null)
                _toast = Toast.MakeText(activity, config.Message, ToastLength.Short);

            // set message 
            _toast.SetText(config.Message);

            // TODO
            // change time 
            if (config.Duration.Seconds >= 2)
            {
                _toast.Duration = ToastLength.Long;
            }


            // _toast.Cancel();

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
            if (_toast != null)
                _toast.Cancel();
        }


        static Snackbar _snackbar;
        public static void ShowSnackbar(Activity activity, ToastConfig config)
        {
            if (_snackbar == null)
            {
                var view = activity.Window.DecorView.RootView.FindViewById(global::Android.Resource.Id.Content);

                _snackbar = Snackbar.Make(view, config.Message, (int)config.Duration.TotalMilliseconds);

            }


            _snackbar.SetDuration((int)config.Duration.TotalMilliseconds);
            _snackbar.SetText(config.Message);


            if (config.Position != ToastPosition.Default)
            {
                // watch for this to change in future support lib versions
                var layoutParams = _snackbar.View.LayoutParameters as FrameLayout.LayoutParams;
                if (layoutParams != null)
                {

                    if (config.Position == ToastPosition.Top)
                    {
                        layoutParams.Gravity = GravityFlags.Top;
                        layoutParams.SetMargins(0, 80, 0, 0);
                    }
                    else if (config.Position == ToastPosition.Center)
                    {
                        layoutParams.Gravity = GravityFlags.Center;
                    }
                    else if (config.Position == ToastPosition.Bottom)
                    {
                        layoutParams.Gravity = GravityFlags.Bottom;
                        layoutParams.SetMargins(0, 0, 0, 30);
                    }

                    _snackbar.View.LayoutParameters = layoutParams;
                }
            }

            _snackbar.Show();
        }


    }
}
