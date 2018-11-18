using CoreGraphics;
using Passingwind.UserDialogs.Platforms;
using Passingwind.UserDialogs.Platforms.Infrastructure;
using System;
using UIKit;

namespace Passingwind.UserDialogs
{
    public class UserDialogsImpl : AbstractUserDialogs
    {
        protected Func<UIViewController> ViewControllerFunc { get; set; }

        public UserDialogsImpl() : this(() => UIApplication.SharedApplication.GetTopViewController())
        {
        }

        public UserDialogsImpl(Func<UIViewController> viewControllerFunc)
        {
            this.ViewControllerFunc = viewControllerFunc;
        }



        public override IDisposable Alert(AlertConfig config)
        {
            return this.Present(AlertBuilder.AlertBuild(config));
        }

        public override IDisposable Toast(ToastConfig config)
        {
            var app = UIApplication.SharedApplication;
            var top = this.ViewControllerFunc();
            return ToastBuilder.Build(app, top.View, config);
        }

        public override IDisposable Snackbar(SnackbarConfig config)
        {
            var app = UIApplication.SharedApplication;
            return new SnackbarBuilder().Build(app, config);
        }

        public override IDisposable ActionSheet(ActionSheetConfig config)
        {
            return this.Present(AlertBuilder.ActionSheetBuild(config));
        }

        public override IDisposable Loading(LoadingConfig config)
        {
            var app = UIApplication.SharedApplication;
            var top = this.ViewControllerFunc();
            return LoadingBuilder.Loading(app, top.View, config);
        }

        public override IProgressDialog Progress(ProgressConfig config)
        {
            var app = UIApplication.SharedApplication;
            var top = this.ViewControllerFunc();
            return LoadingBuilder.Progress(app, top.View, config);
        }



        protected virtual IDisposable Present(Func<UIAlertController> alertFunc)
        {
            UIAlertController alert = null;
            var app = UIApplication.SharedApplication;
            app.SafeInvokeOnMainThread(() =>
            {
                alert = alertFunc();
                var top = this.ViewControllerFunc();
                if (alert.PreferredStyle == UIAlertControllerStyle.ActionSheet && UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
                {
                    var x = top.View.Bounds.Width / 2;
                    var y = top.View.Bounds.Bottom;
                    var rect = new CGRect(x, y, 0, 0);

                    alert.PopoverPresentationController.SourceView = top.View;
                    alert.PopoverPresentationController.SourceRect = rect;
                    alert.PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Unknown;
                }
                top.PresentViewController(alert, true, null);
            });
            return new DisposableAction(() => app.SafeInvokeOnMainThread(() => alert.DismissViewController(true, null)));
        }


        protected virtual IDisposable Present(UIViewController controller)
        {
            var app = UIApplication.SharedApplication;
            var top = this.ViewControllerFunc();

            app.SafeInvokeOnMainThread(() => top.PresentViewController(controller, true, null));
            return new DisposableAction(() => app.SafeInvokeOnMainThread(() => controller.DismissViewController(true, null)));
        }

    }
}
