#pragma checksum "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonalInfo\CommisionedPersonnelUpload.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5a8ba64509747d74dfbf1cca3cb0dbe3c3fec7eb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PersonalInfo_CommisionedPersonnelUpload), @"mvc.1.0.view", @"/Views/PersonalInfo/CommisionedPersonnelUpload.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5a8ba64509747d74dfbf1cca3cb0dbe3c3fec7eb", @"/Views/PersonalInfo/CommisionedPersonnelUpload.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"846a9991e2c6a62c166d5415e65380119a26598e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_PersonalInfo_CommisionedPersonnelUpload : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/UPLOAD/CommisionedPersonnelUpload.xlsx"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonalInfo\CommisionedPersonnelUpload.cshtml"
  
    ViewData["Title"] = "Commissioned Personnel Upload";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section class=""content"">
    <div class=""container-fluid"">
        <!-- SELECT2 EXAMPLE -->
        <div class=""card card-default"">
            <div class=""card-header"">

                <div class=""row"">
                    <div class=""col-12"">
                        <div class=""card"">
                            <div class=""card-header"">
                                <h5 class=""card-title""> Personel Update</h5>
                            </div>

                            ");
#nullable restore
#line 19 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonalInfo\CommisionedPersonnelUpload.cshtml"
                       Write(TempData["message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                            <div class=\"card-body\">\n\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5a8ba64509747d74dfbf1cca3cb0dbe3c3fec7eb4786", async() => {
                WriteLiteral("Download Sample");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                                <hr />\n");
#nullable restore
#line 24 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonalInfo\CommisionedPersonnelUpload.cshtml"
                                 using (Html.BeginForm("CommisionedPersonnelUpload", "PersonalInfo", FormMethod.Post, new { enctype = "multipart/form-data" }))
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                    <div class=""row"">
                                        <div class=""col-md-4"">
                                            <input type=""file"" name=""formFile"" class=""form-control"" required />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class=""row"">
                                        <div class=""col-md-4"">
                                            <input type=""submit"" name=""Submit"" id=""Submit"" class=""btn btn-primary"" value=""Upload"" />
                                        </div>
                                    </div>
");
#nullable restore
#line 37 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\PersonalInfo\CommisionedPersonnelUpload.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                            </div>\n\n\n\n                        </div>\n                    </div>\n                </div>\n\n            </div>\n        </div>\n    </div>\n</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
