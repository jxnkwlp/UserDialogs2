using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public class PromptTextChangedArgs
    {
        /// <summary>
        /// Setting this to false will disable the positive button (defaults to true)
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// This will give you the current value as well as allow you to set the value of the textbox
        /// </summary>
        public string Value { get; set; }
    }
}
