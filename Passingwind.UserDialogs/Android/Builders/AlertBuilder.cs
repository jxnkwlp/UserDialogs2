using System;
using Android.App;
using Android.Support.V7.App;
using AlertDialog = Android.App.AlertDialog;
using AppCompatAlertDialog = Android.Support.V7.App.AlertDialog;

namespace Passingwind.UserDialogs.Platforms
{
    class AlertBuilder
    {
        public Dialog Build(Activity activity, AlertOptions config)
        {
            var builder = new AlertDialog.Builder(activity, config.AndroidStyleId ?? 0)
                .SetCancelable(false)
                .SetMessage(config.Message)
                .SetTitle(config.Title);

            if (config.Buttons.Count > 0)
                builder.SetPositiveButton(config.Buttons[0].Text, (o, e) => config.Buttons[0].Action?.Invoke());

            if (config.Buttons.Count > 1)
                builder.SetNegativeButton(config.Buttons[1].Text, (o, e) => config.Buttons[1].Action?.Invoke());

            if (config.Buttons.Count > 2)
                builder.SetNeutralButton(config.Buttons[2].Text, (o, e) => config.Buttons[2].Action?.Invoke());

            return builder.Create();
        }

        public Dialog Build(AppCompatActivity activity, AlertOptions config)
        {
            var builder = new AppCompatAlertDialog.Builder(activity, config.AndroidStyleId ?? 0)
                .SetCancelable(false)
                .SetMessage(config.Message)
                .SetTitle(config.Title);

            if (config.Buttons.Count > 0)
                builder.SetPositiveButton(config.Buttons[0].Text, (o, e) => config.Buttons[0].Action?.Invoke());

            if (config.Buttons.Count > 1)
                builder.SetNegativeButton(config.Buttons[1].Text, (o, e) => config.Buttons[1].Action?.Invoke());

            if (config.Buttons.Count > 2)
                builder.SetNeutralButton(config.Buttons[2].Text, (o, e) => config.Buttons[2].Action?.Invoke());

            return builder.Create();
        }

    }
}
