#pragma checksum "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b8a13ff925cfd926ba4ea3593c79b22264a20a5a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reports_TrainingReport2), @"mvc.1.0.view", @"/Views/Reports/TrainingReport2.cshtml")]
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
#line 1 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\_ViewImports.cshtml"
using NNPEFWEB;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\_ViewImports.cshtml"
using NNPEFWEB.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b8a13ff925cfd926ba4ea3593c79b22264a20a5a", @"/Views/Reports/TrainingReport2.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"846a9991e2c6a62c166d5415e65380119a26598e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Reports_TrainingReport2 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NNPEFWEB.ViewModel.PersonalInfoModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/styles.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/site.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/datepickeModel.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("background-color: #66edf0;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
  
    ViewData["Title"] = "TrainingReport";
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<!DOCTYPE html>\n<html>\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b8a13ff925cfd926ba4ea3593c79b22264a20a5a5571", async() => {
                WriteLiteral("\n\n    <meta charset=\"utf-8\" />\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1, shrink-to-fit=no\" />\n    <meta name=\"description\"");
                BeginWriteAttribute("content", " content=\"", 293, "\"", 303, 0);
                EndWriteAttribute();
                WriteLiteral(" />\n    <meta name=\"author\"");
                BeginWriteAttribute("content", " content=\"", 331, "\"", 341, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
    <title>NN-CPO</title>
    <link rel=""icon"" type=""image/x-icon"" href=""assets/favicon.ico"" />

    <!-- Font Awesome icons (free version)-->
    <script src=""https://use.fontawesome.com/releases/v5.15.3/js/all.js"" crossorigin=""anonymous""></script>
    <!-- Google fonts-->
    <link href=""https://fonts.googleapis.com/css?family=Montserrat:400,700"" rel=""stylesheet"" type=""text/css"" />
    <link href=""https://fonts.googleapis.com/css?family=Roboto+Slab:400,100,300,700"" rel=""stylesheet"" type=""text/css"" />
    <!-- Core theme CSS (includes Bootstrap)-->
    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b8a13ff925cfd926ba4ea3593c79b22264a20a5a6918", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b8a13ff925cfd926ba4ea3593c79b22264a20a5a8094", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b8a13ff925cfd926ba4ea3593c79b22264a20a5a9270", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
    <style type=""text/css"">
        .waterMark {
            text-align: center;
            position: absolute;
            width: 100%;
            height: 100%;
            z-index: -9999;
            pointer-event: none;
            font-size: 5em;
            font-weight: bold;
            font-family: Consolas;
            color: #EDEDED;
            opacity: 0.1;
        }

        .waterMark-inner {
            transform: rotate(-20deg);
            line-height: 200px
        }
        .waterMark5 {
            text-align: center;
            position: absolute;
            width: 100%;
            height: 100%;
            z-index: -9999;
            pointer-event: none;
            font-size: 5em;
            font-weight: bold;
            font-family: Consolas;
            color: #EDEDED;
            opacity: 1;
        }

        .waterMark5-inner {
            transform: rotate(-20deg);
            line-height: 200px
        }

        .waterMark2 {
            text-align: center;
            po");
                WriteLiteral(@"sition: absolute;
            width: 100%;
            height: 100%;
            z-index: -9999;
            pointer-event: none;
            font-size: 5em;
            font-weight: bold;
            font-family: Consolas;
            color: #EDEDED;
            /*opacity: 0.1;*/
        }

        .waterMark-inner2 {
            transform: rotate(-20deg);
            line-height: 200px
        }
        .centered {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }
    </style>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b8a13ff925cfd926ba4ea3593c79b22264a20a5a12778", async() => {
                WriteLiteral("\n    <div class=\"centered\">\n        <div class=\"waterMark2\">\n            <div class=\"waterMark-inner2\" style=\"margin-left: -120px; margin-top:-180px;\"><h1>");
#nullable restore
#line 96 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                         Write(DateTime.Now.Year);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h1></div>\n        </div>\n    </div>\n    <div class=\"waterMark\">\n        <div class=\"waterMark-inner\">\n            <img id=\"nokpassport\" style=\"width: 840px; height: 1110px; margin-top:350px; object-fit: cover\"");
                BeginWriteAttribute("src", " src=\"", 3094, "\"", 3157, 2);
                WriteAttributeValue("", 3100, "data:image/*;base64,", 3100, 20, true);
#nullable restore
#line 101 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
WriteAttributeValue("", 3120, Convert.ToBase64String(Model.logo), 3120, 37, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\n\n        </div>\n\n    </div>\n");
                WriteLiteral("<div id=\"btnprint\" style=\"margin-left:50px\">\n\n\n    <div style=\"text-align:center\" class=\"row\"><div class=\"col-md-4\"> <h2>RESTRICTED </h2></div> </div>\n");
#nullable restore
#line 112 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
     if (Model.AltNokPassport != null)
    {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"        <label style=""margin-left:1050px; margin-top: -18px""><strong>Alternate NOK Passport</strong></label>
        <div class=""waterMark5"">
            <div class=""waterMark5-inner"">
                <img id=""nokpassport"" style=""width: 250px; height: 250px; margin-top:10px; float:right; margin-right: 100px""");
                BeginWriteAttribute("src", " src=\"", 3895, "\"", 3968, 2);
                WriteAttributeValue("", 3901, "data:image/*;base64,", 3901, 20, true);
#nullable restore
#line 117 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
WriteAttributeValue("", 3921, Convert.ToBase64String(Model.AltNokPassport), 3921, 47, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\n            </div>\n        </div>\n");
#nullable restore
#line 120 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
    }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"    <div class=""row"">
        <h2>b. <u>Alternate Next of Kin <em>(Attach Passport Photographs)</em></u></h2>
    </div>
    <div style=""margin-left:50px"">
        <div class=""row"">
            <div class=""col-md-8""><h2><span>(1)&nbsp;&nbsp;&nbsp;Name :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 126 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                                Write(Model.nok_name2);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </strong> </h2></div>\n        </div>\n        <div class=\"row\">\n            <div class=\"col-md-8\"><h2><span>(2)&nbsp;&nbsp;&nbsp;Relationship :&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 129 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                                  Write(Model.nok_relation2);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </strong> </h2></div>\n        </div>\n        <div class=\"row\">\n            <div class=\"col-md-8\"><h2><span>(3)&nbsp;&nbsp;&nbsp;Address :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 132 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                                   Write(Model.nok_address2);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </strong> </h2></div>\n        </div>\n        <div class=\"row\">\n            <div class=\"col-md-8\"><h3><span>(4)&nbsp;&nbsp;&nbsp;GSM No (a) :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 135 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                                      Write(Model.nok_phone2);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </strong>&nbsp;&nbsp;&nbsp;<span>(b)&nbsp;&nbsp;&nbsp;&nbsp;</span><strong>");
#nullable restore
#line 135 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                                                                                                                                   Write(Model.nok_phone22);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </strong> </h3></div>\n        </div>\n        <div class=\"row\">\n            <div class=\"col-md-8\"><h2><span>(5)&nbsp;&nbsp;&nbsp;E-mail :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 138 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                                  Write(Model.nok_email2);

#line default
#line hidden
#nullable disable
                WriteLiteral("</strong> </h2></div>\n        </div>\n        <div class=\"row\">\n            <div class=\"col-md-8\"><h2><span>(6)&nbsp;&nbsp;&nbsp;NIN of Alternate NOK:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 141 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                                                                       Write(Model.nok_nationalId2);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</strong> </h2></div>
        </div>

    </div>
    <div class=""row"">
        <h2>13. Bank Account Details:</h2>
    </div>
    <div style=""margin-left:50px"">
        <div class=""row"">
            <div class=""col-md-8""><h2><span>a. Salary Bank Account Name :&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 150 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                            Write(Model.AccountName);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </strong> </h2></div>\n        </div>\n        <div class=\"row\">\n            <div class=\"col-md-8\"><h2><span>b. Name of Bank :&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 153 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                Write(Model.Bankcode);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </strong> </h2></div>\n        </div>\n        <div class=\"row\">\n            <div class=\"col-md-8\"><h2><span>c. Branch/Address: &nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 156 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                  Write(Model.bankbranch);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </strong> </h2></div>\n        </div>\n        <div class=\"row\">\n            <div class=\"col-md-8\"><h2><span>d. Account Number:&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 159 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                 Write(Model.BankACNumber);

#line default
#line hidden
#nullable disable
                WriteLiteral(@" </strong> </h2></div>
        </div>


    </div>

</div>
    <div class=""row"" style=""margin-left:50px""><div class=""col-md-10""><h1><i>I undertake that the information provided by me above are correct to the best of my knowledge.</i></h1></div></div>
    <br />
    <div style=""margin-left:50px"">

        <div class=""row"">

            <div class=""row"">
                <div style=""text-align: center; width: 400px"">
                    <hr width=""300"" style=""border:1px dashed; margin-left:200px"" size=""4""> <hr width=""300"" style=""border:1px dashed;margin-left:900px"" size=""4"">
                </div>
                <div class=""row"" style=""margin-top:-10px"">
                    <h2><strong style=""margin-left:300px""><i>Date</i></strong><strong style=""margin-left:600px""><i> Trainee's Signature</i></strong></h2>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div style=""margin-top: -10px; margin-left: 40px;height:50px; padding: 3px; width: 1300px; border: 3px solid #126ea4; text-ali");
                WriteLiteral(@"gn: center "">

        <h2 style=""color: #126ea4; margin-top:10px""> NOTE: CERTIFICATION OF FALSE INFORMATION ATTRACTS SEVERE PUNISHMENT</h2>
    </div>
    <br /><br /><br />
    <div style=""margin-left:100px; margin-top:-10px"">
        <div class=""row"" style=""margin-top:-10px"">
            <div class=""col-md-8"">
                <h1><u>SECTION B</u> &nbsp;&nbsp;&nbsp;(To be completed by TRAINING OFFICER in capital letters)</h1>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-md-8""><h2><span>Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 195 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                                  Write(Model.div_off_name);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </strong> </h2></div>\n        </div>\n        <div class=\"row\">\n            <div class=\"col-md-12\">\n                <h2>\n                    <span>Rank:&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 200 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                            Write(Model.div_off_rank);

#line default
#line hidden
#nullable disable
                WriteLiteral("</strong> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style=\"margin-left:400px\"> Svc Number :&nbsp;&nbsp;&nbsp; <strong> ");
#nullable restore
#line 200 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                                                                                                                                                           Write(Model.div_off_svcno);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</strong></span>
                </h2>
            </div>
        </div>
    </div>
    <div class=""row"" style=""margin-top:-15px"">
        <div class=""row"" style=""margin-left:50px"">
            <div class=""col-md-12"">
                <h1><strong><i>I have verified the information provided by the Trainee and found them to be correct to the best of my knowledge.</i></strong></h1>
            </div>
        </div>
        <br />
        <div class=""row"">

            <div style=""text-align: center; width: 400px"">
                <hr width=""300"" style=""border:1px dashed; margin-left:200px"" size=""4""> <hr width=""300"" style=""border:1px dashed;margin-left:900px"" size=""4"">
            </div>
            <div style=""margin-left: 300px; margin-top: -15px"">
                <h2><strong>Date</strong><strong style=""margin-left:550px"">Signature of Training Officer</strong></h2>
            </div>


        </div>

    </div>
    <br /><br /><br />
    <div style=""margin-left:100px; margin-top:-15px"">
        <div class=""row""");
                WriteLiteral(@" style=""margin-top:-10px"">
            <div class=""col-md-8"">
                <h1><u>SECTION C</u>&nbsp;&nbsp;&nbsp;(To be completed by COMMANDING OFFICER in capital letters)</h1>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-md-8""><h2><span>Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 233 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                                  Write(Model.cdr_name);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </strong> </h2></div>\n        </div>\n        <div class=\"row\">\n            <div class=\"col-md-12\">\n                <h2>\n                    <span>Rank:&nbsp;&nbsp;&nbsp; </span><strong>");
#nullable restore
#line 238 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                            Write(Model.cdr_rank);

#line default
#line hidden
#nullable disable
                WriteLiteral("</strong> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style=\"margin-left:400px\"> Svc Number :&nbsp;&nbsp;&nbsp; <strong> ");
#nullable restore
#line 238 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\TrainingReport2.cshtml"
                                                                                                                                                                                                                                       Write(Model.cdr_svcno);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</strong></span>
                </h2>
            </div>
        </div>
    </div>
    <div class=""row"" style=""margin-top:-10px"">
        <div class=""row"" style=""margin-left:50px"">
            <div class=""col-md-12"">
                <h1><strong><i>I certify that the information provided by the Trainee are correct to the best of my knowledge.</i></strong></h1>
            </div>
        </div>
        <br />
        <div class=""row"">
            <div style=""text-align: center; width: 400px"">
                <hr width=""300"" style=""border:1px dashed; margin-left:200px"" size=""4""> <hr width=""300"" style=""border:1px dashed;margin-left:900px"" size=""4"">
            </div>
            <div style=""margin-left: 300px; margin-top: -15px"">
                <h2><strong><i>Date</i></strong><strong style=""margin-left:500px""><i>Commanding Officer's' Signature and Stamp</i></strong></h2>
            </div>


        </div>

    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />


    <div style=");
                WriteLiteral("\"text-align:center;margin-top:-8px\" class=\"row\"><div class=\"col-md-4\"> <h2>C-2</h2></div> </div>\n\n    <div style=\"text-align:center;margin-top:-8px\" class=\"row\"><div class=\"col-md-4\"> <h2>RESTRICTED </h2></div> </div>\n\n    </div>\n\n");
                WriteLiteral(@"    <script>
        var canvas = document.getElementById('DemoCanvas');
        //Always check for properties and methods, to make sure your code doesn't break in other browsers.
        if (canvas.getContext) {
            var context = canvas.getContext('2d');
            // Reset the current path
            context.beginPath();
            // Staring point (10,45)
            context.moveTo(10, 45);
            // End point (180,47)
            context.lineTo(180, 47);
            // Make the line visible
            context.stroke();

    </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</html>\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NNPEFWEB.ViewModel.PersonalInfoModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
