#pragma checksum "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\UserVM\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "86f3c3fbe89c75c85cc5688bf5c7717d01b3b1d7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserVM_Index), @"mvc.1.0.view", @"/Views/UserVM/Index.cshtml")]
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
#line 1 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\_ViewImports.cshtml"
using BRTF_Booking;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\_ViewImports.cshtml"
using BRTF_Booking.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"86f3c3fbe89c75c85cc5688bf5c7717d01b3b1d7", @"/Views/UserVM/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"234b391ebdb51ff534b90ece12f59ac916ab18c6", @"/Views/_ViewImports.cshtml")]
    public class Views_UserVM_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BRTF_Booking.ViewModels.UserVM>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\UserVM\Index.cshtml"
  
    ViewBag.Title = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>User Role Assignments</h2>\r\n\r\n<table class=\"table\">\r\n    <tr>\r\n        <th>\r\n            ");
#nullable restore
#line 12 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\UserVM\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th>\r\n            ");
#nullable restore
#line 15 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\UserVM\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.UserRoles));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th></th>\r\n    </tr>\r\n\r\n");
#nullable restore
#line 20 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\UserVM\Index.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 24 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\UserVM\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n");
#nullable restore
#line 27 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\UserVM\Index.cshtml"
                  
                    foreach (var r in item.UserRoles)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            WriteLiteral("  ");
#nullable restore
#line 30 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\UserVM\Index.cshtml"
                       Write(r);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <br />\r\n");
#nullable restore
#line 31 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\UserVM\Index.cshtml"
                    }
                

#line default
#line hidden
#nullable disable
            WriteLiteral("            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 35 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\UserVM\Index.cshtml"
           Write(Html.ActionLink("Edit", "Edit", new { id = item.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 38 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\UserVM\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BRTF_Booking.ViewModels.UserVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
