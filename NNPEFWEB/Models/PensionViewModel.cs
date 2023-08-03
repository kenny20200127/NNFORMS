using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NNPEFWEB.ViewModel;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NNPEFWEB.Models
{
    public class PensionViewModel
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string SVC_NO { get; set; }
        public List<SelectListItem> serviceDetails { get; set; }
        public List<SelectListItem> shipDetails { get; set; }
        public List<SelectListItem> rankDetails { get; set; }
        public string Title { get; set; }
        public string ShipEstab { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime senioritydate { get; set; }
        public string Tradecategory { get; set; }
        public DateTime TradeCatDate { get; set; }
        public string PrvEstab { get; set; }
        public string PrvShipEstab { get; set; }
        public DateTime PrvServdate { get; set; }
        public DateTime Enlistmentdate { get; set; }
        public DateTime PreServicefrom { get; set; }
        public DateTime PreServiceTo { get; set; }
        public DateTime Warsrv_from { get; set; }
        public DateTime Warsrv_to { get; set; }
        public int TotalService { get; set; }
        public string Nonrec_Service { get; set; }
        public string NonRecReason { get; set; }
        public string TransAuthority { get; set; }
        public int FinTotalService { get; set; }
        public DateTime PensionDate { get; set; }
        public string PaymentDept { get; set; }
        public string PAddress { get; set; }
        public string CAddress { get; set; }
        public string DisabilityNat { get; set; }
        public string DisabilityDegree { get; set; }
        public DateTime DisabilityDate { get; set; }
        public decimal AnnualBasic { get; set; }
        public decimal AnnualTrans { get; set; }
        public decimal AnnualLodging { get; set; }
        public decimal AnnualServant { get; set; }
        public decimal AnnualMeal { get; set; }
        public decimal AnnualEnter { get; set; }
        public decimal AnnualUtility { get; set; }
        public decimal AnnualHouse { get; set; }
        public decimal AnnualGeneral { get; set; }
        public decimal AnnualMedical { get; set; }
        public decimal AnnualUtlity { get; set; }
        public decimal AnnualUniform { get; set; }
        public decimal TotalEmolument { get; set; }
        public decimal TotalQualify { get; set; }
        public decimal FreeSevcElement { get; set; }
        public decimal Housing_Loan { get; set; }
        public decimal Motor_Loan { get; set; }
        public decimal MCycle_Loan { get; set; }
        public decimal Welfare_Loan { get; set; }
        public decimal Coop_Loan { get; set; }
        public decimal MFinance_Loan { get; set; }
        public decimal SalaryOverpayment { get; set; }
        public decimal Other_Overcharge { get; set; }
        public string createdBy { get; set; }
        public string status { get; set; }
        public string Remark { get; set; }
        public bool IsCertify { get; set; } = false;

        public PensionViewModel()
        {
            BirthDate = DateTime.Now;
            senioritydate = DateTime.Now;
            TradeCatDate = DateTime.Now;
            PrvServdate = DateTime.Now;
            Enlistmentdate = DateTime.Now;
            PreServicefrom = DateTime.Now;
            PreServiceTo = DateTime.Now;
            Warsrv_from = DateTime.Now;
            Warsrv_to = DateTime.Now;
            PensionDate = DateTime.Now;
            DisabilityDate = DateTime.Now;
        }
    }

    public class PensionModel
    {
        public PaginatedList<PensionViewModel> Pensions { get; set; }
        public PensionViewModel pension { get; set; }
    }

    public class DeathModel
    {
        public PaginatedList<DeathViewModel> deaths { get; set; }
        public DeathViewModel death { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ApprovePensionViewModel 
    { 
        public int personID { get; set; }

        [JsonIgnore]
        public string status { get; set; }

        [JsonIgnore]
        public string createdBy { get; set; }
    }


    public class ApproveDeathViewModel
    {
        public int personID { get; set; }

        [JsonIgnore]
        public string status { get; set; }

        [JsonIgnore]
        public string createdBy { get; set; }
    }
    public class Pension
    {
        public PensionDto pensionDto { get; set; }
        public DeathDto deathDto { get; set; }
    }

   

    public class PensionDto
    {
        public int pensionInit { get; set; }
        public int pensionUpd { get; set; }
        public int pensionAppr { get; set; }
    }

    public class DeathDto
    {
        public int deathInit { get; set; }
        public int deathUpd { get; set; }
        public int deathAppr { get; set; }
    }




    public class DeathViewModel
    {
        public int personID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string SVC_NO { get; set; }
        public List<SelectListItem> serviceDetails { get; set; }
        public List<SelectListItem> shipDetails { get; set; }
        public List<SelectListItem> rankDetails { get; set; }
        public string Title { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime senioritydate { get; set; }
        public string ShipEstab { get; set; }
        public DateTime Prevsrv_from { get; set; }
        public DateTime Prevsrv_To { get; set; }
        public string PrvEstab { get; set; }
        public string PrvAuthor { get; set; }
        public DateTime PrvEnListDate { get; set; }
        public DateTime Deathdate { get; set; }
        public string DeathReason { get; set; }
        public string DeathConfirm { get; set; }
        public string TradeClass { get; set; }
        public DateTime Prvwar_from { get; set; }
        public DateTime Prvwar_to { get; set; }
        public DateTime Warsrv_from { get; set; }
        public DateTime Warsrv_to { get; set; }
        public DateTime Postwar_from { get; set; }
        public DateTime Postwar_to { get; set; }
        public string Nonrec_Service { get; set; }
        public string Totalrec_Service { get; set; }
        public string SubTreasury { get; set; }
        public string NOKName { get; set; }
        public string NOKAdress { get; set;}
        public string Totalgratuity { get; set; }
        public string PresentBasicSal { get; set; }
        public string AnnualDiving { get; set; }
        public string AnnualGCB { get; set; }
        public string AnnualLodging { get; set; }
        public string AnnualDMeal { get; set; }
        public string AnnualEntertain { get; set; }
        public string AnnualPersonal { get; set; }
        public string TotAnnualGross { get; set; }
        public string CashAdvance { get; set; }
        public string MotorVehicle { get;set; }
        public string ReducedInterest { get; set; }
        public string HousingLoan { get; set; }
        public string ReducedInterest2 { get; set; }
        public string OtherSubCharge { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public string createdby2 { get; set; }
        public string createdby3 { get; set; }
        public string createdby4 { get; set; }
        public bool isCertify { get; set; }

        public DeathViewModel()
        {
            Prvwar_from = DateTime.Now;
            Prvwar_to = DateTime.Now;
            BirthDate = DateTime.Now;
            senioritydate = DateTime.Now;
            Prevsrv_from = DateTime.Now;
            Prevsrv_To = DateTime.Now;
            Warsrv_from = DateTime.Now;
            Warsrv_to = DateTime.Now;
            Postwar_from = DateTime.Now;
            Postwar_to = DateTime.Now;
            PrvEnListDate = DateTime.Now;
            Deathdate = DateTime.Now;
        }

    }

    public class DeathDTO
    {
        public int personID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string SVC_NO { get; set; }
        public string Title { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime senioritydate { get; set; }
        public string ShipEstab { get; set; }
        public DateTime Prevsrv_from { get; set; }
        public DateTime Prevsrv_To { get; set; }
        public string PrvEstab { get; set; }
        public string PrvAuthor { get; set; }
        public DateTime PrvEnListDate { get; set; }
        public DateTime Deathdate { get; set; }
        public string DeathReason { get; set; }
        public string DeathConfirm { get; set; }
        public string TradeClass { get; set; }
        public DateTime Prvwar_from { get; set; }
        public DateTime Prvwar_to { get; set; }
        public DateTime Warsrv_from { get; set; }
        public DateTime Warsrv_to { get; set; }
        public DateTime Postwar_from { get; set; }
        public DateTime Postwar_to { get; set; }
        public int Nonrec_Service { get; set; }
        public string Totalrec_Service { get; set; }
        public string SubTreasury { get; set; }
        public string NOKName { get; set; }
        public string NOKAdress { get; set; }
        public string Totalgratuity { get; set; }
        public string PresentBasicSal { get; set; }
        public string AnnualDiving { get; set; }
        public string AnnualGCB { get; set; }
        public string AnnualLodging { get; set; }
        public string AnnualDMeal { get; set; }
        public string AnnualEntertain { get; set; }
        public string AnnualPersonal { get; set; }
        public string TotAnnualGross { get; set; }
        public string CashAdvance { get; set; }
        public string MotorVehicle { get; set; }
        public string ReducedInterest { get; set; }
        public string HousingLoan { get; set; }
        public string ReducedInterest2 { get; set; }
        public string OtherSubCharge { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public string createdby2 { get; set; }
        public string createdby3 { get; set; }
        public string createdby4 { get; set; }
        public bool isCertify { get; set; }

    }
}
