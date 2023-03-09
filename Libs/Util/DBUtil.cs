using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System;
using System.Reflection;
using HACCP.Models;
using System.Collections.Generic;
using HACCP.Models.ProductionManage;

namespace HACCP.Libs.Util
{
    public class DBUtil
    {
        public static SqlConnection GetCon()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon2"].ConnectionString);
            con.Open();
            return con;
        }

        /// <summary>
        /// DataTable을 Model 객체에 담는 함수
        /// </summary>
        /// <typeparam name="T">데이터를 담을 Model 객체의 타입</typeparam>
        /// <param name="dt">데이터가 담겨있는 DataTable</param>
        /// <returns></returns>
        public static List<T> DataTableToModel<T>(DataTable dt)
        {
            T bean = default;
            List<T> tmpList = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                bean = Activator.CreateInstance<T>();
                FieldInfo[] fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                for (int i = 0; i < dr.ItemArray.Length; i++)
                {
                    foreach (var field in fields)
                    {
                        string paramNm1 = field.Name.Contains("<") ? field.Name.Substring(1).Split('>')[0] : field.Name.Substring(1);
                        string paramNm2 = dt.Columns[i].ColumnName;
                        var paramValue = dr.ItemArray[i];
                        if (paramNm1 == paramNm2)
                        {
                            if (paramValue.Equals(null) || DBNull.Value.Equals(paramValue))
                            {
                                typeof(T).GetProperty(paramNm2).SetValue(bean, null);
                            }
                            else
                            {
                                typeof(T).GetProperty(paramNm2).SetValue(bean, paramValue);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                tmpList.Add(bean);
            }

            return tmpList;
        }

        /// <summary>
        /// 파라미터 Model List의 데이터를 타입 이름과 같은 테이블에 Insert 하는 구문 생성
        /// </summary>
        /// <typeparam name="T">데이터가 담겨있는 Model의 타입</typeparam>
        /// <param name="tmpList">데이터가 담겨있는 Model의 List</param>
        /// <returns></returns>
        internal static string InsertQuery<T>(T tmpData)
        {
            if (!CommonUtil.valid<T>(tmpData)) { return ""; }

            string query = "insert into [" + tmpData.GetType().Name + "]";
            string propsVal = "";
            query += "(";

            foreach (PropertyInfo propertyInfo in tmpData.GetType().GetProperties())
            {
                query += propertyInfo.Name + ",";
            }

            query = query.Substring(0, query.Length - 1);
            query += ") values (";

            foreach (PropertyInfo propertyInfo in tmpData.GetType().GetProperties())
            {
                var tmpVal = propertyInfo.GetValue(tmpData);
                switch (propertyInfo.PropertyType.Name)
                {
                    case "String":
                        if (tmpVal != null)
                        {
                            propsVal = tmpVal.ToString().Replace("\'", "\'\'");
                            query += ("\'" + propsVal + "\',");
                        }
                        else
                        {
                            query += ("NULL" + ",");
                        }
                        break;
                    default:
                        propsVal = tmpVal.ToString().Replace(",", "");
                        query += (propsVal + ",");
                        break;
                }
            }

            query = query.Substring(0, query.Length - 1);

            query += ");";

            return query;
        }

        /// <summary>
        /// 파라미터로 받은 sql 구문을 실행
        /// </summary>
        /// <param name="query">실행할 sql 구문</param>
        /// <returns></returns>
        public static bool InsertColumString(String query)
        {
            SqlConnection con = null;
            SqlTransaction transaction = null;
            SqlCommand command = null;
            Boolean rs = false;

            try
            {
                con = GetCon();
                transaction = con.BeginTransaction("tran");
                command = con.CreateCommand();
                command.Connection = con;
                command.Transaction = transaction;
                command.CommandText = query;
                int rsInt = command.ExecuteNonQuery();
                command.Transaction.Commit();
                return rs;
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                return rs;
            }
            finally
            {
                cleanUp(con);
            }
        }

        public static List<T> SelectBean<T>()
        {
            SqlConnection con = null;
            T bean = default;
            List<T> tmpList = new List<T>();

            try
            {
                con = GetCon();
                bean = Activator.CreateInstance<T>();
                FieldInfo[] fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                var command = con.CreateCommand();
                command.CommandText = "SELECT * FROM " + bean.GetType().Name + " ORDER BY DTS_ACC DESC";
                IDataReader rd = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (rd.Read())
                {
                    bean = Activator.CreateInstance<T>();
                    for (int i = 0; i < rd.FieldCount; i++)
                    {
                        foreach (var field in fields)
                        {
                            string paramNm1 = field.Name.Substring(1);
                            string paramNm2 = rd.GetName(i);
                            string paramValue = rd.GetValue(i).ToString();
                            if (paramNm1 == paramNm2)
                            {
                                typeof(T).GetProperty(rd.GetName(i)).SetValue(bean, paramValue);
                                //typeof(T).GetMethod("set"+ CommonUtil.firstCharToUpper(rd.GetName(i))).Invoke(bean, new object[] { rd[i] } );
                            }
                        }
                    }
                    tmpList.Add(bean);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.StackTrace);
                return tmpList;
            }
            finally
            {
                cleanUp(con);
            }
            return tmpList;
        }

        public static Object SelectColumString(String query)
        {
            SqlConnection con = null;
            IDataReader rd = null;
            try
            {
                con = GetCon();
                con.Open();
                var command = con.CreateCommand();
                command.CommandText = query;
                rd = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (rd.Read())
                {
                    System.Diagnostics.Debug.WriteLine("field count:" + rd.FieldCount);
                    for (int i = 0; i < rd.FieldCount; i++)
                    {
                        System.Diagnostics.Debug.WriteLine(rd.GetName(i) + rd[0]);
                    }
                }

                return rd;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return rd;
            }
            finally
            {
                cleanUp(con);
            }
        }

        public static void cleanUp(SqlConnection con)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con = null;
            }
        }
    }
}