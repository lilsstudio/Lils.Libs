using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lils.WpfLib.Converters
{
    public enum ArithmeticOperator
    {
        Plus,
        Times,
    }

    public class DoubleArithmeticConverter : IValueConverter
    {
        public ArithmeticOperator Operator { get; set; } = ArithmeticOperator.Plus;

        public double DefaultOperateValue { get; set; } = 1;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                var operateValue = DefaultOperateValue;
                if (parameter is string paraStrValue &&
                    double.TryParse(paraStrValue, out var paraValue))
                {
                    operateValue = paraValue;
                }

                switch (Operator)
                {
                    case ArithmeticOperator.Plus:
                        return doubleValue + operateValue;
                    case ArithmeticOperator.Times:
                        return doubleValue * operateValue;
                    default:
                        break;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                var operateValue = DefaultOperateValue;
                if (parameter is string paraStrValue &&
                    double.TryParse(paraStrValue, out var paraValue))
                {
                    operateValue = paraValue;
                }

                switch (Operator)
                {
                    case ArithmeticOperator.Plus:
                        return doubleValue - operateValue;
                    case ArithmeticOperator.Times:
                        return operateValue == 0 ?
                            double.MaxValue : doubleValue / operateValue;
                    default:
                        break;
                }
            }
            return value;
        }
    }
}
