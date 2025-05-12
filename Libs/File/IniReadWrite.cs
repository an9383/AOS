using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HACCP.Libs.File
{
    public class IniReadWrite
    {
        // ---- ini 파일 의 읽고 쓰기를 위한 API 함수 선언 ----
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(    // ini Read 함수
                    String section,
                    String key,
                    String def,
                    StringBuilder retVal,
                    int size,
                    String filePath);

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(  // ini Write 함수
                    String section,
                    String key,
                    String val,
                    String filePath);

        /// ini파일에 쓰기
        public static void G_IniWriteValue(string Section, string Key, string Value, string avsPath)
        {
            WritePrivateProfileString(Section, Key, Value, avsPath);
        }
        public static void G_IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, Public_Function.CONFIGURATION_FILE);
        }

        /// ini파일에서 읽어 오기
        public static string G_IniReadValue(string Section, string Key, string avsPath)
        {
            StringBuilder temp = new StringBuilder(2000);
            int i = GetPrivateProfileString(Section, Key, "", temp, 2000, avsPath);
            return temp.ToString();
        }
        public static string G_IniReadValue(string Section, string Key)
        {
            //string p = System.AppDomain.CurrentDomain.BaseDirectory;    // Server.MapPath("~");
            StringBuilder temp = new StringBuilder(2000);
            int i = GetPrivateProfileString(Section, Key, "", temp, 2000, Public_Function.CONFIGURATION_FILE);
            return temp.ToString();
        }
    }
}
