using Android.App;
using Android.Support.V7.App;
using Passingwind.UserDialogs.Platforms;
using System;

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

        //public override IDisposable Alert(AlertConfig config)
        //{
        //    var activity = this.TopActivityFunc();

        //    if (activity is AppCompatActivity compatActivity)
        //        return this.ShowDialog<AlertAppCompatDialogFragment, AlertConfig>(compatActivity, config);

        //    return this.Show(activity, () => new AlertBuilder().Build(activity, config));
        //}

        //public override IDisposable Confirm(ConfirmConfig config)
        //{
        //    var activity = this.TopActivityFunc();

        //    if (activity is AppCompatActivity compatActivity)
        //        return this.ShowDialog<ConfirmAppCompatDialogFragment, ConfirmConfig>(compatActivity, config);

        //    return this.Show(activity, () => new ConfirmBuilder().Build(activity, config));
        //}

        public override void Toast(ToastConfig options)
        {
            var activity = this.TopActivityFunc();

            ToastBuilder.Show(activity, options);
        }

        public override IDisposable Snackbar(SnackbarConfig config)
        {
            var activity = this.TopActivityFunc();

            SnackbarBuilder.ShowSnackbar(activity, config);

            return new DisposableAction(() =>
            {
                SnackbarBuilder.Hide();
            });
        }

        public override IDisposable Alert(AlertConfig config)
        {
            var activity = this.TopActivityFunc();

            if (activity is AppCompatActivity compatActivity)
                return this.ShowDialog<AlertAppCompatDialogFragment, AlertConfig>(compatActivity, config);

            return this.Show(activity, () => new AlertBuilder().Build(activity, config));
        }

        public override IDisposable ActionSheet(ActionSheetConfig config)
        {
            var activity = this.TopActivityFunc();

            //if (config.Theme.HasValue && config.Theme == ActionSheetTheme.Theme1)
            //{
            //    return this.Show(activity, () => new ActionSheetTheme1Builder().Build(activity, config));
            //}

            if (activity is AppCompatActivity compatActivity)
            {
                if (config.BottomSheet)
                {
                    return this.ShowDialog<BottomActionSheetDialogFragment, ActionSheetConfig>(compatActivity, config);
                }
                else
                {
                    return this.ShowDialog<ActionSheetAppCompatDialogFragment, ActionSheetConfig>(compatActivity, config);
                }
            }
            else
            {
                if (config.BottomSheet)
                {
                    return this.Show(activity, () => new BottomActionSheetBuilder().Build(activity, config));
                }
                else
                {
                    return this.Show(activity, () => new ActionSheetBuilder().Build(activity, config));
                }
            }
        }

        public override IDisposable Loading(LoadingConfig config)
        {
            var activity = this.TopActivityFunc();

            return new ProgressBuilder().Loading(activity, config);
        }

        public override IProgressDialog Progress(ProgressConfig config)
        {
            var activity = this.TopActivityFunc();

            return new ProgressBuilder().Progress(activity, config);
        }

        public override void Prompt(PromptConfig config)
        {
            var activity = this.TopActivityFunc();

            if (activity is AppCompatActivity compatActivity)
            {
                this.ShowDialog<PromptAppCompatDialogFragment, PromptConfig>(compatActivity, config);
            }
            else
            {
                this.Show(activity, () => new PromptBuilder().Build(activity, config));
            }
        }

        public override void Form(PromptFormConfig config)
        {
            var activity = this.TopActivityFunc();

            if (activity is AppCompatActivity compatActivity)
            {
                this.ShowDialog<PromptFormAppCompatDialogFragment, PromptFormConfig>(compatActivity, config);
            }
            else
            {
                this.Show(activity, () => new PromptBuilder().BuildForm(activity, config));
            }
        }

    }
}