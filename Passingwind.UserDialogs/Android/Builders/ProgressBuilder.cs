﻿using Android.App;
using Android.OS;
using Java.Lang;
using KProgressHUDLib;
using System;

namespace Passingwind.UserDialogs.Platforms
{
	public class ProgressBuilder
	{
		private KProgressHUD _progress;

		private void DelayDismiss(int interval)
		{
			if (_progress == null)
				return;

			Handler handler = new Handler();
			handler.PostDelayed(new Runnable(() =>
			{
				_progress.Dismiss();
			}), interval);
		}

		public IDisposable Loading(Activity activity, LoadingConfig config)
		{
			if (_progress != null && _progress.IsShowing)
			{
				_progress.Dismiss();
			}

			_progress = KProgressHUD.Create(activity, KProgressHUD.Style.SpinIndeterminate).SetLabel(config.Text);

			_progress.SetCancellable(config.Cancellable);

			if (config.Cancellable)
			{
				_progress.SetCancelAction(() =>
				{
					_progress.Dismiss();

					config.CancelAction?.Invoke();
				});
			}

			if (config.MarkType == MarkType.Black)
			{
				_progress.SetDimAmount(0.5f);
			}

			_progress.Show();

			if (config.Duration != null)
			{
				DelayDismiss((int)config.Duration.Value.TotalMilliseconds);
			}

			return new DisposableAction(() => _progress.Dismiss());
		}

		public IProgressDialog Progress(Activity activity, ProgressConfig config)
		{
			if (_progress != null && _progress.IsShowing)
			{
				_progress.Dismiss();
			}

			_progress = KProgressHUD.Create(activity, KProgressHUD.Style.PieDeterminate).SetLabel(config.Text);

			_progress.SetMaxProgress(100);

			_progress.SetCancellable(config.Cancellable);

			if (config.Cancellable)
			{
				_progress.SetCancelAction(() =>
				{
					_progress.Dismiss();

					config.CancelAction?.Invoke();
				});
			}

			if (config.MarkType == MarkType.Black)
			{
				_progress.SetDimAmount(0.5f);
			}

			_progress.Show();

			return new LoadingDialog(_progress);
		}

		public void Hide()
		{
			_progress?.Dismiss();
		}

		public class LoadingDialog : IProgressDialog, IDisposable
		{
			private readonly KProgressHUD _progress;

			public LoadingDialog(KProgressHUD progress)
			{
				_progress = progress;
			}

			public void Dispose()
			{
				Hide();
			}

			public void Hide()
			{
				_progress.Dismiss();
			}

			public void SetProgress(uint value)
			{
				_progress.SetProgress((int)value);
			}
		}
	}
}