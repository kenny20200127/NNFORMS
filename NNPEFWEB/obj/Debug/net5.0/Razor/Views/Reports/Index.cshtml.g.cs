#pragma checksum "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b0be41db706bb409ae59a1b2c7a63a5eae3de464"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reports_Index), @"mvc.1.0.view", @"/Views/Reports/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b0be41db706bb409ae59a1b2c7a63a5eae3de464", @"/Views/Reports/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"846a9991e2c6a62c166d5415e65380119a26598e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Reports_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/assets/mysems2bootstrap.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/mysemsjquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "C:\Users\Home\Downloads\navyproject-master\NNPEFWEB\Views\Reports\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>Reports</h1>\n<section class=\"content\">\n\n    <report></report>\n   \n</section>\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b0be41db706bb409ae59a1b2c7a63a5eae3de4644197", async() => {
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
            WriteLiteral("-->\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b0be41db706bb409ae59a1b2c7a63a5eae3de4645237", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
<script src=""https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js""></script>
<script>window.jQuery || document.write('<script src=""assets/jquery-1.11.2.min.js""><\/script>')</script>
<script>
    $(document).ready(function () {
        ShowAllReservation();

        function ShowAllReservation() {
            $(""table tbody"").html("""");
            $.ajax({
                url: ""https://localhost:44395/api/Reports/getListPerson"",
                type: ""get"",
                contentType: ""application/json"",
                success: function (result, status, xhr) {
                    $.each(result, function (index, value) {
                        $(""tbody"").append($(""<tr>""));
                        appendElement = $(""tbody tr"").last();
                        appendElement.append($(""<td>"").html(value[""serviceNumber""]));
                        appendElement.append($(""<td>"").html(value[""surname""]));
                        appendElement.append($(""<td>"").html(value[""otherName""]));
                 ");
            WriteLiteral(@"       appendElement.append($(""<td>"").html(value[""ship""]));
                        //appendElement.append($(""<td>"").html("" < a href =\""UpdateReservation.html?id ="" + value["" id""] + ""\""><img src=\""icon/edit.png\"" /></a>""));
                        //appendElement.append($(""<td>"").html(""<img class=\""delete\"" src=\""icon/close.png\"" />""));
                    });
                },
                error: function (xhr, status, error) {
                    console.log(xhr)
                }
            });
        }

        $(""#UpdateButton"").click(function (e) {
            let params = (new URL(document.location)).searchParams;
            let id = params.get(""id"");

            data = new FormData();
            data.append(""Id"", $(""#Id"").val());
            data.append(""Name"", $(""#Name"").val());
            data.append(""StartLocation"", $(""#StartLocation"").val());
            data.append(""EndLocation"", $(""#EndLocation"").val());

            $.ajax({
                url: ""https://localhost:44324/api/Reservatio");
            WriteLiteral(@"n"",
                type: ""put"",
                data: data,
                processData: false,
                contentType: false,
                success: function (result, status, xhr) {
                    var str = ""<tr><td>"" + result[""id""] + ""</td><td>"" + result[""name""] + ""</td><td>"" + result[""startLocation""] + ""</td><td>"" + result[
                        $(""table tbody"").append(str);
                    $(""#resultDiv"").show();
                },
                error: function (xhr, status, error) {
                    console.log(xhr)
                }
            });
        });
    });

        });
</script>
<!--<script src=""~/Content/assets/mysems2bootstrap.min.js""></script>-->
");
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