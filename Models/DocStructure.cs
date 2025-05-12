using System;
using System.Data;

namespace HACCP.Models
{
    public class DocStructure
    {
        public string pGubun { get; set; }
        public string select_S { get; set; }
        public string structure_cd { get; set; }
        public string parent_cd { get; set; }
        public string child_cd { get; set; }
        public string structure_nm { get; set; }
        //public string structure_parent_nm { get; set; }
        public int structure_seq { get; set; }
        //public int structure_parent_seq { get; set; }
        public int structure_level { get; set; }
        public string use_yn { get; set; }
        //public string item_cd { get; set; }
        //public string submission_seq { get; set; }
        //public string indication { get; set; }
        //public string substance { get; set; }
        //public string manufacturer { get; set; }
        //public string product_name { get; set; }
        //public string dosageform { get; set; }
        //public string excipient { get; set; }
        //public string structure_info { get; set; }
        //public string ectd_file_yn { get; set; }
        //public string ectd_gubun { get; set; }
        //public string sys_plant_cd { get; set; }
        //public int audittrail_id { get; set; }

        public string ParentID { get; set; }
        public string ID { get; set; }
        public int ImageIndex { get; set; }
        public int num { get; set; }

        public DocStructure()
        {

        }

        public DocStructure(DataRow row)
        {
            this.structure_cd = row["structure_cd"].ToString();
            this.parent_cd = row["parent_cd"].ToString();
            this.child_cd = row["child_cd"].ToString();
            this.structure_nm = row["structure_nm"].ToString();
            //this.structure_parent_nm = row["structure_parent_nm"].ToString();

            this.structure_seq = Int32.Parse(row["structure_seq"].ToString());
            //this.structure_parent_seq = Int32.Parse(row["structure_parent_seq"].ToString());
            this.structure_level = Int32.Parse(row["structure_level"].ToString());

            this.use_yn = row["use_yn"].ToString();
            //this.item_cd = row["item_cd"].ToString();
            //this.submission_seq = row["submission_seq"].ToString();
            //this.indication = row["indication"].ToString();
            //this.substance = row["substance"].ToString();
            //this.manufacturer = row["manufacturer"].ToString();
            //this.product_name = row["product_name"].ToString();
            //this.dosageform = row["dosageform"].ToString();
            //this.excipient = row["excipient"].ToString();
            //this.structure_info = row["structure_info"].ToString();
            //this.ectd_file_yn = row["ectd_file_yn"].ToString();
            //this.ectd_gubun = row["ectd_gubun"].ToString();
            //this.sys_plant_cd = row["sys_plant_cd"].ToString();

            //this.audittrail_id = Int32.Parse(row["audittrail_id"].ToString());

            this.ParentID = row["ParentID"].ToString();
            this.ID = row["ID"].ToString();
            this.ImageIndex = Int32.Parse(row["ImageIndex"].ToString());
            this.num = Int32.Parse(row["num"].ToString());
        }
    }
}
