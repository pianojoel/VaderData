#pragma checksum "C:\Users\joel_\source\repos\VaderData\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "894bfe311f5d71ff1bcd7dc83f645d312411bdc4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(VaderData.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace VaderData.Pages
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
#line 1 "C:\Users\joel_\source\repos\VaderData\Pages\_ViewImports.cshtml"
using VaderData;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"894bfe311f5d71ff1bcd7dc83f645d312411bdc4", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"687232ed49deb103328e1fd9c06a48e972bc5aa9", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\joel_\source\repos\VaderData\Pages\Index.cshtml"
  
    ViewData["Title"] = "Home page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">VäderData</h1>\r\n    <p>Inlämningsuppgift ITHS Jan 2021</p>\r\n    <p>Av Joel Lindberg</p>\r\n</div>\r\n\r\n<canvas id=\"myChart\" width=\"400\" height=\"200\"></canvas>\r\n<script>\r\n\r\n    \r\n    let labels = ");
#nullable restore
#line 17 "C:\Users\joel_\source\repos\VaderData\Pages\Index.cshtml"
            Write(Json.Serialize(Model.Labels));

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n    let temp = ");
#nullable restore
#line 18 "C:\Users\joel_\source\repos\VaderData\Pages\Index.cshtml"
          Write(Json.Serialize(Model.Temperature));

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n    let humidity = ");
#nullable restore
#line 19 "C:\Users\joel_\source\repos\VaderData\Pages\Index.cshtml"
              Write(Json.Serialize(Model.Humidity));

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n    let moldRisk = ");
#nullable restore
#line 20 "C:\Users\joel_\source\repos\VaderData\Pages\Index.cshtml"
              Write(Json.Serialize(Model.Moldrisk));

#line default
#line hidden
#nullable disable
            WriteLiteral(@";



   
var ctx = document.getElementById('myChart').getContext('2d');
var myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: labels,
        datasets: [
            {
            label: 'Utomhustemperatur',
                data: temp,
                
                borderColor: 'red',
            borderWidth: 1
            },
            {
                label: 'Luftfuktighet',
                data: humidity,
                borderWidth: 1,
                borderColor: 'green'
            },
            {
                label: 'Mögelrisk',
                data: moldRisk,
                borderWidth: 1,
                borderColor: 'black'
            }

        ]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
});
</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
