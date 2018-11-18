using Ricardo.RMBProgressHUD.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Passingwind.UserDialogs.Platforms
{
    public static class LoadingBuilder
    {
        static MBProgressHUD _loadingHub;
        static MBProgressHUD _progressHub;

        public static IDisposable Loading(UIApplication app, UIView view, LoadingConfig config)
        {
            app.SafeInvokeOnMainThread(() =>
            {
                _loadingHub = MBProgressHUD.ShowHUD(view, true);
                _loadingHub.Mode = MBProgressHUDMode.Indeterminate;
                _loadingHub.Label.Text = config.Text;

                _loadingHub.MinShowTime = 1; // The minimum time (in seconds) that the HUD is shown. 

                if (config.MarkType == MarkType.Black)
                {
                    _loadingHub.BackgroundView.Style = MBProgressHUDBackgroundStyle.SolidColor;
                    _loadingHub.BackgroundView.Color = UIColor.Black.ColorWithAlpha(0.5f);
                }

                if (config.Cancellable)
                {
                    _loadingHub.Button.SetTitle(config.CancelText, UIControlState.Normal);
                    _loadingHub.Button.AddTarget((_1, _2) =>
                    {
                        config.CancelAction?.Invoke();
                        _loadingHub.Hide(true);
                    }, UIControlEvent.TouchUpInside);
                }

                _loadingHub.Show(true);

                if (config.Duration.HasValue)
                    _loadingHub.Hide(true, config.Duration.Value.TotalSeconds);

                //_loadingHub.Hide(true, 3);  // seconds

            });

            return new DisposableAction(() => _loadingHub.Hide(true));
        }

        public static IProgressDialog Progress(UIApplication app, UIView view, ProgressConfig config)
        {
            app.SafeInvokeOnMainThread(() =>
            {
                _progressHub = MBProgressHUD.ShowHUD(view, true);
                //_progressHub.Mode = MBProgressHUDMode.DeterminateHorizontalBar;
                _progressHub.Mode = MBProgressHUDMode.Determinate;
                _progressHub.Label.Text = config.Text;

                _progressHub.MinShowTime = 1; // The minimum time (in seconds) that the HUD is shown. 

                if (config.MarkType == MarkType.Black)
                {
                    _progressHub.BackgroundView.Style = MBProgressHUDBackgroundStyle.SolidColor;
                    _progressHub.BackgroundView.Color = UIColor.Black.ColorWithAlpha(0.5f);

                }

                if (config.Cancellable)
                {
                    _progressHub.Button.SetTitle(config.CancelText, UIControlState.Normal);
                    _progressHub.Button.AddTarget((_1, _2) =>
                    {
                        config.CancelAction?.Invoke();
                        _progressHub.Hide(true);

                    }, UIControlEvent.TouchUpInside);
                }

                _progressHub.Show(true);

            });

            return new DefaultProgressDialog(app, config, _progressHub);
        }

        public class DefaultProgressDialog : IProgressDialog
        {
            ProgressConfig _config;
            UIApplication _app;
            MBProgressHUD _hub;

            public DefaultProgressDialog(UIApplication app, ProgressConfig config, MBProgressHUD hub)
            {
                _app = app;
                _config = config;
                _hub = hub;

            }

            public void Hide()
            {
                _app.SafeInvokeOnMainThread(() =>
                {
                    _hub.Hide(true);
                });
            }

            public void SetProgress(uint value)
            {
                _app.SafeInvokeOnMainThread(() =>
                {
                    _hub.Progress = (float)(value) / 100;  // 0-1
                });
            }
        }

        //public IDisposable Loading(UIApplication app, LoadingConfig config)
        //{
        //    app.SafeInvokeOnMainThread(() =>
        //    {
        //        SVProgressHUD.SetDefaultStyle(SVProgressHUDStyle.Dark);

        //        if (config.MarkType == MarkType.Black)
        //            SVProgressHUD.ShowWithStatus(config.Text, SVProgressHUDMaskType.Black);
        //        else
        //            SVProgressHUD.ShowWithStatus(config.Text, SVProgressHUDMaskType.Clear);

        //        if (config.Duration != null)
        //            SVProgressHUD.DismissWithDelay(config.Duration.Value.Milliseconds);


        //    });

        //    return new DisposableAction(() => app.SafeInvokeOnMainThread(SVProgressHUD.Dismiss));
        //}

        //public IProgressDialog Progress(UIApplication app, ProgressConfig config)
        //{
        //    app.SafeInvokeOnMainThread(() =>
        //    {
        //        SVProgressHUD.SetDefaultStyle(SVProgressHUDStyle.Dark);

        //        if (config.MarkType == MarkType.Black)
        //            SVProgressHUD.ShowProgress(config.Start, config.Text, SVProgressHUDMaskType.Black);
        //        else
        //            SVProgressHUD.ShowProgress(config.Start, config.Text, SVProgressHUDMaskType.Clear);

        //    });

        //    return new DefaultProgressDialog(app, config);
        //}

        //public class DefaultProgressDialog : IProgressDialog
        //{
        //    ProgressConfig _config;
        //    UIApplication _app;

        //    public DefaultProgressDialog(UIApplication app, ProgressConfig config)
        //    {
        //        _app = app;
        //        _config = config;
        //    }

        //    public void Hide()
        //    {
        //        _app.SafeInvokeOnMainThread(() =>
        //        {
        //            SVProgressHUD.Dismiss();
        //        });
        //    }

        //    public void SetProgress(int value)
        //    {
        //        _app.SafeInvokeOnMainThread(() =>
        //        {
        //            if (_config.MarkType == MarkType.Black)
        //                SVProgressHUD.ShowProgress(value, _config.Text, SVProgressHUDMaskType.Black);
        //            else
        //                SVProgressHUD.ShowProgress(value, _config.Text, SVProgressHUDMaskType.Clear);
        //        });
        //    }
        //}
    }
}
