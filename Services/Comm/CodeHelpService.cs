using DevExpress.CodeParser;
using HACCP.Libs;
using HACCP.Libs.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Comm
{
    public class CodeHelpService
    {
        private CodeHelpType mchtype;
        private string mstrsql;						//sql문
        private string mstrfrom;					//From절까지의 sql문
        private string te_wherevalue;               //검색어
        private bool mresult;						//결과
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        whPlant m_whplant;
        tgTable m_tgtable;
        whColumns m_whcolumn;
        dsColumns[] m_dscolumns;
        outColumns[] m_outcolumns;

        #region 초기 셋팅
        public CodeHelpService()
        {               
            mstrsql = "";
            mresult = false;
        }

        #endregion


        /// <summary>
        ///코드Help 팝업 - 메인로직 적용
        /// </summary>
        /// <param name="chtype">codehelp 종류</param>
        /// <param name="plant">선택사업장</param>
        /// <param name="defaultcode">검색어</param>
        /// <returns>조회팝업Data</returns>
        private DataTable GetCodeMain(CodeHelpType chtype, string plant, string defaultcode)
        {
            DataTable data;
            //mchtype = chtype;

            //기본값
            if (defaultcode != "")
                te_wherevalue = defaultcode;

            //변수 셋팅
            variableSetting2(chtype, plant);

            //사업장
            //Public_Function.GetMaster(plant, "사업장", "S");

            //가져온 정보로 컨트롤과 그리드를 셑팅한다.

            //조회
            //mstrsql = (mstrsql == "") ? getQuery() : mstrsql;
            mstrsql = getQuery();
            data = Grid_Select(mstrsql);
            
            //반환값 셋팅       
            this.mresult = (data.Rows.Count > 0) ? true : false;

            // 보통, 아래코드는 onleave() 이벤트시, 적용
            //if (mresult == true)
            //{
            //    for (int i = 0; i < outcolumns.Length; i++)
            //    {
            //        //outcolumns[i].outdata = gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, gridView1.Columns.ColumnByName(m_dscolumns[i].clmname));
            //        outcolumns[i].outdata = data.Rows[0][m_dscolumns[i].clmname].ToString();
            //    }
            //}

            
            return data;
        }

        /// <summary>
        ///코드Help 팝업 - 메인로직 적용
        /// </summary>
        /// <param name="chtype">codehelp 종류</param>
        /// <param name="plant">선택사업장</param>
        /// <param name="defaultcode">검색어</param>
        /// <param name="outcolumns">결과값 컬럼 중 데이터반환 컬럼</param>
        /// 
        /// <returns>조회팝업Data</returns>
        public DataTable GetCode(CodeHelpType chtype, string plant, string defaultcode)
        {
            mchtype = chtype;
            return this.GetCodeMain(chtype, plant, defaultcode);
        }

        /// <summary>
        /// 코드 Help 팝업 : 내부 디폴트 값이 필요한 경우에 사용.....내부 디폴트 값을 commoncode에 셋팅
        /// </summary>
        /// <param name="chtype">codehelp 종류</param>
        /// <param name="plant">선택사업장</param>
        /// <param name="commoncode"></param>
        /// <param name="defaultcode">조회조건(value)</param>
        /// <param name="outcolumns">결과값 컬럼 중 데이터반환 컬럼</param>
        /// <returns></returns>        
        public DataTable GetCode(CodeHelpType chtype, string plant, string commoncode, string defaultcode)
        {
            m_whcolumn.clmvalue = commoncode;
            return this.GetCodeMain(chtype, plant, defaultcode);
        }

        public string[] GetCodeSet(CodeHelpType chtype, string plant, string defaultcode)
        {
            mchtype = chtype;

            DataTable dt = this.GetCodeMain(chtype, plant, defaultcode);

            string[] tmpArr = new string[2];

            tmpArr[0] = JsonConvert.SerializeObject(m_dscolumns);
            tmpArr[1] = JsonConvert.SerializeObject(dt);

            return tmpArr;
        }

        /// <summary>
        /// 코드 Help팝업 : leave() 이벤트시, 코드 자동채움
        /// </summary>
        /// <param name="chtype">codehelp 종류</param>
        /// <param name="plant">선택사업장</param>
        /// <param name="defaultcode">조회조건(value)</param>
        /// <param name="outcolumns">결과값 컬럼 중 데이터반환 컬럼</param>
        public void GetCodeAuto(CodeHelpType chtype, string plant, string defaultcode, ref outColumns[] outcolumns)
        {
            DataTable data = this.GetCodeMain(chtype, plant, defaultcode);
            if (this.mresult)
            {
                for (int i = 0; i < outcolumns.Length; i++)
                {
                    outcolumns[i].outdata = data.Rows[0][m_dscolumns[i].clmname].ToString();
                }
            }

        }

        /// <summary>
        /// 코드 Help 팝업 : leave() 이벤트시, 코드 자동채움, 내부 디폴트 값이 필요한 경우에 사용.....내부 디폴트 값을 commoncode에 셋팅
        /// </summary>
        /// <param name="chtype">codehelp 종류</param>
        /// <param name="plant">선택사업장</param>
        /// <param name="commoncode"></param>
        /// <param name="defaultcode">조회조건(value)</param>
        /// <param name="outcolumns">결과값 컬럼 중 데이터반환 컬럼</param>
        /// <returns></returns>        
        public void GetCodeAuto(CodeHelpType chtype, string plant, string commoncode, string defaultcode, ref outColumns[] outcolumns)
        {
            m_whcolumn.clmvalue = commoncode;
            DataTable data = this.GetCode(chtype, plant, defaultcode);

            if (this.mresult)
            {
                for (int i = 0; i < outcolumns.Length; i++)
                {
                    outcolumns[i].outdata = data.Rows[0][m_dscolumns[i].clmname].ToString();
                }
            }
        }


        /// <summary>
        /// 데이터조회 : sql문을 받아서 실행
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        private DataTable Grid_Select(string strsql)
        {
            /* Grid Setting */
            DataTable myTable = new DataTable();
            myTable = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);	//그리드 데이타 적용 =>필드, 밴드, 모양등을 결정하고 나서...            			
            
            if (myTable.Rows.Count > 0)
            {
                if (mchtype == CodeHelpType.userid)
                {
                    for (int i = 0; i < myTable.Rows.Count; i++)
                    {
                        myTable.Rows[i]["user_id"] = Encryption.Decrypt("100", myTable.Rows[i]["user_id"].ToString(), true);
                    }
                }                
            }
            return myTable;
        }

        //내부 변수와 컨트롤 속성 값을 이용하여 쿼리를 조합한다. 
        public string getQuery()
        {
            //Like 검색일때는 %를 앞뒤에 붙여주고, 동등 비교일때는 %를 없이 조회한다.(2012-04-30 by 최석중)
            //유효성 체크를 할때 Like 검색을 하지 않고 동등 비교를 한다.
            return getQuery("Y");
        }

        //내부 변수와 컨트롤 속성 값을 이용하여 쿼리를 조합한다. 
        public string getQuery(string LikeYN)
        {

            //Like 검색일때는 %를 앞뒤에 붙여주고, 동등 비교일때는 %를 없이 조회한다.(2012-04-30 by 최석중)
            //유효성 체크를 할때 Like 검색을 하지 않고 동등 비교를 한다.
            string LikeString = (LikeYN == "Y" ? "%" : "");

            string str_where = "";

            if (mchtype == CodeHelpType.userid)
                str_where = Encryption.Encrypt("100", te_wherevalue, true);
            else
                str_where = te_wherevalue;

            try
            {
                mstrfrom = "SELECT ";

                mstrfrom += MakeString();
                if (m_whplant.fneed)
                {
                    mstrfrom += " FROM " + m_tgtable.tbname + " A INNER JOIN V_PLANT B ";
                    mstrfrom += "ON A.plant_cd = B.plant_cd ";
                }
                else
                    mstrfrom += " FROM " + m_tgtable.tbname + " ";

                mstrsql = mstrfrom;

                if (m_whplant.fneed)
                {
                    if (m_whplant.plant_cd == "")
                        mstrsql += "WHERE A.plant_cd LIKE '" + LikeString + "'";
                    else
                        mstrsql += "WHERE A.plant_cd LIKE '" + m_whplant.plant_cd + LikeString + "' ";

                    mstrsql += "AND (" + m_dscolumns[0].clmname + "  LIKE '" + str_where + LikeString + "' ";
                    mstrsql += "OR " + m_dscolumns[1].clmname + "  LIKE '" + LikeString + str_where + LikeString + "') ";
                }
                else
                {
                    //소모품의 경우 4번째 이름까지 찾아지도록
                    if (mchtype == CodeHelpType.expendable)
                    {
                        mstrsql += "WHERE (" + m_dscolumns[0].clmname + "  LIKE '" + LikeString + str_where + LikeString + "' ";
                        mstrsql += "OR " + m_dscolumns[1].clmname + "  LIKE '" + LikeString + str_where + LikeString + "' ";
                        mstrsql += "OR " + m_dscolumns[2].clmname + "  LIKE '" + LikeString + str_where + LikeString + "' ";
                        mstrsql += "OR " + m_dscolumns[5].clmname + "  LIKE '" + LikeString + str_where + LikeString + "' ";
                        mstrsql += "OR " + m_dscolumns[6].clmname + "  LIKE '" + LikeString + str_where + LikeString + "') ";
                    }
                    //관리시약 의 경우 조회조건을 세가지 (타입 , 코드, 명) 
                    else if (mchtype == CodeHelpType.reagent_qc110)
                    {
                        mstrsql += "WHERE (" + m_dscolumns[0].clmname + "  LIKE '" + LikeString + str_where + LikeString + "' ";
                        mstrsql += "OR " + m_dscolumns[1].clmname + "  LIKE '" + LikeString + str_where + LikeString + "') ";
                    }
                    //표준품의 경우 조회조건을 세가지(타입,코드,명) 
                    else if (mchtype == CodeHelpType.reagent_qc111)
                    {
                        mstrsql += "WHERE (" + m_dscolumns[0].clmname + "  LIKE '" + LikeString + str_where + LikeString + "' ";
                        mstrsql += "OR " + m_dscolumns[1].clmname + "  LIKE '" + LikeString + str_where + LikeString + "') ";
                    }
                    //제조처인경우(2010.06.22 추가)
                    else if (mchtype == CodeHelpType.item_maker)
                    {
                        mstrsql += "WHERE (" + m_dscolumns[2].clmname + "  LIKE '" + LikeString + str_where + LikeString + "' ";
                        mstrsql += "OR " + m_dscolumns[3].clmname + "  LIKE '" + LikeString + str_where + LikeString + "') ";
                        mstrsql += "AND " + m_dscolumns[0].clmname + "= 'Y' ";
                    }
                    else
                    {
                        mstrsql += "WHERE (" + m_dscolumns[0].clmname + "  LIKE '" + LikeString + str_where + LikeString + "' ";
                        mstrsql += "OR " + m_dscolumns[1].clmname + "  LIKE '" + LikeString + str_where + LikeString + "') ";
                    }
                }

                if (m_whcolumn.clmcodename != "" && LikeString == "%")
                {
                    mstrsql += "AND " + m_whcolumn.clmcodename + "  LIKE '" + LikeString + m_whcolumn.clmvalue + LikeString + "' ";
                    mstrsql += "AND " + m_whcolumn.clmtitlename + "  LIKE '" + LikeString + m_whcolumn.clmvalue + LikeString + "' ";
                }

                //if (mchtype == CodeHelpType.item_detail2)
                //{
                //    mstrsql += " AND item_gb_cd in ('1', '6')";
                //}

                return mstrsql;
            }
            finally
            {

            }
        }

        //chtype: codehelp 종류
        //plant: 선택사업장
        //defaultcode: 조회조건
        //outcolumns: 결과값 컬럼 중 데이터반환 컬럼
        internal bool CheckValidity(CodeHelpType chtype, string plant, string defaultcode)
        {
            DataTable data;

            //기본값
            if (defaultcode != "")
                te_wherevalue = defaultcode;

            //변수 셋팅
            variableSetting2(chtype, plant);

            //조회
            if (mstrsql == "")
                data = Grid_Select(getQuery("N"));
            else
                data = Grid_Select(mstrsql);

            //조회된 그리드 레코드가 1건이면 true, 그렇지 않으면 false
            if (data.Rows.Count == 1)
            {
                mresult = true;
            }
            else
            {
                mresult = false;
            }

            //반환
            return mresult;
        }

        //코드헬프창을 로드한 폼에서 제공한 컬럼정보로 쿼리문을 작성한다.
        private string MakeString()
        {
            int strLen = 0;
            string sqry = "";

            if (m_whplant.fneed)
                sqry = "plant_nm, ";

            for (int i = 0; i < m_dscolumns.Length; i++)
            {
                sqry += m_dscolumns[i].clmname + ", ";
            }
            strLen = sqry.Length;
            sqry = sqry.Remove(strLen - 2, 2);
            return sqry;
        }


        public void variableSetting(whPlant whplant, tgTable tgtable, whColumns whcolumn, dsColumns[] dscolumns, ref outColumns[] outcolumns)
        {
            m_whplant = whplant;
            m_tgtable = tgtable;
            m_whcolumn = whcolumn;
            m_dscolumns = dscolumns;
            m_outcolumns = outcolumns;
        }

        public void variableSetting2(CodeHelpType chtype, string plant)
        {
            m_whplant.plant_cd = plant;

            //codehelp 종류에 따라 하나씩 추가한다.
            //공정마스터
            if (chtype == CodeHelpType.process)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "process";
                m_tgtable.tbalias = "공정마스터";

                m_whcolumn.clmcodename = "process_cd";
                m_whcolumn.clmtitlename = "process_nm";
                m_whcolumn.clmalias = "공정";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "process_cd";
                m_dscolumns[0].clmalias = "공정코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "process_nm";
                m_dscolumns[1].clmalias = "공정명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "process_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "process_nm";
                m_outcolumns[1].outdata = "";
            }
            //공통코드
            else if (chtype == CodeHelpType.commoncode)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "common";
                m_tgtable.tbalias = "공통코드";

                m_whcolumn.clmcodename = "common_cd";
                m_whcolumn.clmtitlename = "common_cd";
                m_whcolumn.clmalias = "공통코드";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "common_part_cd";
                m_dscolumns[0].clmalias = "구분코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "common_part_nm";
                m_dscolumns[1].clmalias = "구분명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "common_part_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "common_part_nm";
                m_outcolumns[1].outdata = "";
            }
            //=======프로그램=============
            else if (chtype == CodeHelpType.program)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "menu_form";
                m_tgtable.tbalias = "프로그램";

                m_whcolumn.clmcodename = "form_cd";
                m_whcolumn.clmtitlename = "form_cd";
                m_whcolumn.clmalias = "프로그램코드";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "form_cd";
                m_dscolumns[0].clmalias = "프로그램코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "form_nm";
                m_dscolumns[1].clmalias = "프로그램명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "form_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "form_nm";
                m_outcolumns[1].outdata = "";
            }
            //============================

            //=======서명설정=============
            else if (chtype == CodeHelpType.sign_set)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "sign_set";
                m_tgtable.tbalias = "서명설정";

                m_whcolumn.clmcodename = "sign_set_cd";
                m_whcolumn.clmtitlename = "sign_set_cd";
                m_whcolumn.clmalias = "서명코드";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "sign_set_cd";
                m_dscolumns[0].clmalias = "서명코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "sign_set_nm";
                m_dscolumns[1].clmalias = "서명이름";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "sign_set_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "sign_set_nm";
                m_outcolumns[1].outdata = "";
            }
            //============================


            //사업장
            else if (chtype == CodeHelpType.plant)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "V_PLANT";
                m_tgtable.tbalias = "사업장마스터";

                m_whcolumn.clmcodename = "plant_cd";
                m_whcolumn.clmtitlename = "plant_nm";
                m_whcolumn.clmalias = "사업장";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "plant_cd";
                m_dscolumns[0].clmalias = "사업장코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "plant_nm";
                m_dscolumns[1].clmalias = "사업장명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "plant_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "plant_nm";
                m_outcolumns[1].outdata = "";
            }
            //=================추가=================//
            //원료제조처
            else if (chtype == CodeHelpType.materialmaker)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_materialmaker";
                m_tgtable.tbalias = "원료제조처마스터";

                m_whcolumn.clmcodename = "vender_cd";
                m_whcolumn.clmtitlename = "vender_nm";
                m_whcolumn.clmalias = "원료제조처";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "vender_cd";
                m_dscolumns[0].clmalias = "원료제조처 코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "vender_nm";
                m_dscolumns[1].clmalias = "원료제조처명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "vender_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "vender_nm";
                m_outcolumns[1].outdata = "";
            }
            //원자재별제조처
            else if (chtype == CodeHelpType.item_maker)
            {
                //m_whplant.fneed = false;

                //m_tgtable.tbname = "v_item_maker";
                //m_tgtable.tbalias = "원자재별 제조처";

                //m_whcolumn.clmcodename = "material_cd";
                //m_whcolumn.clmtitlename = "material_maker_cd";
                //m_whcolumn.clmalias = "원자재별 제조처";
                //m_whcolumn.clmvalue = "";

                //m_dscolumns = new dsColumns[4];
                //m_dscolumns[0].clmname = "material_cd";
                //m_dscolumns[0].clmalias = "원자재 코드";
                //m_dscolumns[0].fvewing = false;
                //m_dscolumns[1].clmname = "material_nm";
                //m_dscolumns[1].clmalias = "원자재";
                //m_dscolumns[1].fvewing = true;
                //m_dscolumns[2].clmname = "material_maker_cd";
                //m_dscolumns[2].clmalias = "제조처 코드";
                //m_dscolumns[2].fvewing = false;
                //m_dscolumns[3].clmname = "material_maker_nm";
                //m_dscolumns[3].clmalias = "제조처";
                //m_dscolumns[3].fvewing = true;

                //m_outcolumns = new outColumns[4];
                //m_outcolumns[0].clmname = "material_cd";
                //m_outcolumns[0].outdata = "";
                //m_outcolumns[1].clmname = "material_nm";
                //m_outcolumns[1].outdata = "";
                //m_outcolumns[2].clmname = "material_maker_cd";
                //m_outcolumns[2].outdata = "";
                //m_outcolumns[3].clmname = "material_maker_nm";
                //m_outcolumns[3].outdata = "";


                //영일제약 2010.06.25 수정
                m_whplant.fneed = false;

                m_tgtable.tbname = "vender";
                m_tgtable.tbalias = "원자재별 제조처";

                m_whcolumn.clmcodename = "";
                m_whcolumn.clmtitlename = "";
                m_whcolumn.clmalias = "";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[4];

                m_dscolumns[0].clmname = "prod_cust_ck";
                m_dscolumns[0].clmalias = "제조처여부";
                m_dscolumns[0].fvewing = false;

                m_dscolumns[1].clmname = "upjong";
                m_dscolumns[1].clmalias = "업 태";
                m_dscolumns[1].fvewing = false;

                m_dscolumns[2].clmname = "vender_cd";
                m_dscolumns[2].clmalias = "제조처 코드";
                m_dscolumns[2].fvewing = true;

                m_dscolumns[3].clmname = "vender_nm";
                m_dscolumns[3].clmalias = "제조처";
                m_dscolumns[3].fvewing = true;


                m_outcolumns = new outColumns[4];
                m_outcolumns[0].clmname = "vender_cd";
                m_outcolumns[0].outdata = "";

                m_outcolumns[1].clmname = "vender_nm";
                m_outcolumns[1].outdata = "";

                m_outcolumns[2].clmname = "prod_cust_ck";
                m_outcolumns[2].outdata = "";

                m_outcolumns[3].clmname = "upjong";
                m_outcolumns[3].outdata = "";



            }
            //=====================================//

            //=================추가=================//
            //거래처
            else if (chtype == CodeHelpType.vender)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "vender";
                m_tgtable.tbalias = "설비/부품거래처";

                m_whcolumn.clmcodename = "vender_cd";
                m_whcolumn.clmtitlename = "vender_nm";
                m_whcolumn.clmalias = "설비/부품거래처";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "vender_cd";
                m_dscolumns[0].clmalias = "설비/부품거래처코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "vender_nm";
                m_dscolumns[1].clmalias = "설비/부품거래처명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "vender_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "vender_nm";
                m_outcolumns[1].outdata = "";
            }
            //=====================================//
            //=================추가=================//

            //제조업체
            else if (chtype == CodeHelpType.vender_prod)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_vender_prod";
                m_tgtable.tbalias = "제조업체";

                m_whcolumn.clmcodename = "vender_cd";
                m_whcolumn.clmtitlename = "vender_nm";
                m_whcolumn.clmalias = "제조업체";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "vender_cd";
                m_dscolumns[0].clmalias = "제조업체코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "vender_nm";
                m_dscolumns[1].clmalias = "제조업체명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "vender_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "vender_nm";
                m_outcolumns[1].outdata = "";
            }
            //=====================================//

            //=================추가=================//
            //판매업체
            else if (chtype == CodeHelpType.vender_sell)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_vender_sell";
                m_tgtable.tbalias = "판매업체";

                m_whcolumn.clmcodename = "vender_cd";
                m_whcolumn.clmtitlename = "vender_nm";
                m_whcolumn.clmalias = "판매업체";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "vender_cd";
                m_dscolumns[0].clmalias = "판매업체코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "vender_nm";
                m_dscolumns[1].clmalias = "판매업체명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "vender_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "vender_nm";
                m_outcolumns[1].outdata = "";
            }
            //=====================================//

            //=================추가=================//
            //관리업체
            else if (chtype == CodeHelpType.vender_manage)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_vender_manage";
                m_tgtable.tbalias = "관리업체";

                m_whcolumn.clmcodename = "vender_cd";
                m_whcolumn.clmtitlename = "vender_nm";
                m_whcolumn.clmalias = "관리업체";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "vender_cd";
                m_dscolumns[0].clmalias = "관리업체코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "vender_nm";
                m_dscolumns[1].clmalias = "관리업체명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "vender_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "vender_nm";
                m_outcolumns[1].outdata = "";
            }
            //=====================================//

            //=================추가=================//
            //부품/소모품
            else if (chtype == CodeHelpType.parts)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "parts";
                m_tgtable.tbalias = "부품/소모품";

                m_whcolumn.clmcodename = "parts_cd";
                m_whcolumn.clmtitlename = "parts_nm";
                m_whcolumn.clmalias = "부품/소모품";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "parts_cd";
                m_dscolumns[0].clmalias = "부품코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "parts_nm";
                m_dscolumns[1].clmalias = "부품명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "parts_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "parts_nm";
                m_outcolumns[1].outdata = "";
            }
            //=====================================//

            //=================추가=================//
            //QC사원조회
            else if (chtype == CodeHelpType.employee_QC)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_employee_QC";
                m_tgtable.tbalias = "QC팀 사원조회";

                m_whcolumn.clmcodename = "emp_cd";
                m_whcolumn.clmtitlename = "emp_nm";
                m_whcolumn.clmalias = "사원";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "emp_cd";
                m_dscolumns[0].clmalias = "사원코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "emp_nm";
                m_dscolumns[1].clmalias = "사원명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "emp_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "emp_nm";
                m_outcolumns[1].outdata = "";
            }
            //=====================================//

            //=================추가=================//
            //배지조제목록
            else if (chtype == CodeHelpType.culture_usage)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_culture_usage";
                m_tgtable.tbalias = "조제배지목록";

                m_whcolumn.clmcodename = "usage_no";
                m_whcolumn.clmtitlename = "usage_nm";
                m_whcolumn.clmalias = "배지";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "usage_no";
                m_dscolumns[0].clmalias = "조제번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "usage_nm";
                m_dscolumns[1].clmalias = "배지명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "usage_no";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "usage_nm";
                m_outcolumns[1].outdata = "";
            }
            //=====================================//

            //사원 11.02.21 부서명 추가함 lys(영일제약)
            else if (chtype == CodeHelpType.employee)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "V_EMPLOYEE";
                m_tgtable.tbalias = "사원마스터";

                m_whcolumn.clmcodename = "emp_cd";
                m_whcolumn.clmtitlename = "emp_nm";
                m_whcolumn.clmalias = "사원";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[4];
                m_dscolumns[0].clmname = "emp_cd";
                m_dscolumns[0].clmalias = "사원코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "emp_nm";
                m_dscolumns[1].clmalias = "사원명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "dept_cd";
                m_dscolumns[2].clmalias = "부서코드";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "dept_nm";
                m_dscolumns[3].clmalias = "부서명";
                m_dscolumns[3].fvewing = true;

                m_outcolumns = new outColumns[4];
                m_outcolumns[0].clmname = "emp_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "emp_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "dept_cd";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "dept_nm";
                m_outcolumns[3].outdata = "";
            }
            //====================================
            //그룹(추가)
            else if (chtype == CodeHelpType.emp_group)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "emp_group";
                m_tgtable.tbalias = "그룹마스터";

                m_whcolumn.clmcodename = "emp_group_cd";
                m_whcolumn.clmtitlename = "emp_group_nm";
                m_whcolumn.clmalias = "그룹";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "emp_group_cd";
                m_dscolumns[0].clmalias = "그룹코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "emp_group_nm";
                m_dscolumns[1].clmalias = "그룹";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "emp_group_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "emp_group_nm";
                m_outcolumns[1].outdata = "";
            }
            //=======================================

            //부서(사용부서만)
            else if (chtype == CodeHelpType.department)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "V_DEPARTMENT";
                m_tgtable.tbalias = "부서마스터";

                m_whcolumn.clmcodename = "dept_cd";
                m_whcolumn.clmtitlename = "dept_nm";
                m_whcolumn.clmalias = "부서";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "dept_cd";
                m_dscolumns[0].clmalias = "부서코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "dept_nm";
                m_dscolumns[1].clmalias = "부서명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "dept_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "dept_nm";
                m_outcolumns[1].outdata = "";
            }
            //부서(모든부서)
            else if (chtype == CodeHelpType.alldepartment)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "V_DEPARTMENT_ALL";
                m_tgtable.tbalias = "부서마스터";

                m_whcolumn.clmcodename = "dept_cd";
                m_whcolumn.clmtitlename = "dept_nm";
                m_whcolumn.clmalias = "부서";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "dept_cd";
                m_dscolumns[0].clmalias = "부서코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "dept_nm";
                m_dscolumns[1].clmalias = "부서명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "dept_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "dept_nm";
                m_outcolumns[1].outdata = "";
            }
            //품목
            else if (chtype == CodeHelpType.item)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_item_standard";
                m_tgtable.tbalias = "품목마스터";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "품목";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "item_top_nm";
                m_dscolumns[2].clmalias = "대표여부";
                m_dscolumns[2].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
            }

            //품목
            else if (chtype == CodeHelpType.item_detail)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_item_detail";
                m_tgtable.tbalias = "품목마스터";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "품목";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[4];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "item_gb";
                m_dscolumns[2].clmalias = "품목유형";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "pack_unit";
                m_dscolumns[3].clmalias = "포장단위";
                m_dscolumns[3].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
            }

            //품목(제품과 반제품만 출력 될 수 있게)
            //else if (chtype == CodeHelpType.item_detail2)
            //{
            //    m_whplant.fneed = false;

            //    m_tgtable.tbname = "v_item_detail";
            //    m_tgtable.tbalias = "품목마스터";

            //    m_whcolumn.clmcodename = "item_cd";
            //    m_whcolumn.clmtitlename = "item_nm";
            //    m_whcolumn.clmalias = "품목";
            //    m_whcolumn.clmvalue = "";

            //    m_dscolumns = new dsColumns[4];
            //    m_dscolumns[0].clmname = "item_cd";
            //    m_dscolumns[0].clmalias = "품목코드";
            //    m_dscolumns[0].fvewing = true;
            //    m_dscolumns[1].clmname = "item_nm";
            //    m_dscolumns[1].clmalias = "품목명";
            //    m_dscolumns[1].fvewing = true;
            //    m_dscolumns[2].clmname = "item_gb";
            //    m_dscolumns[2].clmalias = "품목유형";
            //    m_dscolumns[2].fvewing = true;
            //    m_dscolumns[3].clmname = "pack_unit";
            //    m_dscolumns[3].clmalias = "포장단위";
            //    m_dscolumns[3].fvewing = true;

            //    m_outcolumns = new outColumns[2];
            //    m_outcolumns[0].clmname = "item_cd";
            //    m_outcolumns[0].outdata = "";
            //    m_outcolumns[1].clmname = "item_nm";
            //    m_outcolumns[1].outdata = "";
            //}

            //기타품목
            else if (chtype == CodeHelpType.etc_item)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_etc_item";
                m_tgtable.tbalias = "품목마스터";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "품목";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "common_part_nm";
                m_dscolumns[2].clmalias = "제품유형";
                m_dscolumns[2].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "common_part_nm";
                m_outcolumns[2].outdata = "";
            }

            else if (chtype == CodeHelpType.making_packing_item)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_making_packing_item_info";
                m_tgtable.tbalias = "품목마스터";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "품목";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "common_part_nm";
                m_dscolumns[2].clmalias = "제품유형";
                m_dscolumns[2].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "common_part_nm";
                m_outcolumns[2].outdata = "";
            }

            //불만사항에서 사용되는 품목(제품, 상품, 제조제품)
            else if (chtype == CodeHelpType.claim_makingitem)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_claim_makingitem";
                m_tgtable.tbalias = "품목마스터";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "품목";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
            }

            else if (chtype == CodeHelpType.makingitem)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = " v_item_makingstandard";
                m_tgtable.tbalias = "품목마스터";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "품목";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
            }
            else if (chtype == CodeHelpType.makingitem2)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = " v_item_makingstandard";
                m_tgtable.tbalias = "품목마스터";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "품목";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[6];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "item_packunit";
                m_dscolumns[2].clmalias = "단위";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "item_unit_qty";
                m_dscolumns[3].clmalias = "함량";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "item_lot_size";
                m_dscolumns[4].clmalias = "로트크기";
                m_dscolumns[4].fvewing = true;
                m_dscolumns[5].clmname = "item_validity_period";
                m_dscolumns[5].clmalias = "유효기간";
                m_dscolumns[5].fvewing = true;

                m_outcolumns = new outColumns[6];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "item_packunit";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "item_exchangerate";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "item_lot_size";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "item_validity_period";
                m_outcolumns[5].outdata = "";
            }

            else if (chtype == CodeHelpType.packingitem)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_packingitem";
                m_tgtable.tbalias = "제품마스터";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "제품";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "제품코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "제품명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
            }
            //포장제품 (포장제품코드, 명, 단위)
            else if (chtype == CodeHelpType.packing_item_unit)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_packing_item_unit";
                m_tgtable.tbalias = "제품마스터";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "제품";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "제품코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "제품명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "item_pack_size";
                m_dscolumns[2].clmalias = "포장단위";
                m_dscolumns[2].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "item_pack_size";
                m_outcolumns[2].outdata = "";


            }

            else if (chtype == CodeHelpType.material)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = " v_item_material";
                m_tgtable.tbalias = "원료/자재";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "원료/자재";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "item_gb_nm";
                m_dscolumns[2].clmalias = "구분";
                m_dscolumns[2].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
            }
            //영일제약 2010.06.25 추가
            else if (chtype == CodeHelpType.material2)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = " v_item_material2";
                m_tgtable.tbalias = "원료/자재";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "원료/자재";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "item_unit";
                m_dscolumns[2].clmalias = "단위";
                m_dscolumns[2].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "item_unit";
                m_outcolumns[2].outdata = "";

            }
            // material(s) 가 원자재를 모두 뜻하는 단어라서, 원료와 자재를 구분 할 수 있는 단어를 찾지 못하여 material 뒤에 3과 4를 붙임.
            else if (chtype == CodeHelpType.material3)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = " v_item_material3";
                m_tgtable.tbalias = "원료";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "원료";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
            }

            // material(s) 가 원자재를 모두 뜻하는 단어라서, 원료와 자재를 구분 할 수 있는 단어를 찾지 못하여 material 뒤에 3과 4를 붙임.
            else if (chtype == CodeHelpType.material4)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = " v_item_material4";
                m_tgtable.tbalias = "자재";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "자재";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
            }


            //작업복장
            else if (chtype == CodeHelpType.workdress)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "workdress";
                m_tgtable.tbalias = "작업복장";

                m_whcolumn.clmcodename = "workdress_cd";
                m_whcolumn.clmtitlename = "workdress_nm";
                m_whcolumn.clmalias = "작업복장";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "workdress_cd";
                m_dscolumns[0].clmalias = "작업복장코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "workdress_nm";
                m_dscolumns[1].clmalias = "작업복장명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "workdress_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "workdress_nm";
                m_outcolumns[1].outdata = "";
            }
            //갱의실
            else if (chtype == CodeHelpType.undress)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "undress";
                m_tgtable.tbalias = "갱의실";

                m_whcolumn.clmcodename = "undress_cd";
                m_whcolumn.clmtitlename = "undress_nm";
                m_whcolumn.clmalias = "갱의실";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "undress_cd";
                m_dscolumns[0].clmalias = "갱의실코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "undress_nm";
                m_dscolumns[1].clmalias = "갱의실명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "undress_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "undress_nm";
                m_outcolumns[1].outdata = "";
            }
            //작업실 2011.02.21 영일제약 작업실 주소추가 (층구분) lys
            else if (chtype == CodeHelpType.workroom)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "workroom";
                m_tgtable.tbalias = "작업실";

                m_whcolumn.clmcodename = "workroom_cd";
                m_whcolumn.clmtitlename = "workroom_nm";
                m_whcolumn.clmalias = "작업실";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "workroom_cd";
                m_dscolumns[0].clmalias = "작업실코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "workroom_nm";
                m_dscolumns[1].clmalias = "작업실명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "workroom_status";
                m_dscolumns[2].clmalias = "작업실 주소";
                m_dscolumns[2].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "workroom_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "workroom_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "workroom_status";
                m_outcolumns[2].outdata = "";
            }
            //작업실(산업용 PC 설치된 반제품 창고만 조회 추가)
            else if (chtype == CodeHelpType.workroom_hardware_yn)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_workroom_hardware_yn";
                m_tgtable.tbalias = "작업실";

                m_whcolumn.clmcodename = "workroom_cd";
                m_whcolumn.clmtitlename = "workroom_nm";
                m_whcolumn.clmalias = "작업실";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "workroom_cd";
                m_dscolumns[0].clmalias = "작업실코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "workroom_nm";
                m_dscolumns[1].clmalias = "작업실명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "common_part_nm";
                m_dscolumns[2].clmalias = "팀구분";
                m_dscolumns[2].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "workroom_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "workroom_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "common_part_nm";
                m_outcolumns[2].outdata = "";
            }
            //작업실(상세정보까지)
            else if (chtype == CodeHelpType.workroom_detail)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_workroom";
                m_tgtable.tbalias = "작업실";

                m_whcolumn.clmcodename = "workroom_cd";
                m_whcolumn.clmtitlename = "workroom_nm";
                m_whcolumn.clmalias = "작업실";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[14];
                m_dscolumns[0].clmname = "workroom_cd";
                m_dscolumns[0].clmalias = "작업실코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "workroom_nm";
                m_dscolumns[1].clmalias = "작업실명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "cleanness_cd";
                m_dscolumns[2].clmalias = "청정도등급";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "workroom_type";
                m_dscolumns[3].clmalias = "작업실종류";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "workroom_period1";
                m_dscolumns[4].clmalias = "일상적청소주기";
                m_dscolumns[4].fvewing = false;
                m_dscolumns[5].clmname = "workroom_period2";
                m_dscolumns[5].clmalias = "일상적보전주기";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "workroom_period3";
                m_dscolumns[6].clmalias = "일상적소독주기";
                m_dscolumns[6].fvewing = false;
                m_dscolumns[7].clmname = "workroom_period4";
                m_dscolumns[7].clmalias = "일상적점검주기";
                m_dscolumns[7].fvewing = false;
                m_dscolumns[8].clmname = "workroom_period5";
                m_dscolumns[8].clmalias = "일상적기타주기";
                m_dscolumns[8].fvewing = false;
                m_dscolumns[9].clmname = "workroom_long_period1";
                m_dscolumns[9].clmalias = "정기적청소주기";
                m_dscolumns[9].fvewing = false;
                m_dscolumns[10].clmname = "workroom_long_period2";
                m_dscolumns[10].clmalias = "정기적보전주기";
                m_dscolumns[10].fvewing = false;
                m_dscolumns[11].clmname = "workroom_long_period3";
                m_dscolumns[11].clmalias = "정기적소독주기";
                m_dscolumns[11].fvewing = false;
                m_dscolumns[12].clmname = "workroom_long_period4";
                m_dscolumns[12].clmalias = "정기적점검주기";
                m_dscolumns[12].fvewing = false;
                m_dscolumns[13].clmname = "workroom_long_period5";
                m_dscolumns[13].clmalias = "정기적기타주기";
                m_dscolumns[13].fvewing = false;

                m_outcolumns = new outColumns[14];
                m_outcolumns[0].clmname = "workroom_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "workroom_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "cleanness_cd";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "workroom_type";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "workroom_period1";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "workroom_period2";
                m_outcolumns[5].outdata = "";
                m_outcolumns[6].clmname = "workroom_period3";
                m_outcolumns[6].outdata = "";
                m_outcolumns[7].clmname = "workroom_period4";
                m_outcolumns[7].outdata = "";
                m_outcolumns[8].clmname = "workroom_period5";
                m_outcolumns[8].outdata = "";
                m_outcolumns[9].clmname = "workroom_long_period1";
                m_outcolumns[9].outdata = "";
                m_outcolumns[10].clmname = "workroom_long_period2";
                m_outcolumns[10].outdata = "";
                m_outcolumns[11].clmname = "workroom_long_period3";
                m_outcolumns[11].outdata = "";
                m_outcolumns[12].clmname = "workroom_long_period4";
                m_outcolumns[12].outdata = "";
                m_outcolumns[13].clmname = "workroom_long_period5";
                m_outcolumns[13].outdata = "";
            }
            //대표작업실
            else if (chtype == CodeHelpType.standardworkroom)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_standardworkroom";
                m_tgtable.tbalias = "대표작업실";

                m_whcolumn.clmcodename = "workroom_cd";
                m_whcolumn.clmtitlename = "workroom_nm";
                m_whcolumn.clmalias = "대표작업실";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[14];
                m_dscolumns[0].clmname = "workroom_cd";
                m_dscolumns[0].clmalias = "대표작업실코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "workroom_nm";
                m_dscolumns[1].clmalias = "대표작업실명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "cleanness_cd";
                m_dscolumns[2].clmalias = "청정도등급";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "workroom_type";
                m_dscolumns[3].clmalias = "작업실종류";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "workroom_period1";
                m_dscolumns[4].clmalias = "일상적청소주기";
                m_dscolumns[4].fvewing = false;
                m_dscolumns[5].clmname = "workroom_period2";
                m_dscolumns[5].clmalias = "일상적보전주기";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "workroom_period3";
                m_dscolumns[6].clmalias = "일상적소독주기";
                m_dscolumns[6].fvewing = false;
                m_dscolumns[7].clmname = "workroom_period4";
                m_dscolumns[7].clmalias = "일상적점검주기";
                m_dscolumns[7].fvewing = false;
                m_dscolumns[8].clmname = "workroom_period5";
                m_dscolumns[8].clmalias = "일상적기타주기";
                m_dscolumns[8].fvewing = false;
                m_dscolumns[9].clmname = "workroom_long_period1";
                m_dscolumns[9].clmalias = "정기적청소주기";
                m_dscolumns[9].fvewing = false;
                m_dscolumns[10].clmname = "workroom_long_period2";
                m_dscolumns[10].clmalias = "정기적보전주기";
                m_dscolumns[10].fvewing = false;
                m_dscolumns[11].clmname = "workroom_long_period3";
                m_dscolumns[11].clmalias = "정기적소독주기";
                m_dscolumns[11].fvewing = false;
                m_dscolumns[12].clmname = "workroom_long_period4";
                m_dscolumns[12].clmalias = "정기적점검주기";
                m_dscolumns[12].fvewing = false;
                m_dscolumns[13].clmname = "workroom_long_period5";
                m_dscolumns[13].clmalias = "정기적기타주기";
                m_dscolumns[13].fvewing = false;

                m_outcolumns = new outColumns[14];
                m_outcolumns[0].clmname = "workroom_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "workroom_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "cleanness_cd";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "workroom_type";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "workroom_period1";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "workroom_period2";
                m_outcolumns[5].outdata = "";
                m_outcolumns[6].clmname = "workroom_period3";
                m_outcolumns[6].outdata = "";
                m_outcolumns[7].clmname = "workroom_period4";
                m_outcolumns[7].outdata = "";
                m_outcolumns[8].clmname = "workroom_period5";
                m_outcolumns[8].outdata = "";
                m_outcolumns[9].clmname = "workroom_long_period1";
                m_outcolumns[9].outdata = "";
                m_outcolumns[10].clmname = "workroom_long_period2";
                m_outcolumns[10].outdata = "";
                m_outcolumns[11].clmname = "workroom_long_period3";
                m_outcolumns[11].outdata = "";
                m_outcolumns[12].clmname = "workroom_long_period4";
                m_outcolumns[12].outdata = "";
                m_outcolumns[13].clmname = "workroom_long_period5";
                m_outcolumns[13].outdata = "";
            }
            //포지션
            else if (chtype == CodeHelpType.position)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_position";
                m_tgtable.tbalias = "컴퓨터위치";

                m_whcolumn.clmcodename = "position_no";
                m_whcolumn.clmtitlename = "workroom_nm";
                m_whcolumn.clmalias = "컴퓨터위치";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "position_no";
                m_dscolumns[0].clmalias = "컴퓨터위치번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "workroom_nm";
                m_dscolumns[1].clmalias = "컴퓨터위치명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "position_no";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "workroom_nm";
                m_outcolumns[1].outdata = "";
            }
            //기계기구
            else if (chtype == CodeHelpType.equipment)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "equipment";
                m_tgtable.tbalias = "기계기구";

                m_whcolumn.clmcodename = "equip_cd";
                m_whcolumn.clmtitlename = "equip_nm";
                m_whcolumn.clmalias = "기계기구";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "equip_cd";
                m_dscolumns[0].clmalias = "기계기구코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "equip_nm";
                m_dscolumns[1].clmalias = "기계기구명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "equip_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "equip_nm";
                m_outcolumns[1].outdata = "";
            }
            // 사용 설비
            else if (chtype == CodeHelpType.equipment_use)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_equipment";
                m_tgtable.tbalias = "설비";

                m_whcolumn.clmcodename = "equip_cd";
                m_whcolumn.clmtitlename = "equip_nm";
                m_whcolumn.clmalias = "설비";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "equip_cd";
                m_dscolumns[0].clmalias = "설비코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "equip_nm";
                m_dscolumns[1].clmalias = "설비명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "equip_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "equip_nm";
                m_outcolumns[1].outdata = "";
            }

            //대표기계기구
            else if (chtype == CodeHelpType.standardequipment)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_standardequipment";
                m_tgtable.tbalias = "기계기구";

                m_whcolumn.clmcodename = "equip_cd";
                m_whcolumn.clmtitlename = "equip_nm";
                m_whcolumn.clmalias = "기계기구";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[13];
                m_dscolumns[0].clmname = "equip_cd";
                m_dscolumns[0].clmalias = "기계기구코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "equip_nm";
                m_dscolumns[1].clmalias = "기계기구명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "equip_type";
                m_dscolumns[2].clmalias = "종류";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "equip_period1";
                m_dscolumns[3].clmalias = "일상적청소주기";
                m_dscolumns[3].fvewing = false;
                m_dscolumns[4].clmname = "equip_period2";
                m_dscolumns[4].clmalias = "일상적보전주기";
                m_dscolumns[4].fvewing = false;
                m_dscolumns[5].clmname = "equip_period3";
                m_dscolumns[5].clmalias = "일상적소독주기";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "equip_period4";
                m_dscolumns[6].clmalias = "일상적점검주기";
                m_dscolumns[6].fvewing = false;
                m_dscolumns[7].clmname = "equip_period5";
                m_dscolumns[7].clmalias = "일상적기타주기";
                m_dscolumns[7].fvewing = false;
                m_dscolumns[8].clmname = "equip_long_period1";
                m_dscolumns[8].clmalias = "정기적청소주기";
                m_dscolumns[8].fvewing = false;
                m_dscolumns[9].clmname = "equip_long_period2";
                m_dscolumns[9].clmalias = "정기적보전주기";
                m_dscolumns[9].fvewing = false;
                m_dscolumns[10].clmname = "equip_long_period3";
                m_dscolumns[10].clmalias = "정기적소독주기";
                m_dscolumns[10].fvewing = false;
                m_dscolumns[11].clmname = "equip_long_period4";
                m_dscolumns[11].clmalias = "정기적점검주기";
                m_dscolumns[11].fvewing = false;
                m_dscolumns[12].clmname = "equip_long_period5";
                m_dscolumns[12].clmalias = "정기적기타주기";
                m_dscolumns[12].fvewing = false;

                m_outcolumns = new outColumns[13];
                m_outcolumns[0].clmname = "equip_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "equip_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "equip_type";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "equip_period1";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "equip_period2";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "equip_period3";
                m_outcolumns[5].outdata = "";
                m_outcolumns[6].clmname = "equip_period4";
                m_outcolumns[6].outdata = "";
                m_outcolumns[7].clmname = "equip_period5";
                m_outcolumns[7].outdata = "";
                m_outcolumns[8].clmname = "equip_long_period1";
                m_outcolumns[8].outdata = "";
                m_outcolumns[9].clmname = "equip_long_period2";
                m_outcolumns[9].outdata = "";
                m_outcolumns[10].clmname = "equip_long_period3";
                m_outcolumns[10].outdata = "";
                m_outcolumns[11].clmname = "equip_long_period4";
                m_outcolumns[11].outdata = "";
                m_outcolumns[12].clmname = "equip_long_period5";
                m_outcolumns[12].outdata = "";
            }
            //시험기기
            else if (chtype == CodeHelpType.tester)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_tester";
                m_tgtable.tbalias = "시험기기";

                m_whcolumn.clmcodename = "equip_cd";
                m_whcolumn.clmtitlename = "equip_nm";
                m_whcolumn.clmalias = "시험기기";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "equip_cd";
                m_dscolumns[0].clmalias = "시험기기코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "equip_nm";
                m_dscolumns[1].clmalias = "시험기기명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "equip_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "equip_nm";
                m_outcolumns[1].outdata = "";
            }

            //대표시험기기
            else if (chtype == CodeHelpType.standardtester)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_standardtester";
                m_tgtable.tbalias = "시험기기";

                m_whcolumn.clmcodename = "tester_cd";
                m_whcolumn.clmtitlename = "tester_nm";
                m_whcolumn.clmalias = "시험기기";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[13];
                m_dscolumns[0].clmname = "tester_cd";
                m_dscolumns[0].clmalias = "시험기기코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "tester_nm";
                m_dscolumns[1].clmalias = "시험기기명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "tester_type";
                m_dscolumns[2].clmalias = "종류";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "tester_period1";
                m_dscolumns[3].clmalias = "일상적청소주기";
                m_dscolumns[3].fvewing = false;
                m_dscolumns[4].clmname = "tester_period2";
                m_dscolumns[4].clmalias = "일상적보전주기";
                m_dscolumns[4].fvewing = false;
                m_dscolumns[5].clmname = "tester_period3";
                m_dscolumns[5].clmalias = "일상적소독주기";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "tester_period4";
                m_dscolumns[6].clmalias = "일상적점검주기";
                m_dscolumns[6].fvewing = false;
                m_dscolumns[7].clmname = "tester_period5";
                m_dscolumns[7].clmalias = "일상적기타주기";
                m_dscolumns[7].fvewing = false;
                m_dscolumns[8].clmname = "tester_long_period1";
                m_dscolumns[8].clmalias = "정기적청소주기";
                m_dscolumns[8].fvewing = false;
                m_dscolumns[9].clmname = "tester_long_period2";
                m_dscolumns[9].clmalias = "정기적보전주기";
                m_dscolumns[9].fvewing = false;
                m_dscolumns[10].clmname = "tester_long_period3";
                m_dscolumns[10].clmalias = "정기적소독주기";
                m_dscolumns[10].fvewing = false;
                m_dscolumns[11].clmname = "tester_long_period4";
                m_dscolumns[11].clmalias = "정기적점검주기";
                m_dscolumns[11].fvewing = false;
                m_dscolumns[12].clmname = "tester_long_period5";
                m_dscolumns[12].clmalias = "정기적기타주기";
                m_dscolumns[12].fvewing = false;

                m_outcolumns = new outColumns[13];
                m_outcolumns[0].clmname = "tester_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "tester_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "tester_type";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "tester_period1";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "tester_period2";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "tester_period3";
                m_outcolumns[5].outdata = "";
                m_outcolumns[6].clmname = "tester_period4";
                m_outcolumns[6].outdata = "";
                m_outcolumns[7].clmname = "tester_period5";
                m_outcolumns[7].outdata = "";
                m_outcolumns[8].clmname = "tester_long_period1";
                m_outcolumns[8].outdata = "";
                m_outcolumns[9].clmname = "tester_long_period2";
                m_outcolumns[9].outdata = "";
                m_outcolumns[10].clmname = "tester_long_period3";
                m_outcolumns[10].outdata = "";
                m_outcolumns[11].clmname = "tester_long_period4";
                m_outcolumns[11].outdata = "";
                m_outcolumns[12].clmname = "tester_long_period5";
                m_outcolumns[12].outdata = "";
            }

            //시험항목 마스터
            else if (chtype == CodeHelpType.testitemmaster)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_testitemmaster";
                m_tgtable.tbalias = "시험항목마스터";

                m_whcolumn.clmcodename = "testitem_cd";
                m_whcolumn.clmtitlename = "testitem_cd";
                m_whcolumn.clmalias = "시험항목";

                m_dscolumns = new dsColumns[5];
                m_dscolumns[0].clmname = "testitem_cd";
                m_dscolumns[0].clmalias = "시험항목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "testitem_nm";
                m_dscolumns[1].clmalias = "시험항목명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "codetype_nm";
                m_dscolumns[2].clmalias = "그룹/코드";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "testitem_charge_nm";
                m_dscolumns[3].clmalias = "시험담당자";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "testitem_type";
                m_dscolumns[4].clmalias = "시험종류";
                m_dscolumns[4].fvewing = true;

                m_outcolumns = new outColumns[5];
                m_outcolumns[0].clmname = "testitem_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "testitem_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "codetype_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "testitem_charge_nm";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "testitem_type";
                m_outcolumns[4].outdata = "";
            }

            //시험항목코드만(재공품항목만)
            else if (chtype == CodeHelpType.testitem)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_testitem";
                m_tgtable.tbalias = "시험항목마스터";

                m_whcolumn.clmcodename = "testitem_cd";
                m_whcolumn.clmtitlename = "testitem_cd";
                m_whcolumn.clmalias = "시험항목";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "testitem_cd";
                m_dscolumns[0].clmalias = "시험항목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "testitem_nm";
                m_dscolumns[1].clmalias = "시험항목명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "testitem_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "testitem_nm";
                m_outcolumns[1].outdata = "";
            }

            //스케줄코드
            else if (chtype == CodeHelpType.schedulecode)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_schedulecode";
                m_tgtable.tbalias = "스케줄코드";

                m_whcolumn.clmcodename = "standard_type_cd";
                m_whcolumn.clmtitlename = "standard_type_cd";
                m_whcolumn.clmalias = "구분";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "standard_cd";
                m_dscolumns[0].clmalias = "스케줄코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "standard_nm";
                m_dscolumns[1].clmalias = "스케줄코드명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "standard_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "standard_nm";
                m_outcolumns[1].outdata = "";
            }


            //스케줄대표코드
            else if (chtype == CodeHelpType.standardschedulecode)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_schedulecode";
                m_tgtable.tbalias = "스케줄대표코드";

                m_whcolumn.clmcodename = "standard_type_cd";
                m_whcolumn.clmtitlename = "standard_type_cd";
                m_whcolumn.clmalias = "구분";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "standard_cd";
                m_dscolumns[0].clmalias = "스케줄대표코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "standard_nm";
                m_dscolumns[1].clmalias = "스케줄대표코드명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "standard_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "standard_nm";
                m_outcolumns[1].outdata = "";
            }
            //세부공정
            else if (chtype == CodeHelpType.detailprocess)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "detailproc";
                m_tgtable.tbalias = "세부공정";

                m_whcolumn.clmcodename = "detailproc_cd";
                m_whcolumn.clmtitlename = "detailproc_nm";
                m_whcolumn.clmalias = "세부공정";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "detailproc_cd";
                m_dscolumns[0].clmalias = "세부공정코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "detailproc_nm";
                m_dscolumns[1].clmalias = "세부공정명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "detailproc_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "detailproc_nm";
                m_outcolumns[1].outdata = "";
            }
            //메뉴모듈
            else if (chtype == CodeHelpType.menumodule)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "menu";
                m_tgtable.tbalias = "메뉴";

                m_whcolumn.clmcodename = "module_cd";
                m_whcolumn.clmtitlename = "module_nm";
                m_whcolumn.clmalias = "메뉴";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "module_cd";
                m_dscolumns[0].clmalias = "모듈코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "module_nm";
                m_dscolumns[1].clmalias = "모듈명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "module_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "module_nm";
                m_outcolumns[1].outdata = "";
            }
            //사용자
            else if (chtype == CodeHelpType.userid)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "menu_user";
                m_tgtable.tbalias = "사용자";

                m_whcolumn.clmcodename = "user_id";
                m_whcolumn.clmtitlename = "user_nm";
                m_whcolumn.clmalias = "사용자";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "user_id";
                m_dscolumns[0].clmalias = "사용자ID";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "user_nm";
                m_dscolumns[1].clmalias = "사용자명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "user_id";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "user_nm";
                m_outcolumns[1].outdata = "";
            }
            //공통코드, 공통명칭(select distinct common_cd, common_nm from common)
            else if (chtype == CodeHelpType.distinctcommon)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_common";
                m_tgtable.tbalias = "공통코드";

                m_whcolumn.clmcodename = "common_cd";
                m_whcolumn.clmtitlename = "common_nm";
                m_whcolumn.clmalias = "공통코드";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "common_cd";
                m_dscolumns[0].clmalias = "공통코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "common_nm";
                m_dscolumns[1].clmalias = "공통명칭";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "common_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "common_nm";
                m_outcolumns[1].outdata = "";
            }
            //창고
            else if (chtype == CodeHelpType.warehouse)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "workroom";
                m_tgtable.tbalias = "반제품보관창고";

                m_whcolumn.clmcodename = "workroom_type";
                m_whcolumn.clmtitlename = "workroom_type";
                m_whcolumn.clmalias = "보관창고";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "workroom_cd";
                m_dscolumns[0].clmalias = "보관창고코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "workroom_nm";
                m_dscolumns[1].clmalias = "보관창고명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "workroom_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "workroom_nm";
                m_outcolumns[1].outdata = "";
            }
            //반제품창고
            else if (chtype == CodeHelpType.semiwarehouse)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "workroom";
                m_tgtable.tbalias = "반제품보관창고";

                m_whcolumn.clmcodename = "workroom_type";
                m_whcolumn.clmtitlename = "workroom_type";
                m_whcolumn.clmalias = "반제품보관창고";
                m_whcolumn.clmvalue = "4"; //반제품 창고만 조회

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "workroom_cd";
                m_dscolumns[0].clmalias = "보관창고코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "workroom_nm";
                m_dscolumns[1].clmalias = "보관창고명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "workroom_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "workroom_nm";
                m_outcolumns[1].outdata = "";
            }
            //거래처
            else if (chtype == CodeHelpType.cust)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_customer";
                m_tgtable.tbalias = "거래처";

                m_whcolumn.clmcodename = "cust_cd";
                m_whcolumn.clmtitlename = "cust_nm";
                m_whcolumn.clmalias = "거래처";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "cust_cd";
                m_dscolumns[0].clmalias = "거래처";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "cust_nm";
                m_dscolumns[1].clmalias = "거래처명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "cust_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "cust_nm";
                m_outcolumns[1].outdata = "";
            }

            else if (chtype == CodeHelpType.reagent)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_search_reagent";
                m_tgtable.tbalias = "시험시약";

                m_whcolumn.clmcodename = "reagent_cd";
                m_whcolumn.clmtitlename = "reagent_nm";
                m_whcolumn.clmalias = "시약";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];

                m_dscolumns[0].clmname = "reagent_cd";
                m_dscolumns[0].clmalias = "시약코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "reagent_nm";
                m_dscolumns[1].clmalias = "시약명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "reagent_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "reagent_nm";
                m_outcolumns[1].outdata = "";
            }

            else if (chtype == CodeHelpType.reagent_S)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_search_standard";
                m_tgtable.tbalias = "표준품";

                m_whcolumn.clmcodename = "reagent_cd";
                m_whcolumn.clmtitlename = "reagent_nm";
                m_whcolumn.clmalias = "표준품";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];

                m_dscolumns[0].clmname = "reagent_cd";
                m_dscolumns[0].clmalias = "표준품코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "reagent_nm";
                m_dscolumns[1].clmalias = "표준품명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "reagent_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "reagent_nm";
                m_outcolumns[1].outdata = "";
            }
            //2010.03.15 유희선 추가
            //상세보관위치추가
            //시약 /표준품 이외 모두 확인
            else if (chtype == CodeHelpType.reagent_A)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_search_ALL";
                m_tgtable.tbalias = "품목";

                m_whcolumn.clmcodename = "reagent_cd";
                m_whcolumn.clmtitlename = "reagent_nm";
                m_whcolumn.clmalias = "품목";
                m_whcolumn.clmvalue = "";


                m_dscolumns = new dsColumns[6];

                m_dscolumns[0].clmname = "reagent_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "reagent_nm";
                m_dscolumns[1].clmalias = "품명";
                m_dscolumns[1].fvewing = true;
                // 2010.06.23 전성배 수정
                m_dscolumns[2].clmname = "reagent_sub_nm";
                m_dscolumns[2].clmalias = "품명2";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "reagent_sub3_nm";
                m_dscolumns[3].clmalias = "품명3";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "reagent_sub4_nm";
                m_dscolumns[4].clmalias = "품명4";
                m_dscolumns[4].fvewing = true;
                m_dscolumns[5].clmname = "reagent_type";
                m_dscolumns[5].clmalias = "관리시약구분";
                m_dscolumns[5].fvewing = false;


                m_outcolumns = new outColumns[5];
                m_outcolumns[0].clmname = "reagent_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "reagent_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "reagent_sub_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "reagent_sub3_nm";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "reagent_sub4_nm";
                m_outcolumns[4].outdata = "";

                //m_dscolumns = new dsColumns[2];

                //m_dscolumns[0].clmname = "reagent_cd";
                //m_dscolumns[0].clmalias = "코  드";
                //m_dscolumns[0].fvewing = true;
                //m_dscolumns[1].clmname = "reagent_nm";
                //m_dscolumns[1].clmalias = "이  름";
                //m_dscolumns[1].fvewing = true;

                //m_outcolumns = new outColumns[2];
                //m_outcolumns[0].clmname = "reagent_cd";
                //m_outcolumns[0].outdata = "";
                //m_outcolumns[1].clmname = "reagent_nm";
                //m_outcolumns[1].outdata = "";
            }
            //관리시약
            //2010.03.15 유희선 추가
            //상세보관위치추가
            else if (chtype == CodeHelpType.reagent_qc110)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_reagent_qc110";
                m_tgtable.tbalias = "시약";

                //clmalias;		//조건절에 사용할 컬럼 별명(레이블을위해 사용)
                //clmcodename;	//조건절에 사용할 컬럼명(코드)
                //clmtitlename;	//조건절에 사용할 컬럼명(명)
                //clmvalue;		//조건절에 사용할 컬럼의 조건값
                m_whcolumn.clmalias = "시약";
                m_whcolumn.clmcodename = "reagent_type";
                m_whcolumn.clmtitlename = "reagent_type";


                m_dscolumns = new dsColumns[11];
                m_dscolumns[0].clmname = "reagent_cd";
                m_dscolumns[0].clmalias = "시약코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "reagent_nm";
                m_dscolumns[1].clmalias = "시약";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "reagent_sub_nm";
                m_dscolumns[2].clmalias = "시약2";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "reagent_sub3_nm";
                m_dscolumns[3].clmalias = "시약3";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "reagent_sub4_nm";
                m_dscolumns[4].clmalias = "시약4";
                m_dscolumns[4].fvewing = true;
                m_dscolumns[5].clmname = "reagent_type";
                m_dscolumns[5].clmalias = "시약구분";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "reagent_sub_cd";
                m_dscolumns[6].clmalias = "관리번호";
                m_dscolumns[6].fvewing = false;
                m_dscolumns[7].clmname = "reagent_pack_volume_unit";
                m_dscolumns[7].clmalias = "포장단위";
                m_dscolumns[7].fvewing = false;
                m_dscolumns[8].clmname = "reagent_keep_place_cd";
                m_dscolumns[8].clmalias = "보관위치코드";
                m_dscolumns[8].fvewing = false;
                m_dscolumns[9].clmname = "reagent_keep_place_nm";
                m_dscolumns[9].clmalias = "보관위치";
                m_dscolumns[9].fvewing = false;
                m_dscolumns[10].clmname = "reagent_sub_keep_place";
                m_dscolumns[10].clmalias = "상세위치";
                m_dscolumns[10].fvewing = false;


                m_outcolumns = new outColumns[11];
                m_outcolumns[0].clmname = "reagent_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "reagent_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "reagent_sub_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "reagent_sub3_nm";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "reagent_sub4_nm";
                m_outcolumns[4].outdata = "";
                m_outcolumns[6].clmname = "reagent_sub_cd";
                m_outcolumns[6].outdata = "";
                m_outcolumns[7].clmname = "reagent_pack_volume_unit";
                m_outcolumns[7].outdata = "";
                m_outcolumns[8].clmname = "reagent_keep_place_cd";
                m_outcolumns[8].outdata = "";
                m_outcolumns[9].clmname = "reagent_keep_place_nm";
                m_outcolumns[9].outdata = "";
                m_outcolumns[10].clmname = "reagent_sub_keep_place";
                m_outcolumns[10].outdata = "";
            }
            //표준품
            //2010.03.15 유희선 추가
            //상세보관위치추가
            else if (chtype == CodeHelpType.reagent_qc111)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_reagent_qc111";
                m_tgtable.tbalias = "표준품";

                m_whcolumn.clmcodename = "reagent_type";
                m_whcolumn.clmtitlename = "reagent_type";
                m_whcolumn.clmalias = "표준품";

                m_dscolumns = new dsColumns[11];
                m_dscolumns[0].clmname = "reagent_cd";
                m_dscolumns[0].clmalias = "표준품코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "reagent_nm";
                m_dscolumns[1].clmalias = "표준품";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "reagent_sub_nm";
                m_dscolumns[2].clmalias = "표준품2";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "reagent_sub3_nm";
                m_dscolumns[3].clmalias = "표준품3";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "reagent_sub4_nm";
                m_dscolumns[4].clmalias = "표준품4";
                m_dscolumns[4].fvewing = true;
                m_dscolumns[5].clmname = "reagent_type";
                m_dscolumns[5].clmalias = "표준품구분";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "reagent_sub_cd";
                m_dscolumns[6].clmalias = "관리번호";
                m_dscolumns[6].fvewing = false;
                m_dscolumns[7].clmname = "reagent_pack_volume_unit";
                m_dscolumns[7].clmalias = "포장단위";
                m_dscolumns[7].fvewing = false;
                m_dscolumns[8].clmname = "reagent_keep_place_cd";
                m_dscolumns[8].clmalias = "보관위치코드";
                m_dscolumns[8].fvewing = false;
                m_dscolumns[9].clmname = "reagent_keep_place_nm";
                m_dscolumns[9].clmalias = "보관위치";
                m_dscolumns[9].fvewing = false;
                m_dscolumns[10].clmname = "reagent_sub_keep_place";
                m_dscolumns[10].clmalias = "상세위치";
                m_dscolumns[10].fvewing = false;


                m_outcolumns = new outColumns[11];
                m_outcolumns[0].clmname = "reagent_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "reagent_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "reagent_sub_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "reagent_sub3_nm";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "reagent_sub4_nm";
                m_outcolumns[4].outdata = "";
                m_outcolumns[6].clmname = "reagent_sub_cd";
                m_outcolumns[6].outdata = "";
                m_outcolumns[7].clmname = "reagent_pack_volume_unit";
                m_outcolumns[7].outdata = "";
                m_outcolumns[8].clmname = "reagent_keep_place_cd";
                m_outcolumns[8].outdata = "";
                m_outcolumns[9].clmname = "reagent_keep_place_nm";
                m_outcolumns[9].outdata = "";
                m_outcolumns[10].clmname = "reagent_sub_keep_place";
                m_outcolumns[10].outdata = "";

            }
            //시약 발주번호 조회
            else if (chtype == CodeHelpType.reagent_order_qc110)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_reagent_order_qc110";
                m_tgtable.tbalias = "시약 발주번호";

                //clmalias;		//조건절에 사용할 컬럼 별명(레이블을위해 사용)
                //clmcodename;	//조건절에 사용할 컬럼명(코드)
                //clmtitlename;	//조건절에 사용할 컬럼명(명)
                //clmvalue;		//조건절에 사용할 컬럼의 조건값
                m_whcolumn.clmcodename = "reagent_cd";
                m_whcolumn.clmtitlename = "reagent_cd";
                m_whcolumn.clmalias = "시약코드";

                m_dscolumns = new dsColumns[11];
                m_dscolumns[0].clmname = "order_cd";
                m_dscolumns[0].clmalias = "발주번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "reagent_cd";
                m_dscolumns[1].clmalias = "품명코드";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "reagent_nm";
                m_dscolumns[2].clmalias = "품명";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "order_qty";
                m_dscolumns[3].clmalias = "발주요청량";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "order_unit";
                m_dscolumns[4].clmalias = "발주단위";
                m_dscolumns[4].fvewing = true;
                m_dscolumns[5].clmname = "purchase_cust_cd";
                m_dscolumns[5].clmalias = "구매업체코드";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "purchase_cust_nm";
                m_dscolumns[6].clmalias = "구매업체";
                m_dscolumns[6].fvewing = true;
                m_dscolumns[7].clmname = "purchase_date";
                m_dscolumns[7].clmalias = "발주처리일";
                m_dscolumns[7].fvewing = false;
                m_dscolumns[8].clmname = "order_status_cd";
                m_dscolumns[8].clmalias = "발주상태코드";
                m_dscolumns[8].fvewing = false;
                m_dscolumns[9].clmname = "order_status_nm";
                m_dscolumns[9].clmalias = "발주상태";
                m_dscolumns[9].fvewing = false;
                m_dscolumns[10].clmname = "reagent_type";
                m_dscolumns[10].clmalias = "구분";
                m_dscolumns[10].fvewing = false;

                m_outcolumns = new outColumns[7];
                m_outcolumns[0].clmname = "order_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "reagent_cd";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "reagent_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "order_qty";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "order_unit";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "purchase_cust_cd";
                m_outcolumns[5].outdata = "";
                m_outcolumns[6].clmname = "purchase_cust_nm";
                m_outcolumns[6].outdata = "";
            }
            //표준품 발주번호 조회
            else if (chtype == CodeHelpType.reagent_order_qc111)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_reagent_order_qc111";
                m_tgtable.tbalias = "표준품 발주번호";

                //clmalias;		//조건절에 사용할 컬럼 별명(레이블을위해 사용)
                //clmcodename;	//조건절에 사용할 컬럼명(코드)
                //clmtitlename;	//조건절에 사용할 컬럼명(명)
                //clmvalue;		//조건절에 사용할 컬럼의 조건값
                m_whcolumn.clmcodename = "reagent_cd";
                m_whcolumn.clmtitlename = "reagent_cd";
                m_whcolumn.clmalias = "표준품코드";

                m_dscolumns = new dsColumns[11];
                m_dscolumns[0].clmname = "order_cd";
                m_dscolumns[0].clmalias = "발주번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "reagent_cd";
                m_dscolumns[1].clmalias = "품명코드";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "reagent_nm";
                m_dscolumns[2].clmalias = "품명";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "order_qty";
                m_dscolumns[3].clmalias = "발주요청량";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "order_unit";
                m_dscolumns[4].clmalias = "발주단위";
                m_dscolumns[4].fvewing = true;
                m_dscolumns[5].clmname = "purchase_cust_cd";
                m_dscolumns[5].clmalias = "구매업체코드";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "purchase_cust_nm";
                m_dscolumns[6].clmalias = "구매업체";
                m_dscolumns[6].fvewing = true;
                m_dscolumns[7].clmname = "purchase_date";
                m_dscolumns[7].clmalias = "발주처리일";
                m_dscolumns[7].fvewing = false;
                m_dscolumns[8].clmname = "order_status_cd";
                m_dscolumns[8].clmalias = "발주상태코드";
                m_dscolumns[8].fvewing = false;
                m_dscolumns[9].clmname = "order_status_nm";
                m_dscolumns[9].clmalias = "발주상태";
                m_dscolumns[9].fvewing = false;
                m_dscolumns[10].clmname = "reagent_type";
                m_dscolumns[10].clmalias = "구분";
                m_dscolumns[10].fvewing = false;

                m_outcolumns = new outColumns[7];
                m_outcolumns[0].clmname = "order_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "reagent_cd";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "reagent_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "order_qty";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "order_unit";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "purchase_cust_cd";
                m_outcolumns[5].outdata = "";
                m_outcolumns[6].clmname = "purchase_cust_nm";
                m_outcolumns[6].outdata = "";
            }
            //랙정보
            else if (chtype == CodeHelpType.rack)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "V_WORKROOM_LOCATION";
                m_tgtable.tbalias = "랙정보";

                m_whcolumn.clmcodename = "location_cd";
                m_whcolumn.clmtitlename = "location_nm";
                m_whcolumn.clmalias = "랙";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "location_cd";
                m_dscolumns[0].clmalias = "랙번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "location_nm";
                m_dscolumns[1].clmalias = "랙명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "location_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "location_nm";
                m_outcolumns[1].outdata = "";
            }
            //창고정보
            else if (chtype == CodeHelpType.workroominfo)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "V_WORKROOM_INFO";
                m_tgtable.tbalias = "창고";

                m_whcolumn.clmcodename = "workroom_cd";
                m_whcolumn.clmtitlename = "workroom_nm";
                m_whcolumn.clmalias = "창고";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "workroom_cd";
                m_dscolumns[0].clmalias = "창고코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "workroom_nm";
                m_dscolumns[1].clmalias = "창고명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "workroom_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "workroom_nm";
                m_outcolumns[1].outdata = "";
            }

            //=================추가=================//
            //소모품 - 07.10.09 이용숙
            else if (chtype == CodeHelpType.expendable)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_expendable";
                m_tgtable.tbalias = "소모품관리마스터";

                m_whcolumn.clmalias = "소모품";
                m_whcolumn.clmcodename = "expendable_cd";
                m_whcolumn.clmtitlename = "expendable_nm";
                m_whcolumn.clmvalue = "";


                m_dscolumns = new dsColumns[9];
                m_dscolumns[0].clmname = "expendable_cd";
                m_dscolumns[0].clmalias = "소모품코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "expendable_nm";
                m_dscolumns[1].clmalias = "소모품명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "expendable2_nm";
                m_dscolumns[2].clmalias = "소모품명2";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "expendable_unit";
                m_dscolumns[3].clmalias = "단위";
                m_dscolumns[3].fvewing = false;
                m_dscolumns[4].clmname = "expendable_amt";
                m_dscolumns[4].clmalias = "단가";
                m_dscolumns[4].fvewing = false;
                m_dscolumns[5].clmname = "expendable3_nm";
                m_dscolumns[5].clmalias = "소모품명3";
                m_dscolumns[5].fvewing = true;
                m_dscolumns[6].clmname = "expendable4_nm";
                m_dscolumns[6].clmalias = "소모품명4";
                m_dscolumns[6].fvewing = false;
                m_dscolumns[7].clmname = "keep_cd";
                m_dscolumns[7].clmalias = "보관위치코드";
                m_dscolumns[7].fvewing = false;
                m_dscolumns[8].clmname = "keep_nm";
                m_dscolumns[8].clmalias = "보관위치";
                m_dscolumns[8].fvewing = true;


                m_outcolumns = new outColumns[9];
                m_outcolumns[0].clmname = "expendable_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "expendable_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "expendable2_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "expendable_unit";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "expendable_amt";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "expendable3_nm";
                m_outcolumns[5].outdata = "";
                m_outcolumns[6].clmname = "expendable4_nm";
                m_outcolumns[6].outdata = "";
                m_outcolumns[7].clmname = "keep_cd";
                m_outcolumns[7].outdata = "";
                m_outcolumns[8].clmname = "keep_nm";
                m_outcolumns[8].outdata = "";



            }

            //=================추가=================//
            //미사용 소모품 - 08.06.10 이용숙
            else if (chtype == CodeHelpType.expendable_no)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_expendable_no";
                m_tgtable.tbalias = "소모품관리마스터";

                m_whcolumn.clmalias = "소모품";
                m_whcolumn.clmcodename = "expendable_cd";
                m_whcolumn.clmtitlename = "expendable_nm";
                m_whcolumn.clmvalue = "";


                m_dscolumns = new dsColumns[4];
                m_dscolumns[0].clmname = "expendable_cd";
                m_dscolumns[0].clmalias = "소모품코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "expendable_nm";
                m_dscolumns[1].clmalias = "소모품명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "expendable_unit";
                m_dscolumns[2].clmalias = "단위";
                m_dscolumns[2].fvewing = false;
                m_dscolumns[3].clmname = "expendable_amt";
                m_dscolumns[3].clmalias = "단가";
                m_dscolumns[3].fvewing = false;


                m_outcolumns = new outColumns[4];
                m_outcolumns[0].clmname = "expendable_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "expendable_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "expendable_unit";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "expendable_amt";
                m_outcolumns[3].outdata = "";


            }
            //=====================================//
            //[소모품 발주목록조회,07.10.16 이용숙]	
            else if (chtype == CodeHelpType.purchaselist)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_purchaselist";
                m_tgtable.tbalias = "발주목록리스트";

                m_whcolumn.clmalias = "발주목록";
                m_whcolumn.clmcodename = "purchase_no";
                m_whcolumn.clmtitlename = "purchase_no";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[14];
                m_dscolumns[0].clmname = "purchase_no";
                m_dscolumns[0].clmalias = "발주번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "expendable_cd";
                m_dscolumns[1].clmalias = "소모품코드";
                m_dscolumns[1].fvewing = false;
                m_dscolumns[2].clmname = "expendable_nm";
                m_dscolumns[2].clmalias = "소모품명";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "request_dept";
                m_dscolumns[3].clmalias = "의뢰업체코드";
                m_dscolumns[3].fvewing = false;
                m_dscolumns[4].clmname = "dept_nm";
                m_dscolumns[4].clmalias = "의뢰업체명";
                m_dscolumns[4].fvewing = false;
                m_dscolumns[5].clmname = "request_emp";
                m_dscolumns[5].clmalias = "의뢰자코드";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "emp_nm";
                m_dscolumns[6].clmalias = "의뢰자명";
                m_dscolumns[6].fvewing = true;
                m_dscolumns[7].clmname = "purchase_qty";
                m_dscolumns[7].clmalias = "발주량";
                m_dscolumns[7].fvewing = true;
                m_dscolumns[8].clmname = "expendable_unit";
                m_dscolumns[8].clmalias = "단위";
                m_dscolumns[8].fvewing = true;
                m_dscolumns[9].clmname = "expendable_amt";
                m_dscolumns[9].clmalias = "단가";
                m_dscolumns[9].fvewing = false;
                m_dscolumns[10].clmname = "purchase_remark";
                m_dscolumns[10].clmalias = "참고사항";
                m_dscolumns[10].fvewing = false;

                //[보관위치,2010.03.16 유희선]
                m_dscolumns[11].clmname = "keep_cd";
                m_dscolumns[11].clmalias = "보관위치코드";
                m_dscolumns[11].fvewing = false;
                m_dscolumns[12].clmname = "keep_nm";
                m_dscolumns[12].clmalias = "보관위치";
                m_dscolumns[12].fvewing = true;
                m_dscolumns[13].clmname = "sub_keep";
                m_dscolumns[13].clmalias = "상세보관위치";
                m_dscolumns[13].fvewing = true;

                m_outcolumns = new outColumns[14];
                m_outcolumns[0].clmname = "purchase_no";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "expendable_cd";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "expendable_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "request_dept";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "dept_nm";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "request_emp";
                m_outcolumns[5].outdata = "";
                m_outcolumns[6].clmname = "emp_nm";
                m_outcolumns[6].outdata = "";
                m_outcolumns[7].clmname = "purchase_qty";
                m_outcolumns[7].outdata = "";
                m_outcolumns[8].clmname = "expendable_unit";
                m_outcolumns[8].outdata = "";
                m_outcolumns[9].clmname = "expendable_amt";
                m_outcolumns[9].outdata = "";
                m_outcolumns[10].clmname = "purchase_remark";
                m_outcolumns[10].outdata = "";
                m_outcolumns[11].clmname = "keep_cd";
                m_outcolumns[11].outdata = "";
                m_outcolumns[12].clmname = "keep_nm";
                m_outcolumns[12].outdata = "";
                m_outcolumns[13].clmname = "sub_keep";
                m_outcolumns[13].outdata = "";
            }


            //07.10.24 최석중 추가
            //fs 번호
            else if (chtype == CodeHelpType.fs_no)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "fs_master";
                m_tgtable.tbalias = "기능 규격";

                m_whcolumn.clmcodename = "fs_no";
                m_whcolumn.clmtitlename = "fs_title";
                m_whcolumn.clmalias = "기능 규격";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "fs_no";
                m_dscolumns[0].clmalias = "FS 번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "fs_title";
                m_dscolumns[1].clmalias = "FS 내용";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "fs_no";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "fs_title";
                m_outcolumns[1].outdata = "";
            }
            //07.10.24 최석중 추가
            //test 번호
            else if (chtype == CodeHelpType.test_no)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "test_master";
                m_tgtable.tbalias = "OQ 시험";

                m_whcolumn.clmcodename = "test_no";
                m_whcolumn.clmtitlename = "test_remark";
                m_whcolumn.clmalias = "OQ 시험";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "test_no";
                m_dscolumns[0].clmalias = "TEST 번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "test_remark";
                m_dscolumns[1].clmalias = "TEST 내용";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "test_no";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "test_remark";
                m_outcolumns[1].outdata = "";
            }

            //07.11.09 함영수 추가
            //제조지시 대표번호
            else if (chtype == CodeHelpType.order_no)
            {

                m_whplant.fneed = false;

                m_tgtable.tbname = "v_workorder";
                m_tgtable.tbalias = "제조지시 대표번호";

                m_whcolumn.clmcodename = "order_no";
                m_whcolumn.clmtitlename = "order_no";
                m_whcolumn.clmalias = "대표번호";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[5];
                m_dscolumns[0].clmname = "order_no";
                m_dscolumns[0].clmalias = "대표번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_cd";
                m_dscolumns[1].clmalias = "품목코드";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "item_nm";
                m_dscolumns[2].clmalias = "품목명";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "item_packunit";
                m_dscolumns[3].clmalias = "품목단위";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "lot_no";
                m_dscolumns[4].clmalias = "제조번호";
                m_dscolumns[4].fvewing = true;


                m_outcolumns = new outColumns[5];
                m_outcolumns[0].clmname = "order_no";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_cd";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "item_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "item_packunit";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "lot_no";
                m_outcolumns[4].outdata = "";

            }
            //07.11.22 함영수 추가
            //제조지시 제조번호
            else if (chtype == CodeHelpType.lot_no)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_workorder";
                m_tgtable.tbalias = "제조지시 제조번호";

                m_whcolumn.clmcodename = "lot_no";
                m_whcolumn.clmtitlename = "lot_no";
                m_whcolumn.clmalias = "제조번호";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[4];
                m_dscolumns[0].clmname = "order_no";
                m_dscolumns[0].clmalias = "대표번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_cd";
                m_dscolumns[1].clmalias = "품목코드";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "item_nm";
                m_dscolumns[2].clmalias = "품목명";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "lot_no";
                m_dscolumns[3].clmalias = "제조번호";
                m_dscolumns[3].fvewing = true;

                m_outcolumns = new outColumns[4];
                m_outcolumns[0].clmname = "order_no";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_cd";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "item_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "lot_no";
                m_outcolumns[3].outdata = "";
            }
            //07.10.28 이용숙 추가
            //fs 번호
            else if (chtype == CodeHelpType.fs_no2)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_fs_master";
                m_tgtable.tbalias = "기능 규격";

                m_whcolumn.clmcodename = "fs_no";
                m_whcolumn.clmtitlename = "fs_title";
                m_whcolumn.clmalias = "기능 규격";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "fs_no";
                m_dscolumns[0].clmalias = "FS 번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "fs_title";
                m_dscolumns[1].clmalias = "FS 내용";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "form_nm";
                m_dscolumns[2].clmalias = "프로그램명";
                m_dscolumns[2].fvewing = false;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "fs_no";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "fs_title";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "form_nm";
                m_outcolumns[2].outdata = "";
            }
            //07.11.29 최석중 추가
            //소스코드 
            else if (chtype == CodeHelpType.source_cd)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_source";
                m_tgtable.tbalias = "소스코드";

                m_whcolumn.clmcodename = "source_cd";
                m_whcolumn.clmtitlename = "source_nm";
                m_whcolumn.clmalias = "소스코드";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "source_cd";
                m_dscolumns[0].clmalias = "소스코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "source_nm";
                m_dscolumns[1].clmalias = "소스명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "source_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "source_nm";
                m_outcolumns[1].outdata = "";
            }
            //07.11.29 최석중 추가
            //어셈블리 이름
            else if (chtype == CodeHelpType.assembly_name)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_assembly";
                m_tgtable.tbalias = "어셈블리(DLL)";

                m_whcolumn.clmcodename = "form_dll";
                m_whcolumn.clmtitlename = "form_dll";
                m_whcolumn.clmalias = "어셈블리(DLL)";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "form_dll";
                m_dscolumns[0].clmalias = "어셈블리";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "form_dll";
                m_dscolumns[1].clmalias = "어셈블리";
                m_dscolumns[1].fvewing = false;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "form_dll";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "form_dll";
                m_outcolumns[1].outdata = "";
            }
            //2009.05.08 박은용 추가
            //설비관련 부품
            else if (chtype == CodeHelpType.part)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "part";
                m_tgtable.tbalias = "부품";

                m_whcolumn.clmcodename = "part_cd";
                m_whcolumn.clmtitlename = "part_nm";
                m_whcolumn.clmalias = "부품";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "part_cd";
                m_dscolumns[0].clmalias = "부품코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "part_nm";
                m_dscolumns[1].clmalias = "부품명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "part_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "part_nm";
                m_outcolumns[1].outdata = "";
            }
            //2009.05.21 박은용 추가
            //페기물
            else if (chtype == CodeHelpType.waste)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "wastemaster";
                m_tgtable.tbalias = "폐기물";

                m_whcolumn.clmcodename = "waste_cd";
                m_whcolumn.clmtitlename = "waste_nm";
                m_whcolumn.clmalias = "페기물";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "waste_cd";
                m_dscolumns[0].clmalias = "폐기물코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "waste_nm";
                m_dscolumns[1].clmalias = "폐기물명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "waste_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "waste_nm";
                m_outcolumns[1].outdata = "";
            }
            //2009.06.08 박은용 추가
            //균주
            else if (chtype == CodeHelpType.bacteria)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "keep_bacteria_master";
                m_tgtable.tbalias = "균주";

                m_whcolumn.clmcodename = "bacteria_code";
                m_whcolumn.clmtitlename = "bacteria_nm";
                m_whcolumn.clmalias = "균주";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[4];
                m_dscolumns[0].clmname = "bacteria_code";
                m_dscolumns[0].clmalias = "균주코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "bacteria_nm";
                m_dscolumns[1].clmalias = "균주명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "buy_cust_cd";
                m_dscolumns[2].clmalias = "구입처코드";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "buy_cust_nm";
                m_dscolumns[3].clmalias = "구입처";
                m_dscolumns[3].fvewing = true;

                m_outcolumns = new outColumns[4];
                m_outcolumns[0].clmname = "bacteria_code";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "bacteria_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "buy_cust_cd";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "buy_cust_nm";
                m_outcolumns[3].outdata = "";
            }
            //2009.06.15 박은용 추가
            //배지
            else if (chtype == CodeHelpType.medium)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "medium_master";
                m_tgtable.tbalias = "배지";

                m_whcolumn.clmcodename = "medium_code";
                m_whcolumn.clmtitlename = "medium_nm";
                m_whcolumn.clmalias = "배지";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "medium_code";
                m_dscolumns[0].clmalias = "배지코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "medium_nm";
                m_dscolumns[1].clmalias = "배지명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "medium_code";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "medium_nm";
                m_outcolumns[1].outdata = "";
            }
            //2009.12.10 이용숙 추가
            //사용배지
            else if (chtype == CodeHelpType.use_medium)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_use_medium";
                m_tgtable.tbalias = "사용배지";

                m_whcolumn.clmcodename = "medium_code";
                m_whcolumn.clmtitlename = "medium_nm";
                m_whcolumn.clmalias = "배지";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[4];
                m_dscolumns[0].clmname = "medium_code";
                m_dscolumns[0].clmalias = "배지코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "medium_nm";
                m_dscolumns[1].clmalias = "배지명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "receipt_no";
                m_dscolumns[2].clmalias = "입고번호";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "test_no";
                m_dscolumns[3].clmalias = "시험번호";
                m_dscolumns[3].fvewing = true;

                m_outcolumns = new outColumns[4];
                m_outcolumns[0].clmname = "medium_code";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "medium_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "receipt_no";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "receipt_no";
                m_outcolumns[3].outdata = "";
            }
            //2010.07.26 유희선 추가
            //배지조회
            else if (chtype == CodeHelpType.medium_order)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_medium_order";
                m_tgtable.tbalias = "배지발주";

                m_whcolumn.clmcodename = "medium_code";
                m_whcolumn.clmtitlename = "medium_nm";
                m_whcolumn.clmalias = "배지";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[7];
                m_dscolumns[0].clmname = "medium_code";
                m_dscolumns[0].clmalias = "배지코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "medium_nm";
                m_dscolumns[1].clmalias = "배지명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "volume";
                m_dscolumns[2].clmalias = "규격";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "volume_unit";
                m_dscolumns[3].clmalias = "단위";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "cust_cd";
                m_dscolumns[4].clmalias = "제조코드";
                m_dscolumns[4].fvewing = false;
                m_dscolumns[5].clmname = "cust_nm";
                m_dscolumns[5].clmalias = "제조처";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "basic_order_qty";
                m_dscolumns[6].clmalias = "기본발주량";
                m_dscolumns[6].fvewing = true;

                m_outcolumns = new outColumns[7];
                m_outcolumns[0].clmname = "medium_code";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "medium_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "volume";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "volume_unit";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "cust_cd";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "cust_nm";
                m_outcolumns[5].outdata = "";
                m_outcolumns[6].clmname = "basic_order_qty";
                m_outcolumns[6].outdata = "";
            }

            //2010.07.26 유희선 추가
            //발주된 배지 조회
            else if (chtype == CodeHelpType.medium_order_list)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_medium_order_list";
                m_tgtable.tbalias = "배지발주";

                m_whcolumn.clmcodename = "order_no";
                m_whcolumn.clmtitlename = "order_no";
                m_whcolumn.clmalias = "배지";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[10];
                m_dscolumns[0].clmname = "order_no";
                m_dscolumns[0].clmalias = "발주번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "medium_cd";
                m_dscolumns[1].clmalias = "배지코드";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "medium_nm";
                m_dscolumns[2].clmalias = "배지명";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "order_qty";
                m_dscolumns[3].clmalias = "발주량";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "volumn";
                m_dscolumns[4].clmalias = "단위";
                m_dscolumns[4].fvewing = false;
                m_dscolumns[5].clmname = "volumn_unit";
                m_dscolumns[5].clmalias = "규격";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "cust_cd";
                m_dscolumns[6].clmalias = "제조처";
                m_dscolumns[6].fvewing = false;
                m_dscolumns[7].clmname = "cust_nm";
                m_dscolumns[7].clmalias = "제조처명";
                m_dscolumns[7].fvewing = false;
                m_dscolumns[8].clmname = "remark";
                m_dscolumns[8].clmalias = "비고";
                m_dscolumns[8].fvewing = false;
                m_dscolumns[9].clmname = "order_receipt_date";
                m_dscolumns[9].clmalias = "입고일";
                m_dscolumns[9].fvewing = false;

                m_outcolumns = new outColumns[10];
                m_outcolumns[0].clmname = "order_no";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "medium_cd";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "medium_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "order_qty";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "volumn";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "volumn_unit";
                m_outcolumns[5].outdata = "";
                m_outcolumns[6].clmname = "cust_cd";
                m_outcolumns[6].outdata = "";
                m_outcolumns[7].clmname = "cust_nm";
                m_outcolumns[7].outdata = "";
                m_outcolumns[8].clmname = "remark";
                m_outcolumns[8].outdata = "";
                m_outcolumns[9].clmname = "order_receipt_date";
                m_outcolumns[9].outdata = "";
            }
            //08.06.02 이용숙 추가   
            //안정성 시험 리스트
            else if (chtype == CodeHelpType.stability_test_list)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_stability_test_list";
                m_tgtable.tbalias = "안정성 시험 종류";

                m_whcolumn.clmcodename = "packing_unit";
                m_whcolumn.clmtitlename = "packing_unit_nm";
                m_whcolumn.clmalias = "안정성 시험";
                m_whcolumn.clmvalue = "";


                m_dscolumns = new dsColumns[7];
                m_dscolumns[0].clmname = "stability_test_id";
                m_dscolumns[0].clmalias = "안정성 시험 일련번호";
                m_dscolumns[0].fvewing = false;
                m_dscolumns[1].clmname = "packing_unit";
                m_dscolumns[1].clmalias = "제품코드";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "packing_unit_nm";
                m_dscolumns[2].clmalias = "제품";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "order_no";
                m_dscolumns[3].clmalias = "제조번호";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "start_qty";
                m_dscolumns[4].clmalias = "단위";
                m_dscolumns[4].fvewing = true;
                m_dscolumns[5].clmname = "stability_test_type";
                m_dscolumns[5].clmalias = "안정성시험타입";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "stability_test_type_nm";
                m_dscolumns[6].clmalias = "안정성시험종류";
                m_dscolumns[6].fvewing = true;



                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "stability_test_id";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "packing_unit";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "packing_unit_nm";
                m_outcolumns[2].outdata = "";
            }
            //08.11.12 이용숙 추가  
            //안정성 시험 - 시험 리스트 
            else if (chtype == CodeHelpType.stability_test_no)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_stability_test_no";
                m_tgtable.tbalias = "안정성 시험 - 시험번호";

                m_whcolumn.clmcodename = "packing_unit";
                m_whcolumn.clmtitlename = "packing_unit_nm";
                m_whcolumn.clmalias = "안정성 시험";
                m_whcolumn.clmvalue = "";


                m_dscolumns = new dsColumns[7];
                m_dscolumns[0].clmname = "stability_test_id";
                m_dscolumns[0].clmalias = "안정성 시험 일련번호";
                m_dscolumns[0].fvewing = false;
                m_dscolumns[1].clmname = "packing_unit";
                m_dscolumns[1].clmalias = "제품코드";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "packing_unit_nm";
                m_dscolumns[2].clmalias = "제품";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "order_no";
                m_dscolumns[3].clmalias = "제조번호";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "start_qty";
                m_dscolumns[4].clmalias = "단위";
                m_dscolumns[4].fvewing = true;
                m_dscolumns[5].clmname = "stability_test_type";
                m_dscolumns[5].clmalias = "안정성시험타입";
                m_dscolumns[5].fvewing = false;
                m_dscolumns[6].clmname = "stability_test_type_nm";
                m_dscolumns[6].clmalias = "안정성시험종류";
                m_dscolumns[6].fvewing = true;



                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "stability_test_id";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "packing_unit";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "packing_unit_nm";
                m_outcolumns[2].outdata = "";
            }
            //기계 파라미터 추가 
            else if (chtype == CodeHelpType.equip_parameter)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_equip_parameter";
                m_tgtable.tbalias = "파라미터";

                m_whcolumn.clmcodename = "equip_cd";
                m_whcolumn.clmtitlename = "equip_cd";
                m_whcolumn.clmalias = "파라미터";
                //m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "equip_parameter";
                m_dscolumns[0].clmalias = "파라미터";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "equip_parameter_nm";
                m_dscolumns[1].clmalias = "파라미터명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "equip_parameter";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "equip_parameter_nm";
                m_outcolumns[1].outdata = "";
            }
            //표시 자재 추가 
            else if (chtype == CodeHelpType.printitem)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_printitem";
                m_tgtable.tbalias = "표시자재";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "표시자재";
                //m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "표시자재코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "표시자재";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
            }
            //2010.3.29 박은용 추가
            //제조용수
            else if (chtype == CodeHelpType.item_water)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_item_water";
                m_tgtable.tbalias = "제조용수";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "제조용수";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "제조용수코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "제조용수명";
                m_dscolumns[1].fvewing = true;


                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
            }

            //2010.05.13 이용숙 추가
            else if (chtype == CodeHelpType.sign_module_list)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_sign_module_list";
                m_tgtable.tbalias = "전자서명";

                m_whcolumn.clmcodename = "sign_set_cd";
                m_whcolumn.clmtitlename = "sign_set_nm";
                m_whcolumn.clmalias = "전자서명";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "sign_set_cd";
                m_dscolumns[0].clmalias = "서명코드";
                m_dscolumns[0].fvewing = false;
                m_dscolumns[1].clmname = "sign_set_nm";
                m_dscolumns[1].clmalias = "서명이름";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "sign_set_content";
                m_dscolumns[2].clmalias = "서명내용";
                m_dscolumns[2].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "sign_set_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "sign_set_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "sign_set_content";
                m_outcolumns[2].outdata = "";
            }
            //컬럼명 조회 : 2010.07.08 전성배 추가
            else if (chtype == CodeHelpType.column)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_column_list";
                m_tgtable.tbalias = "컬럼목록";

                m_whcolumn.clmcodename = "column_cd";
                m_whcolumn.clmtitlename = "column_nm";
                m_whcolumn.clmalias = "컬럼목록";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[6];
                m_dscolumns[0].clmname = "column_cd";
                m_dscolumns[0].clmalias = "컬럼코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "column_nm";
                m_dscolumns[1].clmalias = "컬럼명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "column_type_cd";
                m_dscolumns[2].clmalias = "구분";
                m_dscolumns[2].fvewing = false;
                m_dscolumns[3].clmname = "column_keep_place_cd";
                m_dscolumns[3].clmalias = "보관장소코드";
                m_dscolumns[3].fvewing = false;
                m_dscolumns[4].clmname = "column_keep_place_nm";
                m_dscolumns[4].clmalias = "보관장소";
                m_dscolumns[4].fvewing = false;
                m_dscolumns[5].clmname = "column_keep_place_detail";
                m_dscolumns[5].clmalias = "보관상세";
                m_dscolumns[5].fvewing = false;

                m_outcolumns = new outColumns[6];
                m_outcolumns[0].clmname = "column_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "column_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "column_type_cd";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "column_keep_place_cd";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "column_keep_place_nm";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "column_keep_place_detail";
                m_outcolumns[5].outdata = "";
            }
            //직무 조회 : 2010.07.26 함영수 추가
            else if (chtype == CodeHelpType.Job)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "job_master";
                m_tgtable.tbalias = "직무마스터";

                m_whcolumn.clmcodename = "";
                m_whcolumn.clmtitlename = "";
                m_whcolumn.clmalias = "직무코드";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[4];
                m_dscolumns[0].clmname = "job_cd";
                m_dscolumns[0].clmalias = "직무코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "job_nm";
                m_dscolumns[1].clmalias = "직무명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "job_sort";
                m_dscolumns[2].clmalias = "직무분류";
                m_dscolumns[2].fvewing = false;
                m_dscolumns[3].clmname = "job_standard_time";
                m_dscolumns[3].clmalias = "직무표준시간";
                m_dscolumns[3].fvewing = false;

                m_outcolumns = new outColumns[4];
                m_outcolumns[0].clmname = "job_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "job_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "job_sort";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "job_standard_time";
                m_outcolumns[3].outdata = "";

            }
            //직무가 D인 직무 조회 : 2010.07.26 함영수 추가
            //직무가 E인 직무 추가 : 20110222, 전성배, 광동요청
            else if (chtype == CodeHelpType.Job_D)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_job_master";
                m_tgtable.tbalias = "직무마스터";

                m_whcolumn.clmcodename = "";
                m_whcolumn.clmtitlename = "";
                m_whcolumn.clmalias = "직무코드";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[5];
                m_dscolumns[0].clmname = "job_cd";
                m_dscolumns[0].clmalias = "직무코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "job_nm";
                m_dscolumns[1].clmalias = "직무명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "job_sort";
                m_dscolumns[2].clmalias = "직무분류";
                m_dscolumns[2].fvewing = false;
                m_dscolumns[3].clmname = "job_standard_time";
                m_dscolumns[3].clmalias = "직무표준시간";
                m_dscolumns[3].fvewing = false;
                m_dscolumns[4].clmname = "job_type";
                m_dscolumns[4].fvewing = false;
                m_dscolumns[4].clmalias = "직무타입";

                m_outcolumns = new outColumns[5];
                m_outcolumns[0].clmname = "job_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "job_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "job_sort";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "job_standard_time";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "job_type";
                m_outcolumns[4].outdata = "";
            }
            //배지성능시험에서 사용하는 균주 조회 : 2010.10.10 유희선 추가
            else if (chtype == CodeHelpType.culture_bacteria)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_culture_bacteria";
                m_tgtable.tbalias = "배양균주";

                m_whcolumn.clmcodename = "bacteria_code";
                m_whcolumn.clmtitlename = "bacteria_code";
                m_whcolumn.clmalias = "배양균주바코드";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[4];
                m_dscolumns[0].clmname = "bacteria_code";
                m_dscolumns[0].clmalias = "균주코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "culture_barcode_no";
                m_dscolumns[1].clmalias = "배양균주바코드번호";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "bacteria_nm";
                m_dscolumns[2].clmalias = "균주명";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "regist_no";
                m_dscolumns[3].clmalias = "고유번호";
                m_dscolumns[3].fvewing = true;

                m_outcolumns = new outColumns[4];
                m_outcolumns[0].clmname = "bacteria_code";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "culture_barcode_no";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "bacteria_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "regist_no";
                m_outcolumns[3].outdata = "";
            }
            else if (chtype == CodeHelpType.work_employee)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_work_employee";
                m_tgtable.tbalias = "작업자마스터";

                m_whcolumn.clmcodename = "emp_cd";
                m_whcolumn.clmtitlename = "emp_nm";
                m_whcolumn.clmalias = "사원";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "emp_cd";
                m_dscolumns[0].clmalias = "사원코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "emp_nm";
                m_dscolumns[1].clmalias = "사원명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "dept_nm";
                m_dscolumns[2].clmalias = "부서명";
                m_dscolumns[2].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "emp_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "emp_nm";
                m_outcolumns[1].outdata = "";
            }
            // 사용 설비
            else if (chtype == CodeHelpType.equipment_type3)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_equipment_type3";
                m_tgtable.tbalias = "생산설비";

                m_whcolumn.clmcodename = "equip_cd";
                m_whcolumn.clmtitlename = "equip_nm";
                m_whcolumn.clmalias = "생산설비";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[3];
                m_dscolumns[0].clmname = "equip_cd";
                m_dscolumns[0].clmalias = "설비코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "equip_nm";
                m_dscolumns[1].clmalias = "설비명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "workroom_nm";
                m_dscolumns[2].clmalias = "작업실";
                m_dscolumns[2].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "equip_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "equip_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "workroom_nm";
                m_outcolumns[2].outdata = "";
            }
            else if (chtype == CodeHelpType.vender_buy)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_vender_buy";
                m_tgtable.tbalias = "구입업체";

                m_whcolumn.clmcodename = "vender_cd";
                m_whcolumn.clmtitlename = "vender_nm";
                m_whcolumn.clmalias = "구입업체";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "vender_cd";
                m_dscolumns[0].clmalias = "구입업체코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "vender_nm";
                m_dscolumns[1].clmalias = "구입업체명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "vender_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "vender_nm";
                m_outcolumns[1].outdata = "";
            }
            //2021.06.22 박가희 추가(원료표준코드)
            else if (chtype == CodeHelpType.material_standard_cd)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = " v_material_standard_cd";
                m_tgtable.tbalias = "표준코드";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "표준코드";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "표준코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "표준코드명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
            }
            //2021.06.23 박가희 추가(국가코드)
            else if (chtype == CodeHelpType.material_country_cd)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = " v_material_country_cd";
                m_tgtable.tbalias = "국가코드";

                m_whcolumn.clmcodename = "country_cd";
                m_whcolumn.clmtitlename = "country_nm";
                m_whcolumn.clmalias = "국가코드";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "country_cd";
                m_dscolumns[0].clmalias = "국가코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "country_nm";
                m_dscolumns[1].clmalias = "국가명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "country_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "country_nm";
                m_outcolumns[1].outdata = "";
            }
            //시험규격이 들어가 있는 품목만 조회
            else if (chtype == CodeHelpType.item_testmaster)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "v_item_testmaster";
                m_tgtable.tbalias = "시험품목";

                m_whcolumn.clmcodename = "test_type";
                m_whcolumn.clmtitlename = "test_type";
                m_whcolumn.clmalias = "시험품목";

                m_dscolumns = new dsColumns[9];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "process_cd";
                m_dscolumns[2].clmalias = "공정코드";
                m_dscolumns[2].fvewing = false;
                m_dscolumns[3].clmname = "process_nm";
                m_dscolumns[3].clmalias = "공정";
                m_dscolumns[3].fvewing = true;
                m_dscolumns[4].clmname = "test_type";
                m_dscolumns[4].clmalias = "시험구분코드";
                m_dscolumns[4].fvewing = false;
                m_dscolumns[5].clmname = "test_type_nm";
                m_dscolumns[5].clmalias = "시험종류"; //2012.05.28 김태성 수정 시험구분 -> 시험종류 99
                m_dscolumns[5].fvewing = true;
                m_dscolumns[6].clmname = "test_standard_nm";
                m_dscolumns[6].clmalias = "규격";
                m_dscolumns[6].fvewing = true;
                m_dscolumns[7].clmname = "item_form_cd";
                m_dscolumns[7].clmalias = "제형코드";
                m_dscolumns[7].fvewing = false;
                m_dscolumns[8].clmname = "item_form_nm";
                m_dscolumns[8].clmalias = "제형";
                m_dscolumns[8].fvewing = true;

                m_outcolumns = new outColumns[8];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "process_cd";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "process_nm";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "test_type";
                m_outcolumns[4].outdata = "";
                m_outcolumns[5].clmname = "test_type_nm";
                m_outcolumns[5].outdata = "";
                m_outcolumns[6].clmname = "test_standard_nm";
                m_outcolumns[6].outdata = "";
                m_outcolumns[7].clmname = "item_form_cd";
                m_outcolumns[7].outdata = "";
            }
            //QC,QA보관창고
            else if (chtype == CodeHelpType.qcwarehourse)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "workroom";
                m_tgtable.tbalias = "QC,QA보관창고";

                m_whcolumn.clmcodename = "workroom_type";
                m_whcolumn.clmtitlename = "workroom_type";
                m_whcolumn.clmalias = "QC보관창고";
                m_whcolumn.clmvalue = "30"; //Qc,QA 창고만 조회

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "workroom_cd";
                m_dscolumns[0].clmalias = "보관창고코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "workroom_nm";
                m_dscolumns[1].clmalias = "보관창고명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[3];
                m_outcolumns[0].clmname = "workroom_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "workroom_nm";
                m_outcolumns[1].outdata = "";
            }
            //qc보관위치
            else if (chtype == CodeHelpType.rack_qc)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "V_WORKROOM_LOCATION_QC";
                m_tgtable.tbalias = "랙정보";

                m_whcolumn.clmcodename = "location_cd";
                m_whcolumn.clmtitlename = "location_nm";
                m_whcolumn.clmalias = "랙";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[4];
                m_dscolumns[0].clmname = "location_cd";
                m_dscolumns[0].clmalias = "랙번호";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "location_nm";
                m_dscolumns[1].clmalias = "랙명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "workroom_cd";
                m_dscolumns[2].clmalias = "창고코드";
                m_dscolumns[2].fvewing = false;
                m_dscolumns[3].clmname = "workroom_nm";
                m_dscolumns[3].clmalias = "창고명";
                m_dscolumns[3].fvewing = false;

                m_outcolumns = new outColumns[4];
                m_outcolumns[0].clmname = "location_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "location_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "workroom_cd";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "workroom_nm";
                m_outcolumns[3].outdata = "";
            }
            //안정성시험 품목
            else if (chtype == CodeHelpType.stability_item)
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "V_stability_item";
                m_tgtable.tbalias = "품목마스터";

                m_whcolumn.clmcodename = "item_cd";
                m_whcolumn.clmtitlename = "item_nm";
                m_whcolumn.clmalias = "품목";
                m_whcolumn.clmvalue = "";

                m_dscolumns = new dsColumns[5];
                m_dscolumns[0].clmname = "item_cd";
                m_dscolumns[0].clmalias = "품목코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "item_nm";
                m_dscolumns[1].clmalias = "품목명";
                m_dscolumns[1].fvewing = true;
                m_dscolumns[2].clmname = "item_gb_nm";
                m_dscolumns[2].clmalias = "제품유형";
                m_dscolumns[2].fvewing = true;
                m_dscolumns[3].clmname = "item_gb";
                m_dscolumns[3].clmalias = "제품유형코드";
                m_dscolumns[3].fvewing = false;
                m_dscolumns[4].clmname = "item_pack_size";
                m_dscolumns[4].clmalias = "포장단위";
                m_dscolumns[4].fvewing = true;


                m_outcolumns = new outColumns[5];
                m_outcolumns[0].clmname = "item_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "item_nm";
                m_outcolumns[1].outdata = "";
                m_outcolumns[2].clmname = "item_gb_nm";
                m_outcolumns[2].outdata = "";
                m_outcolumns[3].clmname = "item_gb";
                m_outcolumns[3].outdata = "";
                m_outcolumns[4].clmname = "item_pack_size";
                m_outcolumns[4].outdata = "";
            }
            //default 대표적인 codehelp
            else
            {
                m_whplant.fneed = false;

                m_tgtable.tbname = "process";
                m_tgtable.tbalias = "공정마스터";

                m_whcolumn.clmcodename = "workroom_type";
                m_whcolumn.clmtitlename = "workroom_type";
                m_whcolumn.clmvalue = "4";

                m_dscolumns = new dsColumns[2];
                m_dscolumns[0].clmname = "process_cd";
                m_dscolumns[0].clmalias = "공정코드";
                m_dscolumns[0].fvewing = true;
                m_dscolumns[1].clmname = "process_nm";
                m_dscolumns[1].clmalias = "공정명";
                m_dscolumns[1].fvewing = true;

                m_outcolumns = new outColumns[2];
                m_outcolumns[0].clmname = "process_cd";
                m_outcolumns[0].outdata = "";
                m_outcolumns[1].clmname = "process_nm";
                m_outcolumns[1].outdata = "";
            }
        }
    }
}