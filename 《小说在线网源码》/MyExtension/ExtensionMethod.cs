using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyExtension
{
    public static class ExtensionMethod
    {
        public static DateTime? ToDateTime(this object obj)
        {
            if (Convert.IsDBNull(obj))
                return null;
            else
            {
                DateTime dt;
                if (DateTime.TryParse(obj.ToString(), out dt))
                    return dt;
                else
                    return null;
            }
        }

        public static int? ToInt(this object obj)
        {
            if (Convert.IsDBNull(obj))
                return null;
            else
            {
                int i;
                if (int.TryParse(obj.ToString(), out i))
                    return i;
                else
                    return null;
            }
        }

        public static decimal? ToDecimal(this object obj)
        {
            if (Convert.IsDBNull(obj))
                return null;
            else
            {
                decimal d;
                if (decimal.TryParse(obj.ToString(), out d))
                    return d;
                else
                    return null;
            }
        }

        public static bool ToBool(this object obj)
        {
            if (Convert.IsDBNull(obj))
                return false;
            else
            {
                bool b;
                if (bool.TryParse(obj.ToString(), out b))
                    return b;
                else
                    return false;
            }
        }

        public static string ToStr(this string msg)
        {
            return msg.Replace("'", "\"").Replace("\r", "<br />").Replace("\n", "<br />");
        }
    }
}
