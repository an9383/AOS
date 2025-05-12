using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Mont
{
    public class EquipmentCollectData
    {
        public string gubun { get; set; }
        public string equip_cd { get; set; }
        public string equip_nm { get; set; }
        public string workroom_cd { get; set; }
        public string workroom_nm { get; set; } 
        public string equip_type { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

        
        public string CODE          { get; set; } 
        public string IDX           { get; set; }
	    public string CNT_OK        { get; set; }
        public string CNT_NG        { get; set; }
	    public string CNT_OK_APD    { get; set; }
	    public string CNT_NG_APD    { get; set; }
	    public string START_DT      { get; set; }
	    public string STOP_DT       { get; set; }
	    public string I_DATE        { get; set; }
	    public string O_DATE        { get; set; }
	    public string SENSITIVITY   { get; set; }
	    public string PROPERTY      { get; set; }
	    public string RESULT        { get; set; }
	    public string CONFIRMED     { get; set; }

	    public string DATA1         { get; set; }
	    public string DATA2         { get; set; }
	    public string DATA3         { get; set; }
	    public string DATA4         { get; set; }
	    public string DATA5         { get; set; }
        public string confirm_time  { get; set; }
        
        public string WORK_GU       { get; set; }
        public string WORK_RST      { get; set; }
        public string FILE_IDX      { get; set; }
        public string CONFIRMED_NM  { get; set; }
        public string WORK_ORDER_NO { get; set; }

        public string SPEED         { get; set; }
        public string ALARM_CODE    { get; set; }

        public string CNT_H         { get; set; }
        public string CNT_N         { get; set; }
        public string CNT_L         { get; set; }
        public string CNT_TOT       { get; set; }

        public string TEMPERATURE   { get; set; }
        public string HUMIDITY      { get; set; }
        public string PRESSURE      { get; set; }

        public string WEIGHT        { get; set; }

        public string LITER         { get; set; }
        public string LITER_APD     { get; set; }
        public string LITER_SPEED   { get; set; }

        public string TEMP_HOT { get; set; }
        public string TEMP_COLD { get; set; }
        public string TEMP_DIVISION { get; set; }

        public string STD_TEMP_HOT { get; set; }
        public string STD_TEMP_COLD { get; set; }
        public string STD_TEMP_DIVISION { get; set; }

        public string TEMP { get; set; }
        public string TEMP2 { get; set; }

        public string STD_W_H { get; set; }
        public string STD_W_N { get; set; }
        public string STD_W_L { get; set; }
        public string W1 { get; set; }
        public string W2 { get; set; }
        public string W3 { get; set; }
        public string W4 { get; set; }
        public string W5 { get; set; }
        public string W6 { get; set; }
        public string W7 { get; set; }
        public string W8 { get; set; }
        public string W9 { get; set; }
        public string W10 { get; set; }

        public string DATA_TYPE { get; set; }

        public string audit_remark  { get; set; }
        public string OWD_audittrail_id { get; set; }
        
    }
}