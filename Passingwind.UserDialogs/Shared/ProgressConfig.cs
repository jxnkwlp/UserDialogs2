using System;

namespace Passingwind.UserDialogs
{
    public class ProgressConfig
    {
        public static MarkType DefaultMarkType = MarkType.Clear;
        public static string DefaultCancelText = "Cancel";

        public string Text { get; set; }

        public MarkType MarkType { get; set; } = DefaultMarkType;

        public bool Cancellable { get; set; }

        /// <summary>
        ///  for iOS
        /// </summary>
        public string CancelText { get; set; } = DefaultCancelText;

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
    }
}