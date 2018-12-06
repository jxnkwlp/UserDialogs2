using Ricardo.RMBProgressHUD.iOS;
using UIKit;

namespace Passingwind.UserDialogs.Platforms
{
    public static class ToastBuilder
    {
        static MBProgressHUD _hub;

        public static void Build(UIApplication app, UIView view, ToastConfig config)
        {
            app.SafeInvokeOnMainThread(() =>
            {
                _hub = MBProgressHUD.ShowHUD(view, true);
                _hub.Mode = MBProgressHUDMode.Text;
                _hub.Label.Text = config.Message;
                _hub.Label.LineBreakMode = UILineBreakMode.WordWrap;
                _hub.Label.Lines = int.MaxValue;
                _hub.MinShowTime = 1; // The minimum time (in seconds) that the HUD is shown.

                _hub.UserInteractionEnabled = false;

                if (config.TextColor.HasValue)
                {
                    _hub.Label.TextColor = config.TextColor.Value.ToNative();
                }

                if (config.BackgroundColor.HasValue)
                {
                    _hub.BezelView.BackgroundColor = config.BackgroundColor.Value.ToNative();
                }

                if (config.Position == ToastPosition.Top)
                {
                    _hub.Offset = new CoreGraphics.CGPoint(0, 0);
                }
                else if (config.Position == ToastPosition.Bottom || config.Position == ToastPosition.Default)
                {
                    _hub.Offset = new CoreGraphics.CGPoint(0, 10000);
                }

                _hub.Show(true);

                _hub.Hide(true, config.Duration.TotalSeconds);
            });
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