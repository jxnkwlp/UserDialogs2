using System;
using Android.App;
using Android.Support.V7.App;
using AlertDialog = Android.App.AlertDialog;
using AppCompatAlertDialog = Android.Support.V7.App.AlertDialog;

namespace Passingwind.UserDialogs.Platforms
{
    public class ConfirmBuilder
    {
        public Dialog Build(Activity activity, ConfirmConfig config)
        {
            return new AlertDialog.Builder(activity, config.AndroidStyleId ?? 0)
                .SetCancelable(false)
                .SetMessage(config.Message)
                .SetTitle(config.Title)
                .SetPositiveButton(config.OkText, (o, e) => config.Action?.Invoke(true))
                .SetNegativeButton(config.CancelText, (o, e) => config.Action?.Invoke(false))
                .Create();
        }

        public Dialog Build(AppCompatActivity activity, ConfirmConfig config)
        {
            return new AppCompatAlertDialog.Builder(activity, config.AndroidStyleId ?? 0)
                .SetCancelable(false)
                .SetMessage(config.Message)
                .SetTitle(config.Title)
                .SetPositiveButton(config.OkText, (o, e) => config.Action?.Invoke(true))
                .SetNegativeButton(config.CancelText, (o, e) => config.Action?.Invoke(false))
                .Create();
        }
    }
}