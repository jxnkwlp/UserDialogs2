using System;
using Android.App;
using Android.Content;
using Android.Views;

namespace Passingwind.UserDialogs.Platforms
{
    public class AlertAppCompatDialogFragment : AbstractAppCompatDialogFragment<AlertOptions>
    {
        protected override void OnKeyPress(object sender, DialogKeyEventArgs args)
        {
            base.OnKeyPress(sender, args);

            if (args.KeyCode != Keycode.Back)
                return;

            args.Handled = true;

            this.Dismiss();
        }


        protected override Dialog CreateDialog(AlertOptions config) => new AlertBuilder().Build(this.AppCompatActivity, config);
    }
}
