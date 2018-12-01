using System;

namespace Passingwind.UserDialogs
{
    /// <summary>
    ///  Confirm Config
    /// </summary>
    public class ConfirmConfig
    {
        public static string DefaultOkText = "Ok";
        public static string DefaultCancelText = "Cancel";

        public static int? DefaultAndroidStyleId;

        // internal Action _okAction;

        public string Title { get; set; }

        public string OkText { get; set; } = DefaultOkText;

        public string CancelText { get; set; } = DefaultCancelText;

        public string Message { get; set; }

        public Action<bool> Action { get; set; }

        public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;

        public ConfirmConfig()
        {
        }

        public ConfirmConfig(string title, string message) : this()
        {
            this.Title = title;
            this.Message = message;
        }

        public ConfirmConfig(string message) : this()
        {
            this.Message = message;
        }

        public ConfirmConfig SetOkText(string value)
        {
            this.OkText = value;

            return this;
        }

        public ConfirmConfig SetCancelText(string value)
        {
            this.CancelText = value;

            return this;
        }

        public ConfirmConfig SetTitle(string value)
        {
            this.Title = value;

            return this;
        }

        public ConfirmConfig SetMessage(string value)
        {
            this.Message = value;

            return this;
        }

        public ConfirmConfig SetAction(Action<bool> action)
        {
            this.Action = action;

            return this;
        }
    }
}