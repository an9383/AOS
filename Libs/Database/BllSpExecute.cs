using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Libs.Database
{
	public class BllSpExecute
	{
		private DbAgent myDbAgent;

		public BllSpExecute()
		{
			myDbAgent = new DbAgent("", "", System.Data.CommandType.StoredProcedure);
			//myDbAgent = new DbAgent("", "", System.Data.CommandType.StoredProcedure, Public_Function.selectedDB);
		}

		//파리미터 셋팅하는 부분 분리
		private void paramSetting(string SP_name, string gubun, params string[] Arrparam)
		{
			try
			{
				myDbAgent.CommandText = SP_name;
				myDbAgent.MyCommand.Parameters.Clear();
				myDbAgent.MyCommand.Parameters.AddWithValue("@gubun", gubun);

				int i = 0;
				int k, j;
				while (i < Arrparam.Length)
				{
					//":"의 위치를 인덱스로 반환
					k = Arrparam[i].IndexOf(":");
					//char의 갯수를 반환
					j = Arrparam[i].Length;

					if (k + 1 == j)
						myDbAgent.MyCommand.Parameters.AddWithValue(Arrparam[i].Substring(0, k), null);
					else
						myDbAgent.MyCommand.Parameters.AddWithValue(Arrparam[i].Substring(0, k), Arrparam[i].Substring(k + 1, j - k - 1));
					i++;
				}

				myDbAgent.MyCommand.Parameters.Add("@message", SqlDbType.NVarChar, 2000);
				myDbAgent.MyCommand.Parameters["@message"].Direction = ParameterDirection.Output;

				//2012.04.08 최석중 추가 (전체적으로 사업장 반영, 로그인 사원 추가하여 audit trail에서 사용)
				myDbAgent.MyCommand.Parameters.AddWithValue("@sys_plant_cd", HttpContext.Current.Session["plantCD"]);
				myDbAgent.MyCommand.Parameters.AddWithValue("@sys_emp_cd", HttpContext.Current.Session["loginCD"]);

				//2007.11.20 서형준 추가 (SP에서 Return 값 받는다. 0은 정상)
				myDbAgent.MyCommand.Parameters.Add("ReturnValue", SqlDbType.Int);
				myDbAgent.MyCommand.Parameters["ReturnValue"].Direction = ParameterDirection.ReturnValue;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private void paramSettingWithoutMessage(string SP_name, string gubun, params string[] Arrparam)
		{
			try
			{
				myDbAgent.CommandText = SP_name;
				myDbAgent.MyCommand.Parameters.Clear();
				myDbAgent.MyCommand.Parameters.AddWithValue("@gubun", gubun);

				int i = 0;
				int k, j;
				while (i < Arrparam.Length)
				{
					//":"의 위치를 인덱스로 반환
					k = Arrparam[i].IndexOf(":");
					//char의 갯수를 반환
					j = Arrparam[i].Length;

					if (k + 1 == j)
						myDbAgent.MyCommand.Parameters.AddWithValue(Arrparam[i].Substring(0, k), null);
					else
						myDbAgent.MyCommand.Parameters.AddWithValue(Arrparam[i].Substring(0, k), Arrparam[i].Substring(k + 1, j - k - 1));
					i++;
				}

				//2012.04.08 최석중 추가 (전체적으로 사업장 반영, 로그인 사원 추가하여 audit trail에서 사용)
				myDbAgent.MyCommand.Parameters.AddWithValue("@sys_plant_cd", HttpContext.Current.Session["plantCD"]);
				myDbAgent.MyCommand.Parameters.AddWithValue("@sys_emp_cd", HttpContext.Current.Session["loginCD"]);

				//2007.11.20 서형준 추가 (SP에서 Return 값 받는다. 0은 정상)
				myDbAgent.MyCommand.Parameters.Add("ReturnValue", SqlDbType.Int);
				myDbAgent.MyCommand.Parameters["ReturnValue"].Direction = ParameterDirection.ReturnValue;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				//MessageBox.Show(ex.Message.ToString());
			}
		}


		/// <summary>
		/// 공통 파라메터의 값을 리턴 한다
		/// </summary>
		public string SpGetParam(string paramKey)
		{
			string param = "@s_parameter_code:" + paramKey;
			string param2 = "@s_parameter_remark:";
			DataTable myDataTable = SpExecuteTable("SP_Parameter", "S", param, param2);

			if (myDataTable.Rows.Count > 0)
				return myDataTable.Rows[0]["parameter_value"].ToString();
			else
				return "";
		}

		//string 을 반환할때
		public string SpExecuteString(string SP_name, string gubun, params string[] Arrparam)
		{

			paramSetting(SP_name, gubun, Arrparam);

			string message;

			message = SpExecuteStringCommon(SP_name);

			return message;

		}

		//리턴값, 메세지 참조형식으로 반환
		public void SpExecuteString(string SP_name, string gubun, ref int rValue, ref string Msg, bool TransactionType, params string[] Arrparam)
		{
			paramSetting(SP_name, gubun, Arrparam);

			myDbAgent.ExcuteNonQuery(ref rValue, ref Msg, TransactionType);

			if (rValue != 0)
			{
				//MessageBox.Show("작업실패!!! : " + Msg);
				try
				{
					InsertErrorLog(Public_Function.User_id, Public_Function.program_id, SP_name, Msg, "SQL");

				}
				catch (Exception e)
				{
					if (!EventLog.SourceExists("RTEMES"))
					{
						EventLog.CreateEventSource("RTEMES", "");
					}

					EventLog myLog = new EventLog();
					myLog.Source = "RTEMES";
					myLog.WriteEntry(e.Message.ToString());
				}
			}

		}

		//리턴값, 메세지, 영향받은 행수 참조형식으로 반환
		public void SpExecuteString(string SP_name, string gubun, ref int rValue, ref string Msg, ref int recordsAffected, bool TransactionType, params string[] Arrparam)
		{
			paramSetting(SP_name, gubun, Arrparam);

			myDbAgent.ExcuteNonQuery(ref rValue, ref Msg, ref recordsAffected, TransactionType);

			if (rValue != 0)
			{
				//MessageBox.Show("작업실패!!! : " + Msg);
				try
				{
					InsertErrorLog(Public_Function.User_id, Public_Function.program_id, SP_name, Msg, "SQL");

				}
				catch (Exception e)
				{
					if (!EventLog.SourceExists("RTEMES"))
					{
						EventLog.CreateEventSource("RTEMES", "");
					}

					EventLog myLog = new EventLog();
					myLog.Source = "RTEMES";
					myLog.WriteEntry(e.Message.ToString());

				}
			}

		}

		//첨부파일 
		public string SpExecuteString(string SP_name, string gubun, string param_name, byte[] image_data, params string[] Arrparam)
		{
			paramSetting(SP_name, gubun, Arrparam);

			string message;

			if (param_name != "")
				myDbAgent.MyCommand.Parameters.AddWithValue(param_name, image_data);

			message = SpExecuteStringCommon(SP_name);

			return message;
		}

		//string 을 반환할때 - 인자로 image를 사용할때
		public string SpExecuteString(string SP_name, string gubun, string param_name, byte[] image_data,
			string param_name2, byte[] image_data2, string param_name3, byte[] image_data3,
			params string[] Arrparam)
		{
			paramSetting(SP_name, gubun, Arrparam);

			string message;

			if (param_name != "")
				myDbAgent.MyCommand.Parameters.AddWithValue(param_name, image_data);
			if (param_name2 != "")
				myDbAgent.MyCommand.Parameters.AddWithValue(param_name2, image_data2);
			if (param_name3 != "")
				myDbAgent.MyCommand.Parameters.AddWithValue(param_name3, image_data3);

			message = SpExecuteStringCommon(SP_name);

			return message;
		}

		public string SpExecuteString(string SP_name, string gubun, string param_name, byte[] image_data,
			string param_name2, byte[] image_data2, string param_name3, byte[] image_data3, string param_name4, byte[] image_data4,
			params string[] Arrparam)
		{
			paramSetting(SP_name, gubun, Arrparam);

			string message;

			if (param_name != "")
				myDbAgent.MyCommand.Parameters.AddWithValue(param_name, image_data);
			if (param_name2 != "")
				myDbAgent.MyCommand.Parameters.AddWithValue(param_name2, image_data2);
			if (param_name3 != "")
				myDbAgent.MyCommand.Parameters.AddWithValue(param_name3, image_data3);
			if (param_name4 != "")
				myDbAgent.MyCommand.Parameters.AddWithValue(param_name4, image_data4);

			message = SpExecuteStringCommon(SP_name);

			return message;
		}

		//string 을 반환하는 SpExecuteString 함수의 공통 부분
		private string SpExecuteStringCommon(string SP_name)
		{
			string message = "";

			message = myDbAgent.ExcuteNonQuery();

			if (message.ToString().Length != 0)
			{
				//MessageBox.Show("작업실패!!! : " + message);

				try
				{
					InsertErrorLog(Public_Function.User_id, Public_Function.program_id, SP_name, message, "SQL");

					return "";
				}
				catch (Exception e)
				{
					if (!EventLog.SourceExists("RTEMES"))
					{
						EventLog.CreateEventSource("RTEMES", "");
					}

					EventLog myLog = new EventLog();
					myLog.Source = "RTEMES";

					myLog.WriteEntry(e.Message.ToString());

					return "";
				}
			}
			else
			{
				message = myDbAgent.MyCommand.Parameters["@message"].Value.ToString();

				return myDbAgent.MyCommand.Parameters["@message"].Value.ToString();
			}
		}

		//Table을 반환할때
		public System.Data.DataTable SpExecuteTable(string SP_name, string gubun, params string[] Arrparam)
		{
			paramSetting(SP_name, gubun, Arrparam);

			DataTable dt;

			try
			{
				dt = myDbAgent.ExcuteDataSet().Tables[0];
			}
			catch (Exception e)
			{
				/* 2020.03.24 Bclee 주석처리함 */
				//MessageBox.Show("작업실패!!! : " + e.Message);

				try
				{
					InsertErrorLog(Public_Function.User_id, Public_Function.program_id, SP_name, e.Message, "SQL");

					dt = new DataTable();

					return dt;
				}
				catch (Exception e1)
				{
					if (!EventLog.SourceExists("RTEMES"))
					{
						EventLog.CreateEventSource("RTEMES", "");
					}

					EventLog myLog = new EventLog();
					myLog.Source = "RTEMES";

					myLog.WriteEntry(e1.Message.ToString());

					dt = new DataTable();

					return dt;
				}
			}
			return dt;
		}

		public System.Data.DataTable SpExecuteTableWithoutMessage(string SP_name, string gubun, params string[] Arrparam)
		{
			paramSettingWithoutMessage(SP_name, gubun, Arrparam);

			DataTable dt;

			try
			{
				dt = myDbAgent.ExcuteDataSet().Tables[0];
			}
			catch (Exception e)
			{
				/* 2020.03.24 Bclee 주석처리함 */
				//MessageBox.Show("작업실패!!! : " + e.Message);

				try
				{
					InsertErrorLog(Public_Function.User_id, Public_Function.program_id, SP_name, e.Message, "SQL");

					dt = new DataTable();

					return dt;
				}
				catch (Exception e1)
				{
					if (!EventLog.SourceExists("RTEMES"))
					{
						EventLog.CreateEventSource("RTEMES", "");
					}

					EventLog myLog = new EventLog();
					myLog.Source = "RTEMES";

					myLog.WriteEntry(e1.Message.ToString());

					dt = new DataTable();

					return dt;
				}
			}
			return dt;
		}

		//Table을 반환할때
		public System.Data.DataTable SpExecuteTable(string SP_name, string gubun, string param_name, byte[] image_data, params string[] Arrparam)
		{
			paramSetting(SP_name, gubun, Arrparam);

			DataTable dt;

			if (param_name != "")
				myDbAgent.MyCommand.Parameters.AddWithValue(param_name, image_data);

			try
			{
				dt = myDbAgent.ExcuteDataSet().Tables[0];
			}
			catch (Exception e)
			{
				//MessageBox.Show("작업실패!!! : " + e.Message);

				try
				{
					InsertErrorLog(Public_Function.User_id, Public_Function.program_id, SP_name, e.Message, "SQL");

					dt = new DataTable();

					return dt;
				}
				catch (Exception e1)
				{
					if (!EventLog.SourceExists("RTEMES"))
					{
						EventLog.CreateEventSource("RTEMES", "");
					}

					EventLog myLog = new EventLog();
					myLog.Source = "RTEMES";

					myLog.WriteEntry(e1.Message.ToString());

					dt = new DataTable();

					return dt;
				}
			}

			return dt;
		}

		//DataSet을 반환할때
		public System.Data.DataSet SpExecuteDataSet(string SP_name, string gubun, params string[] Arrparam)
		{
			paramSetting(SP_name, gubun, Arrparam);

			DataSet ds;

			try
			{
				ds = myDbAgent.ExcuteDataSet();
			}
			catch (Exception e)
			{
				//MessageBox.Show("작업실패!!! : " + e.Message);

				try
				{
					InsertErrorLog(Public_Function.User_id, Public_Function.program_id, SP_name, e.Message, "SQL");

					ds = new DataSet();

					return ds;
				}
				catch (Exception e1)
				{
					if (!EventLog.SourceExists("RTEMES"))
					{
						EventLog.CreateEventSource("RTEMES", "");
					}

					EventLog myLog = new EventLog();
					myLog.Source = "RTEMES";

					myLog.WriteEntry(e1.Message.ToString());

					ds = new DataSet();

					return ds;
				}
			}

			return ds;
		}

		//DataSet을 반환할때
		public System.Data.DataSet SpExecuteDataSet(string SP_name, string gubun, string param_name, byte[] image_data, params string[] Arrparam)
		{
			paramSetting(SP_name, gubun, Arrparam);

			DataSet ds;

			if (param_name != "")
				myDbAgent.MyCommand.Parameters.AddWithValue(param_name, image_data);

			try
			{
				ds = myDbAgent.ExcuteDataSet();
			}
			catch (Exception e)
			{
				//MessageBox.Show("작업실패!!! : " + e.Message);

				try
				{
					InsertErrorLog(Public_Function.User_id, Public_Function.program_id, SP_name, e.Message, "SQL");

					ds = new DataSet();

					return ds;
				}
				catch (Exception e1)
				{
					if (!EventLog.SourceExists("RTEMES"))
					{
						EventLog.CreateEventSource("RTEMES", "");
					}

					EventLog myLog = new EventLog();
					myLog.Source = "RTEMES";

					myLog.WriteEntry(e1.Message.ToString());

					ds = new DataSet();

					return ds;
				}
			}

			return ds;
		}

		public DataTable SpExecuteTable(string spName, string strsql)
		{
			try
			{
				myDbAgent.CommandText = spName;
				myDbAgent.MyCommand.Parameters.Clear(); // SP를 새로 시작하기 위해 파라메터 Clear
				myDbAgent.MyCommand.Parameters.AddWithValue("@strsql", strsql);
			}
			finally { }

			DataTable dt = myDbAgent.ExcuteDataSet().Tables[0];

			return dt;
		}

		public void InsertErrorLog(string user_id, string program_id, string sp_name, string message, string type)
		{
			myDbAgent.CommandText = "SP_InsertLog";
			myDbAgent.MyCommand.Parameters.Clear();
			myDbAgent.MyCommand.Parameters.AddWithValue("@user", user_id);
			myDbAgent.MyCommand.Parameters.AddWithValue("@program", program_id);
			myDbAgent.MyCommand.Parameters.AddWithValue("@command", sp_name);
			myDbAgent.MyCommand.Parameters.AddWithValue("@message", message);
			myDbAgent.MyCommand.Parameters.AddWithValue("@type", type);

			myDbAgent.ExcuteNonQuery();
		}
	}

}