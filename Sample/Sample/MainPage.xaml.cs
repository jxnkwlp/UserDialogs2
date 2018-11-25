using Passingwind.UserDialogs;
using Sample.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Alert_Clicked(object sender, EventArgs e)
        {
            // UserDialogs.Instance.Alert(DateTime.Now.ToString());
            UserDialogs.Instance.Alert(new AlertConfig(DateTime.Now.ToString()).AddOkButton());
        }

        private void Confirm_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.Alert(new AlertConfig("you confirm message")
                                              .AddOkButton(action: () =>
                                              {
                                                  // ok handle
                                              })
                                              .AddCancelButton()
                                              // .AddDestructiveButton("Delete")
                                              );
        }

        private void Toast_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.Toast(new ToastConfig()
            {
                Message = DateTime.Now.ToString(),

                Position = ToastPosition.Center,

                Duration = TimeSpan.FromSeconds(5),

                TextColor = Color.Red,
                BackgroundColor = Color.Yellow,

            });

            // await Navigation.PushAsync(new ToastPage());
        }

        private void Snackbar_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.Snackbar(new SnackbarConfig()
            {
                Message = DateTime.Now.ToString(),

                Duration = TimeSpan.FromSeconds(3),

                //TextColor = Color.BurlyWood,
                //BackgroundColor = Color.Yellow,

                ActionText = "ok",
                ActionTextColor = Color.Red,

                Action = () => UserDialogs.Instance.Toast("clicked")

            });

        }

        private void ActionSheet_Clicked(object sender, EventArgs e)
        {
            var config = new ActionSheetConfig
            {
                Title = "Title",
                Message = "Message",

                BottomSheet = false,
            };

            // c.ItemTextAlgin = ActionSheetItemTextAlgin.Center;

            //config.AddItem("item1_icon", icon: "ic_3d_rotation_black_24dp");
            //config.AddItem("item2_icon", icon: "ic_android_black_24dp");
            //config.AddItem("item3_icon", icon: "ic_credit_card_black_24dp");

            config.AddItem("item1");
            config.AddItem("item2");
            config.AddItem("item3", action: () => ToastShow("item3"));


            config.AddCancel(action: () => ToastShow("cancel"));
            config.AddDestructive(action: () => ToastShow("destructive"));

            UserDialogs.Instance.ActionSheet(config);

        }

        void ToastShow(string text)
        {
            UserDialogs.Instance.Toast(text);
        }

        private void Loading_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.Loading(new LoadingConfig("please wait")
            {
                Cancellable = true,
                CancelAction = () => ToastShow("you canceled."),

                Duration = TimeSpan.FromSeconds(5),

                MarkType = MarkType.Black,

            });
        }

        private void Progress_Clicked(object sender, EventArgs e)
        {
            var dialog = UserDialogs.Instance.Progress(new ProgressConfig("download...")
            {
                Cancellable = true,
                CancelAction = () => ToastShow("you canceled."),

            });

            uint i = 1;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                i += 10;

                dialog.SetProgress(i);

                return i <= 100;
            });


        }


    }
}
