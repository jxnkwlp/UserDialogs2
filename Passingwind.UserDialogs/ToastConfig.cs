using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class ToastConfig
    {
        public static TimeSpan DefaultTimeSpan = TimeSpan.FromSeconds(1.5);
        public static ToastPosition DefaultPosition = ToastPosition.Default;
        public static ToastStyle DefaultToastStyle = ToastStyle.Default;


        public string Message { get; set; }

        public TimeSpan Duration { get; set; } = DefaultTimeSpan;

        public ToastPosition Position { get; set; } = DefaultPosition;

        public ToastStyle Style { get; set; } = DefaultToastStyle;



        public ToastConfig()
        {
        }


        public ToastConfig(string message) : this()
        {
            this.Message = message;
        }

        public ToastConfig SetMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public ToastConfig SetDuration(TimeSpan time)
        {
            this.Duration = time;
            return this;
        }

        public ToastConfig SetPosition(ToastPosition position)
        {
            this.Position = position;
            return this;
        }

        public ToastConfig SetStyle(ToastStyle style)
        {
            this.Style = style;
            return this;
        }

    }


}
