using HACCP.Libs.Database;
using HACCP.Models.SysOp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysOp
{
    public class CompanyCalendarService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable selectSchedule(string calendar_cd, int year, int month)
        {
            string sSpName = "SP_CompanyCalendar";

            string gubun = "S";

            string[] param = new string[3];

            param[0] = "@s_calendar_cd:" + calendar_cd;
            param[1] = "@s_year:" + year;
            param[2] = "@s_month:" + month;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string inputMonthSchedule(string calendar_cd, int year, int month)
        {
            string sSpName = "SP_CompanyCalendar";

            string gubun = "I";

            string[] param = new string[3];

            param[0] = "@s_calendar_cd:" + calendar_cd;
            param[1] = "@s_year:" + year;
            param[2] = "@s_month:" + month;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

        internal string updateCalndar(Calendar calendar)
        {
            string sSpName = "SP_CompanyCalendar";

            string gubun = "U";

            string[] param = new string[7];

            DateTime dateTime = calendar.calendar_date;
            int attendHour = Int32.Parse(calendar.attend_time.Substring(0,2));
            int leaveHour = Int32.Parse(calendar.leave_time.Substring(0, 2));

            DateTime attend = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, attendHour, 0, 0);
            DateTime leave = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, leaveHour, 0, 0);

            TimeSpan dateDiff = leave - attend;

            param[0] = "@calendar_cd:" + calendar.calendar_cd;
            param[1] = "@calendar_date:" + calendar.calendar_date;
            param[2] = "@calendar_type:" + calendar.calendar_type;
            param[3] = "@attend_time:" + calendar.attend_time;
            param[4] = "@leave_time:" + calendar.leave_time;
            param[5] = "@working_minute:" + (dateDiff.TotalMinutes - Int32.Parse(calendar.rest_minute));
            param[6] = "@rest_minute:" + calendar.rest_minute;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }
    }
}
