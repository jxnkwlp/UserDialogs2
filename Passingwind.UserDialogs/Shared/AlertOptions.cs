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
        public static string DefaultOkText = "Yes";
        public static string DefaultCancelText = "Cancel";

        public static int? DefaultAndroidStyleId;

        public string Title { get; set; }

        public string Message { get; set; }


        public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;


        public string OkText { get; set; } = DefaultOkText;
        public string CancelText { get; set; } = DefaultCancelText;



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

        public AlertOptions AddOkButton(string text = null, Action action = null)
        {
            text = text ?? OkText;

            this.Buttons.Add(new AlertButtonOption(text) { Action = action });

            return this;
        }

        public AlertOptions AddCancelButton(string text = null, Action action = null)
        {
            text = text ?? CancelText;

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
