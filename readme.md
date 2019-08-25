
# UserDialogs

Common UserDialogs interface for xamarin.forms(android/ios).

# Component

1.  [x] Alert
2.  [x] Confirm
3.  [x] Toast
4.  [x] ActionSheet
5.  [x] Loading
6.  [x] Progress
7.  [ ] DateTimePicker
8.  [ ] OptionsPicker
9.  [x] Prompt
10. [x] Prompt with Multi-line (eg: Login Prompt ) 

# Support Platforms

1. Android 7.1+
2. iOS 10+
3. NET Standard 2.0

# Setup

Install nuget package in share/standard and platform projects. [Passingwind.UserDialogs](https://www.nuget.org/packages/Passingwind.UserDialogs/)

~~~~ powershell
Install-Package Passingwind.UserDialogs
~~~~

a. Android Initialization (In your main activity)  

~~~~ csharp
UserDialogs.Init(this);
~~~~

b. iOS (nothing to do)

# Interface

~~~~ csharp
void Toast(string message);

void Toast(ToastConfig config);

IDisposable Snackbar(string message, Action action = null);

IDisposable Snackbar(SnackbarConfig config);

void Alert(string message);

void Alert(AlertConfig config);

IDisposable ActionSheet(ActionSheetConfig config);

IDisposable Loading(LoadingConfig config);

IProgressDialog Progress(ProgressConfig config);

void Prompt(PromptConfig config);

void Form(PromptFormConfig config);

~~~~

# Useage

1. Alert

    Set default field:

    ~~~~ csharp
    AlertConfig.DefaultOkText = "是"; // default 'yes'
    // AlertConfig.DefaultCancelText
    ~~~~

    Alert message:

    ~~~~ csharp
    UserDialogs.Instance.Alert("you message");

    UserDialogs.Instance.Alert(new AlertConfig("you message").AddOkButton());
    ~~~~

    Confirm Message:

    ~~~~ csharp
    UserDialogs.Instance.Alert(new AlertConfig("you confirm message")
                                            .AddOkButton(action: () => {
                                                // ok handle
                                            })
                                            .AddCancelButton());
    ~~~~

2. Toast
  
    ~~~~ csharp
    UserDialogs.Instance.Toast("you message");
    ~~~~

3. Snackbar

    Set default field：

    ~~~~ csharp
    SnackbarConfig.DefaultTimeSpan = TimeSpan.FromSeconds(1.2); // default 1.2s
    SnackbarConfig.DefaultActionText = null;
    SnackbarConfig.DefaultBackgroundColor = null;
    SnackbarConfig.DefaultTextColor = null;
    SnackbarConfig.DefaultActionTextColor = null;
    ~~~~
  
    ~~~~ csharp
    UserDialogs.Instance.Snackbar(new SnackbarConfig()
    {
        Message = "well down !!!",
        //Duration = TimeSpan.FromSeconds(3),
        //TextColor = Color.BurlyWood,
        //BackgroundColor = Color.Yellow,
        //ActionText = "sure",
        //ActionTextColor = Color.Red,

        Action = () => {
            // you handle here
         }
    });
    ~~~~

4. ActionSheet

    Set default field：

    ~~~~ csharp
    ActionSheetConfig.DefaultCancelText = "Cancel"; // default 'Cancel'
    ActionSheetConfig.DefaultDestructiveText = "Remove"; // default 'Remove'
    ActionSheetConfig.DefaultBottomSheet = false;  // default false
    ~~~~
  
    ~~~~ csharp
    var config = new ActionSheetConfig
    {
        Title = "Title",
        Message = "you message",

        // BottomSheet = false,
        // ItemTextAlgin = ActionSheetItemTextAlgin.Center;
    };
  
    c.AddItem("item1");
    c.AddItem("item2");
    c.AddItem("item3", action: () => {
        // you handle 
    });

    //config.AddCancel();
    //config.AddDestructive();

    UserDialogs.Instance.ActionSheet(config);
    ~~~~

5. Loading

    Show loading hub when task start.

    ~~~~ csharp
    var dialog = UserDialogs.Instance.Loading(new LoadingConfig("please wait")
        {
            Cancellable = true, // can cancel loading when show loading
            CancelAction = () => // you action handle 

            Duration = TimeSpan.FromSeconds(5), // show loading duration second

            // MarkType = MarkType.Black 
        });

    // close dialog if you need.
    dialog.Dispose();
    ~~~~

6. Progress

    show progress hub.

    ~~~~ csharp
    var dialog = UserDialogs.Instance.Progress(new ProgressConfig("download...")
    {
        Cancellable = true, // can cancel loading when show loading 
        CancelAction = () => // you action handle

    });

    // set current progress value when task update
    dialog.SetProgress(30);   // 1-100

    // close dialog if you need.
    dialog.Hide();
    ~~~~


7. Form

    show prompt form 
    
    ~~~~ csharp
    UserDialogs.Instance.Form(new PromptFormConfig()
    {
        Title = "Please Login", 

        OnAction = (result) =>
        {
            if(result.Ok){ 
                var username = result.Result["username"]; 
                // then you code ...
            } 
        }
    }
    .AddItem("username", "username", maxLength: 16) // add 'username' inputbox
    .AddItem("password", "password", InputType.Password, maxLength: 6) // add 'password' password inputbox
    );
    ~~~~
  

# Powered By

1. Android- Progress/Loading uses [KProgressHUD](https://github.com/jxnkwlp/KProgressHUD.csharp)
2. iOS - Progress/Loading/Toast uses  [MBProgressHUD](https://github.com/ricardo-ch/xamarin-mbprogresshud-ios/)
3. iOS - Snackbar uses  [TTGSnackbar](https://github.com/MarcBruins/TTGSnackbar-Xamarin-iOS)
