using AspNetCore.Reporting;
using Dto.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Dahboard.Controllers
{
    public class SalesReportController : Controller
    {
        // IWebHostEnvironment used with sample to get the application data from wwwroot.
        private IWebHostEnvironment _hostingEnvironment;

        // Post action to process the report from server based json parameters and send the result back to the client.
        public SalesReportController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SaleInvoice(int inv_number)
        {
            List<dtoSalesInvForShow> sale = new List<dtoSalesInvForShow>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri("https://localhost:44315/");
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Sales/ReadSalesInvDetailsForReport?sale_inv_id=" + inv_number);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    sale = Res.Content.ReadAsAsync<List<dtoSalesInvForShow>>().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                }
                string mimtype = "";
                int extension = 1;
                var path = $"{_hostingEnvironment.WebRootPath}\\Reports\\sales-order-detail.rdlc";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                LocalReport report = new LocalReport(path);
                report.AddDataSource("InvoiceSalesInfo", sale);
                List<dtoSalesDetailsForShow> lstDetails = new List<dtoSalesDetailsForShow>();
                for (int i = 0; i < sale.Count(); i++)
                {
                    for (int ii = 0; ii < sale[i].lstProducts.Count(); ii++)
                    {
                        lstDetails.Add(sale[i].lstProducts[ii]);
                    }
                }
                report.AddDataSource("InvoiceSalesDetails", lstDetails);

                var result = report.Execute(RenderType.Pdf, extension, parameters, mimtype);
                return File(result.MainStream, "application/pdf");
            }
        }
        public async Task<IActionResult> SalesInvoices(string url, string clientID, string store, string fromDate, string toDate, string fromCode, string toCode, string isDel)
        {
            List<dtoSalesInvForShow> sales = new List<dtoSalesInvForShow>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri("https://localhost:44315/");
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient

                // This is the important part:
                var query = new Dictionary<string, string>
                {
                    ["customer_id"] = clientID,
                    ["store_id"] = store,
                    ["from_date"] = fromDate,
                    ["to_date"] = toDate,
                    ["from_code"] = fromCode,
                    ["to_code"] = toCode,
                    ["is_deleted"] = isDel
                };
                HttpResponseMessage Res = await client.GetAsync(QueryHelpers.AddQueryString(url, query));
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var jsonString = await Res.Content.ReadAsStringAsync();
                    var results = JObject.Parse(jsonString).SelectToken("payload");
                    sales = results.ToObject<List<dtoSalesInvForShow>>();
                    //Deserializing the response recieved from web api and storing into the Employee list
                }
                string mimtype = "";
                int extension = 1;
                var path = $"{_hostingEnvironment.WebRootPath}\\Reports\\all-sales.rdlc";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                LocalReport report = new LocalReport(path);
                report.AddDataSource("SalesInvoiceInfo", sales);
                var result = report.Execute(RenderType.Pdf, extension, parameters, mimtype);
                return File(result.MainStream, "application/pdf");
            }
        }
    }
}
