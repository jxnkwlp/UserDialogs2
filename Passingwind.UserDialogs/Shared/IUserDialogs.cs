using System;

namespace Passingwind.UserDialogs
{
    public interface IUserDialogs
    {
        #region Toast

        /// <summary>
        ///  Toast with message
        /// </summary> 
        void Toast(string message);

        /// <summary>
        ///  Toast with config
        /// </summary> 
        void Toast(ToastConfig config);

        #endregion Toast

        #region Snackbar

        /// <summary>
        ///  Snackbar with message 
        /// </summary> 
        IDisposable Snackbar(string message, Action action = null);

        /// <summary>
        ///  Snackbar with config 
        /// </summary> 
        IDisposable Snackbar(SnackbarConfig config);

        #endregion Snackbar

        #region Alert

        /// <summary>
        ///  Alert with message 
        /// </summary> 
        IDisposable Alert(string message);

        /// <summary>
        ///  Alert with config 
        /// </summary> 
        IDisposable Alert(AlertConfig config);

        //IDisposable Confirm(string message);
        //IDisposable Confirm(AlertOptions config);

        #endregion Alert

        #region ActionSheet

        /// <summary>
        ///  ActionSheet with config 
        /// </summary> 
        IDisposable ActionSheet(ActionSheetConfig config);

        #endregion ActionSheet

        #region Loading

        /// <summary>
        ///  Loading with config 
        /// </summary> 
        IDisposable Loading(LoadingConfig config);

        /// <summary>
        ///  Progress with config 
        /// </summary> 
        IProgressDialog Progress(ProgressConfig config);

        #endregion Loading

        #region Prompt

        void Prompt(PromptConfig config);

        void Form(PromptFormConfig config);

        #endregion

    }
}