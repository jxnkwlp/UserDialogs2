using Foundation;
using System.Text;
using UIKit;

namespace Passingwind.UserDialogs.Platforms
{
    public static class PromptBuilder
    {
        public static UIAlertController Build(PromptConfig config)
        {
            var controller = UIAlertController.Create(config.Title, config.Message, UIAlertControllerStyle.Alert);

            UITextField txt = null;

            var okButton = UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, (_) => config.OnAction?.Invoke(new PromptResult(true, txt?.Text)));
            controller.AddAction(okButton);

            if (config.IsCancellable)
            {
                controller.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Cancel, (_) => config.OnAction?.Invoke(new PromptResult(false, txt?.Text))));
            }

            controller.AddTextField((f) =>
            {
                txt = f;
                SetInputType(txt, config.InputType);

                txt.Placeholder = config.Placeholder ?? string.Empty;
                txt.Text = config.Text ?? string.Empty;

                if (config.MaxLength != null)
                {
                    txt.ShouldChangeCharacters = (field, replacePosition, replacement) =>
                    {
                        var updatedText = new StringBuilder(field.Text);
                        updatedText.Remove((int)replacePosition.Location, (int)replacePosition.Length);
                        updatedText.Insert((int)replacePosition.Location, replacement);
                        return updatedText.ToString().Length <= config.MaxLength.Value;
                    };
                }

                if (config.OnTextChanged != null)
                {
                    txt.AddTarget((sender, e) => ValidatePrompt(txt, okButton, config), UIControlEvent.EditingChanged);
                    ValidatePrompt(txt, okButton, config);
                }
            });

            return controller;
        }

        static void ValidatePrompt(UITextField txt, UIAlertAction btn, PromptConfig config)
        {
            var args = new PromptTextChangedArgs { Value = txt.Text };
            config.OnTextChanged(args);
            btn.Enabled = args.IsValid;
            if (!txt.Text.Equals(args.Value))
                txt.Text = args.Value;
        }

        static void SetInputType(UITextField txt, InputType inputType)
        {
            switch (inputType)
            {
                case InputType.DecimalNumber:
                    txt.KeyboardType = UIKeyboardType.DecimalPad;
                    break;

                case InputType.Email:
                    txt.KeyboardType = UIKeyboardType.EmailAddress;
                    break;

                case InputType.Name:
                    break;

                case InputType.Number:
                    txt.KeyboardType = UIKeyboardType.NumberPad;
                    break;

                case InputType.NumericPassword:
                    txt.SecureTextEntry = true;
                    txt.KeyboardType = UIKeyboardType.NumberPad;
                    break;

                case InputType.Password:
                    txt.SecureTextEntry = true;
                    break;

                case InputType.Phone:
                    txt.KeyboardType = UIKeyboardType.PhonePad;
                    break;

                case InputType.Url:
                    txt.KeyboardType = UIKeyboardType.Url;
                    break;
            }
        }
    }
}
