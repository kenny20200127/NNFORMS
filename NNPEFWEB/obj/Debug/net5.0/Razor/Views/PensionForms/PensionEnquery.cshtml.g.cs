#pragma checksum "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionEnquery.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "45d70f375b6661e1a8ff3b46cee19dad85eae296"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PensionForms_PensionEnquery), @"mvc.1.0.view", @"/Views/PensionForms/PensionEnquery.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"45d70f375b6661e1a8ff3b46cee19dad85eae296", @"/Views/PensionForms/PensionEnquery.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"846a9991e2c6a62c166d5415e65380119a26598e", @"/Views/_ViewImports.cshtml")]
    public class Views_PensionForms_PensionEnquery : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NNPEFWEB.Models.PensionViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "PensionEnquery", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionEnquery.cshtml"
  
    ViewData["Title"] = "PensionEnquery";
    Layout = "~/Views/Shared/_gratuityLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"card\" style=\"width:100%;\">\n    <div class=\"card-header\">\n        Pension Enquiry\n    </div>\n    <div class=\"card-body\">\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "45d70f375b6661e1a8ff3b46cee19dad85eae2964044", async() => {
                WriteLiteral(@"
            <div class=""input-group mb-3"">
                <input name=""svcNo"" type=""text"" class=""form-control"" placeholder=""search"">
                <div class=""input-group-append"">
                    <button class=""btn btn-outline-secondary"" id=""searchPension"" type=""button"">Search</button>
                </div>
            </div>

          
        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 23 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionEnquery.cshtml"
          
            if (Model != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <table class=""table table-striped"">
                    <thead>
                        <tr>
                            <th>
                                Service No
                            </th>
                            <th>
                                LastName
                            </th>
                            <th>
                                FirstName
                            </th>
                            <th>
                                MiddleName
                            </th>
                            <th>
                                Title
                            </th>
                            <th>
                                Status
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>");
#nullable restore
#line 51 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionEnquery.cshtml"
                           Write(Model.SVC_NO);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 52 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionEnquery.cshtml"
                           Write(Model.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 53 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionEnquery.cshtml"
                           Write(Model.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 54 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionEnquery.cshtml"
                           Write(Model.MiddleName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 55 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionEnquery.cshtml"
                           Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 56 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionEnquery.cshtml"
                           Write(Model.status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        </tr>\n                    </tbody>\n                </table>\n");
#nullable restore
#line 60 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\PensionForms\PensionEnquery.cshtml"
            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("       \n    </div>\n</div>\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\n    <script type=\"text/javascript\">\n        $(document).ready(function(){\n            $(\'#searchPension\').click(function () {\n                $(\"Form\").submit();\n            });\n        });\n       \n\n    </script>\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NNPEFWEB.Models.PensionViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
