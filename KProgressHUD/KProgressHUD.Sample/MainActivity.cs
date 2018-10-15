using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Java.Lang;
using Android.Graphics;
using System;

namespace KProgressHUD.Sample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        KProgressHUD hud = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            FindViewById<Button>(Resource.Id.indeterminate).Click += (sender, e) =>
            {
                hud = KProgressHUD
                .Create(this, KProgressHUD.Style.SpinIndeterminate)
                .Show();

                AutoClose();
            };

            FindViewById<Button>(Resource.Id.label_indeterminate).Click += (sender, e) =>
            {
                hud = KProgressHUD
                .Create(this, KProgressHUD.Style.SpinIndeterminate)
                .SetLabel("please wait ...")
                .SetCancelAction(() =>
                {
                    Toast.MakeText(this, "You cancelled manually!", ToastLength.Short).Show();
                })
                .Show();

                AutoClose();
            };


            FindViewById<Button>(Resource.Id.detail_indeterminate).Click += (sender, e) =>
            {
                hud = KProgressHUD
                .Create(this, KProgressHUD.Style.SpinIndeterminate)
                .SetLabel("please wait ...")
                .SetDetailsLabel("Downloading data")
                .Show();

                AutoClose();
            };


            FindViewById<Button>(Resource.Id.grace_indeterminate).Click += (sender, e) =>
            {
                hud = KProgressHUD
                .Create(this, KProgressHUD.Style.SpinIndeterminate)
                .SetGraceTime(1000)
                .SetLabel("please wait ...")
                .Show();

                AutoClose();
            };


            FindViewById<Button>(Resource.Id.determinate).Click += (sender, e) =>
            {
                hud = KProgressHUD
                .Create(this, KProgressHUD.Style.PieDeterminate)
                .SetLabel("please wait ...")
                .Show();

                SimulateProgressUpdate();
            };


            FindViewById<Button>(Resource.Id.annular_determinate).Click += (sender, e) =>
            {
                hud = KProgressHUD
                .Create(this, KProgressHUD.Style.AnnularDeterminate)
                .SetLabel("please wait ...")
                .Show();

                SimulateProgressUpdate();
            };


            FindViewById<Button>(Resource.Id.bar_determinate).Click += (sender, e) =>
            {
                hud = KProgressHUD
                .Create(this, KProgressHUD.Style.BarDeterminate)
                .SetLabel("please wait ...")
                .Show();

                SimulateProgressUpdate();
            };


            FindViewById<Button>(Resource.Id.dim_background).Click += (sender, e) =>
            {
                hud = KProgressHUD
                .Create(this, KProgressHUD.Style.SpinIndeterminate)
                .SetDimAmount(0.5f)
                .Show();

                AutoClose();
            };


            FindViewById<Button>(Resource.Id.custom_color_animate).Click += (sender, e) =>
            {
                hud = KProgressHUD
                .Create(this, KProgressHUD.Style.SpinIndeterminate)
                .SetBackgroundColor(Color.Red)
                .SetAnimationSpeed(2)
                .Show();

                AutoClose();
            };

        }

        int currentProgress = 0;

        private void SimulateProgressUpdate()
        {
            hud.SetMaxProgress(100);
            currentProgress = 0;

            Handler handler = new Handler();
             
            Run(handler);

        }

        /// <summary>
        /// 
        /// </summary>
        private void Run(Handler handler)
        {
            currentProgress += 1;

            hud.SetProgress(currentProgress);

            if (currentProgress == 80)
            {
                hud.SetLabel("Almost finish...");
            }

            if (currentProgress < 100)
            {
                handler.PostDelayed(() => Run(handler), 10);
            }
        }

        private void AutoClose()
        {
            if (hud == null)
                return;

            Handler handler = new Handler();
            handler.PostDelayed(new Runnable(() =>
            {
                hud.Dismiss();
            }), 2000);
        }
    }
}