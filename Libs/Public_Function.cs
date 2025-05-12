using HACCP.Libs.Database;
using HACCP.Libs.File;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace HACCP.Libs
{
	public class Public_Function
	{
		#region 사용자 정의 변수
		//시스템 정보
		//public static string CONFIGURATION_FILE = "RTEGMS_SYSTEM.ini";
		public static string CONFIGURATION_FILE = System.AppDomain.CurrentDomain.BaseDirectory + "//RTEGMS_SYSTEM.ini";
		public static string selectedDB = "";
		public static string selectedDBName = "";
		public static string selectedPLANT = "";
		public static string selectedPLANTName = "";
		public static string ReportServer = "";
		public static string selectedDBIp = "";
		public static string selectedDBId = "";
		public static string selectedDBPw = "";

		// SMTP 정보
		public static int emlPort = 0;
		public static string emlHost = "";
		public static string emlCredentialsID = "";
		public static string emlCredentialsPW = "";
		public static string emlFromAccount = "";
		public static string emlBcc = "";

		// 프로그램정보//
		public static string project = "";
		public static string product_type = "";
		public static string company = "";
		public static string version = "";
		public static string position = "";

		// 공정(반제품)간 인수인계 여부 (Y :  사용,  N : 미사용)
		public static string process_transfer_yn = "";

		// 마스터데이터 코드 자동 채번 여부 ('Y' - 자동채번 / 'N' - 수동채번)
		// ( 부서 / 사원 / 작업실 / 설비 / 원료 / 자재 / 제조제품 / 제품 )
		public static string MasterData_AutoNumbering_yn = "";

		// 로그인 사용자 정보//
		public static string User_id = null; //사용자ID
		public static string User_id_nm = null; //사용자명
		public static string User_cd = null;    //사번
		public static string User_nm = null;    //사원명
		public static string Dept_cd = null;    //부서코드
		public static string Dept_nm = null;    //부서명
		public static string emp_charge = null; //담당코드
		public static string program_id = null;
		public static string End_time = null;	//비밀번호 유효일자

		public static string Source_Dictory = @"\\AnalysisSvr\공유\test";
		public static System.Drawing.Color BrowseColor = System.Drawing.Color.White;
		public static System.Drawing.Color EditColor = System.Drawing.Color.Beige;

		public static bool Bio_Check = false;
		public static bool validation_type = false;
		public static byte validation_type_worker;
		public static byte validation_type_inspect;
		public static bool schedule_ck = false;

		public static bool Pass_Check = false;
		public static bool Num_Check = false;
		public static double NumValue = 0;
		public static double Net_Value = 0;

		public enum StandardUnit { kg, g, mg };

		//기호문자 저장 변수
		public static string Sysbol = "";

		//User_Select : 사원입력
		public static string _user_cd;  //사용자 코드
		public static string _user_nm;  //사용자 성명
		public static string _dept_cd;  //사용자 부서 코드
		public static string _dept_nm;  //사용자 부서 명

		//지문이미지절대좌표위치
		public static int bio_positon_x;
		public static int bio_positon_y;

		// Access Log //
		public static string system_log_id = null;  //시스템 로그 ID

		//ID/PWD부여시 체크사항
		public static int MinimumLengthToID;            //ID 최소자수
		public static int MinimumLengthToPWD;           //PWD 최소자수
		public static string ObligationMixingID;        //ID 영문,숫자 혼합사용 강제 
		public static string ObligationMixingPWD;       //PWD 영문,숫자 혼합사용 강제 
		public static string AllowenceIdentityID;       //ID 동일문자 연속중복 허용
		public static string AllowenceIdentityPWD;      //PWD 동일문자 연속중복 허용
		public static int PreCommonCharID;              //이전ID와 공통으로 사용할 수 있는 문자의 수제한
		public static int PreCommonCharPassword;        //이전ID와 공통으로 사용할 수 있는 문자의 수제한

		public static string AppendID;                  //패스워드 등록 시 사용자의 (등록/임시/사용중)ID
		public static string AppendPWD;                 //패스워드 등록 시 사용자의 등록 패스워드
		public static string NewUser;                   //로그인 및 인증자의 새로운 등록자 여부(Y,N)
		public static string IDReuse;                   //동일ID 재사용 여부
		public static string PasswordReuse;             //동일PASSWORD 재사용 여부

		public static string PreID;                     //이전ID
		public static string PrePWD;                    //이전패스워드

		public static string AllowenceIdentity;         //ID와 PWD 동일스트링 허용

		//ID/PWD인증시 체크사항
		public static string AllowenceRootID;           //원ID허용
		public static string AllowenceRootPWD;          //원PWD허용
		public static int TimeLimitToPWD;               //PWD 사용기한제한(일자입력, 0일 경우 제한없음)
		public static int NumWrongID;                   //ID/PWD 오입력 허용횟수
		public static int failed_num;                   //특정ID에 대한 패스워드 오입력 허용회수

		public static string EncryptIdentification;     //ID/PWD 암호화/복호와 사용여부
		public static string AuthenticType;             //인증타입(P:패스워드, PF:패스워드/지문, F:지문)

		public static int WrongInput;                   //ID/PWD 입력오류횟수
		public static string MultiLogin;                //다중로그인허용여부

		//인증시 메시지 표시
		public static string IdentificationMessage;

		//추가
		public static string BarcodePrefix_Pack = null;     //팩 바코드 Prefix
		public static string BarcodePrefix_Workroom = null; //작업실 바코드 Prefix
		public static string BarcodePrefix_Zone = null;     //구역 바코드 Prefix
		public static string BarcodePrefix_Cell = null;     //셀 바코드 Prefix
		public static string BarcodePrefix_Pallet = null;   //팔레트 바코드 Prefix
		public static string BarcodePrefix_Bulk = null;     //반제품 바코드 Prefix
		public static string BarcodePrefix_Box = null;      //지함 바코드 Prefix
		#endregion

		//시간지연
		public static void TimeDelay(byte delaytime, Control x)
		{
			DateTime dt = DateTime.Now;
			int timelimit = dt.Hour * 3600 + dt.Minute * 60 + dt.Second;
			while (timelimit > 0)
			{
				DateTime dx = DateTime.Now;
				if ((dx.Hour * 3600 + dx.Minute * 60 + dx.Second) - timelimit > delaytime)
					timelimit = 0;
			}
		}

		#region 기초 함수
		//현재 시간표시
		public static string GetDateTime_Now()
		{
			return System.DateTime.Now.ToString("yyyy-MM-dd hh:mm");
		}

		public static string GetDate_Now()
		{
			return System.DateTime.Now.ToString("yyyy-MM-dd");
		}

		//날짜타입으로 값을 넘긴다.
		public static string datetype(string date)  //
		{
			string date_value = "";

			int i;

			for (i = 0; i <= (date.Length - 1); i++)
			{
				date_value = date_value + date.Substring(i, 1).Trim('-');
			}
			if (date_value.Length == 8)
			{
				date_value = date_value.Substring(0, 4) + "-" + date_value.Substring(4, 2) + "-" + date_value.Substring(6, 2);
			}

			return date_value;
		}

		//차트기간 변경시 년도변경월 구함
		public static int Select_YearChange()
		{
			int year_change = 0;

			year_change = System.DateTime.Today.Month;

			if ((year_change >= 1) && (year_change <= 3))
				year_change = 12 - 3 + year_change;
			else
				year_change = year_change - 3;

			year_change = 12 - year_change + 1;

			return year_change;
		}

		//기본값 설정
		public static string SetZero(string strBuffer, string strFill)
		{
			if (strBuffer == "")
				return strFill;
			else
				return strBuffer;
		}
		public static string SetZero(string strBuffer)
		{
			return SetZero(strBuffer, "0");
		}
		#endregion

		#region INI 환경설정파일
		public static string GetIniValues(string section, string key, string path)
		{
			try
			{
				string return_value = IniReadWrite.G_IniReadValue(section, key);
				if (return_value == null)
					return_value = "";

				return return_value;
			}
			catch { return ""; }
		}
		public static void SetIniValues(string section, string key, string value, string path)
		{
			try
			{
				IniReadWrite.G_IniWriteValue(section, key, value);
			}
			catch { }
		}
		#endregion INI 환경설정파일


		#region 기초 데이타 관련 함수 
		//public static void GetMaster(DevExpress.XtraEditors.LookUpEdit le_gb, string vargb, string div)
		//{
		//	GetMaster(le_gb, vargb, div, "");
		//}

		//public static void GetMaster(DevExpress.XtraEditors.LookUpEdit le_gb, string vargb)
		//{
		//	GetMaster(le_gb, vargb, "D", "");
		//}

		//마스터를 데이터테이블로 리턴하도록 추가함.(데이터 재활용을 위해서 추가)
		public static DataTable GetMaster(string vargb, string div, string strWhere)
		{
			DbAgent myDbAgent = new DbAgent("", "", System.Data.CommandType.StoredProcedure, Public_Function.selectedDB);
			// 사용할 SP(or T-SQL문) 지정 
			myDbAgent.CommandText = "SP_GETMASTER";
			myDbAgent.MyCommand.Parameters.AddWithValue("@gb", vargb);
			myDbAgent.MyCommand.Parameters.AddWithValue("@message", "");
			myDbAgent.MyCommand.Parameters.AddWithValue("@div", div);
			myDbAgent.MyCommand.Parameters.AddWithValue("@strwhere", strWhere);

			//2012.04.08 최석중 추가 (전체적으로 사업장 반영, 로그인 사원 추가하여 audit trail에서 사용)
			myDbAgent.MyCommand.Parameters.AddWithValue("@sys_plant_cd", Public_Function.selectedPLANT);
			myDbAgent.MyCommand.Parameters.AddWithValue("@sys_emp_cd", Public_Function.User_cd);

			return myDbAgent.ExcuteDataSet().Tables[0];
		}

		public static DataTable GetMaster(string vargb, string div)
		{
			return GetMaster(vargb, div, "");
		}

		public static DataTable GetMaster(string vargb)
		{
			return GetMaster(vargb, "D", "");
		}

		public static DataTable GetEquipment(string equ_Workroom, string equtype)
		{
			string[] param = new string[2];

			param[0] = "@WORKROOM_CD:" + equ_Workroom;
			param[1] = "@S_EQUIP_TYPE:" + equtype;

			BllSpExecute mBllSpExecute = new BllSpExecute();
			DataTable dt = mBllSpExecute.SpExecuteTable("SP_EQUIPMENT", "S1", param);

			return dt;
		}

		#endregion

		#region 사용자 권한에 따른 폼의 툴바버튼 사용설정 초기화...
		public static bool GetToolBarEnabled(string form_id, ref ItemToUse destItemToUse)
		{
			string form_query;
			string form_edit;
			string form_insert;
			string form_delete;
			string form_print;
			string form_transmission;
			ItemToUse tmpItemtoUse;
			ItemToUse Authority;
			DbAgent myDbAgent = new DbAgent("", "", System.Data.CommandType.StoredProcedure, Public_Function.selectedDB);
			myDbAgent.CommandText = "SP_Menu_GetMenu"; // 사용할 SP(or T-SQL문) 지정
			myDbAgent.MyCommand.Parameters.Clear(); // SP를 새로 시작하기 위해 파라메터 Clear
													// Parameter설정
			myDbAgent.MyCommand.Parameters.AddWithValue("@Gubun", "F");
			myDbAgent.MyCommand.Parameters.AddWithValue("@emp_cd", User_cd);
			myDbAgent.MyCommand.Parameters.AddWithValue("@form_id", form_id);

			//2012.04.08 최석중 추가 (전체적으로 사업장 반영, 로그인 사원 추가하여 audit trail에서 사용)
			myDbAgent.MyCommand.Parameters.AddWithValue("@sys_plant_cd", Public_Function.selectedPLANT);
			myDbAgent.MyCommand.Parameters.AddWithValue("@sys_emp_cd", Public_Function.User_cd);

			System.Data.DataSet myDataSet = myDbAgent.ExcuteDataSet();
			form_query = myDataSet.Tables[0].Rows[0]["form_query"].ToString();
			form_edit = myDataSet.Tables[0].Rows[0]["form_edit"].ToString();
			form_insert = myDataSet.Tables[0].Rows[0]["form_insert"].ToString();
			form_delete = myDataSet.Tables[0].Rows[0]["form_delete"].ToString();
			form_print = myDataSet.Tables[0].Rows[0]["form_print"].ToString();
			form_transmission = myDataSet.Tables[0].Rows[0]["form_transmission"].ToString();

			tmpItemtoUse = destItemToUse;

			Authority.search = false;
			Authority.insert = false;
			Authority.edit = false;
			Authority.delete = false;
			Authority.preview = false;
			Authority.print = false;
			Authority.export = false;

			if (form_query == "Y")
				Authority.search = true;
			if (form_edit == "Y")
				Authority.edit = true;
			if (form_insert == "Y")
				Authority.insert = true;
			if (form_delete == "Y")
				Authority.delete = true;
			if (form_print == "Y")
			{
				Authority.preview = true;
				Authority.print = true;
			}
			if (form_transmission == "Y")
				Authority.export = true;

			destItemToUse.search = tmpItemtoUse.search && Authority.search;
			destItemToUse.insert = tmpItemtoUse.insert && Authority.insert;
			destItemToUse.edit = tmpItemtoUse.edit && Authority.edit;
			destItemToUse.delete = tmpItemtoUse.delete && Authority.delete;
			destItemToUse.preview = tmpItemtoUse.preview && Authority.preview;
			destItemToUse.print = tmpItemtoUse.print && Authority.print;
			destItemToUse.export = tmpItemtoUse.export && Authority.export;

			return true;
		}

		public static bool GetToolBarEnabled2(string form_id, ref ItemToUse2 destItemToUse)
		{
			string form_query;
			string form_edit;
			string form_insert;
			string form_delete;
			string form_print;
			string form_transmission;
			string form_edit2;
			string form_insert2;
			string form_delete2;
			string form_edit3;
			string form_insert3;
			string form_delete3;

			ItemToUse2 tmpItemtoUse;
			ItemToUse2 Authority;
			DbAgent myDbAgent = new DbAgent("", "", System.Data.CommandType.StoredProcedure, Public_Function.selectedDB);
			myDbAgent.CommandText = "SP_Menu_GetMenu"; // 사용할 SP(or T-SQL문) 지정
			myDbAgent.MyCommand.Parameters.Clear(); // SP를 새로 시작하기 위해 파라메터 Clear
													// Parameter설정
			myDbAgent.MyCommand.Parameters.AddWithValue("@Gubun", "F");
			myDbAgent.MyCommand.Parameters.AddWithValue("@emp_cd", User_cd);
			myDbAgent.MyCommand.Parameters.AddWithValue("@form_id", form_id);

			System.Data.DataSet myDataSet = myDbAgent.ExcuteDataSet();
			form_query = myDataSet.Tables[0].Rows[0]["form_query"].ToString();
			form_edit = form_edit2 = form_edit3 = myDataSet.Tables[0].Rows[0]["form_edit"].ToString();
			form_insert = form_insert2 = form_insert3 = myDataSet.Tables[0].Rows[0]["form_insert"].ToString();
			form_delete = form_delete2 = form_delete3 = myDataSet.Tables[0].Rows[0]["form_delete"].ToString();
			form_print = myDataSet.Tables[0].Rows[0]["form_print"].ToString();
			form_transmission = myDataSet.Tables[0].Rows[0]["form_transmission"].ToString();
			tmpItemtoUse = destItemToUse;

			Authority.search = false;
			Authority.insert = Authority.insert2 = Authority.insert3 = false;
			Authority.edit = Authority.edit2 = Authority.edit3 = false;
			Authority.delete = Authority.delete2 = Authority.delete3 = false;
			Authority.preview = false;
			Authority.print = false;
			Authority.export = false;

			if (form_query == "Y")
				Authority.search = true;
			if (form_edit == "Y")
				Authority.edit = Authority.edit2 = Authority.edit3 = true;
			if (form_insert == "Y")
				Authority.insert = Authority.insert2 = Authority.insert3 = true;
			if (form_delete == "Y")
				Authority.delete = Authority.delete2 = Authority.delete3 = true;
			if (form_print == "Y")
			{
				Authority.preview = true;
				Authority.print = true;
			}
			if (form_transmission == "Y")
				Authority.export = true;

			destItemToUse.search = tmpItemtoUse.search && Authority.search;
			destItemToUse.insert = destItemToUse.insert2 = destItemToUse.insert3 = tmpItemtoUse.insert && Authority.insert;
			destItemToUse.edit = destItemToUse.edit2 = destItemToUse.edit3 = tmpItemtoUse.edit && Authority.edit;
			destItemToUse.delete = destItemToUse.delete2 = destItemToUse.delete3 = tmpItemtoUse.delete && Authority.delete;
			destItemToUse.preview = tmpItemtoUse.preview && Authority.preview;
			destItemToUse.print = tmpItemtoUse.print && Authority.print;
			destItemToUse.export = tmpItemtoUse.export && Authority.export;

			return true;
		}
		#endregion

		#region 코드여부 체킹
		public static bool IsCode(string strtemp)
		{
			System.Text.ASCIIEncoding ae = new System.Text.ASCIIEncoding();
			byte[] byteArray = ae.GetBytes(strtemp);

			for (int i = 0; i < byteArray.Length; i++)
				if (!((48 <= byteArray[i] && byteArray[i] <= 57) ||
					  (65 <= byteArray[i] && byteArray[i] <= 90) ||
					  (97 <= byteArray[i] && byteArray[i] <= 122) ||
					  (byteArray[i] == 37)))
					return false;

			return true;
		}
		#endregion

		//기본값 설정
		public static string SetDefValue(string strBuffer, string strFill)
		{
			if (strBuffer == "" || strBuffer == null)
				return strFill;
			else
				return strBuffer;
		}

		public static string SetDefValue(string strBuffer)
		{
			return SetZero(strBuffer, "0");
		}

		//전달인자의 숫자여부 확인
		public static bool IsNumeric(string strtemp)
		{
			int i;
			//			int sPos;
			//			int VarsPos;
			//			bool IsPt;

			if (strtemp == "")
				return false;

			System.Text.ASCIIEncoding ae = new System.Text.ASCIIEncoding();
			byte[] byteArray = ae.GetBytes(strtemp);

			for (i = 0; i < byteArray.Length; i++)
				if (!(48 <= byteArray[i] && byteArray[i] <= 57) &&
					(byteArray[i] != 44) &&
					(byteArray[i] != 46))
					return false;

			return true;
		}

		//전달인자의 알파벳여부 확인
		public static bool IsAlphabet(string strtemp)
		{
			System.Text.ASCIIEncoding ae = new System.Text.ASCIIEncoding();
			byte[] byteArray = ae.GetBytes(strtemp);

			for (int i = 0; i < byteArray.Length; i++)
				if (!((65 <= byteArray[i] && byteArray[i] <= 90) ||
					(97 <= byteArray[i] && byteArray[i] <= 122) ||
					(byteArray[i] == 37)))
					return false;

			return true;
		}

		//전달인자의 16진수 여부 확인
		public static bool IsHexa(string strtemp)
		{
			int i;

			if (strtemp == "")
				return false;

			System.Text.ASCIIEncoding ae = new System.Text.ASCIIEncoding();
			byte[] byteArray = ae.GetBytes(strtemp);

			for (i = 0; i < byteArray.Length; i++)
				if (!(48 <= byteArray[i] && byteArray[i] <= 57) &&
					!(65 <= byteArray[i] && byteArray[i] <= 90) &&
					!(97 <= byteArray[i] && byteArray[i] <= 122))
					return false;

			return true;
		}

		//설명: 받아온 데이터에','를 삽입하여 리턴
		public static string gf_InsertComma(string strTemp)
		{
			if (strTemp == "")
				return "0";
			int nLen;
			strTemp = Convert.ToDouble(strTemp).ToString("###,###,##0.####");

			nLen = strTemp.Length;
			if (strTemp.Substring(nLen - 1) == ".")
				strTemp = strTemp.Substring(1, strTemp.Length - 1);

			return strTemp;
		}

		//통라벨에서 읽어온 바코드데이터를 가공하여 현 코드체계에 맞추어 반환
		public static string gf_AlterBarCode(string strTemp)
		{
			string material;
			string part;
			string size;
			string serial;
			string strBuffer = "";

			if (strTemp.Length == 6)
			{
				part = strTemp.Substring(0, 1);
				material = strTemp.Substring(1, 1);
				size = strTemp.Substring(2, 1);
				serial = strTemp.Substring(3, 3);

				switch (part)
				{
					case "A":
						strBuffer = "01";
						break;
					case "B":
						strBuffer = "02";
						break;
					case "C":
						strBuffer = "03";
						break;
					case "D":
						strBuffer = "04";
						break;
				}
				strTemp = strBuffer;

				switch (material)
				{
					case "A":
						strBuffer = "01";
						break;
					case "B":
						strBuffer = "02";
						break;
					case "C":
						strBuffer = "03";
						break;
					case "D":
						strBuffer = "04";
						break;
				}
				strTemp += strBuffer;

				switch (size)
				{
					case "A":
						strBuffer = "01";
						break;
					case "B":
						strBuffer = "02";
						break;
					case "C":
						strBuffer = "03";
						break;
					case "D":
						strBuffer = "04";
						break;
				}
				strTemp += strBuffer;
				strTemp += serial;
			}
			return strTemp;
		}

		//현 코드체계 바코드데이터를 가공하여 통라벨에서 코드체계에 맞추어 반환
		public static string gf_BeforeBarCode(string strTemp)
		{
			string part;
			string material;
			string size;
			string serial;
			string strBuffer = "";

			if (strTemp.Length == 9)
			{
				part = strTemp.Substring(0, 2);
				material = strTemp.Substring(2, 2);
				size = strTemp.Substring(4, 2);
				serial = strTemp.Substring(6, 3);

				switch (part)
				{
					case "01":
						strBuffer = "A";
						break;
					case "02":
						strBuffer = "B";
						break;
					case "03":
						strBuffer = "C";
						break;
					case "04":
						strBuffer = "D";
						break;
				}

				strTemp = strBuffer;

				switch (material)
				{
					case "01":
						strBuffer = "A";
						break;
					case "02":
						strBuffer = "B";
						break;
					case "03":
						strBuffer = "C";
						break;
					case "04":
						strBuffer = "D";
						break;
				}

				strTemp += strBuffer;

				switch (size)
				{
					case "01":
						strBuffer = "A";
						break;
					case "02":
						strBuffer = "B";
						break;
					case "03":
						strBuffer = "C";
						break;
					case "04":
						strBuffer = "D";
						break;
				}
				strTemp += strBuffer;
				strTemp += serial;
			}
			return strTemp;
		}

		/// <summary>
		/// 저울로 부터 읽어온 무게 데이터 단위 변환 (dataSourceUnit -> targetUnit)
		/// </summary>
		public static double ConvertUnit(string data, StandardUnit targetUnit)
		{
			double result = 0;

			try
			{
				data = data.Split(',')[1].Replace("+", "");
				string sourceValue = string.Empty;
				string sourceUnit = string.Empty;
				bool valueEnd = false;
				for (int i = 0; i < data.Length; i++)
				{
					if (data.Substring(i, 1) == " ")
					{
						valueEnd = true;
						continue;
					}

					if (valueEnd && data.Substring(i, 1) != " ")
						sourceUnit += data.Substring(i, 1);
					else
						sourceValue += data.Substring(i, 1);
				}

				switch (targetUnit)
				{
					case StandardUnit.kg:
						{
							switch (sourceUnit.ToUpper())
							{
								// KG -> KG
								case "KG":
									result = double.Parse(sourceValue);
									break;
								// G -> KG
								case "G":
									result = double.Parse(sourceValue) / 1000;
									break;
								// MG -> KG
								case "MG":
									result = double.Parse(sourceValue) / 1000 / 1000;
									break;
							}
						}
						break;
					case StandardUnit.g:
						{
							switch (sourceUnit.ToUpper())
							{
								// KG -> G
								case "KG":
									result = double.Parse(sourceValue) * 1000;
									break;
								// G -> G
								case "G":
									result = double.Parse(sourceValue);
									break;
								// MG -> G
								case "MG":
									result = double.Parse(sourceValue) / 1000;
									break;
							}
						}
						break;
					case StandardUnit.mg:
						{
							switch (sourceUnit.ToUpper())
							{
								// KG -> MG
								case "KG":
									result = double.Parse(sourceValue) * 1000 * 1000;
									break;
								// G -> MG
								case "G":
									result = double.Parse(sourceValue) * 1000;
									break;
								// MG -> MG
								case "MG":
									result = double.Parse(sourceValue);
									break;
							}
						}
						break;
				}

				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return result;
			}
		}

		#region ID/PWD 부여로직

		public static bool AllowanceCheck(string gb, string strobject, string strID, ref string mesg)
		{
			if (!AllowanceCheck(gb, strobject, ref mesg))
			{
				return false;
			}

			//ID와 패스워드 중복성 체크
			if (!CheckIDPWDChar(strobject, strID, ref mesg))
			{
				return false;
			}

			return true;
		}

		public static bool AllowanceCheck(string gb, string strobject, ref string mesg)
		{
			mesg = "입력허용";

			if (gb == "ID")
			{
				if (MinimumLengthToID > strobject.Length)
				{
					mesg = "입력하신 ID는 허용하는 최소자수 " + MinimumLengthToID.ToString() + "를(을) 만족하지 않습니다.";
					return false;
				}

				//코스맥스 바이오 임시 주석
				//if(ObligationMixingID == "Y")
				//{
				//    if(!CheckMixing(strobject))
				//    {
				//        mesg = "영문과 숫자를 혼용하여 입력하여 주십시오.";
				//        return false;
				//    }
				//}

				//if(AllowenceIdentityID == "N")
				//{
				//    if(CheckIdentity(strobject))
				//    {
				//        mesg = "동일문자를 연속하여 사용하지 마십시오.";
				//        return false;
				//    }
				//}

				//if(AllowenceIdentity == "N")
				//{
				//    if(AppendPWD == strobject)
				//    {
				//        mesg = "ID와 동일한 패스워드를 사용하지 마십시오.";
				//        return false;
				//    }
				//}
			}
			else if (gb == "PWD")
			{
				if (MinimumLengthToPWD > strobject.Length)
				{
					mesg = "입력하신 패스워드는 허용하는 최소자수 " + MinimumLengthToPWD.ToString() + "를(을) 만족하지 않습니다.";
					return false;
				}

				//if(ObligationMixingPWD == "Y")
				//{
				//    if(!CheckMixing(strobject))
				//    {
				//        mesg = "영문과 숫자를 혼용하여 입력하여 주십시오.";
				//        return false;
				//    }
				//}

				//if(AllowenceIdentityPWD == "N")
				//{
				//    if(CheckIdentity_PWD(strobject))
				//    {
				//        mesg = "동일문자를 연속하여 사용할 수 없습니다.";
				//        return false;
				//    }
				//}

				//if(AllowenceIdentity == "N")
				//{
				//    if(AppendID == strobject)
				//    {
				//        mesg = "ID와 동일한 패스워드를 사용하지 마십시오.";
				//        return false;
				//    }
				//}
			}

			if (!CheckPreIDChar(gb, strobject, ref mesg))
			{
				return false;
			}

			return true;
		}

		//알파벳과 패스워드 혼합사용체크(true:숫자와문자 모두사용, false;숫자나 문자만 사용)
		private static bool CheckMixing(string strobject)
		{
			bool fAlphatbet = false;
			bool fNumber = false;
			string strtemp = "";

			for (int i = 0; i < strobject.Length; i++)
			{
				strtemp = strobject.Substring(i, 1);

				if (IsAlphabet(strtemp))
					fAlphatbet = true;

				if (IsNumeric(strtemp))
					fNumber = true;


				if (fAlphatbet && fNumber)
					return true;
			}

			return false;
		}

		//동일문자 연속중복여부 체크(true:동일문자연속사용발견, false:동일문자연속사용 미발견)
		//4번 이상으로 변경 2012-04-09 by 최석중
		private static bool CheckIdentity(string strobject)
		{
			string strpre = "";
			string strtemp = "";

			//for(int i = 0; i < strobject.Length; i++)
			//{
			//    strtemp = strobject.Substring(i,1);

			//    if(strtemp == strpre)
			//        return true;

			//    strpre = strtemp;
			//}

			//return false;

			//4번 이상으로 변경 2012-04-09 by 최석중
			int temp = 0;

			for (int i = 0; i < strobject.Length; i++)
			{
				strtemp = strobject.Substring(i, 1);

				if (strtemp == strpre)
					temp = temp + 1;

				strpre = strtemp;
			}

			if (temp >= 4)
				return true;

			return false;
		}

		private static bool CheckIdentity_PWD(string strobject)
		{
			string strpre = "";
			string strtemp = "";

			//1번 이상으로 변경 2012-04-09 by 최석중
			int temp = 0;

			for (int i = 0; i < strobject.Length; i++)
			{
				strtemp = strobject.Substring(i, 1);

				if (strtemp == strpre)
					temp = temp + 1;

				strpre = strtemp;
			}

			if (temp >= 1)
				return true;

			return false;
		}

		public static byte AuthenticationCheck(string gb, string strobject, ref string mesg)
		{
			mesg = "체크성공";

			if (gb == "ID")
			{
				if (AllowenceRootPWD == "N")
				{
					if (NewUser == "Y")
					{
						mesg = "시스템 최초사용입니다. ID와 패스워드를 변경하여 주십시오.";
						return 1;
					}
				}

				return 0;
			}
			else if (gb == "PWD")
			{
				if (AllowenceRootPWD == "N")
				{
					if (NewUser == "Y")
					{
						mesg = "시스템 최초사용입니다. 패스워드를 변경하여 주십시오.";
						return 2;
					}
				}

				return 0;
			}

			return 0;
		}

		//현ID에대한 이전ID 동일문장 허용 체크루틴
		private static bool CheckPreIDChar(string gb, string strobject, ref string mesg)
		{
			int i = 0;
			int j = 0;
			int ncount = 0;
			string strDest = "";
			string obj = "";

			if (gb == "ID")
				strDest = PreID;
			else
				strDest = PrePWD;


			for (i = 0; i < strDest.Length; i++)
			{
				obj = strDest.Substring(i, 1);
				for (j = 0; j < strobject.Length; j++)
				{
					if (obj == strobject.Substring(j, 1))
						ncount++;
				}
			}

			if (gb == "ID")
			{
				if (ncount > PreCommonCharID)
				{
					mesg = "이전 ID와 동일문자를 제한숫자 " + PreCommonCharID.ToString() + "자 이상 사용하였습니다.";
					return false;
				}
				else
					return true;
			}
			else
			{
				if (ncount > PreCommonCharPassword)
				{
					mesg = "이전 패스워드와 동일문자를 제한숫자 " + PreCommonCharPassword.ToString() + "자 이상 사용하였습니다.";
					return false;
				}
				else
					return true;
			}
		}

		//ID와 패스워드 동일문장 허용 체크루틴
		private static bool CheckIDPWDChar(string strobject, string strID, ref string mesg)
		{
			int i = 0;
			int j = 0;
			int ncount = 0;
			string strDest = "";
			string obj = "";

			strDest = strID;

			for (i = 0; i < strDest.Length; i++)
			{
				obj = strDest.Substring(i, 1);
				for (j = 0; j < strobject.Length; j++)
				{
					if (obj == strobject.Substring(j, 1))
						ncount++;
				}
			}

			if (ncount > 4)
			{
				mesg = "ID와 동일문자를 4자 이상 사용하였습니다.";
				return false;
			}
			else
				return true;
		}
		#endregion

		#region 암호화 정책
		/**
		 * Get ASCII Integer Code
		 *
		 * @param char ch Character to get ASCII value for
		 * @access private
		 * @return int
		 */
		private static int ord(char ch)
		{
			return (int)(Encoding.GetEncoding(1252).GetBytes(ch + "")[0]);
		}
		/**
		 * Get character representation of ASCII Code
		 *
		 * @param int i ASCII code
		 * @access private
		 * @return char
		 */
		private static char chr(int i)
		{
			byte[] bytes = new byte[1];
			bytes[0] = (byte)i;
			return Encoding.GetEncoding(1252).GetString(bytes)[0];
		}
		/**
		 * Convert Hex to Binary (hex2bin)
		 *
		 * @param string packtype Rudimentary in this implementation
		 * @param string datastring Hex to be packed into Binary
		 * @access private
		 * @return string
		 */
		private static string pack(string packtype, string datastring)
		{
			int i, j, datalength, packsize;
			byte[] bytes;
			char[] hex;
			string tmp;

			datalength = datastring.Length;
			packsize = (datalength / 2) + (datalength % 2);
			bytes = new byte[packsize];
			hex = new char[2];

			for (i = j = 0; i < datalength; i += 2)
			{
				hex[0] = datastring[i];
				if (datalength - i == 1)
					hex[1] = '0';
				else
					hex[1] = datastring[i + 1];
				tmp = new string(hex, 0, 2);
				try { bytes[j++] = byte.Parse(tmp, System.Globalization.NumberStyles.HexNumber); }
				catch { } /* grin */
			}
			return Encoding.GetEncoding(1252).GetString(bytes);
		}
		/**
		 * Convert Binary to Hex (bin2hex)
		 *
		 * @param string bindata Binary data
		 * @access public
		 * @return string
		 */
		public static string bin2hex(string bindata)
		{
			int i;
			byte[] bytes = Encoding.GetEncoding(1252).GetBytes(bindata);
			string hexString = "";
			for (i = 0; i < bytes.Length; i++)
			{
				hexString += bytes[i].ToString("x2");
			}
			return hexString;
		}
		/**
		 * The symmetric encryption function
		 *
		 * @param string pwd Key to encrypt with (can be binary of hex)
		 * @param string data Content to be encrypted
		 * @param bool ispwdHex Key passed is in hexadecimal or not
		 * @access public
		 * @return string
		 */
		public static string Encrypt(string pwd, string data, bool ispwdHex)
		{
			int a, i, j, k, tmp, pwd_length, data_length;
			int[] key, box;
			byte[] cipher;
			//string cipher;

			if (ispwdHex)
				pwd = pack("H*", pwd); // valid input, please!
			pwd_length = pwd.Length;
			data_length = data.Length;
			key = new int[256];
			box = new int[256];
			cipher = new byte[data.Length];
			//cipher = "";

			for (i = 0; i < 256; i++)
			{
				key[i] = ord(pwd[i % pwd_length]);
				box[i] = i;
			}
			for (j = i = 0; i < 256; i++)
			{
				j = (j + box[i] + key[i]) % 256;
				tmp = box[i];
				box[i] = box[j];
				box[j] = tmp;
			}
			for (a = j = i = 0; i < data_length; i++)
			{
				a = (a + 1) % 256;
				j = (j + box[a]) % 256;
				tmp = box[a];
				box[a] = box[j];
				box[j] = tmp;
				k = box[((box[a] + box[j]) % 256)];
				cipher[i] = (byte)(ord(data[i]) ^ k);
				//cipher += chr(ord(data[i]) ^ k);
			}
			return Encoding.GetEncoding(1252).GetString(cipher);
			//return cipher;
		}
		/**
		 * Decryption, recall encryption
		 *
		 * @param string pwd Key to decrypt with (can be binary of hex)
		 * @param string data Content to be decrypted
		 * @param bool ispwdHex Key passed is in hexadecimal or not
		 * @access public
		 * @return string
		 */
		public static string Decrypt(string pwd, string data, bool ispwdHex)
		{
			return Encrypt(pwd, data, ispwdHex);
		}
		#endregion

		#region 엑셀 파일저장
		//public static void ExportExcel(string title, DevExpress.XtraGrid.Views.Grid.GridView gview)
		//{
		//	SaveFileDialog saveExcel = new SaveFileDialog();
		//	saveExcel.Filter = "엑셀파일 (*.xls)|*.xls|All files (*.*)|*.*";
		//	saveExcel.FilterIndex = 1;
		//	saveExcel.FileName = title + '_' + System.DateTime.Today.ToString("yyyy-MM-dd");
		//	saveExcel.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		//	saveExcel.RestoreDirectory = true;

		//	if (saveExcel.ShowDialog() == DialogResult.OK)
		//	{
		//		string fileName = saveExcel.FileName;
		//		gview.OptionsPrint.AutoWidth = false;

		//		////필드별 설정 정보 변수
		//		//string[] setting = new string[gview.Columns.Count];

		//		////필드별로 적용
		//		//for (int i = 0; i < gview.Columns.Count; i++)
		//		//{
		//		//    //visible 설정 저장
		//		//    setting[i] = gview.Columns[i].Visible.ToString();

		//		//    //필드 보이게 설정
		//		//    if (setting[i].ToString() == "False")
		//		//    {
		//		//        gview.Columns[i].Visible = true;
		//		//    }
		//		//}

		//		//다운로드
		//		gview.ExportToXls(saveExcel.FileName);

		//		////필드별로 적용
		//		//for (int i = 0; i < gview.Columns.Count; i++)
		//		//{
		//		//    //필드 안보이게 설정
		//		//    if (setting[i].ToString() == "False")
		//		//    {
		//		//        gview.Columns[i].Visible = false;
		//		//    }
		//		//}

		//		try
		//		{
		//			DialogResult result = MessageBox.Show("파일이 저장되었습니다. 확인하시겠습니까?", "파일보기", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		//			if (result == DialogResult.Yes)
		//			{
		//				Process.Start(fileName, "EXCEL");
		//			}
		//		}
		//		catch (Exception)
		//		{
		//			MessageBox.Show("프로그램경로가 잘못되었거나, 엑셀이 설치되어 있지 않습니다.");
		//		}
		//	}
		//}

		////추가 -엑셀파일 저장(GridView 2)
		//public static void ExportExcel(string title, string title2, DevExpress.XtraGrid.Views.Grid.GridView gview, DevExpress.XtraGrid.Views.Grid.GridView gview2)
		//{

		//	string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);          //경로 
		//	string fileName = filePath + title + ".xls";                                                //파일명
		//	string fileName2 = filePath + title2 + ".xls";
		//	gview.ExportToXls(fileName);
		//	gview2.ExportToXls(fileName2);

		//	try
		//	{
		//		DialogResult result = MessageBox.Show("파일이 저장되었습니다. 확인하시겠습니까?", "파일보기", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		//		if (result == DialogResult.Yes)
		//		{
		//			Process.Start(fileName2, "EXCEL");
		//		}
		//	}
		//	catch (Exception)
		//	{
		//		MessageBox.Show("프로그램경로가 잘못되었거나, 엑셀이 설치되어 있지 않습니다.");
		//	}

		//}
		////추가- 엑셀파일 저장(GridView 3)
		//public static void ExportExcel(string title, string title2, string title3, DevExpress.XtraGrid.Views.Grid.GridView gview, DevExpress.XtraGrid.Views.Grid.GridView gview2, DevExpress.XtraGrid.Views.Grid.GridView gview3)
		//{

		//	string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);          //경로 
		//	string fileName = filePath + title + ".xls";                                                //파일명
		//	string fileName2 = filePath + title2 + ".xls";
		//	string fileName3 = filePath + title3 + ".xls";
		//	gview.ExportToXls(fileName);
		//	gview2.ExportToXls(fileName2);
		//	gview3.ExportToXls(fileName3);

		//	try
		//	{
		//		DialogResult result = MessageBox.Show("파일이 저장되었습니다. 확인하시겠습니까?", "파일보기", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		//		if (result == DialogResult.Yes)
		//		{
		//			Process.Start(fileName3, "EXCEL");
		//		}
		//	}
		//	catch (Exception)
		//	{
		//		MessageBox.Show("프로그램경로가 잘못되었거나, 엑셀이 설치되어 있지 않습니다.");
		//	}

		//}

		//public static void ExportExcel_TreeList(string title, DevExpress.XtraTreeList.TreeList gview)
		//{
		//	SaveFileDialog saveExcel = new SaveFileDialog();
		//	saveExcel.Filter = "엑셀파일 (*.xls)|*.xls|All files (*.*)|*.*";
		//	saveExcel.FilterIndex = 1;
		//	saveExcel.FileName = title + '_' + System.DateTime.Today.ToString("yyyy-MM-dd");
		//	saveExcel.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		//	saveExcel.RestoreDirectory = true;

		//	if (saveExcel.ShowDialog() == DialogResult.OK)
		//	{
		//		string fileName = saveExcel.FileName;
		//		gview.OptionsPrint.AutoWidth = false;

		//		////필드별 설정 정보 변수
		//		//string[] setting = new string[gview.Columns.Count];

		//		////필드별로 적용
		//		//for (int i = 0; i < gview.Columns.Count; i++)
		//		//{
		//		//    //visible 설정 저장
		//		//    setting[i] = gview.Columns[i].Visible.ToString();

		//		//    //필드 보이게 설정
		//		//    if (setting[i].ToString() == "False")
		//		//    {
		//		//        gview.Columns[i].Visible = true;
		//		//    }
		//		//}

		//		//다운로드
		//		gview.ExportToXls(saveExcel.FileName);

		//		////필드별로 적용
		//		//for (int i = 0; i < gview.Columns.Count; i++)
		//		//{
		//		//    //필드 안보이게 설정
		//		//    if (setting[i].ToString() == "False")
		//		//    {
		//		//        gview.Columns[i].Visible = false;
		//		//    }
		//		//}

		//		try
		//		{
		//			DialogResult result = MessageBox.Show("파일이 저장되었습니다. 확인하시겠습니까?", "파일보기", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		//			if (result == DialogResult.Yes)
		//			{
		//				Process.Start(fileName, "EXCEL");
		//			}
		//		}
		//		catch (Exception)
		//		{
		//			MessageBox.Show("프로그램경로가 잘못되었거나, 엑셀이 설치되어 있지 않습니다.");
		//		}
		//	}
		//}

		#endregion

		#region 사용자 부서별 권한 가져오기
		public static bool GetRight(string div, string emp_cd)
		{
			string message = "";
			BllSpExecute mBllSpExecute = new BllSpExecute();

			div = "@div:" + div;
			emp_cd = "@emp_cd:" + emp_cd;

			message = mBllSpExecute.SpExecuteString("SP_GetRight", "", div, emp_cd);
			if (message == "")
				return false;
			else
				return true;
		}
		#endregion

		/// <summary>
		/// Convert DataTable to Json 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static string DataTableToJSON(DataTable table)
		{
			string JSONresult;
			JSONresult = JsonConvert.SerializeObject(table);

			if (JSONresult.Equals("[]"))
			{
				JSONresult = "";
			}
			//var JSONString = new StringBuilder();
			//if (table.Rows.Count > 0)
			//{
			//	JSONString.Append("[");
			//	for (int i = 0; i < table.Rows.Count; i++)
			//	{
			//		JSONString.Append("{");
			//		for (int j = 0; j < table.Columns.Count; j++)
			//		{
			//			if (j < table.Columns.Count - 1)
			//			{
			//				JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
			//			}
			//			else if (j == table.Columns.Count - 1)
			//			{
			//				JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
			//			}
			//		}
			//		if (i == table.Rows.Count - 1)
			//		{
			//			JSONString.Append("}");
			//		}
			//		else
			//		{
			//			JSONString.Append("},");
			//		}
			//	}
			//	JSONString.Append("]");
			//}

			return JSONresult;
			//return JSONString.ToString();
		}

		public static DataSet ProgramInitS3(string form_cd)
		{
			string[] param = new string[1];

			param[0] = "@code:" + form_cd;

			BllSpExecute mBllSpExecute = new BllSpExecute();
			DataSet ds = mBllSpExecute.SpExecuteDataSet("SP_ProgramInit", "S3", param);

			return ds;
		}




	}
}
