using Ricardo.RMBProgressHUD.iOS;
using UIKit;

namespace Passingwind.UserDialogs.Platforms
{
    public static class ProgressBuilder
    {
        static MBProgressHUD _progressHub;

        public static IProgressDialog Progress(UIApplication app, UIView view, ProgressConfig config)
        {
            app.SafeInvokeOnMainThread(() =>
            {
                _progressHub = MBProgressHUD.ShowHUD(view, true);
                //_progressHub.Mode = MBProgressHUDMode.DeterminateHorizontalBar;
                _progressHub.Mode = MBProgressHUDMode.Determinate;
                _progressHub.Label.Text = config.Text;
                _progressHub.Label.LineBreakMode = UILineBreakMode.WordWrap;
                _progressHub.Label.Lines = int.MaxValue;
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

                    if (_hub.Progress >= 1)
                    {
                        _hub.Hide(true);
                    }
                });
            }
        }
    }
}