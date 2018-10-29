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

        private async void Alert_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlertPage());
        }

        private void Toast_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.Toast(new ToastOptions()
            {
                Message = DateTime.Now.ToString(),
            });

            // await Navigation.PushAsync(new ToastPage());
        }

        private void Snackbar_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.Snackbar(new SnackbarOptions()
            {
                Message = DateTime.Now.ToString(),

                Duration = TimeSpan.FromSeconds(3),

                TextColor = Color.BurlyWood,
                BackgroundColor = Color.Yellow,

                ActionText = "ok",
                ActionTextColor = Color.Red,

                Action = () => UserDialogs.Instance.Toast("clicked")

            });

        }

        private void ActionSheet_Clicked(object sender, EventArgs e)
        {
            var c = new ActionSheetOptions();

            c.Title = "Title";
            c.Message = "Message";
            // c.BottomSheet = true;

            // c.ItemTextAlgin = ActionSheetItemTextAlgin.Center;

            c.AddItem("item1_icon", icon: "ic_3d_rotation_black_24dp");
            c.AddItem("item2_icon", icon: "ic_android_black_24dp");
            c.AddItem("item3_icon", icon: "ic_credit_card_black_24dp");

            //c.AddItem("item1");
            //c.AddItem("item2");
            //c.AddItem("item3", action: () => ToastShow("item3"));

            c.SetCancel(action: () => ToastShow("cancel"));
            c.SetDestructive(action: () => ToastShow("destructive"));



            UserDialogs.Instance.ActionSheet(c);

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

                MarkType = MarkType.Blank,

            });
        }

        private void Progress_Clicked(object sender, EventArgs e)
        {
            var dialog = UserDialogs.Instance.Progress(new ProgressConfig("download...")
            {
                Cancellable = true,
                CancelAction = () => ToastShow("you canceled."),

                Max = 10,
                Start = 0,
            });

            int i = 1;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                dialog.SetProgress(i++);

                return i <= 10;
            });


        }




        //private void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    // await UserDialogs.Instance.ToastAsync(new ToastConfig(DateTime.Now.ToString()));
        //    UserDialogs.Instance.Toast(new ToastConfig(DateTime.Now.ToString()).SetStyle(ToastStyle.Snackbar));
        //}

        //private void Button_Clicked_2(object sender, EventArgs e)
        //{
        //    var loading = UserDialogs.Instance.Loading(new LoadingConfig("loading...delay 5s "));

        //    Device.StartTimer(TimeSpan.FromSeconds(5), () =>
        //    {
        //        loading.Dispose();

        //        return false;
        //    });
        //}

        //private void Button_Clicked_3(object sender, EventArgs e)
        //{
        //    var config = new ConfirmConfig("标题", "这是提示文本" + DateTime.Now.ToString())
        //        .SetAction(() =>
        //        {
        //            UserDialogs.Instance.Toast(new ToastConfig("you click ok action"));
        //        })
        //        .SetCancelAction(() =>
        //        {
        //            UserDialogs.Instance.Toast(new ToastConfig("cancaled"));
        //        });

        //    var dis = UserDialogs.Instance.Confirm(config);

        //    Device.StartTimer(TimeSpan.FromSeconds(5), () =>
        //    {
        //        dis.Dispose();

        //        return false;
        //    });
        //}

        //private void ActionSheet_Clicked(object sender, EventArgs e)
        //{
        //    var config = new ActionSheetConfig()
        //    {
        //        Title = "title",

        //        BottomSheet = true,
        //    }
        //    .SetCancel(action: () => Toast("canceled"))
        //    .SetDestructive(action: () => Toast("destructived"))
        //    .AddOption("options1", () => Toast("options1 clicked"), "ic_android_black_24dp")
        //    .AddOption("options2", () => Toast("options2 clicked"), "ic_3d_rotation_black_24dp")
        //    .AddOption("options3")
        //    ;

        //    UserDialogs.Instance.ActionSheet(config);
        //}


        //void Toast(string message)
        //{
        //    UserDialogs.Instance.Toast(new ToastConfig(message));
        //}


        //private void Progress_Clicked(object sender, EventArgs e)
        //{
        //    int count = 0;

        //    var hud = UserDialogs.Instance.Progress(new ProgressConfig("download...")
        //    {
        //        Max = 100,
        //        Cancellable = true,
        //        CancelAction = () =>
        //        {
        //            Toast("canced");
        //        }
        //    });

        //    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        //    {
        //        if (count > 100)
        //        {
        //            hud.Hide();

        //            return false;
        //        }

        //        count += 10;

        //        hud.SetProgress(count);

        //        return true;
        //    });
        //}
    }
}
