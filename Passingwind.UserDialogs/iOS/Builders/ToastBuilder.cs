using SVProgressHUDBinding;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Passingwind.UserDialogs.Platforms
{
    public class ToastBuilder
    {
        public IDisposable Build(UIApplication app, ToastConfig config)
        {
            app.SafeInvokeOnMainThread(() =>
            {
                SVProgressHUD.ShowInfoWithStatus(config.Message, SVProgressHUDMaskType.Clear);
                SVProgressHUD.DismissWithDelay(config.Duration.Milliseconds);
            });

            return new DisposableAction(() => app.SafeInvokeOnMainThread(SVProgressHUD.Dismiss));
        }
    }
}
