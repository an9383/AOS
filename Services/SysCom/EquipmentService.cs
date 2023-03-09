using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.SysCom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysCom
{
    public class EquipmentService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable selectEquipment(string equip_cd, string equip_type, string equip_use_gb)
        {
            string sSpName = "SP_EQUIPMENT";

            string gubun = "S";

            string[] param = new string[3];

            param[0] = "@s_equip_cd:" + equip_cd;
            param[1] = "@s_equip_type:" + equip_type;
            param[2] = "@s_equip_use_gb:" + equip_use_gb;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable selectfacilityPopupData()
        {
            string strsql = "SELECT equip_cd, equip_nm";
            strsql += " FROM v_equipment";
            strsql += " WHERE (equip_cd  LIKE '%%' OR equip_nm  LIKE '%%')";
            strsql += " AND equip_cd  LIKE '%%' AND equip_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectWorkroomPopupData()
        {
            string strsql = "	SELECT workroom_cd, workroom_nm, common_part_nm";
            strsql += " 	FROM v_workroom2 	";
            strsql += " 	WHERE (workroom_cd  LIKE '%%' OR workroom_nm  LIKE '%%') 	";
            strsql += " 	AND workroom_cd  LIKE '%%' AND workroom_nm  LIKE '%%' 	";
            strsql += " 	ORDER BY 1	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectEquipmentPopupData()
        {
            string strsql = "	SELECT equip_cd, equip_nm, equip_type, equip_period1, equip_period2, equip_period3, equip_period4, equip_period5, equip_long_period1, equip_long_period2, equip_long_period3, equip_long_period4, equip_long_period5 	";
            strsql += " 	FROM v_standardequipment 	";
            strsql += " 	WHERE (equip_cd  LIKE '%%' OR equip_nm  LIKE '%%') 	";
            strsql += " 	AND equip_cd  LIKE '%%' AND equip_nm  LIKE '%%' 	";
            strsql += " 	ORDER BY 1	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectLineEquipPopupData()
        {
            string strsql = "	SELECT equip_cd, equip_nm 	";
            strsql += " 	FROM equipment 	";
            strsql += " 	WHERE (equip_cd  LIKE '%%' OR equip_nm  LIKE '%%') 	";
            strsql += " 	AND equip_cd  LIKE '%%' AND equip_nm  LIKE '%%' 	";
            strsql += " 	ORDER BY 1	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectReponseEmpPopupData()
        {
            string strsql = "	SELECT emp_cd, emp_nm, dept_cd, dept_nm 	";
            strsql += " 	FROM V_EMPLOYEE 	";
            strsql += " 	WHERE (emp_cd  LIKE '%%' OR emp_nm  LIKE '%%') 	";
            strsql += " 	AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%' 	";
            strsql += " 	ORDER BY 1 	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectDeptPopupData()
        {
            string strsql = "	SELECT dept_cd, dept_nm 	";
            strsql += " 	FROM V_DEPARTMENT 	";
            strsql += " 	WHERE (dept_cd  LIKE '%%' OR dept_nm  LIKE '%%') 	";
            strsql += " 	AND dept_cd  LIKE '%%' AND dept_nm  LIKE '%%' 	";
            strsql += " 	ORDER BY 1 	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectWorkRoomReponseEmpPopupData()
        {
            string strsql = "	SELECT emp_cd, emp_nm, dept_cd, dept_nm 	";
            strsql += " 	FROM V_EMPLOYEE 	";
            strsql += " 	WHERE (emp_cd  LIKE '%%' OR emp_nm  LIKE '%%') 	";
            strsql += " 	AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%' 	";
            strsql += " 	ORDER BY 1	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectEquipManageCustPopupData()
        {
            string strsql = "	SELECT vender_cd, vender_nm 	";
            strsql += " 	FROM v_vender_manage 	";
            strsql += " 	WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%') 	";
            strsql += " 	AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%' 	";
            strsql += " 	ORDER BY 1	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectEquipProdCustPopupData()
        {
            string strsql = "	SELECT vender_cd, vender_nm 	";
            strsql += " 	FROM v_vender_prod 	";
            strsql += " 	WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%') 	";
            strsql += " 	AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%' 	";
            strsql += " 	ORDER BY 1	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectEquipBuyCustPopupData()
        {
            string strsql = "	SELECT vender_cd, vender_nm 	";
            strsql += " 	FROM v_vender_sell 	";
            strsql += " 	WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%') 	";
            strsql += " 	AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%' 	";
            strsql += " 	ORDER BY 1	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }
        internal string selectEquipImage(string equip_cd)
        {
            string sSpName = "SP_EQUIPMENT";
            string sGubun = "P";
            string[] pParam = new string[1];
            pParam[0] = "@EQUIP_CD:" + equip_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();
            string src = "";

            if (ds != null)
            {
                dt = ds.Tables[0];

                if (dt.Rows.Count <= 0)
                {
                    return "";
                }
            }

            if (dt.Rows[0].ItemArray[0] != System.DBNull.Value)
            {
                string str = Convert.ToBase64String((byte[])(dt.Rows[0].ItemArray[0]));
                src = "data:Image/png;base64," + str;
            }

            return src;
        }

        internal DataTable selectAlarmList()
        {
            string sSpName = "[RTEGMS_MONITORING].[dbo].SP_AlarmManage";
            string sGubun = "Select2";
            string[] pParam = new string[1];
            pParam[0] = "@alarm_code:";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal void uploadImage(byte[] myBytes, string equip_cd)
        {
            string sSpName = "SP_EQUIPMENT";
            string sGubun = "IU";
            string[] pParam = new string[1];
            pParam[0] = "@EQUIP_CD:" + equip_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@equip_image", myBytes, pParam);

            return;
        }

        internal void uploadFile(byte[] myBytes, string equip_cd, string fileName, string contentType, string fgubun, string pGubun, string equip_doc_id)
        {
            var doc_type ="";

            if (contentType.Equals("application/haansofthwp"))
            {
                doc_type = ".hwp";
            }
            else if (contentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
            {
                doc_type = ".docx";
            }
            else if (contentType.Equals("application/msword"))
            {
                doc_type = ".doc";
            }

            string sSpName = "SP_EQUIPMENT";
            string sGubun = pGubun;

            if (pGubun.Equals("F"))
            {
                string[] pParam = new string[4];
                pParam[0] = "@equip_cd:" + equip_cd;
                pParam[1] = "@doc_name:" + fileName;
                pParam[2] = "@doc_type:" + doc_type;
                pParam[3] = "@fgubun:" + fgubun;

                string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@file_image", myBytes, pParam);
            }else if (pGubun.Equals("FE"))
            {
                string[] pParam = new string[4];
                pParam[0] = "@doc_name:" + fileName;
                pParam[1] = "@doc_type:" + doc_type;
                pParam[2] = "@fgubun:" + fgubun;
                pParam[3] = "@equip_doc_id:" + equip_doc_id;

                string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@file_image", myBytes, pParam);
            }



            return;
        }

        internal string equipmentCRUD(HACCP.Models.SysCom.Equipment equipment, string pGubun)
        {
            string sSpName = "SP_EQUIPMENT";

            string gubun = pGubun;
            string res = "";

            if (pGubun.Equals("D"))
            {
                string[] param = new string[1];

                param[0] = "@EQUIP_CD:" + equipment.equip_cd;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }
            else
            {
                string[] param = getParam(equipment);

                if (Public_Function.MasterData_AutoNumbering_yn == "N" && pGubun == "I")
                {
                    string message = _bllSpExecute.SpExecuteString(sSpName, "CEC", param);
                    if (message == "Y")
                        res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
                }
                else
                    res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }

            return res;
        }

        internal string deleteAttachedFile(string equip_cd, string fgubun, string equip_doc_id)
        {
            string sSpName = "SP_EQUIPMENT";
            string sGubun = "FD";
            string[] pParam = new string[3];
            pParam[0] = "@equip_cd:" + equip_cd;
            pParam[1] = "@fgubun:" + fgubun;
            pParam[2] = "@equip_doc_id:" + equip_doc_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        private string[] getParam(HACCP.Models.SysCom.Equipment equipment)
        {
            string[] param = new string[60];

            param[0] = "@equip_cd :" + equipment.equip_cd;
            param[1] = "@equip_nm :" + equipment.equip_nm;
            param[2] = "@equip_enm :" + equipment.equip_enm;
            param[3] = "@equip_manage_num :" + equipment.equip_manage_num;
            param[4] = "@equip_item_nm :" + equipment.equip_item_nm;
            param[5] = "@equip_model_num :" + equipment.equip_model_num;
            param[6] = "@equip_serial_num :" + equipment.equip_serial_num;
            param[7] = "@equip_volume :" + equipment.equip_volume;
            param[8] = "@equip_type :" + equipment.equip_type;
            param[9] = "@equip_type2 :" + equipment.equip_type2;
            param[10] = "@equip_prod_cust :" + equipment.equip_prod_cust;
            param[11] = "@equip_prod_num :" + equipment.equip_prod_num;
            param[12] = "@equip_buy_cust:" + equipment.equip_buy_cust;
            param[13] = "@equip_manage_cust:" + equipment.equip_manage_cust;
            param[14] = "@equip_response_emp :" + equipment.equip_response_emp;
            param[15] = "@equip_buy_date :" + equipment.equip_buy_date;
            param[16] = "@equip_install_date :" + equipment.equip_install_date;
            param[17] = "@equip_work_date :" + equipment.equip_work_date;
            param[18] = "@equip_disuse_date :" + equipment.equip_disuse_date;
            param[19] = "@equip_buy_amt :" + equipment.equip_buy_amt;
            param[20] = "@equip_use_gb :" + equipment.equip_use_gb;
            param[21] = "@equip_property :" + equipment.equip_property;
            param[22] = "@plant_cd :" + equipment.plant_cd;
            param[23] = "@equip_mcd :" + equipment.equip_mcd;
            param[24] = "@workroom_cd :" + equipment.workroom_cd;
            param[25] = "@interface_type:" + equipment.interface_type;
            param[26] = "@interface_cd :" + equipment.interface_cd;
            param[27] = "@plc_node_no:" + equipment.plc_node_no;
            param[28] = "@insert_emp :" + Public_Function.User_cd;
            param[29] = "@equip_essential_level:" + equipment.equip_essential_level;
            param[30] = "@equip_prod_cust_cd:" + equipment.equip_prod_cust_cd;
            param[31] = "@equip_buy_cust_cd:" + equipment.equip_buy_cust_cd;
            param[32] = "@equip_manage_cust_cd:" + equipment.equip_manage_cust_cd;
            param[33] = "@equip_disuse_reason:" + equipment.equip_disuse_reason;
            param[34] = "@error_plus:" + equipment.error_plus;
            param[35] = "@error_minus:" + equipment.error_minus;
            param[36] = "@error_unit:" + equipment.error_unit;
            param[37] = "@w_port_no:" + equipment.w_port_no;
            param[38] = "@interface_position_no:" + equipment.interface_position_no;
            param[39] = "@interval:" + equipment.interval;
            param[40] = "@equip_purpose:" + equipment.equip_purpose;
            param[41] = "@manufacture_date:" + equipment.manufacture_date;
            param[42] = "@equip_standard:" + equipment.equip_standard;
            param[43] = "@dept_cd:" + equipment.dept_cd;
            param[44] = "@workroom_response_emp:" + equipment.workroom_response_emp;
            param[45] = "@unusual_memo:" + equipment.unusual_memo;
            param[46] = "@sop_no:" + equipment.sop_no;
            param[47] = "@line_equip_cd:" + equipment.line_equip_cd;
            param[48] = "@ip_address:" + equipment.ip_address;
            param[49] = "@ip_subnet:" + equipment.ip_subnet;
            param[50] = "@ip_port:" + equipment.ip_port;
            param[51] = "@com_port:" + equipment.com_port;
            param[52] = "@com_baud_rate:" + equipment.com_baud_rate;
            param[53] = "@com_data_bits:" + equipment.com_data_bits;
            param[54] = "@com_parity:" + equipment.com_parity;
            param[55] = "@com_stop_bits:" + equipment.com_stop_bits;
            param[56] = "@com_handshaking:" + equipment.com_handshaking;
            param[57] = "@interface_use_gb:" + equipment.interface_use_gb;
            param[58] = "@ccp_mont_gb:" + equipment.ccp_mont_gb;
            param[59] = "@alarm_code:" + equipment.alarm_code;

            return param;
        }

        internal DataTable selectAttachmentFile(string equip_doc_id)
        {
            string sSpName = "SP_EQUIPMENT";

            string gubun = "FS";

            string[] param = new string[1];

            param[0] = "@equip_doc_id:" + equip_doc_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        private IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {

        #region Big freaking list of mime types
        // combination of values from Windows 7 Registry and 
        // from C:\Windows\System32\inetsrv\config\applicationHost.config
        // some added, including .7z and .dat
        {".323", "text/h323"},
        {".3g2", "video/3gpp2"},
        {".3gp", "video/3gpp"},
        {".3gp2", "video/3gpp2"},
        {".3gpp", "video/3gpp"},
        {".7z", "application/x-7z-compressed"},
        {".aa", "audio/audible"},
        {".AAC", "audio/aac"},
        {".aaf", "application/octet-stream"},
        {".aax", "audio/vnd.audible.aax"},
        {".ac3", "audio/ac3"},
        {".aca", "application/octet-stream"},
        {".accda", "application/msaccess.addin"},
        {".accdb", "application/msaccess"},
        {".accdc", "application/msaccess.cab"},
        {".accde", "application/msaccess"},
        {".accdr", "application/msaccess.runtime"},
        {".accdt", "application/msaccess"},
        {".accdw", "application/msaccess.webapplication"},
        {".accft", "application/msaccess.ftemplate"},
        {".acx", "application/internet-property-stream"},
        {".AddIn", "text/xml"},
        {".ade", "application/msaccess"},
        {".adobebridge", "application/x-bridge-url"},
        {".adp", "application/msaccess"},
        {".ADT", "audio/vnd.dlna.adts"},
        {".ADTS", "audio/aac"},
        {".afm", "application/octet-stream"},
        {".ai", "application/postscript"},
        {".aif", "audio/x-aiff"},
        {".aifc", "audio/aiff"},
        {".aiff", "audio/aiff"},
        {".air", "application/vnd.Adobe.air-application-installer-package+Zip"},
        {".amc", "application/x-mpeg"},
        {".application", "application/x-ms-application"},
        {".art", "image/x-jg"},
        {".asa", "application/xml"},
        {".asax", "application/xml"},
        {".ascx", "application/xml"},
        {".asd", "application/octet-stream"},
        {".asf", "video/x-ms-asf"},
        {".ashx", "application/xml"},
        {".asi", "application/octet-stream"},
        {".asm", "text/plain"},
        {".asmx", "application/xml"},
        {".aspx", "application/xml"},
        {".asr", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".atom", "application/atom+xml"},
        {".au", "audio/basic"},
        {".avi", "video/x-msvideo"},
        {".axs", "application/olescript"},
        {".bas", "text/plain"},
        {".bcpio", "application/x-bcpio"},
        {".bin", "application/octet-stream"},
        {".bmp", "image/bmp"},
        {".c", "text/plain"},
        {".cab", "application/octet-stream"},
        {".caf", "audio/x-caf"},
        {".calx", "application/vnd.ms-office.calx"},
        {".cat", "application/vnd.ms-pki.seccat"},
        {".cc", "text/plain"},
        {".cd", "text/plain"},
        {".cdda", "audio/aiff"},
        {".cdf", "application/x-cdf"},
        {".cer", "application/x-x509-ca-cert"},
        {".chm", "application/octet-stream"},
        {".class", "application/x-Java-applet"},
        {".clp", "application/x-msclip"},
        {".cmx", "image/x-cmx"},
        {".cnf", "text/plain"},
        {".cod", "image/cis-cod"},
        {".config", "application/xml"},
        {".contact", "text/x-ms-contact"},
        {".coverage", "application/xml"},
        {".cpio", "application/x-cpio"},
        {".cpp", "text/plain"},
        {".crd", "application/x-mscardfile"},
        {".crl", "application/pkix-crl"},
        {".crt", "application/x-x509-ca-cert"},
        {".cs", "text/plain"},
        {".csdproj", "text/plain"},
        {".csh", "application/x-csh"},
        {".csproj", "text/plain"},
        {".css", "text/css"},
        {".csv", "text/csv"},
        {".cur", "application/octet-stream"},
        {".cxx", "text/plain"},
        {".dat", "application/octet-stream"},
        {".datasource", "application/xml"},
        {".dbproj", "text/plain"},
        {".dcr", "application/x-director"},
        {".def", "text/plain"},
        {".deploy", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dgml", "application/xml"},
        {".dib", "image/bmp"},
        {".dif", "video/x-dv"},
        {".dir", "application/x-director"},
        {".disco", "text/xml"},
        {".dll", "application/x-msdownload"},
        {".dll.config", "text/xml"},
        {".dlm", "text/dlm"},
        {".doc", "application/msword"},
        {".docm", "application/vnd.ms-Word.document.macroEnabled.12"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".dot", "application/msword"},
        {".dotm", "application/vnd.ms-Word.template.macroEnabled.12"},
        {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
        {".dsp", "application/octet-stream"},
        {".dsw", "text/plain"},
        {".dtd", "text/xml"},
        {".dtsConfig", "text/xml"},
        {".dv", "video/x-dv"},
        {".dvi", "application/x-dvi"},
        {".dwf", "drawing/x-dwf"},
        {".dwp", "application/octet-stream"},
        {".dxr", "application/x-director"},
        {".eml", "message/rfc822"},
        {".emz", "application/octet-stream"},
        {".eot", "application/octet-stream"},
        {".eps", "application/postscript"},
        {".etl", "application/etl"},
        {".etx", "text/x-setext"},
        {".evy", "application/envoy"},
        {".exe", "application/octet-stream"},
        {".exe.config", "text/xml"},
        {".fdf", "application/vnd.fdf"},
        {".fif", "application/fractals"},
        {".filters", "Application/xml"},
        {".fla", "application/octet-stream"},
        {".flr", "x-world/x-vrml"},
        {".flv", "video/x-flv"},
        {".fsscript", "application/fsharp-script"},
        {".fsx", "application/fsharp-script"},
        {".generictest", "application/xml"},
        {".gif", "image/gif"},
        {".group", "text/x-ms-group"},
        {".gsm", "audio/x-gsm"},
        {".gtar", "application/x-gtar"},
        {".gz", "application/x-gzip"},
        {".h", "text/plain"},
        {".hdf", "application/x-hdf"},
        {".hdml", "text/x-hdml"},
        {".hhc", "application/x-oleobject"},
        {".hhk", "application/octet-stream"},
        {".hhp", "application/octet-stream"},
        {".hlp", "application/winhlp"},
        {".hpp", "text/plain"},
        {".hqx", "application/mac-binhex40"},
        {".hta", "application/hta"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".htt", "text/webviewhtml"},
        {".hwp", "application/haansofthwp"},
        {".hxa", "application/xml"},
        {".hxc", "application/xml"},
        {".hxd", "application/octet-stream"},
        {".hxe", "application/xml"},
        {".hxf", "application/xml"},
        {".hxh", "application/octet-stream"},
        {".hxi", "application/octet-stream"},
        {".hxk", "application/xml"},
        {".hxq", "application/octet-stream"},
        {".hxr", "application/octet-stream"},
        {".hxs", "application/octet-stream"},
        {".hxt", "text/html"},
        {".hxv", "application/xml"},
        {".hxw", "application/octet-stream"},
        {".hxx", "text/plain"},
        {".i", "text/plain"},
        {".ico", "image/x-icon"},
        {".ics", "application/octet-stream"},
        {".idl", "text/plain"},
        {".ief", "image/ief"},
        {".iii", "application/x-iphone"},
        {".inc", "text/plain"},
        {".inf", "application/octet-stream"},
        {".inl", "text/plain"},
        {".ins", "application/x-internet-signup"},
        {".ipa", "application/x-iTunes-ipa"},
        {".ipg", "application/x-iTunes-ipg"},
        {".ipproj", "text/plain"},
        {".ipsw", "application/x-iTunes-ipsw"},
        {".iqy", "text/x-ms-iqy"},
        {".isp", "application/x-internet-signup"},
        {".ite", "application/x-iTunes-ite"},
        {".itlp", "application/x-iTunes-itlp"},
        {".itms", "application/x-iTunes-itms"},
        {".itpc", "application/x-iTunes-itpc"},
        {".IVF", "video/x-ivf"},
        {".jar", "application/Java-archive"},
        {".Java", "application/octet-stream"},
        {".jck", "application/liquidmotion"},
        {".jcz", "application/liquidmotion"},
        {".jfif", "image/pjpeg"},
        {".jnlp", "application/x-Java-jnlp-file"},
        {".jpb", "application/octet-stream"},
        {".jpe", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".json", "application/json"},
        {".jsx", "text/jscript"},
        {".jsxbin", "text/plain"},
        {".latex", "application/x-latex"},
        {".library-ms", "application/windows-library+xml"},
        {".lit", "application/x-ms-reader"},
        {".loadtest", "application/xml"},
        {".lpk", "application/octet-stream"},
        {".lsf", "video/x-la-asf"},
        {".lst", "text/plain"},
        {".lsx", "video/x-la-asf"},
        {".lzh", "application/octet-stream"},
        {".m13", "application/x-msmediaview"},
        {".m14", "application/x-msmediaview"},
        {".m1v", "video/mpeg"},
        {".m2t", "video/vnd.dlna.mpeg-tts"},
        {".m2ts", "video/vnd.dlna.mpeg-tts"},
        {".m2v", "video/mpeg"},
        {".m3u", "audio/x-mpegurl"},
        {".m3u8", "audio/x-mpegurl"},
        {".m4a", "audio/m4a"},
        {".m4b", "audio/m4b"},
        {".m4p", "audio/m4p"},
        {".m4r", "audio/x-m4r"},
        {".m4v", "video/x-m4v"},
        {".mac", "image/x-macpaint"},
        {".mak", "text/plain"},
        {".man", "application/x-troff-man"},
        {".manifest", "application/x-ms-manifest"},
        {".map", "text/plain"},
        {".master", "application/xml"},
        {".mda", "application/msaccess"},
        {".mdb", "application/x-msaccess"},
        {".mde", "application/msaccess"},
        {".mdp", "application/octet-stream"},
        {".me", "application/x-troff-me"},
        {".mfp", "application/x-shockwave-flash"},
        {".mht", "message/rfc822"},
        {".mhtml", "message/rfc822"},
        {".mid", "audio/mid"},
        {".midi", "audio/mid"},
        {".mix", "application/octet-stream"},
        {".mk", "text/plain"},
        {".mmf", "application/x-smaf"},
        {".mno", "text/xml"},
        {".mny", "application/x-msmoney"},
        {".mod", "video/mpeg"},
        {".mov", "video/quicktime"},
        {".movie", "video/x-sgi-movie"},
        {".mp2", "video/mpeg"},
        {".mp2v", "video/mpeg"},
        {".mp3", "audio/mpeg"},
        {".mp4", "video/mp4"},
        {".mp4v", "video/mp4"},
        {".mpa", "video/mpeg"},
        {".mpe", "video/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpf", "application/vnd.ms-mediapackage"},
        {".mpg", "video/mpeg"},
        {".mpp", "application/vnd.ms-project"},
        {".mpv2", "video/mpeg"},
        {".mqv", "video/quicktime"},
        {".ms", "application/x-troff-ms"},
        {".msi", "application/octet-stream"},
        {".mso", "application/octet-stream"},
        {".mts", "video/vnd.dlna.mpeg-tts"},
        {".mtx", "application/xml"},
        {".mvb", "application/x-msmediaview"},
        {".mvc", "application/x-miva-compiled"},
        {".mxp", "application/x-mmxp"},
        {".nc", "application/x-netcdf"},
        {".nsc", "video/x-ms-asf"},
        {".nws", "message/rfc822"},
        {".ocx", "application/octet-stream"},
        {".oda", "application/oda"},
        {".odc", "text/x-ms-odc"},
        {".odh", "text/plain"},
        {".odl", "text/plain"},
        {".odp", "application/vnd.oasis.opendocument.presentation"},
        {".ods", "application/oleobject"},
        {".odt", "application/vnd.oasis.opendocument.text"},
        {".one", "application/onenote"},
        {".onea", "application/onenote"},
        {".onepkg", "application/onenote"},
        {".onetmp", "application/onenote"},
        {".onetoc", "application/onenote"},
        {".onetoc2", "application/onenote"},
        {".orderedtest", "application/xml"},
        {".osdx", "application/opensearchdescription+xml"},
        {".p10", "application/pkcs10"},
        {".p12", "application/x-pkcs12"},
        {".p7b", "application/x-pkcs7-certificates"},
        {".p7c", "application/pkcs7-mime"},
        {".p7m", "application/pkcs7-mime"},
        {".p7r", "application/x-pkcs7-certreqresp"},
        {".p7s", "application/pkcs7-signature"},
        {".pbm", "image/x-portable-bitmap"},
        {".pcast", "application/x-podcast"},
        {".pct", "image/pict"},
        {".pcx", "application/octet-stream"},
        {".pcz", "application/octet-stream"},
        {".pdf", "application/pdf"},
        {".pfb", "application/octet-stream"},
        {".pfm", "application/octet-stream"},
        {".pfx", "application/x-pkcs12"},
        {".pgm", "image/x-portable-graymap"},
        {".pic", "image/pict"},
        {".pict", "image/pict"},
        {".pkgdef", "text/plain"},
        {".pkgundef", "text/plain"},
        {".pko", "application/vnd.ms-pki.pko"},
        {".pls", "audio/scpls"},
        {".pma", "application/x-perfmon"},
        {".pmc", "application/x-perfmon"},
        {".pml", "application/x-perfmon"},
        {".pmr", "application/x-perfmon"},
        {".pmw", "application/x-perfmon"},
        {".png", "image/png"},
        {".pnm", "image/x-portable-anymap"},
        {".pnt", "image/x-macpaint"},
        {".pntg", "image/x-macpaint"},
        {".pnz", "image/png"},
        {".pot", "application/vnd.ms-PowerPoint"},
        {".potm", "application/vnd.ms-PowerPoint.template.macroEnabled.12"},
        {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
        {".ppa", "application/vnd.ms-PowerPoint"},
        {".ppam", "application/vnd.ms-PowerPoint.addin.macroEnabled.12"},
        {".ppm", "image/x-portable-pixmap"},
        {".pps", "application/vnd.ms-PowerPoint"},
        {".ppsm", "application/vnd.ms-PowerPoint.slideshow.macroEnabled.12"},
        {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
        {".ppt", "application/vnd.ms-PowerPoint"},
        {".pptm", "application/vnd.ms-PowerPoint.presentation.macroEnabled.12"},
        {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
        {".prf", "application/pics-rules"},
        {".prm", "application/octet-stream"},
        {".prx", "application/octet-stream"},
        {".ps", "application/postscript"},
        {".psc1", "application/PowerShell"},
        {".psd", "application/octet-stream"},
        {".psess", "application/xml"},
        {".psm", "application/octet-stream"},
        {".psp", "application/octet-stream"},
        {".pub", "application/x-mspublisher"},
        {".pwz", "application/vnd.ms-PowerPoint"},
        {".qht", "text/x-html-insertion"},
        {".qhtm", "text/x-html-insertion"},
        {".qt", "video/quicktime"},
        {".qti", "image/x-quicktime"},
        {".qtif", "image/x-quicktime"},
        {".qtl", "application/x-quicktimeplayer"},
        {".qxd", "application/octet-stream"},
        {".ra", "audio/x-pn-realaudio"},
        {".ram", "audio/x-pn-realaudio"},
        {".rar", "application/octet-stream"},
        {".ras", "image/x-cmu-raster"},
        {".rat", "application/rat-file"},
        {".rc", "text/plain"},
        {".rc2", "text/plain"},
        {".rct", "text/plain"},
        {".rdlc", "application/xml"},
        {".resx", "application/xml"},
        {".rf", "image/vnd.rn-realflash"},
        {".rgb", "image/x-rgb"},
        {".rgs", "text/plain"},
        {".rm", "application/vnd.rn-realmedia"},
        {".rmi", "audio/mid"},
        {".rmp", "application/vnd.rn-rn_music_package"},
        {".roff", "application/x-troff"},
        {".rpm", "audio/x-pn-realaudio-plugin"},
        {".rqy", "text/x-ms-rqy"},
        {".rtf", "application/rtf"},
        {".rtx", "text/richtext"},
        {".ruleset", "application/xml"},
        {".s", "text/plain"},
        {".safariextz", "application/x-safari-safariextz"},
        {".scd", "application/x-msschedule"},
        {".sct", "text/scriptlet"},
        {".sd2", "audio/x-sd2"},
        {".sdp", "application/sdp"},
        {".sea", "application/octet-stream"},
        {".searchConnector-ms", "application/windows-search-connector+xml"},
        {".setpay", "application/set-payment-initiation"},
        {".setreg", "application/set-registration-initiation"},
        {".settings", "application/xml"},
        {".sgimb", "application/x-sgimb"},
        {".sgml", "text/sgml"},
        {".sh", "application/x-sh"},
        {".shar", "application/x-shar"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".sitemap", "application/xml"},
        {".skin", "application/xml"},
        {".sldm", "application/vnd.ms-PowerPoint.slide.macroEnabled.12"},
        {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
        {".slk", "application/vnd.ms-Excel"},
        {".sln", "text/plain"},
        {".slupkg-ms", "application/x-ms-license"},
        {".smd", "audio/x-smd"},
        {".smi", "application/octet-stream"},
        {".smx", "audio/x-smd"},
        {".smz", "audio/x-smd"},
        {".snd", "audio/basic"},
        {".snippet", "application/xml"},
        {".snp", "application/octet-stream"},
        {".sol", "text/plain"},
        {".sor", "text/plain"},
        {".spc", "application/x-pkcs7-certificates"},
        {".spl", "application/futuresplash"},
        {".src", "application/x-wais-source"},
        {".srf", "text/plain"},
        {".SSISDeploymentManifest", "text/xml"},
        {".ssm", "application/streamingmedia"},
        {".sst", "application/vnd.ms-pki.certstore"},
        {".stl", "application/vnd.ms-pki.stl"},
        {".sv4cpio", "application/x-sv4cpio"},
        {".sv4crc", "application/x-sv4crc"},
        {".svc", "application/xml"},
        {".swf", "application/x-shockwave-flash"},
        {".t", "application/x-troff"},
        {".tar", "application/x-tar"},
        {".tcl", "application/x-tcl"},
        {".testrunconfig", "application/xml"},
        {".testsettings", "application/xml"},
        {".tex", "application/x-tex"},
        {".texi", "application/x-texinfo"},
        {".texinfo", "application/x-texinfo"},
        {".tgz", "application/x-compressed"},
        {".thmx", "application/vnd.ms-officetheme"},
        {".thn", "application/octet-stream"},
        {".tif", "image/tiff"},
        {".tiff", "image/tiff"},
        {".tlh", "text/plain"},
        {".tli", "text/plain"},
        {".toc", "application/octet-stream"},
        {".tr", "application/x-troff"},
        {".trm", "application/x-msterminal"},
        {".trx", "application/xml"},
        {".ts", "video/vnd.dlna.mpeg-tts"},
        {".tsv", "text/tab-separated-values"},
        {".ttf", "application/octet-stream"},
        {".tts", "video/vnd.dlna.mpeg-tts"},
        {".txt", "text/plain"},
        {".u32", "application/octet-stream"},
        {".uls", "text/iuls"},
        {".user", "text/plain"},
        {".ustar", "application/x-ustar"},
        {".vb", "text/plain"},
        {".vbdproj", "text/plain"},
        {".vbk", "video/mpeg"},
        {".vbproj", "text/plain"},
        {".vbs", "text/vbscript"},
        {".vcf", "text/x-vcard"},
        {".vcproj", "Application/xml"},
        {".vcs", "text/plain"},
        {".vcxproj", "Application/xml"},
        {".vddproj", "text/plain"},
        {".vdp", "text/plain"},
        {".vdproj", "text/plain"},
        {".vdx", "application/vnd.ms-visio.viewer"},
        {".vml", "text/xml"},
        {".vscontent", "application/xml"},
        {".vsct", "text/xml"},
        {".vsd", "application/vnd.visio"},
        {".vsi", "application/ms-vsi"},
        {".vsix", "application/vsix"},
        {".vsixlangpack", "text/xml"},
        {".vsixmanifest", "text/xml"},
        {".vsmdi", "application/xml"},
        {".vspscc", "text/plain"},
        {".vss", "application/vnd.visio"},
        {".vsscc", "text/plain"},
        {".vssettings", "text/xml"},
        {".vssscc", "text/plain"},
        {".vst", "application/vnd.visio"},
        {".vstemplate", "text/xml"},
        {".vsto", "application/x-ms-vsto"},
        {".vsw", "application/vnd.visio"},
        {".vsx", "application/vnd.visio"},
        {".vtx", "application/vnd.visio"},
        {".wav", "audio/wav"},
        {".wave", "audio/wav"},
        {".wax", "audio/x-ms-wax"},
        {".wbk", "application/msword"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wcm", "application/vnd.ms-works"},
        {".wdb", "application/vnd.ms-works"},
        {".wdp", "image/vnd.ms-photo"},
        {".webarchive", "application/x-safari-webarchive"},
        {".webtest", "application/xml"},
        {".wiq", "application/xml"},
        {".wiz", "application/msword"},
        {".wks", "application/vnd.ms-works"},
        {".WLMP", "application/wlmoviemaker"},
        {".wlpginstall", "application/x-wlpg-detect"},
        {".wlpginstall3", "application/x-wlpg3-detect"},
        {".wm", "video/x-ms-wm"},
        {".wma", "audio/x-ms-wma"},
        {".wmd", "application/x-ms-wmd"},
        {".wmf", "application/x-msmetafile"},
        {".wml", "text/vnd.wap.wml"},
        {".wmlc", "application/vnd.wap.wmlc"},
        {".wmls", "text/vnd.wap.wmlscript"},
        {".wmlsc", "application/vnd.wap.wmlscriptc"},
        {".wmp", "video/x-ms-wmp"},
        {".wmv", "video/x-ms-wmv"},
        {".wmx", "video/x-ms-wmx"},
        {".wmz", "application/x-ms-wmz"},
        {".wpl", "application/vnd.ms-wpl"},
        {".wps", "application/vnd.ms-works"},
        {".wri", "application/x-mswrite"},
        {".wrl", "x-world/x-vrml"},
        {".wrz", "x-world/x-vrml"},
        {".wsc", "text/scriptlet"},
        {".wsdl", "text/xml"},
        {".wvx", "video/x-ms-wvx"},
        {".x", "application/directx"},
        {".xaf", "x-world/x-vrml"},
        {".xaml", "application/xaml+xml"},
        {".xap", "application/x-silverlight-app"},
        {".xbap", "application/x-ms-xbap"},
        {".xbm", "image/x-xbitmap"},
        {".xdr", "text/plain"},
        {".xht", "application/xhtml+xml"},
        {".xhtml", "application/xhtml+xml"},
        {".xla", "application/vnd.ms-Excel"},
        {".xlam", "application/vnd.ms-Excel.addin.macroEnabled.12"},
        {".xlc", "application/vnd.ms-Excel"},
        {".xld", "application/vnd.ms-Excel"},
        {".xlk", "application/vnd.ms-Excel"},
        {".xll", "application/vnd.ms-Excel"},
        {".xlm", "application/vnd.ms-Excel"},
        {".xls", "application/vnd.ms-Excel"},
        {".xlsb", "application/vnd.ms-Excel.sheet.binary.macroEnabled.12"},
        {".xlsm", "application/vnd.ms-Excel.sheet.macroEnabled.12"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".xlt", "application/vnd.ms-Excel"},
        {".xltm", "application/vnd.ms-Excel.template.macroEnabled.12"},
        {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
        {".xlw", "application/vnd.ms-Excel"},
        {".xml", "text/xml"},
        {".xmta", "application/xml"},
        {".xof", "x-world/x-vrml"},
        {".XOML", "text/plain"},
        {".xpm", "image/x-xpixmap"},
        {".xps", "application/vnd.ms-xpsdocument"},
        {".xrm-ms", "text/xml"},
        {".xsc", "application/xml"},
        {".xsd", "text/xml"},
        {".xsf", "text/xml"},
        {".xsl", "text/xml"},
        {".xslt", "text/xml"},
        {".xsn", "application/octet-stream"},
        {".xss", "application/xml"},
        {".xtp", "application/octet-stream"},
        {".xwd", "image/x-xwindowdump"},
        {".z", "application/x-compress"},
        {".Zip", "application/x-Zip-compressed"},
        #endregion

        };

        public string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime;

            return _mappings.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }
    }
}
