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
            return Toast(new ToastOptions() { Message = message });
        }

        public abstract IDisposable Toast(ToastOptions options);

        public IDisposable Snackbar(string message, Action action = null)
        {
            return Snackbar(new SnackbarOptions() { Message = message, Action = action });
        }

        public abstract IDisposable Snackbar(SnackbarOptions options);


        public IDisposable Alert(string message)
        {
            return Alert(new AlertOptions(message));
        }
        public abstract IDisposable Alert(AlertOptions config);


        public abstract IDisposable ActionSheet(ActionSheetOptions config);



    }
}
