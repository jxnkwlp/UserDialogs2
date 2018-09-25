using Passingwind.UserDialogs.Platforms.Infrastructure;
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
            throw new NotImplementedException();

        }

        public IDisposable Alert(IAlertRequest alert)
        {
            throw new NotImplementedException();

        }

        public IDisposable Alert(AlertConfig config)
        {
            var controller = UIAlertController.Create(config.Title, config.Message, UIAlertControllerStyle.Alert);

            controller.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, (e) => config.OkAction?.Invoke()));

            return controller;
        }

        public IDisposable Confirm(ConfirmConfig config)
        {
            var controller = UIAlertController.Create(config.Title, config.Message, UIAlertControllerStyle.Alert);

            controller.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, (e) => config.Action?.Invoke(true)));
            controller.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Cancel, (e) => config.Action?.Invoke(false)));

            return controller;
        }

        public IDisposable Loading(LoadingConfig config)
        {
            throw new NotImplementedException();
        }

        public IProgressDialog Progress(ProgressConfig config)
        {
            throw new NotImplementedException();
        }

        public IDisposable Toast(ToastConfig toastConfig)
        {
            if (toastConfig.Style == ToastStyle.Default)
            {
                return DefaultToast(toastConfig);
            }


            throw new NotImplementedException();
        }


        private IDisposable DefaultToast(ToastConfig toastConfig)
        {
            SVProgressHUD.ShowInfoWithStatus(toastConfig.Message, SVProgressHUDMaskType.Clear);

            SVProgressHUD.DismissWithDelay(toastConfig.Duration.Milliseconds);

             

            return new DisposableAction(() =>
            {
                SVProgressHUD.Dismiss();
            });
        }

    }
}
