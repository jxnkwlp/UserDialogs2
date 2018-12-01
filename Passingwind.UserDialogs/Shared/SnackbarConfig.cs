using System;
using System.Drawing;

namespace Passingwind.UserDialogs
{
    /// <summary>
    ///  Snackbar Config
    /// </summary>
    public class SnackbarConfig : IAndroidStyleDialogConfig
    {
        public static string DefaultActionText = null;
        public static TimeSpan DefaultTimeSpan = TimeSpan.FromSeconds(1.2);
        public static Color? DefaultBackgroundColor = null;
        public static Color? DefaultTextColor = null;
        public static Color? DefaultActionTextColor = null;

        public static int? DefaultAndroidStyleId;

        public string Message { get; set; }

        public TimeSpan Duration { get; set; } = DefaultTimeSpan;

        public Color? BackgroundColor { get; set; } = DefaultBackgroundColor;
        public Color? TextColor { get; set; } = DefaultTextColor;

        public string ActionText { get; set; } = DefaultActionText;
        public Color? ActionTextColor { get; set; } = DefaultActionTextColor;

        public Action Action { get; set; }

        public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;

        public SnackbarConfig()
        {
        }

        public SnackbarConfig(string message) : this()
        {
            this.Message = message;
        }

        public SnackbarConfig SetMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public SnackbarConfig SetDuration(TimeSpan time)
        {
            this.Duration = time;
            return this;
        }

        public SnackbarConfig SetActionText(string text)
        {
            this.ActionText = text;
            return this;
        }

        public SnackbarConfig SetAction(Action action)
        {
            this.Action = action;
            return this;
        }
    }
}