using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using AlertDialog = Android.App.AlertDialog;
using AppCompatAlertDialog = Android.Support.V7.App.AlertDialog;
namespace Passingwind.UserDialogs.Platforms
{
    public class ActionSheetTheme1Builder
    {
        Activity Activity { get; set; }

        public Dialog Build(Activity activity, ActionSheetConfig config)
        {
            this.Activity = activity;

            var di = new Dialog(activity);

            var windows = di.Window;

            // 1.
            windows.RequestFeature(WindowFeatures.NoTitle);

            // 2.
            var viewBuilder = new ActionSheetTheme1ViewBuilder(activity, config);
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


        public class ActionSheetTheme1ViewBuilder
        {
            public Action Clicked { get; set; }

            public Activity Activity { get; private set; }

            ActionSheetConfig _config;

            public ActionSheetTheme1ViewBuilder(Activity activity, ActionSheetConfig config)
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
                    container.AddView(GetHeaderText(_config.Title, _config.Message));
                    container.AddView(this.CreateDivider(1));
                }

                foreach (var item in _config.Items)
                {
                    container.AddView(this.CreateRow(item, false));

                    if (item != _config.Items.Last())
                        container.AddView(this.CreateDivider(1));
                }

                if (_config.Destructive != null || _config.Cancel != null)
                {
                    container.AddView(this.CreateDivider(15));
                }

                if (_config.Destructive != null)
                {
                    container.AddView(this.CreateRow(_config.Destructive, true));
                }
                if (_config.Cancel != null)
                {
                    container.AddView(this.CreateRow(_config.Cancel, false));
                }

                return container;
            }


            protected View CreateRow(ActionSheetItem item, bool isDestructive)
            {
                var row = new LinearLayout(this.Activity)
                {
                    Clickable = true,
                    Orientation = Orientation.Horizontal,
                    LayoutParameters = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, this.DpToPixels(48))
                };

                Color? textColor = null;
                if (item.TextColor != null)
                {
                    textColor = item.TextColor.Value.ToNativeColor();
                }

                row.AddView(this.GetText(item.Text, item.ItemIcon, isDestructive, textColor));

                // row.SetGravity(GravityFlags.CenterHorizontal | GravityFlags.CenterVertical);


                row.Click += (sender, args) =>
                {
                    item.Action?.Invoke();

                    Clicked?.Invoke();
                };

                return row;

            }

            protected virtual View GetHeaderText(string title, string message)
            {
                var row = new LinearLayout(this.Activity)
                {
                    Clickable = false,
                    Orientation = Orientation.Vertical,
                    LayoutParameters = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, this.DpToPixels(48))
                };

                var layout = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, this.DpToPixels(56))
                {
                    LeftMargin = this.DpToPixels(16),
                    RightMargin = this.DpToPixels(16),
                };

                var txt = new TextView(this.Activity)
                {
                    Text = title,
                    LayoutParameters = layout,
                    Gravity = GravityFlags.CenterVertical,
                };
                txt.SetTextSize(ComplexUnitType.Sp, 16);
                txt.Paint.FakeBoldText = true;
                txt.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;

                row.AddView(txt);

                if (!string.IsNullOrEmpty(message))
                {
                    var txt2 = new TextView(this.Activity)
                    {
                        Text = title,
                        LayoutParameters = layout,
                        Gravity = GravityFlags.CenterVertical,
                    };
                    txt2.SetTextSize(ComplexUnitType.Sp, 12);
                    txt2.Paint.FakeBoldText = true;
                    txt2.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;

                    row.AddView(txt2);
                }

                return row;
            }


            protected virtual TextView GetText(string text, string icon, bool isDestructive, Color? textColor)
            {
                var layout = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent)
                {
                    TopMargin = this.DpToPixels(8),
                    BottomMargin = this.DpToPixels(8),
                    LeftMargin = this.DpToPixels(16),
                    RightMargin = this.DpToPixels(16),
                };

                var txt = new TextViewFix(this.Activity)
                {
                    Text = text,
                    LayoutParameters = layout,
                    Gravity = GravityFlags.CenterVertical | GravityFlags.Left,
                };
                txt.SetTextSize(ComplexUnitType.Sp, 16);
                //  txt.SetBackgroundResource(global::Android.Resource.Attribute.SelectableItemBackground);

                if (!string.IsNullOrWhiteSpace(icon))
                {
                    var drawable = ImageLoader.Load(icon);
                    drawable.SetBounds(0, 0, this.DpToPixels(32), this.DpToPixels(32));
                    txt.SetCompoundDrawables(drawable, null, null, null);
                    txt.CompoundDrawablePadding = this.DpToPixels(6);

                    txt.CenterText = true;
                }

                txt.Gravity = GravityFlags.CenterHorizontal | GravityFlags.CenterVertical;

                if (textColor.HasValue)
                {
                    txt.SetTextColor(textColor.Value);
                }
                else
                {
                    txt.SetTextColor(Color.Rgb(73, 73, 73));
                }

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


            protected virtual View CreateDivider(int height = 1)
            {
                var view = new View(this.Activity)
                {
                    Background = new ColorDrawable(Color.Rgb(239, 239, 239)),
                    LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, this.DpToPixels(height))
                };
                view.SetPadding(0, this.DpToPixels(7), 0, this.DpToPixels(8));
                return view;
            }


            protected virtual int DpToPixels(int dp)
            {
                var value = TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, this.Activity.Resources.DisplayMetrics);
                return Convert.ToInt32(value);
            }



            public class TextViewFix : TextView
            {
                public bool CenterText { get; set; }

                public TextViewFix(Context context) : base(context)
                {
                }

                public TextViewFix(Context context, IAttributeSet attrs) : base(context, attrs)
                {
                }

                public TextViewFix(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
                {
                }

                public TextViewFix(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
                {
                }

                protected TextViewFix(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
                {
                }


                protected override void OnDraw(Canvas canvas)
                {
                    if (!CenterText)
                    {
                        base.OnDraw(canvas);
                        return;
                    }

                    Drawable[] drawables = GetCompoundDrawables();
                    if (drawables != null)
                    {
                        Drawable drawableLeft = drawables[0];
                        if (drawableLeft != null)
                        {
                            float textWidth = Paint.MeasureText(Text);
                            int drawablePadding = CompoundDrawablePadding;
                            int drawableWidth = drawableLeft.IntrinsicWidth;
                            float bodyWidth = textWidth + drawableWidth + drawablePadding;
                            canvas.Translate((Width - bodyWidth) / 2, 0);
                        }
                    }

                    base.OnDraw(canvas);
                }

            }
        }
    }
}
