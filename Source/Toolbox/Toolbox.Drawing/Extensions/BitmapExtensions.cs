﻿using System;
using System.Drawing;

namespace Toolbox.Extensions
{
    public static class BitmapExtensions
    {
        public static Bitmap DeepCopy(this Bitmap bitmap)
        {
            if (bitmap is null)
            {
                throw new ArgumentException("Bitmap cannot be null");
            }

            return bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
        }
    }
}
