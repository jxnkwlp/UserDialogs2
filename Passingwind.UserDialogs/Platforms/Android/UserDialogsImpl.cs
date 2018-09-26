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


        KProgressHUD.KProgressHUD _progressHUD1;

        public override IDisposable Loading(LoadingConfig config)
        {
            var activity = this.TopActivityFunc();

            if (_progressHUD1 != null && _progressHUD1.IsShowing)
            {
                _progressHUD1.Dismiss();
            }

            _progressHUD1 = KProgressHUD.KProgressHUD.Create(activity)
                  .SetLabel(config.Text);

            if (config.Duration != null)
            {
                // _kProgressHUD.SetAutoDismiss(true);
                // _kProgressHUD.SetGraceTime((int)config.Duration.Value.TotalSeconds);
            }

            _progressHUD1.Show();

            return new DisposableAction(() => _progressHUD1.Dismiss());
        }

        KProgressHUD.KProgressHUD _progressHUD2;

        public override IProgressDialog Progress(ProgressConfig config)
        {
            var activity = this.TopActivityFunc();

            if (_progressHUD2 != null && _progressHUD2.IsShowing)
            {
                _progressHUD2.Dismiss();
            }

            _progressHUD2 = KProgressHUD.KProgressHUD.Create(activity, KProgressHUD.KProgressHUD.Style.PieDeterminate)
                  .SetLabel(config.Text);

            _progressHUD2.SetMaxProgress(config.Max);
            _progressHUD2.SetProgress(config.Start);

            _progressHUD2.SetCancellable(config.Cancellable);

            if (config.Cancellable)
            {
                _progressHUD2.SetCancelAction(() =>
                {
                    _progressHUD2.Dismiss();

                    config.CancelAction?.Invoke();
                });
            }

            _progressHUD2.Show();

            return new LoadingDialog(_progressHUD2);
        }

        public class LoadingDialog : IProgressDialog, IDisposable
        {
            KProgressHUD.KProgressHUD _hud;

            public LoadingDialog(KProgressHUD.KProgressHUD hud)
            {
                _hud = hud;
            }

            public void Dispose()
            {
                _hud.Dismiss();
            }

            public void Hide()
            {
                _hud.Dismiss();
            }

            public void SetProgress(int value)
            {
                _hud.SetProgress(value);
            }
        }

        //public override void HideLoading()
        //{
        //    var activity = this.TopActivityFunc();

        //    if (_kProgressHUD != null)
        //    {
        //        _kProgressHUD.Dismiss();
        //    }

        //}

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
