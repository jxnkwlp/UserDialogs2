﻿using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AlertDialog = Android.App.AlertDialog;
using AppCompatAlertDialog = Android.Support.V7.App.AlertDialog;

namespace Passingwind.UserDialogs.Platforms
{
    public class ActionSheetBuilder
    {
        //private View GetCustomTitle(Activity activity, ActionSheetConfig config)
        //{
        //    LinearLayout parent = new LinearLayout(activity);

        //    var titleView = new TextView(activity);
        //    titleView.Text = config.Title;

        //    parent.AddView(titleView, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));

        //    return parent;
        //}

        public Dialog Build(Activity activity, ActionSheetConfig config)
        {
            var dialog = new AlertDialog.Builder(activity, config.AndroidStyleId ?? 0)
                .SetTitle(config.Title);


            if (config.Cancel != null)
            {
                dialog.SetNeutralButton(config.Cancel.Text, (s, a) =>
                {
                    config.Cancel.Action?.Invoke();
                });
            }

            if (config.Destructive != null)
            {
                dialog.SetNegativeButton(config.Destructive.Text, (s, a) =>
                {
                    config.Destructive.Action?.Invoke();
                });
            }


            if (config.Options != null && config.Options.Count > 0)
            {
                if (config.Options.All(t => t.ItemIcon == null))
                {
                    var array = config.Options.Select(t => t.Text).ToArray();

                    dialog.SetItems(array, (s, a) =>
                    {
                        config.Options[a.Which].Action?.Invoke();
                    });
                }
                else
                {
                    var adapter = new ActionSheetListAdapter(activity, global::Android.Resource.Layout.SelectDialogItem, global::Android.Resource.Id.Text1, config);

                    dialog.SetAdapter(adapter, (s, a) =>
                    {
                        config.Options[a.Which].Action?.Invoke();
                    });
                }
            }

            return dialog.Create();
        }

        public Dialog Build(AppCompatActivity activity, ActionSheetConfig config)
        {
            var dialog = new AppCompatAlertDialog.Builder(activity, config.AndroidStyleId ?? 0)
                .SetTitle(config.Title);


            if (config.Cancel != null)
            {
                dialog.SetNeutralButton(config.Cancel.Text, (s, a) =>
                {
                    config.Cancel.Action?.Invoke();
                });
            }

            if (config.Destructive != null)
            {
                dialog.SetNegativeButton(config.Destructive.Text, (s, a) =>
                {
                    config.Destructive.Action?.Invoke();
                });
            }


            if (config.Options != null && config.Options.Count > 0)
            {
                if (config.Options.Any(t => t.ItemIcon != null))
                {
                    var adapter = new ActionSheetListAdapter(activity, global::Android.Resource.Layout.SelectDialogItem, global::Android.Resource.Id.Text1, config);

                    dialog.SetAdapter(adapter, (s, a) =>
                    {
                        config.Options[a.Which].Action?.Invoke();
                    });
                }
                else
                {
                    var array = config.Options.Select(t => t.Text).ToArray();

                    dialog.SetItems(array, (s, a) =>
                    {
                        config.Options[a.Which].Action?.Invoke();
                    });
                }
            }

            return dialog.Create();
        }

    }

    public class ActionSheetListAdapter : ArrayAdapter<ActionSheetOption>
    {
        private Context _context;
        private int _resource;
        private ActionSheetConfig _config;

        public bool AddMarginForImage { get; set; } = true;

        public ActionSheetListAdapter(Context context, int resource, int textViewResourceId, ActionSheetConfig config) : base(context, resource, textViewResourceId, config.Options)
        {
            this._context = context;
            this._config = config;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);

            var textView = view.FindViewById<TextView>(global::Android.Resource.Id.Text1);

            var item = _config.Options[position];

            textView.Text = item.Text;

            if (item.ItemIcon != null)
            {
                var icon = ImageLoader.Load(item.ItemIcon);
                if (item.IconPosition == ActionSheetOption.ItemIconPosition.Left)
                    textView.SetCompoundDrawablesWithIntrinsicBounds(icon, null, null, null);
                else if (item.IconPosition == ActionSheetOption.ItemIconPosition.Right)
                    textView.SetCompoundDrawablesWithIntrinsicBounds(null, null, icon, null);

                if (AddMarginForImage)
                {
                    //Add margin between image and text (support various screen densities)
                    var dp = (int)(10 * parent.Context.Resources.DisplayMetrics.Density + 0.5f);
                    textView.CompoundDrawablePadding = dp;
                }
            }

            return view;
        }

    }
}