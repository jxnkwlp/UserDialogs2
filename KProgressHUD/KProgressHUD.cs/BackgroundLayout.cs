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
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace KProgressHUDLib
{
    public class BackgroundLayout : LinearLayout
    {
        private float mCornerRadius;
        private int mBackgroundColor;

        public BackgroundLayout(Context context) : base(context)
        {
            Init();
        }

        public BackgroundLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        public BackgroundLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init();
        }

        private void Init()
        {
            int color = Context.Resources.GetColor(Resource.Color.kprogresshud_default_color);
            InitBackground(color, mCornerRadius);
        }

        private void InitBackground(int color, float cornerRadius)
        {
            GradientDrawable drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetColor(color);
            drawable.SetCornerRadius(cornerRadius);
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBean)
            {
                Background = drawable;
            }
            else
            {
                //noinspection deprecation
                SetBackgroundDrawable(drawable);
            }
        }

        public void SetCornerRadius(float radius)
        {
            mCornerRadius = Helper.DpToPixel(radius, Context);
            InitBackground(mBackgroundColor, mCornerRadius);
        }

        public void SetBaseColor(int color)
        {
            mBackgroundColor = color;
            InitBackground(mBackgroundColor, mCornerRadius);
        }
    }
}