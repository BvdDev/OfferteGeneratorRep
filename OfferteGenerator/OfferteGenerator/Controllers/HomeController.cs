using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CsvHelper;
using OfferteGenerator.Models;
using System.Dynamic;

namespace OfferteGenerator.Controllers
{
    public class HomeController : Controller
    {
        public string DBlocation = "Werk";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Articles(int startRow, int showNr)
        {
            OfferteGenerator.Library.DBConnect newSqlConnection = new Library.DBConnect("OfferteGenerator" + DBlocation);
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
            var output = newSqlConnection.Select("ImportArtikelen", startRow, showNr);
            foreach(var item in output)
            {
                AxArticle newObject = new AxArticle();
                newObject.Id = (int)item.GetType().GetProperty("Id").GetValue(output, null);
            }
            return View(output);
        }

        public ActionResult Configuratie()
        {
            OfferteGenerator.Library.DBConnect newSqlConnection = new Library.DBConnect("OfferteGenerator" + DBlocation);
            var output = newSqlConnection.Select("ImportArtikelen", 0, 0);
            return View(output);
        }

        [HttpGet]
        public JsonResult GetArticles()
        {
            OfferteGenerator.Library.DBConnect newSqlConnection = new Library.DBConnect("OfferteGenerator");
            return Json(newSqlConnection.Select("ImportArtikelen", 0, 20), JsonRequestBehavior.AllowGet);
        }
    }
}