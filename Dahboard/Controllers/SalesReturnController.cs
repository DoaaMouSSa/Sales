using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dahboard.Controllers
{
    public class SalesReturnController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult PurchaseInvoiceDetails()
        {
            return View();
        }
        
    }
}
