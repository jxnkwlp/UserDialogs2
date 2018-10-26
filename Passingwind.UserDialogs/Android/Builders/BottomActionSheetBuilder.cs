using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
///  https://juejin.im/post/58de0a9a44d904006d04cead
/// </summary>
namespace Passingwind.UserDialogs.Platforms
{
    class BottomActionSheetBuilder
    {
        Activity Activity { get; set; }

        public Dialog Build(Activity activity, ActionSheetOptions config)
        {
            var di = new Dialog(activity, config.AndroidStyleId ?? 0);

            var windows = di.Window;

            // 1.
            windows.RequestFeature(WindowFeatures.NoTitle);

            // 2.
            var viewBuilder = new BottomActionViewBuilder(activity, config);
            viewBuilder.Clicked = () => di.Dismiss();
            di.SetContentView(viewBuilder.CreateView());

            // 3.
            windows.SetBackgroundDrawable(new ColorDrawable(Color.White)); // 背景 

            // 4.
            windows.SetLayout(-1, -2);

            windows.SetGravity(GravityFlags.Bottom);

            windows.Attributes.Width = windows.WindowManager.DefaultDisplay.Width;

            windows.DecorView.SetPadding(0, 0, 0, 0);

            windows.SetDimAmount(0.5f);


            return di;

        }



        //protected override Dialog CreateDialog(ActionSheetOptions config)
        //{
        //    var contentView = CreateView(this.Config);

        //    //dialog.SetContentView();

        //    //var windows = this.Dialog.Window;
        //    //windows.SetGravity(GravityFlags.Bottom);
        //    //windows.Attributes.Gravity = GravityFlags.Bottom;

        //    //dialog.Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

        //    // ButtomDialogView dialog = new ButtomDialogView(this.Activity, contentView);

        //    var dialog = new Dialog(this.Context);

        //    dialog.SetContentView(contentView);

        //    return dialog;
        //}





    }


    class BottomActionViewBuilder
    {
        /// <summary>
        ///  items start 2
        ///  0: remove
        ///  1: cancel
        /// </summary>
        public Action Clicked { get; set; }


        public Activity Activity { get; private set; }


        ActionSheetOptions _config;

        public BottomActionViewBuilder(Activity activity, ActionSheetOptions config)
        {
            Activity = activity;
            _config = config;
        }


        public virtual View CreateView()
        {
            var container = new LinearLayout(this.Activity);
            container.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            container.Orientation = Orientation.Vertical;

            if (!string.IsNullOrEmpty(_config.Title))
            {
                container.AddView(GetHeaderText(_config.Title, _config.ItemTextAlgin));
            }

            foreach (var item in _config.Items)
            {
                container.AddView(this.CreateRow(item, false, _config.ItemTextAlgin));
            }

            if (_config.Destructive != null)
            {
                container.AddView(this.CreateDivider());
                container.AddView(this.CreateRow(_config.Destructive, true, _config.ItemTextAlgin));
            }
            if (_config.Cancel != null)
            {
                if (_config.Destructive == null)
                    container.AddView(this.CreateDivider());

                container.AddView(this.CreateRow(_config.Cancel, false, _config.ItemTextAlgin));
            }

            return container;
        }


        protected View CreateRow(ActionSheetItemOption option, bool isDestructive, ActionSheetItemTextAlgin textAlgin)
        {
            var row = new LinearLayout(this.Activity)
            {
                Clickable = true,
                Orientation = Orientation.Horizontal,
                LayoutParameters = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, this.DpToPixels(48))
            };

            //if (option.ItemIcon != null)
            //    row.AddView(this.GetIcon(option.ItemIcon));

            row.AddView(this.GetText(option.Text, option.ItemIcon, isDestructive, textAlgin));

            // row.SetGravity(GravityFlags.CenterHorizontal | GravityFlags.CenterVertical);


            row.Click += (sender, args) =>
            {
                option.Action?.Invoke();

                Clicked?.Invoke();
            };

            return row;

        }

        protected virtual TextView GetHeaderText(string text, ActionSheetItemTextAlgin textAlgin)
        {
            var layout = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, this.DpToPixels(56))
            {
                LeftMargin = this.DpToPixels(16)
            };
            var txt = new TextView(this.Activity)
            {
                Text = text,
                LayoutParameters = layout,
                Gravity = GravityFlags.CenterVertical,
            };
            txt.SetTextSize(ComplexUnitType.Sp, 16);
            txt.Paint.FakeBoldText = true;

            if (textAlgin == ActionSheetItemTextAlgin.Center || textAlgin == ActionSheetItemTextAlgin.Right)
            {
                txt.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
            }

            return txt;
        }


        protected virtual TextView GetText(string text, string icon, bool isDestructive, ActionSheetItemTextAlgin textAlgin)
        {
            var layout = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent)
            {
                TopMargin = this.DpToPixels(8),
                BottomMargin = this.DpToPixels(8),
                LeftMargin = this.DpToPixels(16),
                RightMargin = this.DpToPixels(16),
            };

            var txt = new TextView(this.Activity)
            {
                Text = text,
                LayoutParameters = layout,
                // Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal,
            };
            txt.SetTextSize(ComplexUnitType.Sp, 16);
            //  txt.SetBackgroundResource(global::Android.Resource.Attribute.SelectableItemBackground);

            if (!string.IsNullOrWhiteSpace(icon))
            {
                var drawable = ImageLoader.Load(icon);
                drawable.SetBounds(0, 0, drawable.MinimumWidth, drawable.MinimumHeight);
                txt.SetCompoundDrawables(drawable, null, null, null);
                txt.CompoundDrawablePadding = this.DpToPixels(6);
            }


            //switch (textAlgin)
            //{
            //    case ActionSheetItemTextAlgin.Center:
            //        txt.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
            //        break;
            //    case ActionSheetItemTextAlgin.Right:
            //        txt.Gravity = GravityFlags.CenterVertical | GravityFlags.Right;
            //        break;

            //    default:
            //        txt.Gravity = GravityFlags.CenterVertical | GravityFlags.Left;

            //        break;
            //}



            if (isDestructive)
                txt.SetTextColor(Color.Red);

            return txt;
        }


        protected virtual ImageView GetIcon(string icon)
        {
            var layout = new LinearLayout.LayoutParams(this.DpToPixels(24), this.DpToPixels(24))
            {
                TopMargin = this.DpToPixels(8),
                BottomMargin = this.DpToPixels(8),
                LeftMargin = this.DpToPixels(16),
                RightMargin = this.DpToPixels(16),
                Gravity = GravityFlags.Center
            };

            var img = new ImageView(this.Activity)
            {
                LayoutParameters = layout
            };
            if (icon != null)
                img.SetImageDrawable(ImageLoader.Load(icon));

            return img;
        }


        protected virtual View CreateDivider()
        {
            var view = new View(this.Activity)
            {
                Background = new ColorDrawable(Color.LightGray),
                LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, this.DpToPixels(1))
            };
            view.SetPadding(0, this.DpToPixels(7), 0, this.DpToPixels(8));
            return view;
        }


        protected virtual int DpToPixels(int dp)
        {
            var value = TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, this.Activity.Resources.DisplayMetrics);
            return Convert.ToInt32(value);
        }

    }


    //public class BottomActionDialog : Dialog
    //{
    //    ActionSheetOptions _options;

    //    public BottomActionDialog(Context context, ActionSheetOptions options) : base(context)
    //    {
    //        _options = options;
    //    }

    //    protected override void OnCreate(Bundle savedInstanceState)
    //    {
    //        base.OnCreate(savedInstanceState);

    //        BottomActionViewBuilder viewBuilder = new BottomActionViewBuilder(this.OwnerActivity);

    //        this.SetContentView(viewBuilder.CreateView(_options));

    //        this.Window.Attributes.Width = WindowManagerLayoutParams.MatchParent;
    //        this.Window.Attributes.Height = WindowManagerLayoutParams.WrapContent;
    //        this.Window.SetGravity(Android.Views.GravityFlags.Bottom);
    //    }
    //}
}
