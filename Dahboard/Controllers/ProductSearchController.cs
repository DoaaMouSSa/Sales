using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dahboard.Controllers
{
    public class ProductSearchController : Controller
    {
        // GET: ProductSearchController
        public ActionResult Index()
        {
            return View();
        }
 
    }
}
