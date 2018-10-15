using Passingwind.UserDialogs;
using Xamarin.Forms;

namespace Sample.ViewModels
{
    public class AlertViewModel : BaseViewModel
    {
        private AlertConfig _alertConfig = new AlertConfig();
        private string _title;
        private string _okText;
        private string _message;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); _alertConfig.Title = value; }
        }

        public string OkText
        {
            get { return _okText; }
            set { SetProperty(ref _okText, value); _alertConfig.OkText = value; }
        }

        public string Message
        {
            get => _message; set { SetProperty(ref _message, value); _alertConfig.Message = value; }
        }


        public AlertViewModel()
        {
            _alertConfig.OkAction = () =>
            {
                this.UserDialog.Toast(new ToastConfig("click ok button"));
            };
        }


        public Command OkCommand => new Command(() =>
        {

            UserDialog.Alert(_alertConfig);

        });

    }
}
