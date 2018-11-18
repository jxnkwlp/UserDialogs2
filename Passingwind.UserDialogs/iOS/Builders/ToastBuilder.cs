using Ricardo.RMBProgressHUD.iOS;
using SVProgressHUDBinding;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Passingwind.UserDialogs.Platforms
{
    public static class ToastBuilder
    {
        static MBProgressHUD _hub;

        public static IDisposable Build(UIApplication app, UIView view, ToastConfig config)
        {
            app.SafeInvokeOnMainThread(() =>
            {
                _hub = MBProgressHUD.ShowHUD(view, true);
                _hub.Mode = MBProgressHUDMode.Text;
                _hub.Label.Text = config.Message;

                _hub.MinShowTime = 1; // The minimum time (in seconds) that the HUD is shown. 

                if (config.TextColor.HasValue)
                {
                    _hub.Label.TextColor = config.TextColor.Value.ToNative();
                }

                if (config.BackgroundColor.HasValue)
                {
                    _hub.BackgroundColor = config.BackgroundColor.Value.ToNative();
                }

                _hub.Show(true);

                _hub.Hide(true, config.Duration.TotalSeconds);

            });

            return new DisposableAction(() => app.SafeInvokeOnMainThread(SVProgressHUD.Dismiss));
        }

        //public IDisposable Build(UIApplication app, ToastConfig config)
        //{
        //    app.SafeInvokeOnMainThread(() =>
        //    {
        //        SVProgressHUD.ShowInfoWithStatus(config.Message, SVProgressHUDMaskType.Clear);
        //        SVProgressHUD.DismissWithDelay(config.Duration.Milliseconds);
        //    });

        //    return new DisposableAction(() => app.SafeInvokeOnMainThread(SVProgressHUD.Dismiss));
        //}
    }
}
