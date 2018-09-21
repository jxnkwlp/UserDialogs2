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

namespace KProgressHUD
{
    public class KProgressHUD
    {
        public enum Style
        {
            SPIN_INDETERMINATE, PIE_DETERMINATE, ANNULAR_DETERMINATE, BAR_DETERMINATE
        }

        // To avoid redundant APIs, make the HUD as a wrapper class around a Dialog
        private ProgressDialog mProgressDialog;
        private float mDimAmount;
        private int mWindowColor;
        private float mCornerRadius;
        private Context mContext;

        private int mAnimateSpeed;

        private int mMaxProgress;
        private bool mIsAutoDismiss;

        private int mGraceTimeMs;
        private Handler mGraceTimer;
        private bool mFinished;

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

            SetStyle(Style.SPIN_INDETERMINATE);
        }

        /**
         * Create a new HUD. Have the same effect as the constructor.
         * For convenient only.
         * @param context Activity context that the HUD bound to
         * @return An unique HUD instance
         */
        public static KProgressHUD Create(Context context)
        {
            return new KProgressHUD(context);
        }

        /**
         * Create a new HUD. specify the HUD style (if you use a custom view, you need {@code KProgressHUD.create(Context context)}).
         *
         * @param context Activity context that the HUD bound to
         * @param style One of the KProgressHUD.Style values
         * @return An unique HUD instance
         */
        public static KProgressHUD create(Context context, Style style)
        {
            return new KProgressHUD(context).SetStyle(style);
        }

        /**
         * Specify the HUD style (not needed if you use a custom view)
         * @param style One of the KProgressHUD.Style values
         * @return Current HUD
         */
        public KProgressHUD SetStyle(Style style)
        {
            View view = null;
            switch (style)
            {
                case Style.SPIN_INDETERMINATE:
                    view = new SpinView(mContext);
                    break;
                case Style.PIE_DETERMINATE:
                    view = new PieView(mContext);
                    break;
                case Style.ANNULAR_DETERMINATE:
                    view = new AnnularView(mContext);
                    break;
                case Style.BAR_DETERMINATE:
                    view = new BarView(mContext);
                    break;
                    // No custom view style here, because view will be added later
            }

            mProgressDialog.SetView(view);
            return this;
        }

        /**
         * Specify the dim area around the HUD, like in Dialog
         * @param dimAmount May take value from 0 to 1. Default to 0 (no dimming)
         * @return Current HUD
         */
        public KProgressHUD SetDimAmount(float dimAmount)
        {
            if (dimAmount >= 0 && dimAmount <= 1)
            {
                mDimAmount = dimAmount;
            }
            return this;
        }

        /**
         * Set HUD size. If not the HUD view will use WRAP_CONTENT instead
         * @param width in dp
         * @param height in dp
         * @return Current HUD
         */
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

        /**
         * Specify the HUD background color
         * @param color ARGB color
         * @return Current HUD
         */
        public KProgressHUD SetBackgroundColor(int color)
        {
            mWindowColor = color;
            return this;
        }

        /**
         * Specify corner radius of the HUD (default is 10)
         * @param radius Corner radius in dp
         * @return Current HUD
         */
        public KProgressHUD SetCornerRadius(float radius)
        {
            mCornerRadius = radius;
            return this;
        }

        /**
         * Change animation speed relative to default. Used with indeterminate style
         * @param scale Default is 1. If you want double the speed, Set the param at 2.
         * @return Current HUD
         */
        public KProgressHUD SetAnimationSpeed(int scale)
        {
            mAnimateSpeed = scale;
            return this;
        }

        /**
         * Optional label to be displayed.
         * @return Current HUD
         */
        public KProgressHUD SetLabel(string label)
        {
            mProgressDialog.SetLabel(label);
            return this;
        }

        /**
         * Optional label to be displayed
         * @return Current HUD
         */
        public KProgressHUD SetLabel(string label, Color color)
        {
            mProgressDialog.SetLabel(label, color);
            return this;
        }

        /**
         * Optional detail description to be displayed on the HUD
         * @return Current HUD
         */
        public KProgressHUD SetDetailsLabel(string detailsLabel)
        {
            mProgressDialog.SetDetailsLabel(detailsLabel);
            return this;
        }

        /**
         * Optional detail description to be displayed
         * @return Current HUD
         */
        public KProgressHUD SetDetailsLabel(string detailsLabel, Color color)
        {
            mProgressDialog.SetDetailsLabel(detailsLabel, color);
            return this;
        }

        /**
         * Max value for use in one of the determinate styles
         * @return Current HUD
         */
        public KProgressHUD SetMaxProgress(int maxProgress)
        {
            mMaxProgress = maxProgress;
            return this;
        }

        /**
         * Set current progress. Only have effect when use with a determinate style, or a custom
         * view which , Determinate interface.
         */
        public void SetProgress(int progress)
        {
            mProgressDialog.SetProgress(progress);
        }

        /**
         * Provide a custom view to be displayed.
         * @param view Must not be null
         * @return Current HUD
         */
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

        /**
         * Specify whether this HUD can be cancelled by using back button (default is false)
         *
         * Setting a cancelable to true with this method will Set a null callback,
         * clearing any callback previously Set with
         * {@link #SetCancellable(DialogInterface.OnCancelListener)}
         *
         * @return Current HUD
         */
        public KProgressHUD SetCancellable(bool isCancellable)
        {
            mProgressDialog.SetCancelable(isCancellable);
            mProgressDialog.SetOnCancelListener(null);
            return this;
        }

        /**
         * Specify a callback to run when using the back button (default is null)
         *
         * @param listener The code that will run if the user presses the back
         * button. If you pass null, the dialog won't be cancellable, just like
         * if you had called {@link #SetCancellable(bool)} passing false.
         *
         * @return Current HUD
         */
        public KProgressHUD SetCancellable(IDialogInterfaceOnCancelListener listener)
        {
            mProgressDialog.SetCancelable(null != listener);
            mProgressDialog.SetOnCancelListener(listener);
            return this;
        }

        /**
         * Specify whether this HUD closes itself if progress reaches max. Default is true.
         * @return Current HUD
         */
        public KProgressHUD SetAutoDismiss(bool isAutoDismiss)
        {
            mIsAutoDismiss = isAutoDismiss;
            return this;
        }

        /**
         * Grace period is the time (in milliseconds) that the invoked method may be run without
         * showing the HUD. If the task finishes before the grace time runs out, the HUD will
         * not be shown at all.
         * This may be used to prevent HUD display for very short tasks.
         * Defaults to 0 (no grace time).
         * @param graceTimeMs Grace time in milliseconds
         * @return Current HUD
         */
        public KProgressHUD SetGraceTime(int graceTimeMs)
        {
            mGraceTimeMs = graceTimeMs;
            return this;
        }

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

        public bool IsShowing
        {
            get
            {
                return mProgressDialog != null && mProgressDialog.IsShowing;
            }
        }

        public void dismiss()
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
            KProgressHUD _instance;


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
                        _instance.dismiss();
                    }
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