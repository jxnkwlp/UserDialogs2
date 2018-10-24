using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class ActionSheetItemOption
    {
        public string Text { get; set; }

        public Action Action { get; set; }

        public string ItemIcon { get; set; }

        public ItemIconPosition IconPosition { get; set; }

        public ActionSheetItemOption(string text, Action action = null, string icon = null)
        {
            this.Text = text;
            this.Action = action;
            this.ItemIcon = icon;
        }


        public enum ItemIconPosition
        {
            Left,
            Right,
        }
    }
}
