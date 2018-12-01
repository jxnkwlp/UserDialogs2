using Android.App;
using Android.Graphics;
using Android.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs.Platforms
{
    public static class Extensions
    {
        public static Color ToNativeColor(this System.Drawing.Color This) => new Color(This.R, This.G, This.B, This.A);

        public static void SafeRunOnUi(this Activity activity, Action action) => activity.RunOnUiThread(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Log.Error("UserDialogs", ex.ToString());
                System.Diagnostics.Debug.WriteLine(ex);
            }
        });
    }
}