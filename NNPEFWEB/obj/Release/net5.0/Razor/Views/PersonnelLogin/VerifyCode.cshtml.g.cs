#pragma checksum "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonnelLogin\VerifyCode.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1ae40be6836949445a1310b50f1e3f73b7a0110a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PersonnelLogin_VerifyCode), @"mvc.1.0.view", @"/Views/PersonnelLogin/VerifyCode.cshtml")]
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
#nullable restore
#line 2 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonnelLogin\VerifyCode.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1ae40be6836949445a1310b50f1e3f73b7a0110a", @"/Views/PersonnelLogin/VerifyCode.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"846a9991e2c6a62c166d5415e65380119a26598e", @"/Views/_ViewImports.cshtml")]
    public class Views_PersonnelLogin_VerifyCode : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NNPEFWEB.ViewModel.PasswordCode>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("margin-left:150px;width:150px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Image/navyImage-removebg.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("-intro-x w-1/2 -mt-16"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "text", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("intro-x login__input input input--lg border border-gray-300 block"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("placeholder", new global::Microsoft.AspNetCore.Html.HtmlString("code"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("account"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonnelLogin\VerifyCode.cshtml"
  
    ViewData["Title"] = "Verify code";
    Layout = "~/Views/Shared/loginLayout.cshtml";


#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"hidden xl:flex flex-col min-h-screen\">\n    <a");
            BeginWriteAttribute("href", " href=\"", 225, "\"", 232, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"-intro-x flex items-center pt-5\">\n\n");
            WriteLiteral("    </a>\n    <div class=\"my-auto\">\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "1ae40be6836949445a1310b50f1e3f73b7a0110a7163", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            WriteLiteral("        <div class=\"-intro-x text-white font-medium text-4xl leading-tight mt-10\">\n            Nigerian Navy Form System\n\n        </div>\n");
            WriteLiteral("    </div>\n</div>\n<!-- END: Login Info -->\n<!-- BEGIN: Login Form -->\n<div class=\"h-screen xl:h-auto flex py-5 xl:py-0 my-10 xl:my-0\">\n");
#nullable restore
#line 28 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonnelLogin\VerifyCode.cshtml"
     if (@TempData["errorMessage"] != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div id=\"alert_div\" class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">\n            <h2 style=\"color:#ff0000\">  ");
#nullable restore
#line 31 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonnelLogin\VerifyCode.cshtml"
                                   Write(TempData["errorMessage"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\n            <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">\n                <span aria-hidden=\"true\">&times;</span>\n            </button>\n        </div>\n");
#nullable restore
#line 36 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonnelLogin\VerifyCode.cshtml"
    }
    

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""my-auto mx-auto xl:ml-20 bg-white xl:bg-transparent px-5 sm:px-8 py-8 xl:p-0 rounded-md shadow-md xl:shadow-none w-full sm:w-3/4 lg:w-2/4 xl:w-auto"">
        <h2 class=""intro-x font-bold text-2xl xl:text-3xl text-center xl:text-left"">
            Verify Code 
        </h2>
        <div class=""intro-x mt-2 text-gray-500 xl:hidden text-center"">A few more clicks to sign in to your account. Manage all your e-commerce accounts in one place</div>
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1ae40be6836949445a1310b50f1e3f73b7a0110a10270", async() => {
                WriteLiteral("\n");
#nullable restore
#line 44 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonnelLogin\VerifyCode.cshtml"
             if (@TempData["verifymessage"] != null)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <div id=\"alert_div\" class=\"alert alert-default-success alert-dismissible fade show\" role=\"alert\">\n                    <h2 style=\"color:#10912d\">  ");
#nullable restore
#line 47 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonnelLogin\VerifyCode.cshtml"
                                           Write(TempData["verifymessage"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h2>\n                    <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">\n                        <span aria-hidden=\"true\">&times;</span>\n                    </button>\n                </div>\n");
#nullable restore
#line 52 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonnelLogin\VerifyCode.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <div class=\"intro-x mt-8\">\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "1ae40be6836949445a1310b50f1e3f73b7a0110a11771", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#nullable restore
#line 54 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonnelLogin\VerifyCode.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Code);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("required", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n                <br />\n            </div>\n\n    \n            <div class=\"intro-x mt-5 xl:mt-8 text-center xl:text-left\">\n                <button class=\"button button--lg w-full xl:w-32 text-white bg-theme-1 xl:mr-3 align-top\">Verify Code</button>\n");
                WriteLiteral("            </div>\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n    </div>\n</div>\n\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NNPEFWEB.ViewModel.PasswordCode> Html { get; private set; }
    }
}
#pragma warning restore 1591
