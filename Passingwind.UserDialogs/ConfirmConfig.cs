using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
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

        public Action OkAction { get; set; }

        public Action CancelAction { get; set; }


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

        public ConfirmConfig SetOkAction(Action action)
        {
            this.OkAction = action;

            return this;
        }

        public ConfirmConfig SetCancelAction(Action action)
        {
            this.CancelAction = action;

            return this;
        }

    }
}
