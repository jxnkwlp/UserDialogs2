using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;

namespace Passingwind.UserDialogs.Platforms
{
    public class ConfirmAppCompatDialogFragment : AbstractAppCompatDialogFragment<ConfirmConfig>
    {
        protected override void OnKeyPress(object sender, DialogKeyEventArgs args)
        {
            base.OnKeyPress(sender, args);

            if (args.KeyCode == Android.Views.Keycode.Back)
            {
                args.Handled = true;

                this.Config?.CancelAction?.Invoke();

                this.Dismiss();
            }

        }

        protected override Dialog CreateDialog(ConfirmConfig config) => new ConfirmBuilder().Build(this.AppCompatActivity, config);
    }
}
