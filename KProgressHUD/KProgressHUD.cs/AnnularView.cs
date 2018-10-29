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
    public class AnnularView : View, IDeterminate
    {
        private Paint mWhitePaint;
        private Paint mGreyPaint;
        private RectF mBound;
        private int mMax = 100;
        private int mProgress = 0;

        public AnnularView(Context context) : base(context)
        {
            init(context);
        }

        public AnnularView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            init(context);
        }

        public AnnularView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            init(context);
        }

        private void init(Context context)
        {
            mWhitePaint = new Paint(PaintFlags.AntiAlias);
            mWhitePaint.SetStyle(Paint.Style.Stroke);
            mWhitePaint.StrokeWidth = Helper.DpToPixel(3, Context);
            mWhitePaint.Color = Color.White;

            mGreyPaint = new Paint(PaintFlags.AntiAlias);
            mGreyPaint.SetStyle(Paint.Style.Stroke);
            mGreyPaint.StrokeWidth = Helper.DpToPixel(3, Context);
            mGreyPaint.Color = context.Resources.GetColor(Resource.Color.kprogresshud_grey_color);

            mBound = new RectF();
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            int padding = Helper.DpToPixel(4, Context);
            mBound.Set(padding, padding, w - padding, h - padding);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            float mAngle = mProgress * 360f / mMax;
            canvas.DrawArc(mBound, 270, mAngle, false, mWhitePaint);
            canvas.DrawArc(mBound, 270 + mAngle, 360 - mAngle, false, mGreyPaint);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            int dimension = Helper.DpToPixel(40, Context);
            SetMeasuredDimension(dimension, dimension);
        }

        public virtual void SetMax(int max)
        {
            this.mMax = max;
        }

        public virtual void SetProgress(int progress)
        {
            mProgress = progress;
            Invalidate();
        }
    }
}