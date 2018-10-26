﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class ActionSheetOptions : IAndroidStyleDialogConfig
    {
        public static int? DefaultAndroidStyleId { get; set; }

        public static string DefaultCancelText { get; set; } = "Cancel";
        public static string DefaultDestructiveText { get; set; } = "Remove";

        public static bool DefaultBottomSheet { get; set; } = false;

        public static string DefaultItemIcon { get; set; }


        public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;


        public string Title { get; set; }
        public string Message { get; set; }

        public string ItemIcon { get; set; } = DefaultItemIcon;


        public ActionSheetItemTextAlgin ItemTextAlgin { get; set; }

        /// <summary>
        ///  cancel option item 
        /// </summary>
        public ActionSheetItemOption Cancel { get; set; }

        /// <summary>
        ///  destructive option item
        /// </summary>
        public ActionSheetItemOption Destructive { get; set; }

        public IList<ActionSheetItemOption> Items { get; } = new List<ActionSheetItemOption>();


        public bool BottomSheet { get; set; } = DefaultBottomSheet;



        public ActionSheetOptions SetTitle(string title)
        {
            this.Title = title;
            return this;
        }

        public ActionSheetOptions SetMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public ActionSheetOptions SetBottomSheet(bool bottomSheet)
        {
            this.BottomSheet = bottomSheet;
            return this;
        }

        public ActionSheetOptions SetCancel(string text = null, Action action = null, string icon = null)
        {
            this.Cancel = new ActionSheetItemOption(text ?? DefaultCancelText, action, icon);
            return this;
        }

        public ActionSheetOptions SetDestructive(string text = null, Action action = null, string icon = null)
        {
            this.Destructive = new ActionSheetItemOption(text ?? DefaultDestructiveText, action, icon);
            return this;
        }

        public ActionSheetOptions AddItem(string text, Action action = null, string icon = null)
        {
            this.Items.Add(new ActionSheetItemOption(text, action, icon ?? DefaultItemIcon));
            return this;
        }

    }
}
