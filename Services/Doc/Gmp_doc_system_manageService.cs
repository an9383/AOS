using HACCP.Libs.Database;
using HACCP.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HACCP.Services.Doc
{
    public class Gmp_doc_system_manageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// Select
        /// 
        /// </summary>
        /// <param name="pSelect_S"></param>
        /// <returns></returns>
        public List<DocStructure> Select(string pSelect_S)
        {
            string sSpName = "SP_Gmp_doc_system_manage";
            string sGubun = "S";
            string[] pParam = new string[1];

            if (pSelect_S == "" || pSelect_S == null)
            {
                pParam[0] = "@select_S:" + "1";
            }
            else
            {
                pParam[0] = "@select_S:" + pSelect_S;
            }

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            List<DocStructure> docStructures = new List<DocStructure>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                DocStructure docStructure = new DocStructure(row);
                docStructures.Add(docStructure);
            }

            return docStructures;
        }

        /// <summary>
        /// Save
        /// 
        /// </summary>
        /// <param name="docStructure"></param>
        /// <returns></returns>
        public string Save(DocStructure docStructure)
        {
            string sSpName = "SP_Gmp_doc_system_manage";
            string sGubun = docStructure.pGubun;
            string[] pParam = new string[7];

            pParam[0] = "@structure_cd:" + docStructure.parent_cd+ docStructure.child_cd;
            pParam[1] = "@structure_nm:" + docStructure.structure_nm;
            pParam[2] = "@structure_seq:" + docStructure.structure_seq;
            pParam[3] = "@use_yn:" + docStructure.use_yn;
            pParam[4] = "@parent_cd:" + docStructure.parent_cd;
            pParam[5] = "@child_cd:" + docStructure.child_cd;
            pParam[6] = "@structure_level:" + docStructure.structure_level;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            //List<DocStructure> docStructures = new List<DocStructure>();

            return message;
        }

        /// <summary>
        /// Delete
        /// 
        /// </summary>
        /// <param name="structure_cd"></param>
        /// <returns></returns>
        internal string Delete(string pStructureCd)
        {
            string sSpName = "SP_Gmp_doc_system_manage";
            string sGubun = "D";
            string[] pParam = new string[1];
            pParam[0] = "@structure_cd:" + pStructureCd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }
    }
}
