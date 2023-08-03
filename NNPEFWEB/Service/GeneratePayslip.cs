using Dapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using NNPEFWEB.Enum;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace NNPEFWEB.Service
{
    public class GeneratePayslip : IGeneratePayslip
    {
        private readonly IDapper dapper;
        public GeneratePayslip(IDapper dapper)
        {
            this.dapper = dapper;
        }

        public List<SelectListItem> GetFilterMonth(List<Payslip2ViewModel> listModel)
        {
            var oppy = RearrangeRecord(listModel);
            var pp = oppy.Select(x => new SelectListItem
            {
                Text = x.pay_month,
                Value = x.pay_month
            });

            return pp.ToList();
        }

        public List<SelectListItem> GetFilterYear(List<Payslip2ViewModel> listModel)
        {
            var distinctRecord = listModel.GroupBy(x => x.pay_year).Select(x => x.First());
            var pp = distinctRecord.Select(x => new SelectListItem
            {
                Value=x.pay_year,
                Text=x.pay_year
            });

            return pp.ToList();
        }

        public List<Payslip2ViewModel> RearrangeRecord(List<Payslip2ViewModel> opo)
        {
            var pp= new List<Payslip2ViewModel>();
            foreach(var item in opo)
            {
                if (item.pay_month.ToLower() == "january")
                    pp.Add(new Payslip2ViewModel() { id = 1,pay_month=item.pay_month,pay_year=item.pay_year });
                if (item.pay_month.ToLower() == "february")
                    pp.Add(new Payslip2ViewModel() { id = 2 ,pay_month = item.pay_month, pay_year = item.pay_year });
                if (item.pay_month.ToLower() == "march")
                    pp.Add(new Payslip2ViewModel() { id = 3, pay_month = item.pay_month, pay_year = item.pay_year });
                if (item.pay_month.ToLower() == "april")
                    pp.Add(new Payslip2ViewModel() { id = 4, pay_month = item.pay_month, pay_year = item.pay_year });
                if (item.pay_month.ToLower() == "may")
                    pp.Add(new Payslip2ViewModel() { id = 5, pay_month = item.pay_month, pay_year = item.pay_year });
                if (item.pay_month.ToLower() == "june")
                    pp.Add(new Payslip2ViewModel() { id = 6, pay_month = item.pay_month, pay_year = item.pay_year });
                if (item.pay_month.ToLower() == "july")
                    pp.Add(new Payslip2ViewModel() { id = 7, pay_month = item.pay_month, pay_year = item.pay_year });
                if (item.pay_month.ToLower() == "august")
                    pp.Add(new Payslip2ViewModel() { id = 8, pay_month = item.pay_month, pay_year = item.pay_year });
                if (item.pay_month.ToLower() == "september")
                    pp.Add(new Payslip2ViewModel() { id = 9, pay_month = item.pay_month, pay_year = item.pay_year });
                if (item.pay_month.ToLower() == "october")
                    pp.Add(new Payslip2ViewModel() { id = 10, pay_month = item.pay_month, pay_year = item.pay_year });
                if (item.pay_month.ToLower() == "november")
                    pp.Add(new Payslip2ViewModel() { id = 11, pay_month = item.pay_month, pay_year = item.pay_year });
                if (item.pay_month.ToLower() == "december")
                    pp.Add(new Payslip2ViewModel() { id = 12, pay_month = item.pay_month, pay_year = item.pay_year });
            }


            pp=pp.OrderBy(x => x.id).ToList();

            return pp;
        }

        public List<PayslipViewModel> GetpaySlip(string month, string svcNo,string year)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@month", month);
                param.Add("@svc_no", svcNo);
                param.Add("@year", year);

                var slipList = dapper.GetAll<PayslipViewModel>(ApplicationConstant.FilterSlipByMonth, param, commandType:System.Data.CommandType.StoredProcedure);
                return slipList;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

        public List<Payslip2ViewModel> GetpayslipMonth()
        {
            try
            {
                var param = new DynamicParameters();
               
                var slipList = dapper.GetAll<Payslip2ViewModel>(ApplicationConstant.getByMonthnadyear, param, commandType: System.Data.CommandType.StoredProcedure);
                return slipList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PayslipModel ProcessPayslip(string month,string svcNo,string year)
        {
            PayslipModel klp = new();
            var rr = GetpaySlip(month,svcNo,year);

            if (rr.Count > 0)
            {
                var IppisRecord = rr.Where(x => x.gradetype.ToLower() == "yes");
                var navyRecord = rr.Where(x => x.gradetype.ToLower() == "no");

                var taxableippis = (from a in IppisRecord
                                    where a.bpc.ToUpper() == "BP"
                                    select new Taxable()
                                    {
                                        amount = Math.Round(a.BPM,2),
                                        amount2= a.BPM.ToString("#,##0.##"),
                                        text = a.BP
                                    }).ToList();

                var deductionIppis = (from a in IppisRecord
                                      where a.bpc.ToUpper() == "PR" || a.bpc.ToUpper() == "PL"
                                      select new Deductions()
                                      {
                                          amount = Math.Round(a.BPM, 2),
                                          amount2 = Math.Round(a.BPM, 2).ToString("#,##0.##"),
                                          text = a.BP,
                                          lbal = Convert.ToDecimal(a.lbal.ToString("#,##0.##")),
                                          ltenor = a.ltenor,
                                          lmth = a.lmth
                                      }).ToList();



                var taxablenavy = (from a in navyRecord
                                   where a.bpc.ToUpper() == "PT"
                                   select new Taxable()
                                   {
                                       amount = Math.Round(a.BPM, 2),
                                       amount2 = a.BPM.ToString("#,##0.##"),
                                       text = a.BP
                                   }).ToList();

                var deductionNavy = (from a in navyRecord
                                     where a.bpc.ToUpper() == "PR"|| a.bpc.ToUpper() == "PL"
                                     select new Deductions()
                                     {
                                         amount = Math.Round(a.BPM, 2),
                                         amount2 = a.BPM.ToString("#,##0.##"),
                                         text = a.BP,
                                         lbal = Convert.ToDecimal(a.lbal.ToString("#,##0.##")),
                                         ltenor = a.ltenor,
                                         lmth = a.lmth
                                     }).ToList();

                decimal IppsTotal1 = taxableippis.Sum(x => x.amount);
                decimal ippsTotal2 = deductionIppis.Sum(x => x.amount);

                decimal navyTotal1 = taxablenavy.Sum(x => x.amount);
                decimal navyTotal2 = deductionNavy.Sum(x => x.amount);


                //do Ippis
                IPpisModel pps = new();

                pps.Totalipps = (IppsTotal1 - ippsTotal2).ToString("#,##0.##");
                pps.taxables = taxableippis;
                pps.deductions = deductionIppis;

                klp.ippis = pps;


                //do navy
                NavyModel mb = new();
                mb.TotalnavyP = (navyTotal1 - navyTotal2).ToString("#,##0.##");
                mb.taxables = taxablenavy;
                mb.deductions = deductionNavy;

                klp.navy = mb;


                

                klp.month = rr.FirstOrDefault().pay_month;
                klp.year = rr.FirstOrDefault().pay_year;
                klp.title = rr.FirstOrDefault().title;
                klp.othername = rr.FirstOrDefault().othername;
                klp.surname = rr.FirstOrDefault().surname;
               
                klp.serviceNo = rr.FirstOrDefault().NUMB;
                klp.NetPay = ((IppsTotal1 - ippsTotal2 )+(navyTotal1 - navyTotal2)).ToString("#,##0.##");
                klp.gradelevel = rr.FirstOrDefault().gradelevel;
                klp.gradetype = rr.FirstOrDefault().gradetype;
                klp.title = rr.FirstOrDefault().title;
                klp.bankacnumber = rr.FirstOrDefault().bankacnumber;
                klp.bankname = rr.FirstOrDefault().bankname;
            }

            var pop = DateTime.Now;

            klp.dateInMonth = pop.ToString("dd/MM/yyyy").Replace('-','/');
            klp.time = String.Format("{0:T}", pop);

            




            return klp;

        }


    }
}
