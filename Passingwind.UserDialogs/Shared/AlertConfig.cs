using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    /// <summary>
    ///  Define Alert config
    /// </summary>
    public class AlertOptions : IAndroidStyleDialogConfig
    {
        //public static string DefaultOkText = "Yes";

        public static int? DefaultAndroidStyleId;

        public string Title { get; set; }

        //public string OkText { get; set; } = DefaultOkText;

        public string Message { get; set; }

        //public Action OkAction { get; set; }


        public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;



        public IList<AlertButtonOption> Buttons { get; } = new List<AlertButtonOption>();


        public AlertOptions()
        {
        }

        public AlertOptions(string title, string message) : this()
        {
            this.Title = title;
            this.Message = message;
        }

        public AlertOptions(string message) : this()
        {
            this.Message = message;
        }

        public AlertOptions SetTitle(string value)
        {
            this.Title = value;

            return this;
        }

        public AlertOptions SetMessage(string value)
        {
            this.Message = value;

            return this;
        }

        public AlertOptions AddButton(string text, Action action = null)
        {
            this.Buttons.Add(new AlertButtonOption(text) { Action = action });

            return this;
        }

    }

    public class AlertButtonOption
    {
        public string Text { get; set; }

        public Action Action { get; set; }

        public AlertButtonOption(string text)
        {
            this.Text = text;
        }
    }
}
