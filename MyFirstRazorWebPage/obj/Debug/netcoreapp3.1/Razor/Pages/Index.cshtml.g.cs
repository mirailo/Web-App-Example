#pragma checksum "C:\Users\nusay\source\repos\Web-App-Example\MyFirstRazorWebPage\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f0732d7fc2ca3aee0b289606fb43a27c9ba55ae6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(MyFirstRazorWebPage.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace MyFirstRazorWebPage.Pages
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
#line 1 "C:\Users\nusay\source\repos\Web-App-Example\MyFirstRazorWebPage\Pages\_ViewImports.cshtml"
using MyFirstRazorWebPage;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f0732d7fc2ca3aee0b289606fb43a27c9ba55ae6", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34e6902c8448f144381195ab923bf5252a34c2da", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/site.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 3 "C:\Users\nusay\source\repos\Web-App-Example\MyFirstRazorWebPage\Pages\Index.cshtml"
   ViewData["Title"] = "Index"; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<style>

    #map {
        width: 650px;
        height: 650px;
        background-color: grey;
        margin: auto;
    }
</style>

<div class=""hero-image"">
    <div class=""hero-text"">
        <h1 style=""font-size:50px"">Generic Hair Salon</h1>
        <h3>Welcome to our Appointment Booking System!</h3>
        <h4> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum hendrerit lorem ipsum, in dictum ligula faucibus a. Fusce at ligula et elit dapibus ultricies sit amet at elit. Praesent sed diam dolor. Duis ornare volutpat dignissim. Phasellus arcu turpis, convallis vitae lacus sit amet, vehicula suscipit quam. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin quis metus ipsum. Phasellus iaculis venenatis est ac ornare. Sed iaculis at magna vel blandit.</h4>
    </div>
</div>

");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f0732d7fc2ca3aee0b289606fb43a27c9ba55ae64360", async() => {
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
            WriteLiteral("\r\n<script type=\"text/javascript\"\r\n        src=\"https://maps.googleapis.com/maps/api/js?key=AIzaSyDNS6tsvRWNeCkHyvMc7I4gFkEsILPdLE4\"></script>\r\n\r\n\r\n\r\n<div>\r\n    <p");
            BeginWriteAttribute("asp-for", " asp-for=\"", 1154, "\"", 1178, 1);
#nullable restore
#line 31 "C:\Users\nusay\source\repos\Web-App-Example\MyFirstRazorWebPage\Pages\Index.cshtml"
WriteAttributeValue("", 1164, Model.Message, 1164, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"word-wrap: break-word; text-align: justify;\r\n            text-justify: inter-word; padding-left: 60px; padding-right: 60px \" class=\"control-label\">");
#nullable restore
#line 32 "C:\Users\nusay\source\repos\Web-App-Example\MyFirstRazorWebPage\Pages\Index.cshtml"
                                                                                                 Write(Model.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <br />\r\n\r\n  \r\n\r\n       \r\n    \r\n\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MyFirstRazorWebPage.Pages.IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<MyFirstRazorWebPage.Pages.IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<MyFirstRazorWebPage.Pages.IndexModel>)PageContext?.ViewData;
        public MyFirstRazorWebPage.Pages.IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
