using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class SnackbarOptions : IAndroidStyleDialogConfig
    {
        public static TimeSpan DefaultTimeSpan = TimeSpan.FromSeconds(1.5);

        public static int? DefaultAndroidStyleId;


        public string Message { get; set; }

        public TimeSpan Duration { get; set; } = DefaultTimeSpan;

        public Color? BackgroundColor { get; set; }

        public Color? TextColor { get; set; }


        public string ActionText { get; set; }
        public Color? ActionTextColor { get; set; }
        public Action Action { get; set; }

        public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;


        public SnackbarOptions()
        {
        }


        public SnackbarOptions(string message) : this()
        {
            this.Message = message;
        }

        public SnackbarOptions SetMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public SnackbarOptions SetDuration(TimeSpan time)
        {
            this.Duration = time;
            return this;
        }

    }
}
