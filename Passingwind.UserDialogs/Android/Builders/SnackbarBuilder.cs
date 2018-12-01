using Android.App;
using Android.Support.Design.Widget;
using Passingwind.UserDialogs.Platforms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs.Platforms
{
    public static class SnackbarBuilder
    {
        static Snackbar _snackbar;

        public static void ShowSnackbar(Activity activity, SnackbarConfig config)
        {
            var view = activity.Window.DecorView.RootView.FindViewById(global::Android.Resource.Id.Content);

            if (_snackbar == null)
                _snackbar = Snackbar.Make(view, config.Message, (int)config.Duration.TotalMilliseconds);

            if (_snackbar.IsShownOrQueued)
                _snackbar = Snackbar.Make(view, config.Message, (int)config.Duration.TotalMilliseconds);

            _snackbar.SetDuration((int)config.Duration.TotalMilliseconds);
            _snackbar.SetText(config.Message);

            if (!string.IsNullOrWhiteSpace(config.ActionText))
            {
                _snackbar.SetAction(config.ActionText, (v) =>
                {
                    config.Action?.Invoke();

                    _snackbar.Dismiss();
                });

                if (config.ActionTextColor.HasValue)
                    _snackbar.SetActionTextColor(config.ActionTextColor.Value.ToNativeColor());
            }

            if (config.TextColor.HasValue)
            {
                //TextView textView = _snackbar.View.FindViewById<TextView>(Resource.Id.snackbar_text);
                //textView.SetTextColor(config.TextColor.Value.ToNativeColor());

                // TODO
            }

            if (config.BackgroundColor.HasValue)
            {
                //TextView textView = _snackbar.View.FindViewById<TextView>(Resource.Id.snackbar_text);
                //textView.SetBackgroundColor(config.BackgroundColor.Value.ToNativeColor());

                // TODO
            }

            //if (config.Position != ToastPosition.Default)
            //{
            //    // watch for this to change in future support lib versions
            //    var layoutParams = _snackbar.View.LayoutParameters as FrameLayout.LayoutParams;
            //    if (layoutParams != null)
            //    {
            //        if (config.Position == ToastPosition.Top)
            //        {
            //            layoutParams.Gravity = GravityFlags.Top;
            //            layoutParams.SetMargins(0, 80, 0, 0);
            //        }
            //        else if (config.Position == ToastPosition.Center)
            //        {
            //            layoutParams.Gravity = GravityFlags.Center;
            //        }
            //        else if (config.Position == ToastPosition.Bottom)
            //        {
            //            layoutParams.Gravity = GravityFlags.Bottom;
            //            layoutParams.SetMargins(0, 0, 0, 30);
            //        }

            //        _snackbar.View.LayoutParameters = layoutParams;
            //    }
            //}

            _snackbar.Show();
        }

        public static void Hide()
        {
            _snackbar?.Dismiss();
        }
    }
}