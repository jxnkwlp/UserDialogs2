using SVProgressHUDBinding;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Passingwind.UserDialogs.Platforms
{
    public class LoadingBuilder
    {
        public IDisposable Loading(UIApplication app, LoadingConfig config)
        {
            app.SafeInvokeOnMainThread(() =>
            {
                if (config.MarkType == MarkType.Black)
                    SVProgressHUD.ShowWithStatus(config.Text, SVProgressHUDMaskType.Black);
                else
                    SVProgressHUD.ShowWithStatus(config.Text, SVProgressHUDMaskType.Clear);

                if (config.Duration != null)
                    SVProgressHUD.DismissWithDelay(config.Duration.Value.Milliseconds);


            });

            return new DisposableAction(() => app.SafeInvokeOnMainThread(SVProgressHUD.Dismiss));
        }

        public IProgressDialog Progress(UIApplication app, ProgressConfig config)
        {
            app.SafeInvokeOnMainThread(() =>
            {
                if (config.MarkType == MarkType.Black)
                    SVProgressHUD.ShowProgress(config.Start, config.Text, SVProgressHUDMaskType.Black);
                else
                    SVProgressHUD.ShowProgress(config.Start, config.Text, SVProgressHUDMaskType.Clear);

            });

            return new DefaultProgressDialog(app, config);
        }

        public class DefaultProgressDialog : IProgressDialog
        {
            ProgressConfig _config;
            UIApplication _app;

            public DefaultProgressDialog(UIApplication app, ProgressConfig config)
            {
                _app = app;
                _config = config;
            }

            public void Hide()
            {
                _app.SafeInvokeOnMainThread(() =>
                {
                    SVProgressHUD.Dismiss();
                });
            }

            public void SetProgress(int value)
            {
                _app.SafeInvokeOnMainThread(() =>
                {
                    if (_config.MarkType == MarkType.Black)
                        SVProgressHUD.ShowProgress(value, _config.Text, SVProgressHUDMaskType.Black);
                    else
                        SVProgressHUD.ShowProgress(value, _config.Text, SVProgressHUDMaskType.Clear);
                });
            }
        }
    }
}
