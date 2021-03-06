﻿using System;
using System.Drawing;

namespace Passingwind.UserDialogs
{
    /// <summary>
    /// Define toast config options
    /// </summary>
    public class ToastConfig : IAndroidStyleDialogConfig
    {
        public static TimeSpan DefaultTimeSpan = TimeSpan.FromSeconds(2);
        public static ToastPosition DefaultPosition = ToastPosition.Default;
        public static int? DefaultAndroidStyleId;

        public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;

        public string Message { get; set; }

        public TimeSpan Duration { get; set; } = DefaultTimeSpan;

        public ToastPosition Position { get; set; } = DefaultPosition;

        public Color? BackgroundColor { get; set; }

        public Color? TextColor { get; set; }

        public ToastConfig()
        {
        }

        public ToastConfig(string message)
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
    }
}