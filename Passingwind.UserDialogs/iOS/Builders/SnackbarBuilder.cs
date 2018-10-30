using Passingwind.UserDialogs.TTGSnackBar;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Passingwind.UserDialogs.Platforms
{
    public class SnackbarBuilder
    {
        TTGSnackbar _snackbar;

        public IDisposable Build(UIApplication app, SnackbarConfig config)
        {
            app.SafeInvokeOnMainThread(() =>
            {
                _snackbar?.Dismiss();

                _snackbar = new TTGSnackbar(config.Message);
                _snackbar.Duration = config.Duration;
                _snackbar.AnimationType = TTGSnackbarAnimationType.FadeInFadeOut;

                if (config.ActionText != null && config.Action != null)
                {
                    _snackbar.ActionText = config.ActionText;
                    _snackbar.ActionBlock = (_) => config.Action?.Invoke();

                    if (config.ActionTextColor != null)
                        _snackbar.ActionTextColor = config.ActionTextColor.Value.ToNative();

                }

                if (config.TextColor != null)
                    _snackbar.MessageTextColor = config.TextColor.Value.ToNative();

                if (config.BackgroundColor != null)
                    _snackbar.BackgroundColor = config.BackgroundColor.Value.ToNative();

                _snackbar.Show();

            });

            return new DisposableAction(() => app.SafeInvokeOnMainThread(() => _snackbar?.Dismiss()));
        }
    }
}
