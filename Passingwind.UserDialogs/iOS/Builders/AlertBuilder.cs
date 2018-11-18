using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Passingwind.UserDialogs.Platforms
{
    public static class AlertBuilder
    {
        public static UIAlertController AlertBuild(AlertConfig config)
        {
            var controller = UIAlertController.Create(config.Title, config.Message, UIAlertControllerStyle.Alert);

            if (config.OkButton != null)
            {
                controller.AddAction(UIAlertAction.Create(config.OkButton.Text, UIAlertActionStyle.Default, (_) => config.OkButton.Action?.Invoke()));
            }

            if (config.CancelButton != null)
            {
                controller.AddAction(UIAlertAction.Create(config.CancelButton.Text, UIAlertActionStyle.Cancel, (_) => config.CancelButton.Action?.Invoke()));
            }

            if (config.DestructiveButton != null)
            {
                controller.AddAction(UIAlertAction.Create(config.DestructiveButton.Text, UIAlertActionStyle.Default, (_) => config.DestructiveButton.Action?.Invoke()));
            }


            return controller;
        }


        public static UIAlertController ActionSheetBuild(ActionSheetConfig config)
        {
            var controller = UIAlertController.Create(config.Title, config.Message, UIAlertControllerStyle.ActionSheet);

            if (config.Cancel != null)
            {
                controller.AddAction(UIAlertAction.Create(config.Cancel.Text, UIAlertActionStyle.Cancel, (_) => config.Cancel.Action?.Invoke()));
            }

            if (config.Destructive != null)
            {
                controller.AddAction(UIAlertAction.Create(config.Destructive.Text, UIAlertActionStyle.Destructive, (_) => config.Destructive.Action?.Invoke()));
            }

            if (config.Items != null)
            {
                foreach (var item in config.Items)
                {
                    var actionItem = UIAlertAction.Create(item.Text, UIAlertActionStyle.Default, (_) => item.Action?.Invoke());

                    if (item.ItemIcon != null)
                    {
                        var icon = UIImage.FromBundle(item.ItemIcon);
                        actionItem.SetValueForKey(icon, new NSString("image"));
                    }

                    controller.AddAction(actionItem);
                }
            }

            return controller;
        }

    }
}
