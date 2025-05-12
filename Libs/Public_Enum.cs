using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Libs
{
    public class Public_Enum
    {
    }

    public enum ChartType
    {
        Doughnut,
        Bar,
        Line,
        Stack,
        Combo,
        Scatter,
        Olap
    }

    public enum DsState
    {
        dsInit,
        dsBrowse,
        dsInsert,
        dsEdit
    }

    public enum ControlMode
    {
        Browse,
        Insert,
        Edit,
        Clear
    }

    public enum ButtonMode
    {
        Browse,
        Insert,
        Edit,
        Empty
    }

    public enum DsStateSe
    {
        dsInit,
        dsInsert
    }

    public enum TransFlowType
    {
        typeBegin,
        typePass,
        typeCommit,
        typeTrans
    }

    public enum Verification
    {
        Barcode,
        Password,
        Finger
    }

    #region CodeHelp에서 사용하는 사용자 정의 데이터 형식

    public enum CodeHelpType
    {
        common,                 //공통(UI에서 직접 셋팅해서 사용)
        process,                //공정
        commoncode,             //공통코드
        distinctcommon,         //공통코드, 공통코드명(select distinct common_cd, common_nm from common)
        plant,					//사업장
        //부서관련
        department,				//부서(사용부서만)
        alldepartment,          //부서(모든부서)
                                //사원관련
        employee,               //사원        

        //거래처 관련
        vender,                 //(거래처) ----->추가
        vender_prod,            //제조업체 ----->추가
        vender_sell,            //판매업체 ----->추가
        vender_manage,          //관리업체 ----->추가
        vender_buy,             //구입업체 
        materialmaker,			//원료제조원----->추가(원료별 구입업체 설정시 표시)
        packing_materialmaker,  //자재공급원(자재별 구입업체 설정시 표시)
        item_maker,             //원자재의 공급처(원/자재의 구입업체 설정시 표시)
        cust,					//거래처       

        //장비 관련
        equipment,				//기계기구코드
        equipment_use,          //사용하는 설비만 조회
        equipment_type3,        //사용하는 설비중 생산설비만 조회
        standardequipment,		//대표기계기구코드
        standardtester,			//대표시험기기
        tester,					//시험기기
        production_equipment,   //생산설비
        support_equipment,      //생산지원설비
        computer_system,        //컴퓨터시스템

        //작업실 관련
        workroom,				//작업실
        position,
        workroom_detail,		//작업실(상세정보까지)
        standardworkroom,		//대표작업실
        rack,				    //랙정보
        rack_qc,                //qc보관위치
        workroominfo,          //창고정보
        warehouse,				//보관창고
        semiwarehouse,          //반제품보관창고 12.01.18 이효주 추가
        qcwarehourse,            //QC관련 창고   12.05.28 오종택
        workroominfo_qc,        //qc창고정보 120718  오종택
        workroom_hardware_yn,     // 산업용 PC 설치된 창고 조회 추가 2012-12-14 김정

        //제품 관련
        item,					//품목
        makingitem,				//제조제품
        makingitem2,            //제조제품코드, 제조제품명, 제조제품단위, 제조제품함량, 제조제품로트크기, 제조제품유효기간
        packingitem,            //포장제품
        making_packing_item,    //포장제품,제조제품
        claim_makingitem,		//불만사항에서 사용되는 제조제품, 제품, 상품 조회 --->추가
        material,				//원료, 자재
        material2,              //원료 단위까지 포함
        material3,              //원료만
        material4,               //자재만
        etc_item,               //기타품목
        packing_item_unit,      // 포장제품(코드,명,단위)                           //08.05.20 이용숙 
        printitem,              // 표시자재                                          //09.05.13 이용숙
        item_testtype,          //시험종류에 따라 품목 조회
        item_detail,           //품목마스터(상세)
        item_water,	            //포인트(기타제품을 의미) --------> 추가(10.3.29) 박은용
        stability_item,         //안정성시험 품목 추가 (12-04-16 LYS) - 동아제약
        test_item,             // 시험검체 수령 및 배분등에 사용용 추가(v_item_standard 에 코드,품목명, 제품유형, 포장단위 나오게 수정)  2012-05-28 김정

        parts,				    //부품/소모품	----->추가
        part,                   //부품 -------->추가(09.05.08) 박은용
        waste,                  //폐기물 --------> 추가(09.5.21) 박은용
        bacteria,               //균주 --------> 추가(09.6.08) 박은용
        medium,                 //배지 --------> 추가(09.6.15) 박은용
        use_medium,             //사용배지 --------> 추가(09.12.10) 이용숙

        program,                //프로그램----->추가
        sign_set,               //서명설정 ------>추가
        culture_usage,          //조제배지목록 ---->추가
        emp_group,              //그룹목록 ---->추가


        workdress,              //작업복장
        undress,                //갱의실마스터
        employee_QC,            //QC사원조회

        workorder,              //제조지시번호

        schedulecode,           //스케줄코드
        standardschedulecode,   //스케줄대표코드
        detailprocess,          //세부공정
        menumodule,             //메뉴모듈
        userid,                 //사용자ID



        testitemmaster,         //시험항목마스터
        testitem,               //시험항목코드만(재공품항목만)
        reagent,                //시약조회
        reagent_S,				//표준품조회
        reagent_A,              //시약 / 표준품 외에 모든 품목

        expendable,		        //소모품정보            //07.10.09 이용숙 추가
        expendable_no,          //미사용 소모품 정보    //08.06.10 이용숙 추가
        purchaselist,	    	//소모품 발주목록조회   //07.10.16 이용숙 추가
        fs_no,				    //기능 규격 번호    	//07.10.24 최석중 추가
        test_no,		    	//oq 테스트 번호    	//07.10.24 최석중 추가
        order_no,               //제조지시 대표번호,품목코드,품목명,작업지시일      //07.11.09 함영수 추가
        lot_no,                 //제조지시 대표번호,품목코드,품목명,제조번호        //07.11.22 함영수 추가
        fs_no2,                 //기능 규격 번호, 제목,프로그램명                   //07.11.28 이용숙 추가
        source_cd,              //소스코드, 프로그램명                              //07.11.29 최석중
        assembly_name,          //어셈블리 이름                                     //07.11.29 최석중

        stability_test_list,    // 안정성 시험 조회 추가                            //08.06.02 이용숙     
        stability_test_no,      // 안정성 일정 - 시험 번호 조회 추가                //08.06.02 이용숙 
        reagent_qc110,          // 관리시약 전체                                    //09.01.16 이용숙
        reagent_qc111           // 표준품 전체                                      //09.01.16 이용숙


        , reagent_qc110_v        // 관리시약(바이알)                                  //12.04.12 이용숙//표준품/시약 모듈에 바이알 기능 추가            
        , reagent_qc111_v        // 표준품(바이알)                                    //12.04.12 이용숙//표준품/시약 모듈에 바이알 기능 추가 

        , equip_parameter        //기계별파라미터//09.01.03 최석중                                             

        , sign_module_list       //10.05.13 lys 서명리스트 (일반서명)
        , column
        , medium_order           //배지조회 ------------> 추가(2010.07.26) 유희선//컬럼명 조회 추가 2010.07.08 전성배
        , medium_order_list      //배지발주조회  ------------> 추가(2010.07.26) 유희선
        , reagent_order_qc110    //관리시약 발주코드 조회 추가 2010.06.28 전성배
        , reagent_order_qc111    //표준품 발주코드 조회 추가 2010.06.28 전성배
        , culture_bacteria       //배양균주조회  ------------> 추가(2010.11.02) 유희선
        , Job                    //직무코드 ------------>추가(10.07.26) 함영수
        , Job_D                  //직무코드(D만)------------>추가(10.07.26) 함영수

        , receipt_by_testno		//원료코드에 따른 현재 재고/예약량 2012-04-19 by 유치준
        , item_testmaster       //시험규격이 등록되어 있는 품목 120507  이용숙 - 동아제약
        , equip_balance      //저울정보 조회 2012-05-14 함영수. 동아제약 

        , item_standard_workroom     // 작업실에 생산가능한 제품등록용  2013-04-02  김정
        , pallet //2013.10.23 김태성 팔레트 추가
        , process_detail //2014.03.28 세부공정 조회 추가
        , material5             //원료 : 원료코드+원료명    20140512임소희추가
        , trade
        , vender_material      //원료 거래처 출력 (CM100에서 3,5) 20140712임소희추가
        , vender_pack           //자재 거래처 출력 (CM100에서 2,5) 20140712임소희추가
        , vender_item           //완제품 거래처 출력 (CM100에서 6,5) 20140712임소희추가
        , vender_manufacture    //제조처 출력 (CM100에서 9)  20140712임소희추가
        , packingitem2          //완제품코드+완제품명+거래처제품코드 20140712임소희추가
        , vender_custreg        //간납처 출력 20140820임소희추가
        , material_standard_cd  //원료 표준코드 20140902임소희추가
        , receipt               //원료 조회 201409036 코스맥스바이오 추가
        , work_order               //제조지시 조회 201409036 코스맥스바이오 추가
        , material_country_cd   //원료 국가코드 20140912 코스맥스 추가
        , work_employee         //생산부서 작업자 조회 20140919 코스맥스 바이오 추가
        , order_request_h_item    //2014.10.28 김소형 - APS에서 사용(반제품)
        , order_request_c_item    //2014.11.03 김소형 - APS에서 사용(완제품)
        , material_country_cd_Y   //원료 국가코드 사용여부 Y만 20141231 코스맥스 추가
        , material_standard_cd_Y  //원료 표준코드 사용여부 Y만 20141231 코스맥스 추가
        , material_m1       //원료 사용여부 체크 없이 모두 조회 <마스터용> 20150109 코스맥스 추가
        , material_m2       //자재 사용여부 체크 없이 모두 조회 <마스터용> 20150109 코스맥스 추가
        , packingitem_m     //제품 사용여부 체크 없이 모두 조회 <마스터용> 20150109 코스맥스 추가
      , employee_marketing  //APS 생산의뢰 입력시 마케팅부서만 조회 2015-01-29 김소형 추가

    }
    public struct whPlant
    {
        public bool fneed;      //조회 시 사업장구분 필요여부
        public string plant_cd;     //조회에 사용할 기본 사업장
    }
    //검색대상 테이블
    public struct tgTable
    {
        public string tbname;       //검색대상 테이블
        public string tbalias;      //검색대상 테이블 별명(레이블을위해 사용)
    };
    //조회컬럼
    public struct whColumns
    {
        public string clmalias;     //조건절에 사용할 컬럼 별명(레이블을위해 사용)
        public string clmcodename;  //조건절에 사용할 컬럼명(코드)
        public string clmtitlename; //조건절에 사용할 컬럼명(명)
        public string clmvalue;     //조건절에 사용할 컬럼의 조건값
    };
    //Diplay컬럼
    public struct dsColumns
    {
        public string clmname;      //Disyplay용도로 사용할 컬럼
        public string clmalias;     //Disyplay에 사용할 컬럼 별명(레이블을위해 사용)
        public int clmwidth;        //컬럼 길이설정
        public bool fvewing;        //실제 컬럼이 코드헬프에 디스플레이 될지 결정
    };
    //결과값
    public struct outColumns
    {
        public string clmname;      //결과값을 인계할 컬럼
        public string outdata;      //해당 컬럼의 실제 결과값
    };
    #endregion

    //각폼의 툴바버튼 현재 Enabled속성 
    public struct ItemToUse
    {
        public bool search;
        public bool insert;
        public bool edit;
        public bool delete;
        public bool preview;
        public bool print;
        public bool export;
        public bool help;
        public bool screen;
    };

    public struct ItemToUse2
    {
        public bool search;
        public bool insert;
        public bool edit;
        public bool delete;
        public bool preview;
        public bool print;
        public bool export;
        public bool help;
        public bool insert2;
        public bool edit2;
        public bool delete2;
        public bool insert3;
        public bool edit3;
        public bool delete3;
    };

    //TreeList에서 Node를 Drag할 때 각 컬럼의 Node데이터를 저장하는 데이터 형식
    public struct DragData
    {
        public object SourceObject;
        public string level;
        public string clmname1;
        public object clmdata1;
        public string clmname2;
        public object clmdata2;
        public string clmname3;
        public object clmdata3;
        public string clmname4;
        public object clmdata4;
        public string clmname5;
        public object clmdata5;
    };

    public struct InterfaceData
    {
        /// /////////////// 인터페이스 변수 ///////////////////
        public string order_no;
        public int order_proc_id;
        public string workroom_cd;
        public string item_cd;
        public string equip_cd;
        public string interface_cd;
        public string plc_node_no;
        public string port_no;
        public string param_date;
        public string equip_pre_status;
        public string equip_status;         //R:가동, S:정지
        public string AA_Name;
        public string AA_Value;
        public string AB_Name;
        public string AB_Value;
        public string AC_Name;
        public string AC_Value;
        public string AD_Name;
        public string AD_Value;
        public string AE_Name;
        public string AE_Value;
        public string AF_Name;
        public string AF_Value;
        public string AG_Name;
        public string AG_Value;
        public string AH_Name;
        public string AH_Value;
        public string AI_Name;
        public string AI_Value;
        public string AJ_Name;
        public string AJ_Value;
        public string AK_Name;
        public string AK_Value;
        public string AL_Name;
        public string AL_Value;
        public string AM_Name;
        public string AM_Value;
        public string AN_Name;
        public string AN_Value;
        public string AO_Name;
        public string AO_Value;
        public string AP_Name;
        public string AP_Value;
        public string AQ_Name;
        public string AQ_Value;
        public string AR_Name;
        public string AR_Value;
        public string AS_Name;
        public string AS_Value;
        public string AT_Name;
        public string AT_Value;
        public string AU_Name;
        public string AU_Value;
        public string AV_Name;
        public string AV_Value;
        public string AW_Name;
        public string AW_Value;
        public string AX_Name;
        public string AX_Value;
        public string AY_Name;
        public string AY_Value;
        public string AZ_Name;
        public string AZ_Value;
        public string BA_Name;
        public string BA_Value;
        public string BB_Name;
        public string BB_Value;
        public string BC_Name;
        public string BC_Value;
        public string BD_Name;
        public string BD_Value;
        public string BE_Name;
        public string BE_Value;
        public string BF_Name;
        public string BF_Value;
        public string BG_Name;
        public string BG_Value;
        public string BH_Name;
        public string BH_Value;
        public string BI_Name;
        public string BI_Value;
        public string BJ_Name;
        public string BJ_Value;
        public string BK_Name;
        public string BK_Value;
        public string BL_Name;
        public string BL_Value;
        public string BM_Name;
        public string BM_Value;
        public string BN_Name;
        public string BN_Value;
        /// //////////////////////////////////////////////////////////
    }

    #region    //유효성 검사를 위한 나열형
    public struct Valid_chk
    {
        public string name;
        public string YorN_msg;
    }
    #endregion

    #region 트랜잭션 타입 (I,U,D,N)
    public enum TrxType
    {
        /// <summary>
        /// INSERT
        /// </summary>
        I,
        /// <summary>
        /// UPDATE
        /// </summary>
        U,
        /// <summary>
        /// DELTE
        /// </summary>
        D,
        /// <summary>
        /// NORMAL
        /// </summary>
        N

    }
        
    /// <summary>
    /// 로깅 - 로깅타입(Action:actionlog, Error:exception,error 로그)
    /// </summary>
    public enum LoggingType
    {
        Action,
        Error        
    }

    /// <summary>
    /// 로깅- 로깅에러타입(Action,Global)
    /// </summary>
    public enum LoggingErrorType
    {
        Action,
        Global
    }


    #endregion

}
