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
            // await Navigation.PushAsync(new AlertPage());

            UserDialogs.Instance.Alert(DateTime.Now.ToString());

        }

        private void Toast_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.Toast(new ToastConfig()
            {
                Message = DateTime.Now.ToString(),
            });

            // await Navigation.PushAsync(new ToastPage());
        }

        private void Snackbar_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.Snackbar(new SnackbarConfig()
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

                MarkType = MarkType.Black,

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



    }
}
