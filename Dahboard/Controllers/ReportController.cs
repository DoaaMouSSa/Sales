using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using BoldReports.Web.ReportViewer;
using Microsoft.AspNetCore.Hosting;
using AspNetCore.Reporting;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Dto.Dto;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.WebUtilities;

namespace Dahboard.Controllers
{
    public class ReportController : Controller
    {
        // IWebHostEnvironment used with sample to get the application data from wwwroot.
        private IWebHostEnvironment _hostingEnvironment;

        // Post action to process the report from server based json parameters and send the result back to the client.
        public ReportController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        } 
        ///test for showing data in report///
        public async Task<IActionResult> index(string url,string sup,string store,string from,string to)
        {
            List<dtoPurchaseInvForShow> purchase = new List<dtoPurchaseInvForShow>();
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
                    ["supplier_id"] = sup,
                    ["store_id"] = store,
                    ["from_date"] = from,
                    ["to_date"] = to,
                };
                HttpResponseMessage Res = await client.GetAsync(QueryHelpers.AddQueryString(url, query));
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var jsonString = await Res.Content.ReadAsStringAsync();
                    var results = JObject.Parse(jsonString).SelectToken("payload");
                    purchase = results.ToObject<List<dtoPurchaseInvForShow>>();
                    //Deserializing the response recieved from web api and storing into the Employee list
                }
              
            }
            return View(purchase);
        }
        public async Task<IActionResult> PurchaseInvoice(int inv_number)
        {
            List<dtoPurchaseInvForShow> purchase = new List<dtoPurchaseInvForShow>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri("https://localhost:44315/");
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Purchase/GetCustomPurchaseInvoice?pur_inv_id=" + inv_number);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var jsonString = await Res.Content.ReadAsStringAsync();
                    var results = JObject.Parse(jsonString).SelectToken("payload");
                    purchase = results.ToObject<List<dtoPurchaseInvForShow>>();
                    //Deserializing the response recieved from web api and storing into the Employee list
                }
                string mimtype = "";
                int extension = 1;
                var path = $"{_hostingEnvironment.WebRootPath}\\Reports\\purchase-order-detail.rdlc";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                LocalReport report = new LocalReport(path);
                report.AddDataSource("PurchaseInvoiceInfo", purchase);

                List<dtoPurchaseDetialsForShow> lstDetails = new List<dtoPurchaseDetialsForShow>();
                for (int i = 0; i < purchase.Count(); i++)
                {
                    for (int ii = 0; ii < purchase[i].lstProducts.Count(); ii++)
                    {
                        lstDetails.Add(purchase[i].lstProducts[ii]);
                    }
                }
                report.AddDataSource("PurchaseInvoiceDetails", lstDetails);
                var result = report.Execute(RenderType.Pdf, extension, parameters, mimtype);
                return File(result.MainStream, "application/pdf");
            }
        }
        public async Task<IActionResult> PurchaseInvoices(string url, string sup, string store, string fromDate, string toDate, string fromCode, string toCode, string isDel)
        {
            List<dtoPurchaseInvForShow> purchase = new List<dtoPurchaseInvForShow>();
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
                    ["supplier_id"] = sup,
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
                    purchase = results.ToObject<List<dtoPurchaseInvForShow>>();
                    //Deserializing the response recieved from web api and storing into the Employee list
                }
                string mimtype = "";
                int extension = 1;
                var path = $"{_hostingEnvironment.WebRootPath}\\Reports\\all-purchase.rdlc";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                LocalReport report = new LocalReport(path);
                report.AddDataSource("PurchaseInvoiceInfo", purchase);
                var result = report.Execute(RenderType.Pdf, extension, parameters, mimtype);
                return File(result.MainStream, "application/pdf");
            }
        }

        public async Task<IActionResult> ReturnSalesInvoice(int inv_number)
        {
            List<dtoSalesReturnInvForShow> sales = new List<dtoSalesReturnInvForShow>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri("https://localhost:44315/");
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/SalesReturn/ReadCustomSalesReturnInvoiceForReport?pur_inv_id=" + inv_number);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    sales = Res.Content.ReadAsAsync<List<dtoSalesReturnInvForShow>>().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                }
                string mimtype = "";
                int extension = 1;
                var path = $"{_hostingEnvironment.WebRootPath}\\Reports\\return-sales-order-detail.rdlc";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                LocalReport report = new LocalReport(path);
                report.AddDataSource("InvoiceReturnSalesInfo", sales);
                List<dtoSalesReturnDetailsForShow> lstDetails = new List<dtoSalesReturnDetailsForShow>();
                for (int i = 0; i < sales.Count(); i++)
                {
                    for (int ii = 0; ii < sales[i].lstProducts.Count(); ii++)
                    {
                        lstDetails.Add(sales[i].lstProducts[ii]);
                    }
                }
                report.AddDataSource("InvoiceReturnSalesDetails", lstDetails);

                var result = report.Execute(RenderType.Pdf, extension, parameters, mimtype);
                return File(result.MainStream, "application/pdf");
            }
        }
        public async Task<IActionResult> ReturnPurchaseInvoice(int inv_number)
        {
            List<dtoPurchaseInvForShow> purchase = new List<dtoPurchaseInvForShow>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri("https://localhost:44315/");
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/PurchaseReturn/GetCustomReturnPurchaseInvoiceForReport?pur_inv_id=" + inv_number);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    purchase = Res.Content.ReadAsAsync<List<dtoPurchaseInvForShow>>().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                }
                string mimtype = "";
                int extension = 1;
                var path = $"{_hostingEnvironment.WebRootPath}\\Reports\\return-purchase-order-detail.rdlc";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                LocalReport report = new LocalReport(path);
                report.AddDataSource("InvoiceReturnPurchaseInfo", purchase);
                List<dtoPurchaseDetialsForShow> lstDetails = new List<dtoPurchaseDetialsForShow>();
                for (int i = 0; i < purchase.Count(); i++)
                {
                    for (int ii = 0; ii < purchase[i].lstProducts.Count(); ii++)
                    {
                        lstDetails.Add(purchase[i].lstProducts[ii]);
                    }
                }
                report.AddDataSource("InvoiceReturnPurchaseDetails", lstDetails);

                var result = report.Execute(RenderType.Pdf, extension, parameters, mimtype);
                return File(result.MainStream, "application/pdf");
            }
        }

    }

}