using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class ProgressConfig
    {
        public string Text { get; set; }

        public MarkType MarkType { get; set; }

        public int Max { get; set; } = 100;

        public int Start { get; set; } = 0;

         
        public bool Cancellable { get; set; }

        public Action CancelAction { get; set; }

        public ProgressConfig()
        {
        }

        public ProgressConfig(string text)
        {
            this.Text = text;
        }


        public ProgressConfig SetText(string text)
        {
            this.Text = text;
            return this;
        }

        public ProgressConfig SetStart(int value)
        {
            this.Start = value;
            return this;
        }


        public ProgressConfig SetMax(int value)
        {
            this.Max = value;
            return this;
        }


    }
}
