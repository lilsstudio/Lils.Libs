using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lils.WpfLib.Tools
{
    static class MathTools
    {
        /// <summary>
        /// coerce the current double value is in range [min, max]
        /// </summary>
        /// <param name="value">current value</param>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns>coerced value</returns>
        public static double Coerce(this double value, double min, double max)
        {
            if (min > max)
            {
                var message = $"{nameof(min)} must lesser than {nameof(max)}";
                throw new ArgumentException(message);
            }
            value = Math.Max(value, min);
            value = Math.Min(value, max);
            return value;
        }

        /// <summary>
        /// coerce the current point value is in the rect range [(min, min) (max, max)]
        /// </summary>
        /// <param name="value">current value</param>
        /// <param name="min">coordinate x and y of the min point</param>
        /// <param name="max">coordinate x and y of the max point</param>
        /// <returns>coerced value</returns>
        public static Point Coerce(this Point value, double min, double max)
        {
            if (min > max)
            {
                var message = $"{nameof(min)} must lesser than {nameof(max)}";
                throw new ArgumentException(message);
            }
            return value.Coerce(new Point(min, min), new Point(max, max));
        }

        /// <summary>
        /// coerce the current point value is in the rect range [min, max]
        /// </summary>
        /// <param name="value">current value</param>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns></returns>
        public static Point Coerce(this Point value, Point min, Point max)
        {
            if (min.X > max.X)
            {
                var message = $"{nameof(min)}.X must lesser than {nameof(max)}.X";
                throw new ArgumentException(message);
            }
            if (min.Y > max.Y)
            {
                var message = $"{nameof(min)}.Y must lesser than {nameof(max)}.Y";
                throw new ArgumentException(message);
            }

            value.X = Math.Max(value.X, min.X);
            value.X = Math.Min(value.X, max.X);
            value.Y = Math.Max(value.Y, min.Y);
            value.Y = Math.Min(value.Y, max.Y);

            return value;
        }
    }
}
