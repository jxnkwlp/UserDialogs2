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

using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Widget;
using Java.Lang;

namespace KProgressHUDLib
{
    public class SpinView : ImageView, IIndeterminate
    {
        private float mRotateDegrees;
        private int mFrameTime;
        private bool mNeedToUpdateView;
        private Runnable mUpdateViewRunnable;

        public SpinView(Context context) : base(context)
        {
            Init();
        }

        public SpinView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        private void Init()
        {
            SetImageResource(Resource.Drawable.kprogresshud_spinner);
            mFrameTime = 1000 / 12;
            mUpdateViewRunnable = new Runnable(() =>
            {
                mRotateDegrees += 30;
                mRotateDegrees = mRotateDegrees < 360 ? mRotateDegrees : mRotateDegrees - 360;
                Invalidate();
                if (mNeedToUpdateView)
                {
                    PostDelayed(mUpdateViewRunnable, mFrameTime);
                }
            });
        }

        public virtual void SetAnimationSpeed(float scale)
        {
            mFrameTime = (int)(1000 / 12 / scale);
        }

        protected override void OnDraw(Canvas canvas)
        {
            canvas.Rotate(mRotateDegrees, Width / 2, Height / 2);
            base.OnDraw(canvas);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            mNeedToUpdateView = true;
            Post(mUpdateViewRunnable);
        }

        protected override void OnDetachedFromWindow()
        {
            mNeedToUpdateView = false;
            base.OnDetachedFromWindow();
        }
    }
}