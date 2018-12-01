using System;

namespace Passingwind.UserDialogs
{
    public interface IUserDialogs
    {
        #region Toast

        void Toast(string message);

        void Toast(ToastConfig config);

        #endregion Toast

        #region Snackbar

        IDisposable Snackbar(string message, Action action = null);

        IDisposable Snackbar(SnackbarConfig config);

        #endregion Snackbar

        #region Alert

        IDisposable Alert(string message);

        IDisposable Alert(AlertConfig config);

        //IDisposable Confirm(string message);
        //IDisposable Confirm(AlertOptions config);

        #endregion Alert

        #region ActionSheet

        IDisposable ActionSheet(ActionSheetConfig config);

        #endregion ActionSheet

        #region Loading

        IDisposable Loading(LoadingConfig config);

        IProgressDialog Progress(ProgressConfig config);

        #endregion Loading

        #region MyRegion

        // IDisposable DateTimerPicker();

        // IDisposable OptionsPicker();

        #endregion MyRegion
    }
}