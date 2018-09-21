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


        //public async Task AlertAsync(AlertConfig config, CancellationToken? cancelToken = null)
        //{
        //    var tcs = new TaskCompletionSource<object>();

        //    //config.OkAction = () => tcs.TrySetResult(null);

        //    var disp = this.Alert(config);

        //    using (cancelToken?.Register(() => Cancel(disp, tcs)))
        //    {
        //        await tcs.Task;
        //    }
        //}

        public Task AlertAsync(AlertConfig config)
        {
            this.Alert(config);

            return Task.FromResult(1);
        }


        #endregion



        #region Toast

        public abstract IDisposable Toast(ToastConfig toastConfig);

        //public async Task ToastAsync(ToastConfig toastConfig, CancellationToken? cancelToken = null)
        //{
        //    var tcs = new TaskCompletionSource<object>();

        //    var disp = this.Toast(toastConfig);

        //    using (cancelToken?.Register(() => Cancel(disp, tcs)))
        //    {
        //        await tcs.Task;
        //    }

        //}

        public Task ToastAsync(ToastConfig toastConfig)
        {
            this.Toast(toastConfig);

            return Task.FromResult(1);
        }



        #endregion
         


        static void Cancel<TResult>(IDisposable disp, TaskCompletionSource<TResult> tcs)
        {
            disp.Dispose();
            tcs.TrySetCanceled();
        }

        public abstract void ShowLoading(LoadingConfig config);
        public abstract void HideLoading();
    }
}
