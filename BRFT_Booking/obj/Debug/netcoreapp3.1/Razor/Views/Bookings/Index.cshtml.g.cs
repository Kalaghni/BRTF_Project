#pragma checksum "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0a1a736e63f684ffda99607a28afe722a684d12e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Bookings_Index), @"mvc.1.0.view", @"/Views/Bookings/Index.cshtml")]
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
#line 1 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\_ViewImports.cshtml"
using BRTF_Booking;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\_ViewImports.cshtml"
using BRTF_Booking.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0a1a736e63f684ffda99607a28afe722a684d12e", @"/Views/Bookings/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"234b391ebdb51ff534b90ece12f59ac916ab18c6", @"/Views/_ViewImports.cshtml")]
    public class Views_Bookings_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BRTF_Booking.Models.Booking>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString(" background: #097dea; background: linear-gradient(65deg, #0270D7 0, #0F8AFD 100%); color: #fff !important; border: none; border-radius: 2px; "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DownloadBookings", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_PagingNavBar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Bookings</h1>\r\n\r\n<p>\r\n    <div class=\"form-horizontal\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0a1a736e63f684ffda99607a28afe722a684d12e7811", async() => {
                WriteLiteral("New Booking");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</p>\r\n");
#nullable restore
#line 14 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
 if (User.IsInRole("Top-Level Admin") || User.IsInRole("Admin"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0a1a736e63f684ffda99607a28afe722a684d12e9439", async() => {
                WriteLiteral("\r\n        <button");
                BeginWriteAttribute("class", " class=\"", 521, "\"", 557, 2);
                WriteAttributeValue("", 529, "btn", 529, 3, true);
#nullable restore
#line 17 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
WriteAttributeValue(" ", 532, ViewData["Filtering"], 533, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" type=""button"" data-toggle=""collapse"" id=""reportToggle"" data-target=""#collapsereport"" aria-expanded=""false"" aria-controls=""collapseFilter"" style="" background: #097dea; background: linear-gradient(65deg, #0270D7 0, #0F8AFD 100%); color: #fff !important; border: none; border-radius: 2px; "">
            Download Bookings Report
        </button>
        <div class=""collapse"" id=""collapsereport"">
            <div class=""card card-body bg-light"">
                <div class=""row"">
                    <div class=""form-group col-md-6"">
                        <label class=""control-label"">Set Start Date:</label>
                        <input type=""date"" id=""reportStart"" name=""reportStart"" class=""form-control"" />
                    </div>
                    <div class=""form-group col-md-6"">
                        <label class=""control-label"">Set End Date:</label>
                        <input type=""date"" id=""reportEnd"" name=""reportEnd"" class=""form-control"" />
                    </div>
              ");
                WriteLiteral("      <div class=\"form-group col-md-6\">\r\n                        <input type=\"submit\" value=\"Download\" class=\"btn btn-primary\" />\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 38 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0a1a736e63f684ffda99607a28afe722a684d12e12880", async() => {
                WriteLiteral("\r\n    <input type=\"hidden\" name=\"sortDirection\"");
                BeginWriteAttribute("value", " value=\"", 1902, "\"", 1936, 1);
#nullable restore
#line 40 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
WriteAttributeValue("", 1910, ViewData["sortDirection"], 1910, 26, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n    <input type=\"hidden\" name=\"sortField\"");
                BeginWriteAttribute("value", " value=\"", 1983, "\"", 2013, 1);
#nullable restore
#line 41 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
WriteAttributeValue("", 1991, ViewData["sortField"], 1991, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n    <div class=\"form-horizontal\">\r\n        <button");
                BeginWriteAttribute("class", " class=\"", 2069, "\"", 2105, 2);
                WriteAttributeValue("", 2077, "btn", 2077, 3, true);
#nullable restore
#line 43 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
WriteAttributeValue(" ", 2080, ViewData["Filtering"], 2081, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" style="" margin-top: 10px;"" type=""button"" data-toggle=""collapse"" id=""filterToggle"" data-target=""#collapseFilter"" aria-expanded=""false"" aria-controls=""collapseFilter"">
            Filter/Search
        </button>
        <div class=""collapse"" id=""collapseFilter"">
            <div class=""card card-body bg-light"">
                <div class=""row"">
                    <div class=""form-group col-md-4"">
                        <label class=""control-label"">Search Student:</label>
                        ");
#nullable restore
#line 51 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
                   Write(Html.TextBox("SearchName", null, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </div>\r\n                    <div class=\"form-group col-md-4\">\r\n                        <label class=\"control-label\">Search Room:</label>\r\n                        ");
#nullable restore
#line 55 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
                   Write(Html.TextBox("SearchRoom", null, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </div>\r\n                    <div class=\"form-group col-md-4\">\r\n                        <label class=\"control-label\">Search Area:</label>\r\n                        ");
#nullable restore
#line 59 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
                   Write(Html.TextBox("SearchArea", null, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                    </div>
                    <div class=""form-group col-md-4"">
                        <label class=""control-label"">Search By Date:</label>
                        <input type=""date"" id=""SearchDate"" name=""SearchDate"" class=""form-control"" />
                    </div>
                    <div class=""form-group col-md-4"">
                        <label class=""control-label"">Search Start Time:</label>
                        <input type=""time"" id=""SearchStartTime"" name=""SearchStartTime"" class=""form-control"" />
                    </div>
                    <div class=""form-group col-md-4 align-self-end"">
                        <input type=""submit"" name=""actionButton"" value=""Filter"" class=""btn btn-outline-primary"" />
                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0a1a736e63f684ffda99607a28afe722a684d12e17239", async() => {
                    WriteLiteral("Clear");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                    </div>
                </div>
            </div>
        </div>
    </div>
    <table class=""table"">
        <thead>
            <tr>
                <th>
                    <input type=""submit"" name=""actionButton"" value=""User"" class=""btn btn-link"" />
                </th>
                <th>
                    <input type=""submit"" name=""actionButton"" value=""Room"" class=""btn btn-link"" />
                </th>
                <th>
                    <input type=""submit"" name=""actionButton"" value=""Area"" class=""btn btn-link"" />
                </th>
                <th>
                    <input type=""submit"" name=""actionButton"" value=""Date Requested"" class=""btn btn-link"" />
                </th>
                <th>
                    <input type=""submit"" name=""actionButton"" value=""Start"" class=""btn btn-link"" />
                </th>
                <th>
                    <input type=""submit"" name=""actionButton"" value=""End"" class=""btn btn-link"" />
         ");
                WriteLiteral("       </th>\r\n                <th>\r\n                    <input type=\"submit\" name=\"actionButton\" value=\"Status\" class=\"btn btn-link\" />\r\n                </th>\r\n                <th></th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 105 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 109 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.User.FullName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 112 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Room.Name));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 115 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Room.Area.Name));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 118 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.BookingRequested));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n");
                WriteLiteral("                    ");
#nullable restore
#line 122 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.StartDate));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n");
                WriteLiteral("                    ");
#nullable restore
#line 126 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.EndDate));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <th>\r\n                    ");
#nullable restore
#line 129 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </th>\r\n                <td>\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0a1a736e63f684ffda99607a28afe722a684d12e22808", async() => {
                    WriteLiteral("Edit");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 132 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
                                           WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(" |\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0a1a736e63f684ffda99607a28afe722a684d12e25094", async() => {
                    WriteLiteral("Details");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 133 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
                                              WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(" |\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0a1a736e63f684ffda99607a28afe722a684d12e27386", async() => {
                    WriteLiteral("Delete");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_8.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 134 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
                                             WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 137 "C:\Users\thesa\OneDrive\Documents\GitHub\BRFT_Project\BRFT_Booking\Views\Bookings\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </tbody>\r\n    </table>\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0a1a736e63f684ffda99607a28afe722a684d12e29967", async() => {
                    WriteLiteral("Back to Home");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_9.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "0a1a736e63f684ffda99607a28afe722a684d12e31411", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_10.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_11.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BRTF_Booking.Models.Booking>> Html { get; private set; }
    }
}
#pragma warning restore 1591
