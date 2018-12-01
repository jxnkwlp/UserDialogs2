namespace Passingwind.UserDialogs
{
    /// <summary>
    /// </summary>
    public interface IProgressDialog
    {
        /// <summary>
        ///  hide the progress dialog hub
        /// </summary>
        void Hide();

        /// <summary>
        ///  set the progress values, value between 0-100
        /// </summary>
        /// <param name="value">value</param>
        void SetProgress(uint value);
    }
}