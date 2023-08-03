using System.Collections.Generic;

namespace NNPEFWEB.ViewModel
{
    public class PayslipModel
    {
        public string month { get; set; }
        public string year { get; set; }
        public string dateInMonth { get; set; }
        public string time { get; set; }
        public string serviceNo { get; set; }
        public string title { get; set; }
        public string surname { get; set; }
        public string othername { get; set; }
        public IPpisModel ippis { get; set; }
        public NavyModel navy { get; set; }
        public string NetPay { get; set; }
        public string bankacnumber { get; set; }
        public string bankname { get; set; }
        public string gradelevel { get; set; }
        public string gradetype { get; set; }

    }

    public class Taxable 
    {
        public string text { get;set;}
        public decimal amount { get; set; }
        public string amount2 { get; set; }
    }

    public class Deductions
    {
        public string text { get; set; }
        public decimal amount { get; set; }
        public string amount2 { get; set; }
        public int? ltenor { get; set; }
        public decimal? lbal { get; set; }
        public int? lmth {get;set;}
    }

    public class IPpisModel
    {
        public string Totalipps { get; set; }
        public List<Taxable> taxables { get; set; }
        public List<Deductions> deductions { get; set; }
    }

    public class NavyModel
    {
        public string TotalnavyP { get; set; }
        public List<Taxable> taxables { get; set; }
        public List<Deductions> deductions { get; set; }
    }

}
