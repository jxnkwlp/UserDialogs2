using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;

namespace Passingwind.UserDialogs.Platforms
{
    class ActionSheetAppCompatDialogFragment : AbstractAppCompatDialogFragment<ActionSheetOptions>
    {
        protected override void SetDialogDefaults(Dialog dialog)
        {
            base.SetDialogDefaults(dialog);

            var cancellable = this.Config.Cancel != null;
            dialog.SetCancelable(cancellable);
            dialog.SetCanceledOnTouchOutside(cancellable);

            dialog.CancelEvent += (s, a) => this.Config.Cancel?.Action?.Invoke();
        }

        protected override void OnKeyPress(object sender, DialogKeyEventArgs args)
        {
            base.OnKeyPress(sender, args);

            if (args.KeyCode != Android.Views.Keycode.Back)
                return;

            args.Handled = true;

            this.Dismiss();
        }

        protected override Dialog CreateDialog(ActionSheetOptions config)
        {
            return new ActionSheetBuilder().Build(this.AppCompatActivity, config);
        }
    }
}
