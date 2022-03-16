using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dahboard.Controllers
{
    public class SubCategoryController : Controller
    {
        // GET: SubCategoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SubCategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SubCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SubCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
