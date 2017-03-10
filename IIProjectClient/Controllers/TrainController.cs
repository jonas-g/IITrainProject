using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace IIProjectClient.Controllers
{
    public class TrainController : Controller
    {
        TrainServiceReference.TrainServiceClient localService = new TrainServiceReference.TrainServiceClient();

        public TrainController()
        {

        }

        // GET: Train
        public ActionResult Index()
        {
            List<SelectListItem> test = new List<SelectListItem>();

            test.Add(new SelectListItem { Text = "Location 1" });
            test.Add(new SelectListItem { Text = "Location 2" });
            test.Add(new SelectListItem { Text = "Location 3" });

            ViewBag.DropDownValues = new SelectList(test);
            ViewBag.Message = "Sök här";
            return View();
        }

        // GET: Train/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Train/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Train/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Train/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Train/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Train/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Train/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
