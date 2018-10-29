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
using Android.Views;

namespace KProgressHUDLib
{
    public class BarView : View, IDeterminate
    {
        private Paint mOuterPaint;
        private Paint mInnerPaint;
        private RectF mBound;
        private RectF mInBound;
        private int mMax = 100;
        private int mProgress = 0;
        private float mBoundGap;

        public BarView(Context context) : base(context)
        {
            init();
        }

        public BarView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            init();
        }

        public BarView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            init();
        }

        private void init()
        {
            mOuterPaint = new Paint(PaintFlags.AntiAlias);
            mOuterPaint.SetStyle(Paint.Style.Stroke);
            mOuterPaint.StrokeWidth = Helper.DpToPixel(2, Context);
            mOuterPaint.Color = Color.White;

            mInnerPaint = new Paint(PaintFlags.AntiAlias);
            mInnerPaint.SetStyle(Paint.Style.Fill);
            mInnerPaint.Color = Color.White;

            mBoundGap = Helper.DpToPixel(5, Context);
            mInBound = new RectF(mBoundGap, mBoundGap,
                    (Width - mBoundGap) * mProgress / mMax, Height - mBoundGap);

            mBound = new RectF();
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            int padding = Helper.DpToPixel(2, Context);
            mBound.Set(padding, padding, w - padding, h - padding);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            canvas.DrawRoundRect(mBound, mBound.Height() / 2, mBound.Height() / 2, mOuterPaint);
            canvas.DrawRoundRect(mInBound, mInBound.Height() / 2, mInBound.Height() / 2, mInnerPaint);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            int widthDimension = Helper.DpToPixel(100, Context);
            int heightDimension = Helper.DpToPixel(20, Context);
            SetMeasuredDimension(widthDimension, heightDimension);
        }

        public virtual void SetMax(int max)
        {
            this.mMax = max;
        }

        public virtual void SetProgress(int progress)
        {
            this.mProgress = progress;
            mInBound.Set(mBoundGap, mBoundGap, (Width - mBoundGap) * mProgress / mMax, Height - mBoundGap);
            Invalidate();
        }
    }
}