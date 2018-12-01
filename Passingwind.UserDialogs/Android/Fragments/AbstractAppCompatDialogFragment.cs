using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;

namespace Passingwind.UserDialogs.Platforms
{
    public abstract class AbstractAppCompatDialogFragment<T> : AppCompatDialogFragment where T : class
    {
        public T Config { get; set; }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            ConfigStore.Instance.Store(outState, this.Config);
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            Dialog dialog = null;
            if (this.Config == null && !ConfigStore.Instance.Contains(savedInstanceState))
            {
                this.ShowsDialog = false;
                this.Dismiss();
            }
            else
            {
                this.Config = this.Config ?? ConfigStore.Instance.Pop<T>(savedInstanceState);
                dialog = this.CreateDialog(this.Config);
                this.SetDialogDefaults(dialog);
            }
            return dialog;
        }

        protected virtual void SetDialogDefaults(Dialog dialog)
        {
            // dialog.Window.SetSoftInputMode(SoftInput.StateHidden);

            dialog.SetCancelable(false);
            dialog.SetCanceledOnTouchOutside(false);
            dialog.KeyPress += this.OnKeyPress;
            // TODO: fix for immersive mode - http://stackoverflow.com/questions/22794049/how-to-maintain-the-immersive-mode-in-dialogs/23207365#23207365
            //dialog.getWindow().setFlags(WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE, WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE);
        }

        public override void OnDetach()
        {
            base.OnDetach();
            if (this.Dialog != null)
                this.Dialog.KeyPress -= this.OnKeyPress;
        }

        protected virtual void OnKeyPress(object sender, DialogKeyEventArgs args)
        {
        }

        protected abstract Dialog CreateDialog(T config);

        protected AppCompatActivity AppCompatActivity => this.Activity as AppCompatActivity;
    }
}