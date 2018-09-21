using Android.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Text.Style;
using Android.Widget;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Passingwind.UserDialogs.Platforms;

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
            where TFragment : AbstractDialogFragment<TConfig>
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

        public override void ShowLoading(LoadingConfig config)
        {
            KProgressHUD
        }

        public override void HideLoading()
        {
            throw new NotImplementedException();
        }
    }
}
