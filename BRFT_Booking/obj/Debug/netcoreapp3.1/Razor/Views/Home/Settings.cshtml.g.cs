#pragma checksum "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Home\Settings.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e1efe2d4c6f62e784e3493a319cea2a808c329b7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Settings), @"mvc.1.0.view", @"/Views/Home/Settings.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e1efe2d4c6f62e784e3493a319cea2a808c329b7", @"/Views/Home/Settings.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"234b391ebdb51ff534b90ece12f59ac916ab18c6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Settings : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Home\Settings.cshtml"
  
    ViewData["Title"] = "Settings";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    .btn-primary {
        color: #fff;
        background-color: #1dc21d;
        border-color: #1aab1a;
    }

        .btn-primary:hover {
            background-color: #21db21;
            border-color: #1dc41d;
        }
</style>

<h1>Settings</h1>
<hr />
    <div class=""row"">
        <div class=""col-sm-6"">
            <table id=""settingTable""
                   class=""table table-bordered
                  table-condensed table-striped"">
                <thead>
                    <tr>
                        <th>Setting</th>
                        <th>Value</th>
                    </tr>
                    <tr>
                        <td>Office Hours Start</td>
                        <td>
                            <input type=""time"" id=""officeStart"" />
                        </td>
                    </tr>
                    <tr>
                        <td>Office Hours End</td>
                        <td>
                            <input type=""tim");
            WriteLiteral(@"e"" id=""officeEnd"" />
                        </td>
                    </tr>
                    <tr>
                        <td>Email Extension</td>
                        <td>
                            <input type=""text"" id=""emailExtension"" />
                        </td>
                    </tr>
                    <tr>
                        <td>Term Start Date</td>
                        <td>
                            <input type=""date"" id=""termStart"" />
                        </td>
                    </tr>
                    <tr>
                        <td>Term End Date</td>
                        <td>
                            <input type=""date"" id=""termEnd"" />
                        </td>
                    </tr>
                </thead>
            </table>
            <input type=""button"" id=""btnPost"" value=""Save Changes"" class=""btn btn-primary"" /><a href=""/"" style=""float: right"">Back to Home</a>
        </div>
    </div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 67 "C:\Users\Curtis\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Home\Settings.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
                WriteLiteral(@"    <script src=""//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js""></script>
    <script src=""//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js""></script>
    <script src=""//ajax.aspnetcdn.com/ajax/jquery.validate/1.11.1/jquery.validate.min.js""></script>
    <script src=""//ajax.aspnetcdn.com/ajax/mvc/4.0/jquery.validate.unobtrusive.min.js""></script>

    <script>
        $.getJSON('/Home/GetSettings', function (data) {
            $('#officeStart').val(data.officeStartHours);
            $('#officeEnd').val(data.officeEndHours);
            $('#emailExtension').val(data.emailExtension);
            $('#termStart').val(data.termStart);
            $('#termEnd').val(data.termEnd);
        });

    </script>
    <script>
        $(function () {
            $('#btnPost').click(function () {
                var alertStr = """";
                if ($('#officeStart').val() == """") {
                    alertStr += ""Please enter a start time for Office Hours\r\n"";
            ");
                WriteLiteral(@"    }
                if ($('#officeEnd').val() == """") {
                    alertStr += ""Please enter an end time for Office Hours\r\n"";
                }
                if ($('#emailExtension').val() == """") {
                    alertStr += ""Please enter an email extension\r\n"";
                }
                if ($('#termStart').val() == """") {
                    alertStr += ""Please enter a start date for the Term\r\n"";
                }
                if ($('#termEnd').val() == """") {
                    alertStr += ""Please enter an end date for the Term\r\n"";
                }
                if ($('#officeStart').val() == """" || $('#officeEnd').val() == """" || $('#emailExtension').val() == """" || $('#termStart').val() == """" || $('#termEnd').val() == """") {
                    alert(alertStr);
                }
                else {
                    console.log($('#officeStart').val());
                    data = {
                        ""OfficeStartHours"": $('#officeStart').val(),");
                WriteLiteral(@"
                        ""OfficeEndHours"": $('#officeEnd').val(),
                        ""EmailExtension"": $('#emailExtension').val(),
                        ""TermStart"": $('#termStart').val(),
                        ""TermEnd"": $('#termEnd').val()
                    }
                    $.post(""/Home/PostSettings/"",
                        data,
                        function (response) {
                            alert('Changes Saved')
                        });
                }
            });
        });
    </script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591