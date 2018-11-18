using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class LoadingConfig
    {
        public static TimeSpan? DefaultDuration;
        public static MarkType DefaultMarkType;
        public static string DefaultCancelText = "Cancel";

        public string Text { get; set; }

        public TimeSpan? Duration { get; set; } = DefaultDuration;

        public MarkType MarkType { get; set; } = DefaultMarkType;


        public bool Cancellable { get; set; }

        /// <summary>
        ///  for iOS 
        /// </summary>
        public string CancelText { get; set; } = DefaultCancelText;

        public Action CancelAction { get; set; }


        public LoadingConfig()
        {
        }

        public LoadingConfig(string text)
        {
            this.Text = text;
        }


        public LoadingConfig SetText(string text)
        {
            this.Text = text;

            return this;
        }


        public LoadingConfig SetDuration(TimeSpan time)
        {
            this.Duration = time;

            return this;
        }

    }
}
