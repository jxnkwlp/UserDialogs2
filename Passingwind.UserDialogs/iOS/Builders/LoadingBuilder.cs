using Ricardo.RMBProgressHUD.iOS;
using System;
using UIKit;

namespace Passingwind.UserDialogs.Platforms
{
    public static class LoadingBuilder
    {
        static MBProgressHUD _loadingHub;

        public static IDisposable Loading(UIApplication app, UIView view, LoadingConfig config)
        {
            app.SafeInvokeOnMainThread(() =>
            {
                _loadingHub = MBProgressHUD.ShowHUD(view, true);
                _loadingHub.Mode = MBProgressHUDMode.Indeterminate;
                _loadingHub.Label.Text = config.Text;
                _loadingHub.Label.LineBreakMode = UILineBreakMode.WordWrap;
                _loadingHub.Label.Lines = int.MaxValue;
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