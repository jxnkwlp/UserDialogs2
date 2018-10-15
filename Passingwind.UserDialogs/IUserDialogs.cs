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

        IDisposable Toast(ToastConfig toastConfig);

        #endregion

        #region Alert

        //IDisposable Alert(IAlertRequest alert);

        IDisposable Alert(AlertConfig config);

        #endregion

        #region Confirm

        IDisposable Confirm(ConfirmConfig config);

        #endregion

        #region ActionSheet

        IDisposable ActionSheet(ActionSheetConfig config);

        #endregion

        #region Loading

        IDisposable Loading(LoadingConfig config);

        IProgressDialog Progress(ProgressConfig config);

        #endregion

        #region Show


        #endregion

        #region Pickerview

        // IDisposable DateTimerPicker();

        // IDisposable OptionsPicker();

        #endregion
    }
}
