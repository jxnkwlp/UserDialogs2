using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Passingwind.UserDialogs
{
    public interface IUserDialogs
    {
        #region Toast

        Task ToastAsync(ToastConfig toastConfig);

        #endregion

        #region Alert

        Task AlertAsync(AlertConfig config);

        #endregion

        #region ActionSheet

        #endregion

        #region Confirm

        #endregion

        #region Loading

        void ShowLoading(LoadingConfig config);

        void HideLoading();


        #endregion


    }
}
