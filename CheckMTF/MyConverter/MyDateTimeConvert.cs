using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CheckMTF.MyConverter
{
    public class MyDateTimeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? selecteddate = value as DateTime?;
            if (selecteddate == null)
                return null;
            return new string(selecteddate.Value.Day.ToString()+"/"+selecteddate.Value.Month.ToString()+"/"+selecteddate.Value.Year.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string mydatetime = (string)value;
            string[] arraymydatetime = mydatetime.Split('/');
            return new DateTime(Int32.Parse(arraymydatetime[2]), Int32.Parse(arraymydatetime[1]), Int32.Parse(arraymydatetime[0]));

        }
    }
}
