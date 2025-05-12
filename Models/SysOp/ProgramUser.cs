using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysOp
{
    public class ProgramUser
    {
        public string empCd { get; set; }
        public string formCd { get; set; }
        public string formQuery { get; set; }
        public string formEdit { get; set; }
        public string formInsert { get; set; }
        public string formPrint { get; set; }
        public string formDelete { get; set; }
        public string formTransmission { get; set; }
        public string formFavoriteOk { get; set; }

        public ProgramUser()
        {

        }
    }
}
