using Android.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Text.Style;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Passingwind.UserDialogs.Platforms;
using KProgressHUD;

namespace Passingwind.UserDialogs
{
    public class UserDialogsImpl : AbstractUserDialogs
    {
        public static string FragmentTag { get; set; } = "UserDialogs";

        protected internal Func<Activity> TopActivityFunc { get; set; }


        public UserDialogsImpl(Func<Activity> getTopActivity)
        {
            this.TopActivityFunc = getTopActivity;
        }




        public override IDisposable Alert(AlertConfig config)
        {
            var activity = this.TopActivityFunc();

            if (activity is AppCompatActivity compatActivity)
                return this.ShowDialog<AlertAppCompatDialogFragment, AlertConfig>(compatActivity, config);

            return this.Show(activity, () => new AlertBuilder().Build(activity, config));
        }


        public override IDisposable Confirm(ConfirmConfig config)
        {
            var activity = this.TopActivityFunc();

            if (activity is AppCompatActivity compatActivity)
                return this.ShowDialog<ConfirmAppCompatDialogFragment, ConfirmConfig>(compatActivity, config);

            return this.Show(activity, () => new ConfirmBuilder().Build(activity, config));
        }


        public override IDisposable Toast(ToastConfig toastConfig)
        {
            var activity = this.TopActivityFunc();

            if (toastConfig.Style == ToastStyle.Snackbar)
            {
                ToastBuilder.ShowSnackbar(activity, toastConfig);
            }
            else
            {
                ToastBuilder.Show(activity, toastConfig);
            }

            return new DisposableAction(() =>
            {
                ToastBuilder.Hide();
            });
        }





        protected virtual IDisposable Show(Activity activity, Func<Dialog> dialogBuilder)
        {
            Dialog dialog = null;
            activity.SafeRunOnUi(() =>
            {
                dialog = dialogBuilder();
                dialog.Show();
            });
            return new DisposableAction(() =>
                activity.RunOnUiThread(dialog.Dismiss)
            );
        }


        protected virtual IDisposable ShowDialog<TFragment, TConfig>(AppCompatActivity activity, TConfig config)
            where TFragment : AbstractAppCompatDialogFragment<TConfig>
            where TConfig : class, new()
        {
            var frag = (TFragment)Activator.CreateInstance(typeof(TFragment));

            activity.SafeRunOnUi(() =>
            {
                frag.Config = config;
                frag.Show(activity.SupportFragmentManager, FragmentTag);
            });

            return new DisposableAction(() =>
                activity.SafeRunOnUi(frag.Dismiss)
            );
        }


        KProgressHUD.KProgressHUD _kProgressHUD;

        public override void ShowLoading(LoadingConfig config)
        {
            var activity = this.TopActivityFunc();

            if (_kProgressHUD != null && _kProgressHUD.IsShowing)
            {
                _kProgressHUD.Dismiss();
            }

            _kProgressHUD = KProgressHUD.KProgressHUD.Create(activity)
                  .SetLabel(config.Text);

            if (config.Duration != null)
            {
                // _kProgressHUD.SetAutoDismiss(true);
                // _kProgressHUD.SetGraceTime((int)config.Duration.Value.TotalSeconds);
            }

            _kProgressHUD.Show();
        }

        public override void HideLoading()
        {
            var activity = this.TopActivityFunc();

            if (_kProgressHUD != null)
            {
                _kProgressHUD.Dismiss();
            }

        }

        public override IDisposable ActionSheet(ActionSheetConfig config)
        {
            var activity = this.TopActivityFunc();

            if (activity is AppCompatActivity compatActivity)
            {
                if (config.BottomSheet)
                {
                    return this.ShowDialog<BottomActionSheetDialogFragment, ActionSheetConfig>(compatActivity, config);
                }
                return this.ShowDialog<ActionSheetAppCompatDialogFragment, ActionSheetConfig>(compatActivity, config);
            }

            return this.Show(activity, () => new ActionSheetBuilder().Build(activity, config));
        }

        public override IDisposable Alert(IAlertRequest alert)
        {
            return null;
        }
    }
}
