using System;
using Android.App;
using Android.Support.V7.App;
using AlertDialog = Android.App.AlertDialog;
using AppCompatAlertDialog = Android.Support.V7.App.AlertDialog;

namespace Passingwind.UserDialogs.Platforms
{
    class AlertBuilder
    {
        public Dialog Build(Activity activity, AlertConfig config)
        {
            var builder = new AlertDialog.Builder(activity, config.AndroidStyleId ?? 0)
                .SetCancelable(false)
                .SetMessage(config.Message)
                .SetTitle(config.Title);

            if (config.OkButton != null)
                builder.SetPositiveButton(config.OkButton.Text, (o, e) => config.OkButton.Action?.Invoke());

            if (config.CancelButton != null)
                builder.SetNegativeButton(config.CancelButton.Text, (o, e) => config.CancelButton.Action?.Invoke());

            if (config.DestructiveButton != null)
                builder.SetNeutralButton(config.DestructiveButton.Text, (o, e) => config.DestructiveButton.Action?.Invoke());

            return builder.Create();
        }

        public Dialog Build(AppCompatActivity activity, AlertConfig config)
        {
            var builder = new AppCompatAlertDialog.Builder(activity, config.AndroidStyleId ?? 0)
                .SetCancelable(false)
                .SetMessage(config.Message)
                .SetTitle(config.Title);

            if (config.OkButton != null)
                builder.SetPositiveButton(config.OkButton.Text, (o, e) => config.OkButton.Action?.Invoke());

            if (config.CancelButton != null)
                builder.SetNegativeButton(config.CancelButton.Text, (o, e) => config.CancelButton.Action?.Invoke());

            if (config.DestructiveButton != null)
                builder.SetNeutralButton(config.DestructiveButton.Text, (o, e) => config.DestructiveButton.Action?.Invoke());

            return builder.Create();
        }
    }
}