using System;
using System.Collections.Generic;

namespace Pelikan_Project.Models
{
    public class Operator
    {
        public int ID { get; set; }
        public int OperatorID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Password { get; set; }
        public string Designation { get; set; }
    }
    public class Inspection
    {
        public int ID { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public int CHK { get; set; }    
        public int DEF { get; set; }
        public DateTime? Inspection_Date { get; set; }
        /*public int Operation_ID { get; set; }*/
        public string Operation { get; set; }
        public int Operator_ID { get; set; }
        public Dictionary<string, int> Operation_Obj { get; set; }
        public string Name { get; set; }

    }
}



