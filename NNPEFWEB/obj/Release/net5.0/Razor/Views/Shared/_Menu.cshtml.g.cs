#pragma checksum "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "240b1e3ebc6a7b87a8d84f7c721882b9952be5e8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Menu), @"mvc.1.0.view", @"/Views/Shared/_Menu.cshtml")]
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
#line 1 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
using NNPEFWEB.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"240b1e3ebc6a7b87a8d84f7c721882b9952be5e8", @"/Views/Shared/_Menu.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"846a9991e2c6a62c166d5415e65380119a26598e", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Menu : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<User>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nav-link"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n\n");
            WriteLiteral("\n");
#nullable restore
#line 6 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
  
    var userMenus = new List<RoleMenu>();
  
    var counter = 0;
    foreach (var role in Model.UserRoles)
    {

        userMenus.AddRange(role.Role.RoleMenus);
    }

    var menuGroups = userMenus.GroupBy(
        x => x.Menu.MenuGroup,
        x => x.Menu,
        (menuGroup, menu) => new { Group = menuGroup, menus = menu.ToList() });

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<ul class=\"nav nav-pills nav-sidebar flex-column\" data-widget=\"treeview\" role=\"menu\" data-accordion=\"false\">\n");
#nullable restore
#line 23 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
     foreach (var group in menuGroups)
    {
        counter++;
        if (group.Group == null)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
             foreach (var menu in group.menus)
            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"nav-item has-treeview\">\n                    <a class=\"nav-link\"");
            BeginWriteAttribute("href", " href=\"", 755, "\"", 792, 4);
            WriteAttributeValue("", 762, "/", 762, 1, true);
#nullable restore
#line 32 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
WriteAttributeValue("", 763, menu.Controller, 763, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 779, "/", 779, 1, true);
#nullable restore
#line 32 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
WriteAttributeValue("", 780, menu.Action, 780, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n                        <i class=\"nav-icon far fa-adobe\"></i>\n                        <span class=\"align-middle\"> ");
#nullable restore
#line 34 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
                                               Write(menu.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span>\n                    </a>\n                </li>\n");
#nullable restore
#line 37 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"

            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
             
        }
        else
        {


#line default
#line hidden
#nullable disable
            WriteLiteral("<li class=\"nav-item has-treeview\">\n    <a href=\"javascript:void(0)\" class=\"nav-link\">\n        <i class=\"nav-icon far fa-adobe\"></i>\n        <p>\n            ");
#nullable restore
#line 47 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
       Write(group.Group.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            <i class=\"fas fa-angle-left right\"></i>\n        </p>\n\n    </a>\n    <ul class=\"nav nav-treeview\">\n");
#nullable restore
#line 53 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
             foreach (var menu in group.menus)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"nav-item\">\n            \n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "240b1e3ebc6a7b87a8d84f7c721882b9952be5e86992", async() => {
                WriteLiteral("\n                <i class=\"far fa-app-store nav-icon\"></i>\n                <p>");
#nullable restore
#line 59 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
              Write(menu.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 57 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
                   WriteLiteral(menu.Controller);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 57 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
                                                 WriteLiteral(menu.Action);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n        </li>");
#nullable restore
#line 61 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
             }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\n</li>\n");
#nullable restore
#line 64 "C:\Users\Home\source\repos\NNFORMS\NNPEFWEB\Views\Shared\_Menu.cshtml"
        }

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n</ul>\n\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<User> Html { get; private set; }
    }
}
#pragma warning restore 1591
