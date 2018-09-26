using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Passingwind.UserDialogs
{
    public abstract class AbstractUserDialogs : IUserDialogs
    {
        #region Alert

        public abstract IDisposable Alert(AlertConfig config);

        #endregion
         
        #region Toast

        public abstract IDisposable Toast(ToastConfig toastConfig);

        #endregion



        //static void Cancel<TResult>(IDisposable disp, TaskCompletionSource<TResult> tcs)
        //{
        //    disp.Dispose();
        //    tcs.TrySetCanceled();
        //}
         
        public abstract IDisposable Confirm(ConfirmConfig config);
        public abstract IDisposable ActionSheet(ActionSheetConfig config);
        public abstract IDisposable Alert(IAlertRequest alert);
        public abstract IDisposable Loading(LoadingConfig config);
        public abstract IProgressDialog Progress(ProgressConfig config);
        public abstract IDisposable DateTimerPicker();
    }
}
