using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class ActionSheetConfig : IAndroidStyleDialogConfig
    {
        public static string DefaultCancelText { get; set; } = "Cancel";
        public static string DefaultDestructiveText { get; set; } = "Remove";

        public static bool DefaultBottomSheet { get; set; } = false;

        public static string DefaultItemIcon { get; set; }


        public int? AndroidStyleId { get; set; }


        public string Title { get; set; }
        public string Message { get; set; }

        public string ItemIcon { get; set; } = DefaultItemIcon;



        public ActionSheetOption Cancel { get; set; }
        public ActionSheetOption Destructive { get; set; }

        public IList<ActionSheetOption> Options { get; } = new List<ActionSheetOption>();


        public bool BottomSheet { get; set; } = DefaultBottomSheet;



        public ActionSheetConfig SetTitle(string title)
        {
            this.Title = title;
            return this;
        }

        public ActionSheetConfig SetMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public ActionSheetConfig SetBottomSheet(bool bottomSheet)
        {
            this.BottomSheet = bottomSheet;
            return this;
        }

        public ActionSheetConfig SetCancel(string text = null, Action action = null, string icon = null)
        {
            this.Cancel = new ActionSheetOption(text ?? DefaultCancelText, action, icon);
            return this;
        }

        public ActionSheetConfig SetDestructive(string text = null, Action action = null, string icon = null)
        {
            this.Destructive = new ActionSheetOption(text ?? DefaultDestructiveText, action, icon);
            return this;
        }

        public ActionSheetConfig AddOption(string text, Action action = null, string icon = null)
        {
            this.Options.Add(new ActionSheetOption(text, action, icon ?? DefaultItemIcon));
            return this;
        }

    }
}
