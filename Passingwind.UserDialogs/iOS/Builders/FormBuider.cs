using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Passingwind.UserDialogs.Platforms
{
    public static class FormBuider
    {
        public static UIAlertController Build(PromptFormConfig config)
        {
            var controller = UIAlertController.Create(config.Title, config.Message, UIAlertControllerStyle.Alert);

            var result = new Dictionary<string, string>();
            var inputs = new Dictionary<string, UITextField>();
            foreach (var item in config.Items)
            {
                var txt = new UITextField();

                controller.AddTextField((f) =>
                {
                    txt = f;
                    SetInputType(txt, item.InputType);

                    txt.Placeholder = item.Placeholder ?? string.Empty;
                    txt.Text = item.Text ?? string.Empty;

                    if (item.MaxLength != null)
                    {
                        txt.ShouldChangeCharacters = (field, replacePosition, replacement) =>
                        {
                            var updatedText = new StringBuilder(field.Text);
                            updatedText.Remove((int)replacePosition.Location, (int)replacePosition.Length);
                            updatedText.Insert((int)replacePosition.Location, replacement);
                            return updatedText.ToString().Length <= item.MaxLength.Value;
                        };
                    }
                });

                inputs[item.Key] = txt;
            }

            Action action = () =>
            {
                foreach (var item in inputs)
                {
                    result[item.Key] = item.Value.Text.Trim();
                }
            };

            var okButton = UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, (_) =>
            {
                action();
                config.OnAction?.Invoke(new PromptFormResult(true, result));
            });

            controller.AddAction(okButton);

            if (config.IsCancellable)
            {
                controller.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Cancel, (_) =>
                {
                    action();
                    config.OnAction?.Invoke(new PromptFormResult(false, result));
                }));
            }

            return controller;
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
