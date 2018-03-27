using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Extensions
{
    public static class BitmapExtensions
    {
        public static Bitmap DeepCopy(this Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentException("Bitmap cannot be null");

            return bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
        }
    }
}
