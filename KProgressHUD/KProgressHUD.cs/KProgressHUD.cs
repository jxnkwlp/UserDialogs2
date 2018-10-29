/*
 *    Copyright 2015 Kaopiz Software Co., Ltd.
 *
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 */

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;

namespace KProgressHUDLib
{
    /// <summary>
    /// KProgressHUD
    /// </summary>
    public class KProgressHUD
    {
        public enum Style
        {
            SpinIndeterminate,
            PieDeterminate,
            AnnularDeterminate,
            BarDeterminate
        }

        // To avoid redundant APIs, make the HUD as a wrapper class around a Dialog
        private ProgressDialog mProgressDialog;

        private float mDimAmount;
        private Color mWindowColor;
        private float mCornerRadius;
        private Context mContext;

        private int mAnimateSpeed;

        private int mMaxProgress;
        private bool mIsAutoDismiss;

        private int mGraceTimeMs;
        private Handler mGraceTimer;
        private bool mFinished;

        /// <summary> 
        /// </summary>
        public KProgressHUD(Context context)
        {
            mContext = context;
            mProgressDialog = new ProgressDialog(context, this);
            mDimAmount = 0;
            //noinspection deprecation
            mWindowColor = context.Resources.GetColor(Resource.Color.kprogresshud_default_color);
            mAnimateSpeed = 1;
            mCornerRadius = 10;
            mIsAutoDismiss = true;
            mGraceTimeMs = 0;
            mFinished = false;

            SetStyle(Style.SpinIndeterminate);
        }

        /// <summary>
        /// Create a new HUD. Have the same effect as the constructor.
        /// For convenient only. </summary>
        /// <param name="context"> Activity context that the HUD bound to </param>
        /// <returns> An unique HUD instance </returns>
        public static KProgressHUD Create(Context context)
        {
            return new KProgressHUD(context);
        }

        /// <summary>
        /// Create a new HUD. specify the HUD style (if you use a custom view, you need {@code KProgressHUD.create(Context context)}).
        /// </summary>
        /// <param name="context"> Activity context that the HUD bound to </param>
        /// <param name="style"> One of the KProgressHUD.Style values </param>
        /// <returns> An unique HUD instance </returns>

        public static KProgressHUD Create(Context context, Style style)
        {
            return new KProgressHUD(context).SetStyle(style);
        }

        /// <summary>
        /// Specify the HUD style (not needed if you use a custom view) </summary>
        /// <param name="style"> One of the KProgressHUD.Style values </param>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetStyle(Style style)
        {
            View view = null;
            switch (style)
            {
                case Style.SpinIndeterminate:
                    view = new SpinView(mContext);
                    break;

                case Style.PieDeterminate:
                    view = new PieView(mContext);
                    break;

                case Style.AnnularDeterminate:
                    view = new AnnularView(mContext);
                    break;

                case Style.BarDeterminate:
                    view = new BarView(mContext);
                    break;
                    // No custom view style here, because view will be added later
            }

            mProgressDialog.SetView(view);
            return this;
        }

        /// <summary>
        /// Specify the dim area around the HUD, like in Dialog </summary>
        /// <param name="dimAmount"> May take value from 0 to 1. Default to 0 (no dimming) </param>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetDimAmount(float dimAmount)
        {
            if (dimAmount >= 0 && dimAmount <= 1)
            {
                mDimAmount = dimAmount;
            }
            return this;
        }

        /// <summary>
        /// Set HUD size. If not the HUD view will use WRAP_CONTENT instead </summary>
        /// <param name="width"> in dp </param>
        /// <param name="height"> in dp </param>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetSize(int width, int height)
        {
            mProgressDialog.SetSize(width, height);
            return this;
        }

        ///**
        // * @deprecated  As of release 1.1.0, replaced by {@link #SetBackgroundColor(int)}
        // * @param color ARGB color
        // * @return Current HUD
        // */
        //@Deprecated
        //public KProgressHUD SetWindowColor(int color)
        //{
        //    mWindowColor = color;
        //    return this;
        //}

        /// <summary>
        /// Specify the HUD background color </summary>
        /// <param name="color"> ARGB color </param>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetBackgroundColor(Color color)
        {
            mWindowColor = color;
            return this;
        }

        /// <summary>
        /// Specify corner radius of the HUD (default is 10) </summary>
        /// <param name="radius"> Corner radius in dp </param>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetCornerRadius(float radius)
        {
            mCornerRadius = radius;
            return this;
        }

        /// <summary>
        /// Change animation speed relative to default. Used with indeterminate style </summary>
        /// <param name="scale"> Default is 1. If you want double the speed, Set the param at 2. </param>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetAnimationSpeed(int scale)
        {
            mAnimateSpeed = scale;
            return this;
        }

        /// <summary>
        /// Optional label to be displayed. </summary>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetLabel(string label)
        {
            mProgressDialog.SetLabel(label);
            return this;
        }

        /// <summary>
        /// Optional label to be displayed </summary>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetLabel(string label, Color color)
        {
            mProgressDialog.SetLabel(label, color);
            return this;
        }

        /// <summary>
        /// Optional detail description to be displayed on the HUD </summary>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetDetailsLabel(string detailsLabel)
        {
            mProgressDialog.SetDetailsLabel(detailsLabel);
            return this;
        }

        /// <summary>
        /// Optional detail description to be displayed </summary>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetDetailsLabel(string detailsLabel, Color color)
        {
            mProgressDialog.SetDetailsLabel(detailsLabel, color);
            return this;
        }

        /// <summary>
        /// Max value for use in one of the determinate styles </summary>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetMaxProgress(int maxProgress)
        {
            mMaxProgress = maxProgress;
            mProgressDialog.SetMaxProgressValue(maxProgress);
            return this;
        }

        /// <summary>
        /// Set current progress. Only have effect when use with a determinate style, or a custom
        /// view which , Determinate interface.
        /// </summary>
        public void SetProgress(int progress)
        {
            mProgressDialog.SetProgress(progress);
        }

        /// <summary>
        /// Provide a custom view to be displayed. </summary>
        /// <param name="view"> Must not be null </param>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetCustomView(View view)
        {
            if (view != null)
            {
                mProgressDialog.SetView(view);
            }
            else
            {
                throw new Java.Lang.Exception("Custom view must not be null!");
            }
            return this;
        }

        /// <summary>
        /// Specify whether this HUD can be cancelled by using back button (default is false)
        /// Setting a cancelable to true with this method will Set a null callback,
        /// clearing any callback previously Set with <seealso cref="bool"/>
        /// </summary>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetCancellable(bool isCancellable)
        {
            mProgressDialog.SetCancelable(isCancellable);
            mProgressDialog.SetOnCancelListener(null);
            return this;
        }

        /// <summary>
        /// Specify a callback to run when using the back button (default is null)
        /// </summary>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetCancelListener(IDialogInterfaceOnCancelListener listener)
        {
            mProgressDialog.SetCancelable(null != listener);
            mProgressDialog.SetOnCancelListener(listener);
            return this;
        }

        /// <summary>
        ///  Specify a callback to run when using the back button (default is null)
        /// </summary>
        public KProgressHUD SetCancelAction(Action action)
        {
            return SetCancelListener(new DefaultOnCancelListener(action));
        }

        private class DefaultOnCancelListener : Java.Lang.Object, IDialogInterfaceOnCancelListener
        {
            private readonly Action _action;

            public DefaultOnCancelListener(Action action)
            {
                _action = action;
            }

            public void OnCancel(IDialogInterface dialog)
            {
                _action?.Invoke();
            }
        }

        /// <summary>
        /// Specify whether this HUD closes itself if progress reaches max. Default is true. </summary>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetAutoDismiss(bool isAutoDismiss)
        {
            mIsAutoDismiss = isAutoDismiss;
            return this;
        }

        /// <summary>
        /// Grace period is the time (in milliseconds) that the invoked method may be run without
        /// showing the HUD. If the task finishes before the grace time runs out, the HUD will
        /// not be shown at all.
        /// This may be used to prevent HUD display for very short tasks.
        /// Defaults to 0 (no grace time). </summary>
        /// <param name="graceTimeMs"> Grace time in milliseconds </param>
        /// <returns> Current HUD </returns>
        public KProgressHUD SetGraceTime(int graceTimeMs)
        {
            mGraceTimeMs = graceTimeMs;
            return this;
        }

        /// <summary>
        ///  show the hub
        /// </summary>
        /// <returns></returns>
        public KProgressHUD Show()
        {
            if (!IsShowing)
            {
                mFinished = false;
                if (mGraceTimeMs == 0)
                {
                    mProgressDialog.Show();
                }
                else
                {
                    mGraceTimer = new Handler();
                    mGraceTimer.PostDelayed(new Runnable(() =>
                    {
                        if (mProgressDialog != null && !mFinished)
                        {
                            mProgressDialog.Show();
                        }
                    }), mGraceTimeMs);
                }
            }

            return this;
        }

        /// <summary>
        ///  the hud is show
        /// </summary>
        public bool IsShowing
        {
            get
            {
                return mProgressDialog != null && mProgressDialog.IsShowing;
            }
        }

        /// <summary>
        ///  close
        /// </summary>
        public void Dismiss()
        {
            mFinished = true;
            if (mContext != null && !((Activity)mContext).IsFinishing && mProgressDialog != null && mProgressDialog.IsShowing)
            {
                mProgressDialog.Dismiss();
            }
            if (mGraceTimer != null)
            {
                mGraceTimer.RemoveCallbacksAndMessages(null);
                mGraceTimer = null;
            }
        }

        private class ProgressDialog : Dialog
        {
            private KProgressHUD _instance;

            private IDeterminate mDeterminateView;
            private IIndeterminate mIndeterminateView;
            private View mView;
            private TextView mLabelText;
            private TextView mDetailsText;
            private string mLabel;
            private string mDetailsLabel;
            private FrameLayout mCustomViewContainer;
            private BackgroundLayout mBackgroundLayout;
            private int mWidth, mHeight;
            private Color mLabelColor = Color.White;
            private Color mDetailColor = Color.White;

            public ProgressDialog(Context context, KProgressHUD progressHUD) : base(context)
            {
                _instance = progressHUD;
            }

            protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                RequestWindowFeature((int)WindowFeatures.NoTitle);
                SetContentView(Resource.Layout.kprogresshud_hud);

                Window window = Window;
                window.SetBackgroundDrawable(new ColorDrawable());
                window.AddFlags(WindowManagerFlags.DimBehind);
                // var layoutParams = window.Attributes;
                window.Attributes.DimAmount = _instance.mDimAmount;
                window.Attributes.Gravity = GravityFlags.Center;
                //window.Attributes = layoutParams;

                SetCanceledOnTouchOutside(false);

                InitViews();
            }

            private void InitViews()
            {
                mBackgroundLayout = (BackgroundLayout)FindViewById(Resource.Id.background);
                mBackgroundLayout.SetBaseColor(_instance.mWindowColor);
                mBackgroundLayout.SetCornerRadius(_instance.mCornerRadius);
                if (mWidth != 0)
                {
                    UpdateBackgroundSize();
                }

                mCustomViewContainer = (FrameLayout)FindViewById(Resource.Id.container);
                AddViewToFrame(mView);

                if (mDeterminateView != null)
                {
                    mDeterminateView.SetMax(_instance.mMaxProgress);
                }

                if (mIndeterminateView != null)
                {
                    mIndeterminateView.SetAnimationSpeed(_instance.mAnimateSpeed);
                }

                mLabelText = (TextView)FindViewById(Resource.Id.label);
                SetLabel(mLabel, mLabelColor);
                mDetailsText = (TextView)FindViewById(Resource.Id.details_label);
                SetDetailsLabel(mDetailsLabel, mDetailColor);
            }

            private void AddViewToFrame(View view)
            {
                if (view == null) return;
                int wrapParam = ViewGroup.LayoutParams.WrapContent;
                ViewGroup.LayoutParams @params = new ViewGroup.LayoutParams(wrapParam, wrapParam);
                mCustomViewContainer.AddView(view, @params);
            }

            private void UpdateBackgroundSize()
            {
                ViewGroup.LayoutParams @params = mBackgroundLayout.LayoutParameters;
                @params.Width = Helper.DpToPixel(mWidth, Context);
                @params.Height = Helper.DpToPixel(mHeight, Context);
                mBackgroundLayout.LayoutParameters = @params;
            }

            public void SetProgress(int progress)
            {
                if (mDeterminateView != null)
                {
                    mDeterminateView.SetProgress(progress);

                    if (_instance.mIsAutoDismiss && progress >= _instance.mMaxProgress)
                    {
                        _instance.Dismiss();
                    }
                }
            }

            public void SetMaxProgressValue(int value)
            {
                if (mDeterminateView != null)
                {
                    mDeterminateView.SetMax(value);
                }
            }

            public void SetView(View view)
            {
                if (view != null)
                {
                    if (view is IDeterminate)
                    {
                        mDeterminateView = (IDeterminate)view;
                    }
                    if (view is IIndeterminate)
                    {
                        mIndeterminateView = (IIndeterminate)view;
                    }
                    mView = view;
                    if (IsShowing)
                    {
                        mCustomViewContainer.RemoveAllViews();
                        AddViewToFrame(view);
                    }
                }
            }

            public void SetLabel(string label)
            {
                mLabel = label;
                if (mLabelText != null)
                {
                    if (label != null)
                    {
                        mLabelText.Text = (label);
                        mLabelText.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        mLabelText.Visibility = ViewStates.Gone;
                    }
                }
            }

            public void SetDetailsLabel(string detailsLabel)
            {
                mDetailsLabel = detailsLabel;
                if (mDetailsText != null)
                {
                    if (detailsLabel != null)
                    {
                        mDetailsText.Text = (detailsLabel);
                        mDetailsText.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        mDetailsText.Visibility = ViewStates.Gone;
                    }
                }
            }

            public void SetLabel(string label, Color color)
            {
                mLabel = label;
                mLabelColor = color;
                if (mLabelText != null)
                {
                    if (label != null)
                    {
                        mLabelText.Text = (label);
                        mLabelText.SetTextColor(color);
                        mLabelText.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        mLabelText.Visibility = ViewStates.Gone;
                    }
                }
            }

            public void SetDetailsLabel(string detailsLabel, Color color)
            {
                mDetailsLabel = detailsLabel;
                mDetailColor = color;
                if (mDetailsText != null)
                {
                    if (detailsLabel != null)
                    {
                        mDetailsText.Text = (detailsLabel);
                        mDetailsText.SetTextColor(color);
                        mDetailsText.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        mDetailsText.Visibility = ViewStates.Gone;
                    }
                }
            }

            public void SetSize(int width, int height)
            {
                mWidth = width;
                mHeight = height;
                if (mBackgroundLayout != null)
                {
                    UpdateBackgroundSize();
                }
            }
        }
    }
}