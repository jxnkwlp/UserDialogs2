using Android.App;
using Android.Support.V7.App;
using Passingwind.UserDialogs.Platforms;
using System;

namespace Passingwind.UserDialogs
{
	/// <summary> 
	/// Impl 
	/// </summary>
	public class UserDialogsImpl : AbstractUserDialogs
	{
		public static string FragmentTag { get; set; } = "UserDialogs";

		protected internal Func<Activity> TopActivityFunc { get; set; }

		private ProgressBuilder _loadingBuilder;

		public UserDialogsImpl(Func<Activity> getTopActivity)
		{
			TopActivityFunc = getTopActivity;
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
			var activity = TopActivityFunc();
			activity.SafeRunOnUi(() =>
			{
				ToastBuilder.Show(activity, options);
			});
		}

		public override IDisposable Snackbar(SnackbarConfig config)
		{
			var activity = TopActivityFunc();

			SnackbarBuilder.ShowSnackbar(activity, config);

			return new DisposableAction(() =>
			{
				SnackbarBuilder.Hide();
			});
		}

		public override IDisposable Alert(AlertConfig config)
		{
			var activity = TopActivityFunc();

			if (activity is AppCompatActivity compatActivity)
				return ShowDialog<AlertAppCompatDialogFragment, AlertConfig>(compatActivity, config);

			return Show(activity, () => new AlertBuilder().Build(activity, config));
		}

		public override IDisposable ActionSheet(ActionSheetConfig config)
		{
			var activity = TopActivityFunc();

			//if (config.Theme.HasValue && config.Theme == ActionSheetTheme.Theme1)
			//{
			//    return this.Show(activity, () => new ActionSheetTheme1Builder().Build(activity, config));
			//}

			if (activity is AppCompatActivity compatActivity)
			{
				if (config.BottomSheet)
				{
					return ShowDialog<BottomActionSheetDialogFragment, ActionSheetConfig>(compatActivity, config);
				}
				else
				{
					return ShowDialog<ActionSheetAppCompatDialogFragment, ActionSheetConfig>(compatActivity, config);
				}
			}
			else
			{
				if (config.BottomSheet)
				{
					return Show(activity, () => new BottomActionSheetBuilder().Build(activity, config));
				}
				else
				{
					return Show(activity, () => new ActionSheetBuilder().Build(activity, config));
				}
			}
		}


		public override IDisposable Loading(LoadingConfig config)
		{
			var activity = TopActivityFunc();

			_loadingBuilder = new ProgressBuilder();
			return _loadingBuilder.Loading(activity, config);
		}

		public override void HideLoading()
		{
			_loadingBuilder?.Hide();
		}

		public override IProgressDialog Progress(ProgressConfig config)
		{
			var activity = TopActivityFunc();

			return new ProgressBuilder().Progress(activity, config);
		}

		public override void Prompt(PromptConfig config)
		{
			var activity = TopActivityFunc();

			if (activity is AppCompatActivity compatActivity)
			{
				ShowDialog<PromptAppCompatDialogFragment, PromptConfig>(compatActivity, config);
			}
			else
			{
				Show(activity, () => new PromptBuilder().Build(activity, config));
			}
		}

		public override void Form(PromptFormConfig config)
		{
			var activity = TopActivityFunc();

			if (activity is AppCompatActivity compatActivity)
			{
				ShowDialog<PromptFormAppCompatDialogFragment, PromptFormConfig>(compatActivity, config);
			}
			else
			{
				Show(activity, () => new PromptBuilder().BuildForm(activity, config));
			}
		}

	}
}