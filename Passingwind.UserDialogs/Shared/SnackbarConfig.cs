using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class SnackbarConfig : IAndroidStyleDialogConfig
    {
        public static TimeSpan DefaultTimeSpan = TimeSpan.FromSeconds(1.2);

        public static int? DefaultAndroidStyleId;


        public string Message { get; set; }

        public TimeSpan Duration { get; set; } = DefaultTimeSpan;

        public Color? BackgroundColor { get; set; }

        public Color? TextColor { get; set; }


        public string ActionText { get; set; }
        public Color? ActionTextColor { get; set; }
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

    }
}
