using Foundation;
using Passingwind.UserDialogs.Platforms;
using Passingwind.UserDialogs.Platforms.Infrastructure;
using Passingwind.UserDialogs.TTGSnackBar;
using SVProgressHUDBinding;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Passingwind.UserDialogs
{
    public class UserDialogsImpl : IUserDialogs
    {
        readonly Func<UIViewController> _viewControllerFunc;

        public UserDialogsImpl() : this(() => UIApplication.SharedApplication.GetTopViewController())
        {
        }

        public UserDialogsImpl(Func<UIViewController> viewControllerFunc)
        {
            this._viewControllerFunc = viewControllerFunc;
        }


        public IDisposable ActionSheet(ActionSheetConfig config)
        {
            var controller = UIAlertController.Create(config.Title, config.Message, UIAlertControllerStyle.ActionSheet);

            if (config.Cancel != null)
                controller.AddAction(UIAlertAction.Create(config.Cancel.Text, UIAlertActionStyle.Cancel, (_) => config.Cancel.Action?.Invoke()));

            if (config.Destructive != null)
                controller.AddAction(UIAlertAction.Create(config.Destructive.Text, UIAlertActionStyle.Destructive, (_) => config.Destructive.Action?.Invoke()));

            if (config.Options != null && config.Options.Count > 0)
            {
                foreach (var item in config.Options)
                {
                    var actionItem = UIAlertAction.Create(item.Text, UIAlertActionStyle.Default, (_) => item.Action?.Invoke());

                    if (!string.IsNullOrEmpty(item.ItemIcon))
                    {
                        var icon = UIImage.FromBundle(item.ItemIcon);
                        actionItem.SetValueForKey(icon, new NSString("image"));
                    }

                    controller.AddAction(actionItem);
                }

            }

            return controller;

        }

        public IDisposable Alert(IAlertRequest alert)
        {
            throw new NotImplementedException();

        }

        public IDisposable Alert(AlertConfig config)
        {
            var controller = UIAlertController.Create(config.Title, config.Message, UIAlertControllerStyle.Alert);

            controller.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, (_) => config.OkAction?.Invoke()));

            return controller;
        }

        public IDisposable Confirm(ConfirmConfig config)
        {
            var controller = UIAlertController.Create(config.Title, config.Message, UIAlertControllerStyle.Alert);

            controller.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, (_) => config.Action?.Invoke(true)));
            controller.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Cancel, (_) => config.Action?.Invoke(false)));

            return controller;
        }

        public IDisposable Loading(LoadingConfig config)
        {
            var app = UIApplication.SharedApplication;
            app.SafeInvokeOnMainThread(() =>
            {
                if (config.MarkType == MarkType.Blank)
                    SVProgressHUD.ShowWithStatus(config.Text, SVProgressHUDMaskType.Black);
                else
                    SVProgressHUD.ShowWithStatus(config.Text, SVProgressHUDMaskType.Clear);

                if (config.Duration != null)
                    SVProgressHUD.DismissWithDelay(config.Duration.Value.Milliseconds);

            });

            return new DisposableAction(() => app.SafeInvokeOnMainThread(SVProgressHUD.Dismiss));
        }

        public IProgressDialog Progress(ProgressConfig config)
        {
            var app = UIApplication.SharedApplication;
            app.SafeInvokeOnMainThread(() =>
            {
                if (config.MarkType == MarkType.Blank)
                    SVProgressHUD.ShowProgress(config.Start, config.Text, SVProgressHUDMaskType.Black);
                else
                    SVProgressHUD.ShowProgress(config.Start, config.Text, SVProgressHUDMaskType.Clear);

            });

            return new DefaultProgressDialog(config);
        }

        public class DefaultProgressDialog : IProgressDialog
        {
            ProgressConfig _config;

            public DefaultProgressDialog(ProgressConfig config)
            {
                _config = config;
            }

            public void Hide()
            {
                var app = UIApplication.SharedApplication;
                app.SafeInvokeOnMainThread(() =>
                {
                    SVProgressHUD.Dismiss();
                });
            }

            public void SetProgress(int value)
            {
                var app = UIApplication.SharedApplication;

                app.SafeInvokeOnMainThread(() =>
                {
                    if (_config.MarkType == MarkType.Blank)
                        SVProgressHUD.ShowProgress(value, _config.Text, SVProgressHUDMaskType.Black);
                    else
                        SVProgressHUD.ShowProgress(value, _config.Text, SVProgressHUDMaskType.Clear);
                });
            }
        }

        public IDisposable Toast(ToastConfig toastConfig)
        {
            if (toastConfig.Style == ToastStyle.Default)
            {
                return DefaultToast(toastConfig);
            }
            else if (toastConfig.Style == ToastStyle.Snackbar)
            {
                return SnackBar(toastConfig);
            }

            throw new NotSupportedException();
        }


        private IDisposable DefaultToast(ToastConfig toastConfig)
        {
            var app = UIApplication.SharedApplication;
            app.SafeInvokeOnMainThread(() =>
            {
                SVProgressHUD.ShowInfoWithStatus(toastConfig.Message, SVProgressHUDMaskType.Clear);
                SVProgressHUD.DismissWithDelay(toastConfig.Duration.Milliseconds);
            });

            return new DisposableAction(() => app.SafeInvokeOnMainThread(SVProgressHUD.Dismiss));
        }


        TTGSnackbar _snackbar;

        public IDisposable SnackBar(ToastConfig toastConfig)
        {
            _snackbar?.Dismiss();

            var app = UIApplication.SharedApplication;
            app.SafeInvokeOnMainThread(() =>
            {
                _snackbar = new TTGSnackbar(toastConfig.Message);
                _snackbar.Duration = toastConfig.Duration;
                _snackbar.AnimationType = TTGSnackbarAnimationType.FadeInFadeOut;

                if (toastConfig.Position == ToastPosition.Top)
                    _snackbar.LocationType = TTGSnackbarLocation.Top;
                else if (toastConfig.Position == ToastPosition.Bottom)
                    _snackbar.LocationType = TTGSnackbarLocation.Bottom;

                _snackbar.Show();
            });

            return new DisposableAction(() => app.SafeInvokeOnMainThread(() => _snackbar?.Dismiss()));
        }

    }
}
