using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Passingwind.UserDialogs
{
    /// <summary>
    /// Define toast config options 
    /// </summary>
    public class ToastOptions
    {
        public static TimeSpan DefaultTimeSpan = TimeSpan.FromSeconds(1.5);
        public static ToastPosition DefaultPosition = ToastPosition.Default;


        public string Message { get; set; }

        public TimeSpan Duration { get; set; } = DefaultTimeSpan;

        public ToastPosition Position { get; set; } = DefaultPosition;



        public Color? BackgroundColor { get; set; }

        public Color? TextColor { get; set; }


        public string ActionText { get; set; }
        public Color? ActionTextColor { get; set; }
        public Action Action { get; set; }


        public ToastOptions()
        {
        }


        public ToastOptions(string message) : this()
        {
            this.Message = message;
        }

        public ToastOptions SetMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public ToastOptions SetDuration(TimeSpan time)
        {
            this.Duration = time;
            return this;
        }

        public ToastOptions SetPosition(ToastPosition position)
        {
            this.Position = position;
            return this;
        }

    }


}
