﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Passingwind.UserDialogs
{
    public interface IUserDialogs
    {
        #region Toast

        void Toast(string message);
        void Toast(ToastConfig config);

        #endregion

        #region Snackbar

        IDisposable Snackbar(string message, Action action = null);
        IDisposable Snackbar(SnackbarConfig config);

        #endregion

        #region Alert

        IDisposable Alert(string message);
        IDisposable Alert(AlertConfig config);

        //IDisposable Confirm(string message);
        //IDisposable Confirm(AlertOptions config);

        #endregion

        #region ActionSheet

        IDisposable ActionSheet(ActionSheetConfig config);

        #endregion

        #region Loading

        IDisposable Loading(LoadingConfig config);

        IProgressDialog Progress(ProgressConfig config);

        #endregion

        #region MyRegion

        // IDisposable DateTimerPicker();

        // IDisposable OptionsPicker();

        #endregion



    }
}
