using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class AlertConfig
    {
        public static string DefaultOkText = "Yes";

        public static int? DefaultAndroidStyleId;


        internal Action _okAction;


        public string Title { get; set; }

        public string OkText { get; set; } = DefaultOkText;

        public string Message { get; set; }

        public Action OkAction { get; set; }


        public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;



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


        public AlertConfig SetOkText(string value)
        {
            this.OkText = value;

            return this;
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

        public AlertConfig SetOkAction(Action action)
        {
            this.OkAction = action;

            return this;
        }
    }
}
