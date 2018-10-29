using System;
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
        public Dialog Build(Activity activity, ActionSheetOptions config)
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


            if (config.Items != null && config.Items.Count > 0)
            {
                if (config.Items.Any(t => t.ItemIcon != null))
                {
                    var array = config.Items.Select(t => t.Text).ToArray();

                    dialog.SetItems(array, (s, a) =>
                    {
                        config.Items[a.Which].Action?.Invoke();
                    });
                }
                else
                {
                    var adapter = new ActionSheetListAdapter(activity, global::Android.Resource.Layout.SelectDialogItem, global::Android.Resource.Id.Text1, config);

                    dialog.SetAdapter(adapter, (s, a) =>
                    {
                        config.Items[a.Which].Action?.Invoke();
                    });
                }
            }

            return dialog.Create();
        }

        public Dialog Build(AppCompatActivity activity, ActionSheetOptions config)
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


            if (config.Items != null && config.Items.Count > 0)
            {
                if (config.Items.Any(t => t.ItemIcon != null))
                {
                    var adapter = new ActionSheetListAdapter(activity, global::Android.Resource.Layout.SelectDialogItem, global::Android.Resource.Id.Text1, config);

                    dialog.SetAdapter(adapter, (s, a) =>
                    {
                        config.Items[a.Which].Action?.Invoke();
                    });
                }
                else
                {
                    var array = config.Items.Select(t => t.Text).ToArray();

                    dialog.SetItems(array, (s, a) =>
                    {
                        config.Items[a.Which].Action?.Invoke();
                    });

                }
            }

            return dialog.Create();
        }

    }

    public class ActionSheetListAdapter : ArrayAdapter<ActionSheetItemOption>
    {
        private Context _context;
        private int _resource;
        private ActionSheetOptions _config;

        public bool AddMarginForImage { get; set; } = true;

        public ActionSheetListAdapter(Context context, int resource, int textViewResourceId, ActionSheetOptions config) : base(context, resource, textViewResourceId, config.Items)
        {
            this._context = context;
            this._config = config;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);

            var textView = view.FindViewById<TextView>(global::Android.Resource.Id.Text1);

            var item = _config.Items[position];

            textView.Text = item.Text;

            if (item.ItemIcon != null)
            {
                var icon = ImageLoader.Load(item.ItemIcon);
                if (item.IconPosition == ActionSheetItemOption.ItemIconPosition.Left)
                    textView.SetCompoundDrawablesWithIntrinsicBounds(icon, null, null, null);
                else if (item.IconPosition == ActionSheetItemOption.ItemIconPosition.Right)
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
