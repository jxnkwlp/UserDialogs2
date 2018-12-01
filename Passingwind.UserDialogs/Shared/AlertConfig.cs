using System;

namespace Passingwind.UserDialogs
{
    /// <summary>
    ///  Alert config
    /// </summary>
    public class AlertConfig : IAndroidStyleDialogConfig
    {
        public static string DefaultOkText = "Yes";
        public static string DefaultCancelText = "Cancel";
        public static int? DefaultAndroidStyleId;

        public string Title { get; set; }

        public string Message { get; set; }

        public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;

        public string OkText { get; set; } = DefaultOkText;
        public string CancelText { get; set; } = DefaultCancelText;

        public AlertButtonItem OkButton { get; set; }
        public AlertButtonItem CancelButton { get; set; }
        public AlertButtonItem DestructiveButton { get; set; }

        public AlertConfig()
        {
        }

        public AlertConfig(string title, string message) : this()
        {
            this.Title = title;
            this.Message = message;
        }

        public AlertConfig(string message) : this()
        {
            this.Message = message;
        }

        public AlertConfig SetTitle(string value)
        {
            this.Title = value;

            return this;
        }

        public AlertConfig SetMessage(string value)
        {
            this.Message = value;

            return this;
        }

        public AlertConfig AddDestructiveButton(string text, Action action = null)
        {
            this.DestructiveButton = new AlertButtonItem(text)
            {
                Action = action,
            };

            return this;
        }

        public AlertConfig AddOkButton(string text = null, Action action = null)
        {
            text = text ?? OkText;

            this.OkButton = new AlertButtonItem(text)
            {
                Action = action,
            };

            return this;
        }

        public AlertConfig AddCancelButton(string text = null, Action action = null)
        {
            text = text ?? CancelText;

            this.CancelButton = new AlertButtonItem(text)
            {
                Action = action,
            };

            return this;
        }
    }

    public class AlertButtonItem
    {
        public string Text { get; set; }

        public Action Action { get; set; }

        public AlertButtonItem(string text)
        {
            this.Text = text;
        }
    }
}