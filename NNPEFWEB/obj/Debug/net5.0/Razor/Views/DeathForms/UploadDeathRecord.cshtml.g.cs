#pragma checksum "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\DeathForms\UploadDeathRecord.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2460bc2ae7484341b417ce6211b881825a8eb225"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DeathForms_UploadDeathRecord), @"mvc.1.0.view", @"/Views/DeathForms/UploadDeathRecord.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2460bc2ae7484341b417ce6211b881825a8eb225", @"/Views/DeathForms/UploadDeathRecord.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"846a9991e2c6a62c166d5415e65380119a26598e", @"/Views/_ViewImports.cshtml")]
    public class Views_DeathForms_UploadDeathRecord : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\DeathForms\UploadDeathRecord.cshtml"
  
    ViewData["Title"] = "UploadDeathRecord";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"card\" style=\"width:100%;\">\n    <div class=\"card-header\">\n        Upload of Death Gratuity form\n    </div>\n    <div class=\"card-body\">\n");
#nullable restore
#line 12 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\DeathForms\UploadDeathRecord.cshtml"
          
            if (TempData["messageDExcel"] != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"alert alert-default-success\">\n                    ");
#nullable restore
#line 16 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\DeathForms\UploadDeathRecord.cshtml"
               Write(TempData["messageDExcel"].ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral(";\n                </div>\n");
#nullable restore
#line 18 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\DeathForms\UploadDeathRecord.cshtml"
            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n        <a");
            BeginWriteAttribute("href", " href=\"", 501, "\"", 549, 1);
#nullable restore
#line 22 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\DeathForms\UploadDeathRecord.cshtml"
WriteAttributeValue("", 508, Url.Action("DownloadCForm","DeathForms"), 508, 41, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Download Sample</a>\n\n");
#nullable restore
#line 24 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\DeathForms\UploadDeathRecord.cshtml"
         using (Html.BeginForm("UploadDeathRecord", "DeathForms", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\DeathForms\UploadDeathRecord.cshtml"
       Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""row"">
                <div class=""col-md-4"">
                    <div class=""form-group"">
                        <input type=""file"" name=""formFile"" class=""form-control"" required />
                    </div>
                </div>
                <div class=""col-md-1"">
                    <div class=""form-group"">
                        <input type=""submit"" name=""Submit"" id=""Submit"" class=""btn btn-primary"" value=""Upload"" />
                    </div>
                </div>
            </div>
");
#nullable restore
#line 40 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\DeathForms\UploadDeathRecord.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n    </div>\n</div>\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
