using Passingwind.UserDialogs;
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await UserDialogs.Instance.AlertAsync(new AlertConfig("you title", "message: " + DateTime.Now.ToString()));
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            // await UserDialogs.Instance.ToastAsync(new ToastConfig(DateTime.Now.ToString()));
            await UserDialogs.Instance.ToastAsync(new ToastConfig(DateTime.Now.ToString()).SetStyle(ToastStyle.Snackbar));
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading(new LoadingConfig("loading..."));
        }
    }
}
