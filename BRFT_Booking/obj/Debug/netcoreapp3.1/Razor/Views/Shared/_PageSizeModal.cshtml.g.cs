#pragma checksum "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Shared\_PageSizeModal.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3baaddb725818cc025352a4595bd0f00632998fe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__PageSizeModal), @"mvc.1.0.view", @"/Views/Shared/_PageSizeModal.cshtml")]
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
using BRFT_Booking;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\_ViewImports.cshtml"
using BRFT_Booking.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3baaddb725818cc025352a4595bd0f00632998fe", @"/Views/Shared/_PageSizeModal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"91412d88fdc2a48c9fde2812c0af2f59c2e0a28a", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__PageSizeModal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""modal fade"" id=""pageSizeModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""pageSizeModalLabel"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"" id=""pageSizeModalLabel"">Set Page Size</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>
            </div>
            <div class=""modal-body"">
                <div class=""form-row"">
                    <div class=""form-group col-md-12"">
                        <div class=""input-group"">
                            <div class=""input-group-prepend"">
                                <span class=""input-group-text"">Page Size: </span>
                            </div>
                            ");
#nullable restore
#line 15 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Shared\_PageSizeModal.cshtml"
                       Write(Html.DropDownList("pageSizeID", null, htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            <div class=""input-group-append"">
                                <input type=""submit"" class=""btn btn-primary"" value=""Set Page Size"" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>");
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
