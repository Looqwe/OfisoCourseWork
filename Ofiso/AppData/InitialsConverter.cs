using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ofiso.AppData
{
    public class InitialsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string name && !string.IsNullOrEmpty(name))
            {
                var parts = name.Split(' ');
                if (parts.Length == 0)
                    return "";

                if (parts.Length == 1)
                    return parts[0].Length > 0
                        ? $"{parts[0][0]}".ToUpper()
                        : "";

                // Для версий ниже C# 8.0
                var lastIndex = parts.Length - 1;
                return $"{parts[0][0]}{parts[lastIndex][0]}".ToUpper();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
