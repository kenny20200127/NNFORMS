#pragma checksum "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Shared\loginLayout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "40c589ab2c6d76a7b652e9b9a19d902f74227a2e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_loginLayout), @"mvc.1.0.view", @"/Views/Shared/loginLayout.cshtml")]
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
#line 2 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Shared\loginLayout.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"40c589ab2c6d76a7b652e9b9a19d902f74227a2e", @"/Views/Shared/loginLayout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"846a9991e2c6a62c166d5415e65380119a26598e", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_loginLayout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NNPEFWEB.ViewModel.personLoginVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("login"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Shared\loginLayout.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<!DOCTYPE html>
<!--
Template Name: Midone - HTML Admin Dashboard Template
Author: Left4code
Website: http://www.left4code.com/
Contact: muhammadrizki@left4code.com
Purchase: https://themeforest.net/user/left4code/portfolio
Renew Support: https://themeforest.net/user/left4code/portfolio
License: You must have a valid license purchased only from themeforest(the above link) in order to legally use the theme for your project.
-->
<html lang=""en"" class=""light"">
<!-- BEGIN: Head -->
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "40c589ab2c6d76a7b652e9b9a19d902f74227a2e4477", async() => {
                WriteLiteral(@"
    <meta charset=""utf-8"">
    <link href=""dist2/images/logo.svg"" rel=""shortcut icon"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <meta name=""description"" content=""Midone admin is super flexible, powerful, clean & modern responsive tailwind admin template with unlimited possibilities."">
    <meta name=""keywords"" content=""admin template, Midone admin template, dashboard template, flat admin template, responsive admin template, web app"">
    <meta name=""author"" content=""LEFT4CODE"">
    <title>Login - NNFORMS</title>
    <!-- BEGIN: CSS Assets-->
    <link href=""../dist2/css/app.css"" rel=""stylesheet"" />
    <!-- END: CSS Assets-->
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
            WriteLiteral("\n<!-- END: Head -->\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "40c589ab2c6d76a7b652e9b9a19d902f74227a2e6155", async() => {
                WriteLiteral("\n    <div class=\"container sm:px-10\">\n        <div class=\"block xl:grid grid-cols-2 gap-4\">\n           ");
#nullable restore
#line 34 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Shared\loginLayout.cshtml"
      Write(RenderBody());

#line default
#line hidden
#nullable disable
                WriteLiteral("\n        </div>\n    </div>\n\n    <!-- BEGIN: JS Assets-->\n    <script src=\"../dist2/js/app.js\"></script>\n    <div class=\"alert-default-warning\">\n");
#nullable restore
#line 41 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Shared\loginLayout.cshtml"
         if (@Context.Session.GetString("InvalidPassword") != null)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Shared\loginLayout.cshtml"
       Write(Context.Session.GetString("InvalidPassword"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Shared\loginLayout.cshtml"
                                                         ;

#line default
#line hidden
#nullable disable
                WriteLiteral("            <script>\n                alert(\"Invalid User Name or Password\");\n            </script>\n");
#nullable restore
#line 47 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Shared\loginLayout.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"    </div>
    <script>
        $('.alert').alert()
        $(function () {
            $("".btn"").click(function () {
                $("".form-signin"").toggleClass(""form-signin-left"");
                $("".form-signup"").toggleClass(""form-signup-left"");
                $("".frame"").toggleClass(""frame-long"");
                $("".signup-inactive"").toggleClass(""signup-active"");
                $("".signin-active"").toggleClass(""signin-inactive"");
                $("".forgot"").toggleClass(""forgot-left"");
                $(this).removeClass(""idle"").addClass(""active"");
            });
        });

        $(function () {
            $("".btn-signup"").click(function () {
                $("".nav"").toggleClass(""nav-up"");
                $("".form-signup-left"").toggleClass(""form-signup-down"");
                $("".success"").toggleClass(""success-left"");
                $("".frame"").toggleClass(""frame-short"");
            });
        });

        $(function () {
            $("".btn-signin"").click(function () {
                $("".");
                WriteLiteral(@"btn-animate"").toggleClass(""btn-animate-grow"");
                $("".welcome"").toggleClass(""welcome-left"");
                $("".cover-photo"").toggleClass(""cover-photo-down"");
                $("".frame"").toggleClass(""frame-short"");
                $("".profile-photo"").toggleClass(""profile-photo-down"");
                $("".btn-goback"").toggleClass(""btn-goback-up"");
                $("".forgot"").toggleClass(""forgot-fade"");
            });
        });

    </script>
    <script>
        $("".toggle-password"").click(function () {

            $(this).toggleClass(""fa-eye fa-eye-slash"");
            var input = $($(this).attr(""toggle""));
            if (input.attr(""type"") == ""password"") {
                input.attr(""type"", ""text"");
            } else {
                input.attr(""type"", ""password"");
            }
        });
    </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
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
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NNPEFWEB.ViewModel.personLoginVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
