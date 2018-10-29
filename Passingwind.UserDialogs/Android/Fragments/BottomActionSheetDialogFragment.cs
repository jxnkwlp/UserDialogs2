using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Passingwind.UserDialogs.Platforms
{
    public class BottomActionSheetDialogFragment : AbstractAppCompatDialogFragment<ActionSheetOptions>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //windows.SetGravity(GravityFlags.Bottom);
            //windows.Attributes.Width = WindowManagerLayoutParams.MatchParent;
            //windows.Attributes.Height = WindowManagerLayoutParams.WrapContent;



        }

        //public override void OnStart()
        //{
        //    base.OnStart();

        //    var dialog = this.Dialog;

        //    if (dialog != null && dialog.Window != null)
        //    {
        //        dialog.Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);
        //        dialog.Window.SetLayout(-1, -2);
        //        dialog.Window.Attributes.Gravity = GravityFlags.Bottom;

        //    }
        //}

        //public override Dialog OnCreateDialog(Bundle bundle)
        //{
        //    var di = base.OnCreateDialog(bundle);

        //    var windows = this.Dialog.Window;
        //    windows.SetGravity(GravityFlags.Bottom);
        //    windows.Attributes.Gravity = GravityFlags.Bottom;

        //    return di;
        //}


        protected override void SetDialogDefaults(Dialog dialog)
        {
            // base.SetDialogDefaults(dialog);

            if (this.Config.Cancel == null)
            {
                dialog.SetCancelable(false);
                dialog.SetCanceledOnTouchOutside(false);
            }
            else
            {
                dialog.SetCancelable(true);
                dialog.SetCanceledOnTouchOutside(true);
                dialog.CancelEvent += (sender, args) => this.Config.Cancel.Action.Invoke();
            }

            //this.Dialog.Window.SetLayout(WindowManagerLayoutParams.MatchParent, -2);
            //this.Dialog.Window.SetGravity(GravityFlags.Bottom);

        }

        protected override void OnKeyPress(object sender, DialogKeyEventArgs args)
        {
            if (args.KeyCode == Keycode.Back)
            {
                args.Handled = true;
                this.Config?.Cancel?.Action?.Invoke();
                this.Dismiss();
            }

            base.OnKeyPress(sender, args);
        }

        protected override Dialog CreateDialog(ActionSheetOptions config)
        {
            return new BottomActionSheetBuilder().Build(this.AppCompatActivity, config);
        }


        //private View CreateView(ActionSheetOptions config)
        //{
        //    var container = new LinearLayout(this.Activity);
        //    container.Orientation = Orientation.Vertical;

        //    if (!string.IsNullOrEmpty(config.Title))
        //    {
        //        container.AddView(GetHeaderText(config.Title));
        //    }

        //    foreach (var action in config.Items)
        //        container.AddView(this.CreateRow(action, false));

        //    if (config.Destructive != null)
        //    {
        //        container.AddView(this.CreateDivider());
        //        container.AddView(this.CreateRow(config.Destructive, true));
        //    }
        //    if (config.Cancel != null)
        //    {
        //        if (config.Destructive == null)
        //            container.AddView(this.CreateDivider());

        //        container.AddView(this.CreateRow(config.Cancel, false));
        //    }

        //    return container;
        //}


        //private View CreateRow(ActionSheetItemOption option, bool isDestructive)
        //{
        //    var row = new LinearLayout(this.Activity)
        //    {
        //        Clickable = true,
        //        Orientation = Orientation.Horizontal,
        //        LayoutParameters = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, this.DpToPixels(48))
        //    };
        //    if (option.ItemIcon != null)
        //        row.AddView(this.GetIcon(option.ItemIcon));

        //    row.AddView(this.GetText(option.Text, isDestructive));
        //    row.Click += (sender, args) =>
        //    {
        //        option.Action?.Invoke();
        //        this.Dismiss();
        //    };
        //    return row;


        //}

        //protected virtual TextView GetHeaderText(string text)
        //{
        //    var layout = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, this.DpToPixels(56))
        //    {
        //        LeftMargin = this.DpToPixels(16)
        //    };
        //    var txt = new TextView(this.Activity)
        //    {
        //        Text = text,
        //        LayoutParameters = layout,
        //        Gravity = GravityFlags.CenterVertical
        //    };
        //    txt.SetTextSize(ComplexUnitType.Sp, 16);
        //    return txt;
        //}


        //protected virtual TextView GetText(string text, bool isDestructive)
        //{
        //    var layout = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent)
        //    {
        //        TopMargin = this.DpToPixels(8),
        //        BottomMargin = this.DpToPixels(8),
        //        LeftMargin = this.DpToPixels(16)
        //    };

        //    var txt = new TextView(this.Activity)
        //    {
        //        Text = text,
        //        LayoutParameters = layout,
        //        Gravity = GravityFlags.CenterVertical
        //    };
        //    txt.SetTextSize(ComplexUnitType.Sp, 16);
        //    if (isDestructive)
        //        txt.SetTextColor(Color.Red);

        //    return txt;
        //}


        //protected virtual ImageView GetIcon(string icon)
        //{
        //    var layout = new LinearLayout.LayoutParams(this.DpToPixels(24), this.DpToPixels(24))
        //    {
        //        TopMargin = this.DpToPixels(8),
        //        BottomMargin = this.DpToPixels(8),
        //        LeftMargin = this.DpToPixels(16),
        //        RightMargin = this.DpToPixels(16),
        //        Gravity = GravityFlags.Center
        //    };

        //    var img = new ImageView(this.Activity)
        //    {
        //        LayoutParameters = layout
        //    };
        //    if (icon != null)
        //        img.SetImageDrawable(ImageLoader.Load(icon));

        //    return img;
        //}


        //protected virtual View CreateDivider()
        //{
        //    var view = new View(this.Activity)
        //    {
        //        Background = new ColorDrawable(Color.LightGray),
        //        LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, this.DpToPixels(1))
        //    };
        //    view.SetPadding(0, this.DpToPixels(7), 0, this.DpToPixels(8));
        //    return view;
        //}


        //protected virtual int DpToPixels(int dp)
        //{
        //    var value = TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, this.Activity.Resources.DisplayMetrics);
        //    return Convert.ToInt32(value);
        //}


    }


    //public class CustomBottomSheetDialog : Dialog
    //{
    //    ActionSheetOptions _sheetConfig;

    //    public CustomBottomSheetDialog(Context context) : base(context)
    //    {
    //    }

    //    public override void Create()
    //    {
    //        base.Create();

    //        //var windows = this.Window;

    //        //windows.SetGravity(GravityFlags.Bottom);
    //        //windows.Attributes.Width = WindowManagerLayoutParams.MatchParent;
    //        //windows.Attributes.Height = WindowManagerLayoutParams.WrapContent;
    //    }

    //}

    ///// <summary>
    /////   https://blog.csdn.net/zhao_doubi/article/details/77895028
    ///// </summary>
    //public class ButtomDialogView : Dialog
    //{
    //    Context _context;
    //    View _view;

    //    public ButtomDialogView(Context context, View view) : base(context)
    //    {
    //        _context = context;
    //        _view = view;

    //    }

    //    protected override void OnCreate(Bundle savedInstanceState)
    //    {
    //        base.OnCreate(savedInstanceState);

    //        SetContentView(_view);

    //        SetCancelable(false);
    //        SetCanceledOnTouchOutside(false);

    //        var attribute = Window.Attributes;
    //        attribute.Width = WindowManagerLayoutParams.MatchParent;
    //        attribute.Height = WindowManagerLayoutParams.WrapContent;

    //        Window.Attributes = attribute;
    //    }



    //}

}
