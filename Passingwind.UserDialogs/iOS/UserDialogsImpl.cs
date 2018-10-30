using Passingwind.UserDialogs.Platforms;
using Passingwind.UserDialogs.Platforms.Infrastructure;
using System;
using UIKit;

namespace Passingwind.UserDialogs
{
    public class UserDialogsImpl : AbstractUserDialogs
    {
        readonly Func<UIViewController> _viewControllerFunc;

        public UserDialogsImpl() : this(() => UIApplication.SharedApplication.GetTopViewController())
        {
        }

        public UserDialogsImpl(Func<UIViewController> viewControllerFunc)
        {
            this._viewControllerFunc = viewControllerFunc;
        }



        public override IDisposable Alert(AlertConfig config)
        {
            return AlertBuilder.AlertCreate(config);
        }

        public override IDisposable Toast(ToastConfig options)
        {
            var app = UIApplication.SharedApplication;
            return new ToastBuilder().Build(app, options);
        }

        public override IDisposable Snackbar(SnackbarConfig config)
        {
            var app = UIApplication.SharedApplication;
            return new SnackbarBuilder().Build(app, config);
        }

        public override IDisposable ActionSheet(ActionSheetOptions config)
        {
            return AlertBuilder.ActionSheetCreate(config);
        }

        public override IDisposable Loading(LoadingConfig config)
        {
            var app = UIApplication.SharedApplication;
            return new LoadingBuilder().Loading(app, config);
        }

        public override IProgressDialog Progress(ProgressConfig config)
        {
            var app = UIApplication.SharedApplication;
            return new LoadingBuilder().Progress(app, config);
        }





    }
}
