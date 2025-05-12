using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.Cp
{
    public class ClaimDetailCheck
    {
        public int claim_id { get; set; }
        public int claim_dt_id { get; set; }
        public string judge_value { get; set; }

        //public ClaimDetailCheck(DataRow row)
        //{
        //    claim_id = Int32.Parse(row["claim_id"].ToString());
        //    claim_dt_id = Int32.Parse(row["claim_dt_id"].ToString());
        //    judge_value = row["judge_value"].ToString();
        //}

    }
}