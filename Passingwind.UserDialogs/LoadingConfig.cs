using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class LoadingConfig
    {
        public string Text { get; set; }


        public TimeSpan? Duration { get; set; }


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
