#pragma checksum "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionReview.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4dd3248a0767fcc9cc020bae6c5206620559df6b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PensionForms_PensionReview), @"mvc.1.0.view", @"/Views/PensionForms/PensionReview.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\_ViewImports.cshtml"
using NNPEFWEB;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\_ViewImports.cshtml"
using NNPEFWEB.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4dd3248a0767fcc9cc020bae6c5206620559df6b", @"/Views/PensionForms/PensionReview.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"846a9991e2c6a62c166d5415e65380119a26598e", @"/Views/_ViewImports.cshtml")]
    public class Views_PensionForms_PensionReview : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NNPEFWEB.Models.PensionModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionReview.cshtml"
  
    ViewData["Title"] = "PensionReview";
    Layout = "~/Views/Shared/_gratuityLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""card"" style=""width:100%;"">
    <div class=""card-header"">
        <h1>Pension Review</h1>
    </div>
    <div class=""card-body"">
        <table class=""table table-striped"">
            <thead>
                <tr>
                    <th>Fullname</th>
                    <th>ServiceNo</th>
                    <th>Rank</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 23 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionReview.cshtml"
                  

                    string fullname = "";

                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionReview.cshtml"
                     foreach (var k in Model.Pensions)
                    {
                        fullname = k.LastName + " " + k.FirstName;

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\n                            <td>");
#nullable restore
#line 31 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionReview.cshtml"
                           Write(fullname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 32 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionReview.cshtml"
                           Write(k.SVC_NO);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 33 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionReview.cshtml"
                           Write(k.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>\n                                <a");
            BeginWriteAttribute("onclick", " onclick=\"", 1010, "\"", 1043, 3);
            WriteAttributeValue("", 1020, "getPension(", 1020, 11, true);
#nullable restore
#line 35 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionReview.cshtml"
WriteAttributeValue("", 1031, k.PersonID, 1031, 11, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1042, ")", 1042, 1, true);
            EndWriteAttribute();
            WriteLiteral("\n                                   class=\"btn btn-primary\">\n                                    Preview\n                                </a>\n                            </td>\n                        </tr>\n");
#nullable restore
#line 41 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionReview.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </tbody>\n        </table>\n\n");
            WriteLiteral("\n    </div>\n</div>\n\n\n\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script type=""text/javascript"">
        $(document).ready(function () {

            var currentTab = 0; // Current tab is set to be the first tab (0)
            showTab(currentTab);

        });

        function getPension(id) {
            $.ajax({
                method: ""GET"",
                url: `/api/Pension/GetPensionByPersonID?personID=${id}`,
                success: function (data) {
                    alert(""i am here"");
                    console.log(data);
                    console.log(""i am here"");
                    console.log(data.birthDate);
                    $('#pension_FirstName').val(data.firstName);
                    $('#pension_LastName').val(data.lastName);
                    $('#pension_Title').val(data.title);
                    $('#pension_SVC_NO').val(data.svC_NO);
                    $('#pension_MiddleName').val(data.middleName);
                    $('#pension_ShipEstab').val(data.shipEstab);
                    $('#pension_BirthDate').val(data.birthDate);
    ");
                WriteLiteral(@"                $('#pension_senioritydate').val(data.senioritydate);
                    $('#pension_Tradecategory').val(data.tradecategory);
                    $('#pension_TradeCatDate').val(data.tradeCatDate);
                    $('#pension_PrvEstab').val(data.prvEstab);
                    $('#pension_PrvShipEstab').val(data.prvShipEstab);
                    $('#pension_Enlistmentdate').val(data.enlistmentdate);
                    $('#pension_PreServicefrom').val(data.preServicefrom);
                    $('#pension_PreServiceTo').val(data.preServiceTo);
                    $('#pension_PrvServdate').val(data.prvServdate);
                    $('#pension_Warsrv_from').val(data.warsrv_from);
                    $('#pension_Warsrv_to').val(data.warsrv_to);
                    $('#pension_TotalService').val(data.totalService);
                    $('#pension_Nonrec_Service').val(data.nonrec_Service);
                    $('#pension_NonRecReason').val(data.nonRecReason);
                    $('#pension_Tran");
                WriteLiteral(@"sAuthority').val(data.transAuthority);
                    $('#pension_FinTotalService').val(data.finTotalService);
                    $('#pension_pensionDate').val(data.pensionDate);
                    $('#pension_PaymentDept').val(data.paymentDept);
                    $('#pension_PAddress').val(data.pAddress);
                    $('#pension_CAddress').val(data.cAddress);
                    $('#pension_DisabilityNat').val(data.disabilityNat);
                    $('#pension_DisabilityDegree').val(data.disabilityDegree);
                    $('#pension_DisabilityDate').val(data.disabilityDate);
                    $('#pension_AnnualBasic').val(data.annualBasic);
                    $('#pension_AnnualTransport').val(data.annualTransport);
                    $('#pension_AnnualLodging').val(data.annualLodging);
                    $('#pension_AnnualServant').val(data.annualServant);
                    $('#pension_AnnualMeal').val(data.annualMeal);
                    $('#pension_AnnualEnter').val(data.ann");
                WriteLiteral(@"ualEnter);
                    $('#pension_AnnualUtility').val(data.annualUtility);
                    $('#pension_AnnualHouse').val(data.annualHouse);
                    $('#pension_AnnualGeneral').val(data.annualGeneral);
                    $('#pension_AnnualMedical').val(data.annualMedical);
                    $('#pension_AnnualUtlity').val(data.annualUtlity);
                    $('#pension_AnnualUniform').val(data.annualUniform);
                    $('#pension_TotalEmolument').val(data.totalEmolument);
                    $('#pension_TotalQualify').val(data.totalQualify);
                    $('#pension_FreeSevcElement').val(data.freeSevcElement);
                    $('#pension_Housing_Loan').val(data.housing_Loan);
                    $('#pension_Motor_Loan').val(data.motor_Loan);
                    $('#pension_MCycle_Loan').val(data.mCycle_Loan);
                    $('#pension_Welfare_Loan').val(data.welfare_Loan);
                    $('#pension_Coop_Loan').val(data.coop_Loan);
               ");
                WriteLiteral(@"     $('#pension_MFinance_Loan').val(data.mFinance_Loan);
                    $('#pension_SalaryOverpayment').val(data.salaryOverpayment);
                    $('#pension_Other_Overcharge').val(data.other_Overcharge);

                    $('#exampleModal').modal('show');
                }
            });
        }

        function showTab(n) {
            // This function will display the specified tab of the form...
            var x = document.getElementsByClassName(""tab-pane"");

            console.log(n);
            x[n].style.display = ""block"";
            //... and fix the Previous/Next buttons:
            if (n == 0) {
                document.getElementById(""prevBtn"").style.display = ""none"";
            } else {
                document.getElementById(""prevBtn"").style.display = ""inline"";
            }
            if (n == (x.length - 1)) {
                document.getElementById(""nextBtn"").innerHTML = ""Submit"";
            } else {
                document.getElementById(""nextBtn"").innerHTML = ""Ne");
                WriteLiteral(@"xt"";
            }

            $('#currentTabId').val(n);

        }


        function nextPrev(n) {
            // This function will figure out which tab to display
            var x = document.getElementsByClassName(""tab-pane"");
            // Exit the function if any field in the current tab is invalid:
            //if (n == 1 && !validateForm()) return false;
            // Hide the current tab:
            var currentTab = Number($('#currentTabId').val());

            x[currentTab].style.display = ""none"";
            // Increase or decrease the current tab by 1:
            currentTab = currentTab + n;
            // if you have reached the end of the form...

            // Otherwise, display the correct tab:
            showTab(currentTab);

        }


    </script>
 ");
            }
            );
            WriteLiteral("\n\n\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NNPEFWEB.Models.PensionModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
