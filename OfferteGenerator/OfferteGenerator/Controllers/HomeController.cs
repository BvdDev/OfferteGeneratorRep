using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CsvHelper;
using OfferteGenerator.Models;

namespace OfferteGenerator.Controllers
{
    public class HomeController : Controller
    {
        public OfferteGenerator.Library.DBConnect newSqlConnection = new Library.DBConnect();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Articles(int startRow, int showNr)
        {
            string countRows = newSqlConnection.CountRows("ImportArtikelen");
            int countRowsInt = Convert.ToInt32(countRows);
            if (startRow < 0) startRow = 0;
            if (startRow + showNr > countRowsInt)
            {
                ViewBag.pageshowNrArt = showNr;
                showNr = countRowsInt - startRow;
            }
            else ViewBag.pageshowNrArt = showNr;
            ViewBag.SqlCount = countRows;
            ViewBag.startRow = startRow;
            return View(newSqlConnection.Select("ImportArtikelen", startRow, showNr));
        }

        [HttpGet]
        public JsonResult GetArticles()
        {
            
            return Json(newSqlConnection.Select("ImportArtikelen", 0, 20), JsonRequestBehavior.AllowGet);
        }
    }
}