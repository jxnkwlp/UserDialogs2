using Android.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs.Platforms
{
    public class LoadingBuilder
    {

        //KProgressHUD.KProgressHUD _progressHUD1;

        //public IDisposable Loading(Activity activity, LoadingConfig config)
        //{
        //    if (_progressHUD1 != null && _progressHUD1.IsShowing)
        //    {
        //        _progressHUD1.Dismiss();
        //    }

        //    _progressHUD1 = KProgressHUD.KProgressHUD.Create(activity)
        //          .SetLabel(config.Text);

        //    if (config.Duration != null)
        //    {
        //        // _kProgressHUD.SetAutoDismiss(true);
        //        // _kProgressHUD.SetGraceTime((int)config.Duration.Value.TotalSeconds);
        //    }

        //    _progressHUD1.Show();

        //    return new DisposableAction(() => _progressHUD1.Dismiss());
        //}

        //KProgressHUD.KProgressHUD _progressHUD2;

        //public override IProgressDialog Progress(ProgressConfig config)
        //{
        //    var activity = this.TopActivityFunc();

        //    if (_progressHUD2 != null && _progressHUD2.IsShowing)
        //    {
        //        _progressHUD2.Dismiss();
        //    }

        //    _progressHUD2 = KProgressHUD.KProgressHUD.Create(activity, KProgressHUD.KProgressHUD.Style.PieDeterminate)
        //          .SetLabel(config.Text);

        //    _progressHUD2.SetMaxProgress(config.Max);
        //    _progressHUD2.SetProgress(config.Start);

        //    _progressHUD2.SetCancellable(config.Cancellable);

        //    if (config.Cancellable)
        //    {
        //        _progressHUD2.SetCancelAction(() =>
        //        {
        //            _progressHUD2.Dismiss();

        //            config.CancelAction?.Invoke();
        //        });
        //    }

        //    _progressHUD2.Show();

        //    return new LoadingDialog(_progressHUD2);
        //}

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


    }
}
