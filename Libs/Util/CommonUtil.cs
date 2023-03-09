using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HACCP.Libs.Util
{
    public class CommonUtil
    {
        public static bool valid(String str)
        {
            if (str == null || str.Trim().Equals("") || str.Trim().ToLower().Equals("null") || str.Trim().ToLower().Equals("undefined"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string ckStr(string str)
        {
            if (!valid(str))
            {
                return "";
            }
            else
            {
                return str;
            }
        }

        public static bool valid<T>(T bean)
        {
            if (bean == null)
            {
                return false;
            }
            else
            {
                if (bean.GetType().GetProperties().Length <= 0)
                {
                    return false;
                }

                return true;
            }
        }

        public static T convertValue<T, U>(U value) where U : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static int toInt(string str)
        {
            return convertValue<int, string>(str);
        }

        public static float toFloat(string str)
        {
            return convertValue<float, string>(str);
        }

        public static string firstCharToUpper(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }
            return str.First().ToString().ToUpper() + str.Substring(1);
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>(); foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row); data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T); T obj = Activator.CreateInstance<T>(); foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (dr[column.ColumnName] is DBNull)
                        {
                            pro.SetValue(obj, null);//, null); 
                        }
                        else
                        {
                            pro.SetValue(obj, dr[column.ColumnName]);//, null); 
                        }
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}