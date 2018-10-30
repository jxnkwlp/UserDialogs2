using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Passingwind.UserDialogs
{
    public abstract class AbstractUserDialogs : IUserDialogs
    {
        //static void Cancel<TResult>(IDisposable disp, TaskCompletionSource<TResult> tcs)
        //{
        //    disp.Dispose();
        //    tcs.TrySetCanceled();
        //}



        public IDisposable Toast(string message)
        {
            return Toast(new ToastConfig() { Message = message });
        }

        public abstract IDisposable Toast(ToastConfig options);

        public IDisposable Snackbar(string message, Action action = null)
        {
            return Snackbar(new SnackbarConfig() { Message = message, Action = action });
        }

        public abstract IDisposable Snackbar(SnackbarConfig options);


        public IDisposable Alert(string message)
        {
            return Alert(new AlertConfig(message));
        }

        public abstract IDisposable Alert(AlertConfig config);


        public abstract IDisposable ActionSheet(ActionSheetConfig config);

        public abstract IDisposable Loading(LoadingConfig config);

        public abstract IProgressDialog Progress(ProgressConfig config);

    }
}
