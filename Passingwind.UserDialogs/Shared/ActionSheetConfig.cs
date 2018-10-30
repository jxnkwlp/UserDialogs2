using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class ActionSheetConfig : IAndroidStyleDialogConfig
    {
        public static int? DefaultAndroidStyleId;
        public static string DefaultCancelText = "Cancel";
        public static string DefaultDestructiveText = "Remove";
        public static bool DefaultBottomSheet = false;
        public static string DefaultItemIcon;




        public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;


        public string Title { get; set; }
        public string Message { get; set; }

        public string ItemIcon { get; set; } = DefaultItemIcon;


        public ActionSheetItemTextAlgin ItemTextAlgin { get; set; }

        /// <summary>
        ///  cancel option item 
        /// </summary>
        public ActionSheetItem Cancel { get; set; }

        /// <summary>
        ///  destructive option item
        /// </summary>
        public ActionSheetItem Destructive { get; set; }

        public IList<ActionSheetItem> Items { get; } = new List<ActionSheetItem>();


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

        public ActionSheetConfig AddCancel(string text = null, Action action = null, string icon = null)
        {
            this.Cancel = new ActionSheetItem(text ?? DefaultCancelText, action, icon);
            return this;
        }

        public ActionSheetConfig AddDestructive(string text = null, Action action = null, string icon = null)
        {
            this.Destructive = new ActionSheetItem(text ?? DefaultDestructiveText, action, icon);
            return this;
        }

        public ActionSheetConfig AddItem(string text, Action action = null, string icon = null)
        {
            this.Items.Add(new ActionSheetItem(text, action, icon ?? DefaultItemIcon));
            return this;
        }

    }
}
