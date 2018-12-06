using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    /// <summary>
    ///  PromptFormConfig
    /// </summary>
    public class PromptFormConfig : IAndroidStyleDialogConfig
    {
        public static string DefaultOkText { get; set; } = "Ok";
        public static string DefaultCancelText { get; set; } = "Cancel";
        public static int? DefaultAndroidStyleId { get; set; }
        public static int? DefaultMaxLength { get; set; }

        public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;

        public string Title { get; set; }
        public string Message { get; set; }

        public Action<PromptFormResult> OnAction { get; set; }

        public string OkText { get; set; } = DefaultOkText;
        public string CancelText { get; set; } = DefaultCancelText;
        public bool IsCancellable { get; set; } = true;

        public IList<PromptFormItemOption> Items { get; } = new List<PromptFormItemOption>();


        public PromptFormConfig SetTitle(string title)
        {
            this.Title = title;
            return this;
        }

        public PromptFormConfig SetMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public PromptFormConfig SetCancellable(bool cancel)
        {
            this.IsCancellable = cancel;
            return this;
        }

        public PromptFormConfig SetOkText(string text)
        {
            this.OkText = text;
            return this;
        }

        public PromptFormConfig SetCancelText(string cancelText)
        {
            this.IsCancellable = true;
            this.CancelText = cancelText;
            return this;
        }

        /// <summary>
        ///  add new form item
        /// </summary>
        /// <param name="key">the item key</param>
        /// <param name="placeholder">the item placeholder</param>
        /// <param name="inputType"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public PromptFormConfig AddItem(string key, string placeholder = null, InputType inputType = InputType.Default, int? maxLength = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            this.Items.Add(new PromptFormItemOption()
            {
                Key = key,
                InputType = inputType,
                MaxLength = maxLength,
                Placeholder = placeholder,
            });

            return this;
        }
    }

    public class PromptFormItemOption
    {
        public string Key { get; set; }

        public string Text { get; set; }

        public string Placeholder { get; set; }

        public int? MaxLength { get; set; }

        public InputType InputType { get; set; } = InputType.Default;

    }

    public class PromptFormResult
    {
        public bool Ok { get; }

        public Dictionary<string, string> Result { get; }

        public PromptFormResult(bool ok, Dictionary<string, string> result)
        {
            this.Ok = ok;
            this.Result = result;
        }

    }

}
