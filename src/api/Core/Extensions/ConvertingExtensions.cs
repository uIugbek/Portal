using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Portal.Apis.Core.Extensions
{
    public static class ConvertingExtensions
    {
        /// <summary>
        ///     Safe ToString() method
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AsString(this object value)
        {
            return (value == null ? string.Empty : value.ToString());
        }

        /// <summary>
        ///     Converts this value to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Returns Nullable&lt;int&gt;</returns>
        public static int? AsInt(this string value)
        {
            if (!value.IsNullOrEmpty())
            {
                int k;
                if (int.TryParse(value, out k))
                {
                    return k;
                }
            }

            return default(int?);
        }

        /// <summary>
        ///     Converts this value to long
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Returns Nullable&lt;long&gt;</returns>
        public static long? AsLongInt(this string value)
        {
            if (!value.IsNullOrEmpty())
            {
                long k;
                if (long.TryParse(value, out k))
                {
                    return k;
                }
            }

            return default(long?);
        }

        /// <summary>
        ///     Converts this value to double
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Returns Nullable&lt;double&gt;</returns>
        public static double? AsDouble(this string value)
        {
            if (!value.IsNullOrEmpty())
            {
                double k;
                if (double.TryParse(value, out k))
                {
                    return k;
                }
            }

            return default(double?);
        }

        /// <summary>
        ///     Converts this value to decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Returns Nullable&lt;decimal&gt;</returns>
        public static decimal? AsDecimal(this string value)
        {
            if (!value.IsNullOrEmpty())
            {
                decimal k;
                if (decimal.TryParse(value, out k))
                {
                    return k;
                }
            }

            return default(decimal?);
        }

        /// <summary>
        ///     Converts this value to DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Returns Nullable&lt;DateTime&gt;</returns>
        public static DateTime? AsDateTime(this string value)
        {
            if (!value.IsNullOrEmpty())
            {
                DateTime d;
                if (DateTime.TryParse(value, Thread.CurrentThread.CurrentCulture.DateTimeFormat, DateTimeStyles.None, out d))
                {
                    return d;
                }
            }

            return default(DateTime?);
        }

        /// <summary>
        ///     Converts this value to bool, if parsing fails, will be returned <paramref name="defaultValue" />
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="defaultValue">the default value for fail case</param>
        /// <returns>Returns bool</returns>
        public static bool? AsBoolean(this string value)
        {
            if (!value.IsNullOrEmpty())
            {
                bool d;
                if (bool.TryParse(value, out d))
                {
                    return d;
                }

                if (value.Equals("on", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (value.Equals("off", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return default(bool?);
        }

        /// <summary>
        ///     Converts this value to int, if parsing fails, will be returned <paramref name="defaultValue" />
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="defaultValue">the default value for fail case</param>
        /// <returns>Returns int</returns>
        public static int AsInt(this string value, int defaultValue)
        {
            if (!value.IsNullOrEmpty())
            {
                int k;
                if (int.TryParse(value, out k))
                {
                    return k;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts this value to long, if parsing fails, will be returned <paramref name="defaultValue" />
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="defaultValue">the default value for fail case</param>
        /// <returns>Returns int</returns>
        public static long AsLongInt(this string value, long defaultValue)
        {
            if (!value.IsNullOrEmpty())
            {
                long k;
                if (long.TryParse(value, out k))
                {
                    return k;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts this value to double, if parsing fails, will be returned <paramref name="defaultValue" />
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="defaultValue">the default value for fail case</param>
        /// <returns>Returns double</returns>
        public static double AsDouble(this string value, double defaultValue)
        {
            if (!value.IsNullOrEmpty())
            {
                double k;
                if (double.TryParse(value, out k))
                {
                    return k;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts this value to decimal, if parsing fails, will be returned <paramref name="defaultValue" />
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="defaultValue">the default value for fail case</param>
        /// <returns>Returns decimal</returns>
        public static decimal AsDecimal(this string value, decimal defaultValue)
        {
            if (!value.IsNullOrEmpty())
            {
                decimal k;
                if (decimal.TryParse(value, out k))
                {
                    return k;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts this value to DateTime, if parsing fails, will be returned <paramref name="defaultValue" />
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="defaultValue">the default value for fail case</param>
        /// <returns>Returns DateTime</returns>
        public static DateTime AsDateTime(this string value, DateTime defaultValue)
        {
            if (!value.IsNullOrEmpty())
            {
                DateTime d;
                if (DateTime.TryParse(value, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.None, out d))
                {
                    return d;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts this value to bool, if parsing fails, will be returned <paramref name="defaultValue" />
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="defaultValue">the default value for fail case</param>
        /// <returns>Returns bool</returns>
        public static bool AsBoolean(this string value, bool defaultValue)
        {
            if (!value.IsNullOrEmpty())
            {
                bool d;
                if (bool.TryParse(value, out d))
                {
                    return d;
                }

                if (value.Equals("on", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (value.Equals("off", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     CHeck is not null or empty
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        ///     Shu string ni url ga convert qiladi. Masalan 'Man kecha TATU ga bordim' = 'man-kecha-tatu-ga-bordim'
        /// </summary>
        /// <param name="s">...</param>
        /// <returns></returns>
        public static string ToUrl(this string s)
        {
            if (s.IsNullOrEmpty())
                return string.Empty;

            s = s.ToLower();

            string url = string.Empty;
            int index = 0;

            while (index != s.Length)
            {
                if (char.IsLetter(s[index]) || char.IsDigit(s[index]) || s[index] == '-')
                    url += s[index].ToString(CultureInfo.InvariantCulture);
                else
                {
                    if (index != 0 && s[index - 1] != '-')
                        url += '-'.ToString(CultureInfo.InvariantCulture);
                }
                index++;
            }
            return url;
        }

        public static Boolean IsGuid(String s)
        {
            Guid value;

            // this is before the overhead of setting up the try/catch block.
            if (s == null || s.Length != 36)
            {
                value = Guid.Empty;
                return false;
            }

            var format =
                new Regex(
                    "^[A-Fa-f0-9]{32}$|^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
            Match match = format.Match(s);
            try
            {
                if (match.Success)
                {
                    value = new Guid(s);
                    return true;
                }
                else
                {
                    value = Guid.Empty;
                    return false;
                }
            }
            catch (FormatException)
            {
                value = Guid.Empty;
                return false;
            }
        }
    }
}
