#pragma checksum "C:\Users\Pero\Desktop\OzoMvc\Views\Posao\indexsimple.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6bbcdec625c17241d73fa5606c57298173e3f9c7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Posao_indexsimple), @"mvc.1.0.view", @"/Views/Posao/indexsimple.cshtml")]
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
#line 1 "C:\Users\Pero\Desktop\OzoMvc\Views\_ViewImports.cshtml"
using OzoMvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Pero\Desktop\OzoMvc\Views\_ViewImports.cshtml"
using OzoMvc.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Pero\Desktop\OzoMvc\Views\Posao\indexsimple.cshtml"
using OzoMvc.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6bbcdec625c17241d73fa5606c57298173e3f9c7", @"/Views/Posao/indexsimple.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"853571156c22dba85f43b2a635e593228deb132b", @"/Views/_ViewImports.cshtml")]
    public class Views_Posao_indexsimple : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OzoMvc.Models.Posao>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Pero\Desktop\OzoMvc\Views\Posao\indexsimple.cshtml"
  
    ViewData["Title"] = "Popis poslova";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<table class=""table table-sm table-striped table-hover"" id=""tabledrzave"">
    <thead>
        <tr>
            <th>id</th>
            <th>cijena</th>
            <th>usluga</th>
            <th>troskovi</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 18 "C:\Users\Pero\Desktop\OzoMvc\Views\Posao\indexsimple.cshtml"
         foreach (var poslovi in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td class=\"text-center\">");
#nullable restore
#line 21 "C:\Users\Pero\Desktop\OzoMvc\Views\Posao\indexsimple.cshtml"
                                   Write(poslovi.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"text-left\">");
#nullable restore
#line 22 "C:\Users\Pero\Desktop\OzoMvc\Views\Posao\indexsimple.cshtml"
                                 Write(poslovi.Cijena);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"text-center\">");
#nullable restore
#line 23 "C:\Users\Pero\Desktop\OzoMvc\Views\Posao\indexsimple.cshtml"
                                   Write(poslovi.UslugaId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"text-center\">");
#nullable restore
#line 24 "C:\Users\Pero\Desktop\OzoMvc\Views\Posao\indexsimple.cshtml"
                                   Write(poslovi.Troskovi);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 26 "C:\Users\Pero\Desktop\OzoMvc\Views\Posao\indexsimple.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OzoMvc.Models.Posao>> Html { get; private set; }
    }
}
#pragma warning restore 1591
