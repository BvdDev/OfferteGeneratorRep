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
        public string DBlocation = "Thuis";

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
            List<AxArticle> axArticles = new List<AxArticle>();
            foreach(dynamic item in newSqlConnection.Select("ImportArtikelen", startRow, showNr))
            {
                AxArticle newObject = new AxArticle();
                newObject.Id = item.Id;
                newObject.WSP_Code = item.WSP_Code;
                newObject.Art_code_Lev = item.Art_code_Lev;
                newObject.Art_nr_Merk = item.Art_nr_Merk;
                newObject.Omschrijving = item.Omschrijving;
                newObject.Veel_Gebruikt = item.Veel_Gebruikt;
                newObject.Bruto_Prijs = item.Bruto_Prijs;
                newObject.Netto_Prijs = item.Netto_Prijs;
                newObject.Korting = item.Korting;
                newObject.Leverancier = item.Leverancier;
                axArticles.Add(newObject);
            }
            return View(axArticles);
        }

        public ActionResult Configuratie()
        {
            OfferteGenerator.Library.DBConnect newSqlConnection = new Library.DBConnect("OfferteGenerator" + DBlocation);
            List<WwsObject> wwsObjecten = new List<WwsObject>();
            foreach (dynamic item in newSqlConnection.Select("WwsObjecten", 0, 0))
            {
                WwsObject newObject = new WwsObject();
                newObject.Id = item.Id;
                newObject.ObjectNaam = item.ObjectNaam;
                newObject.Aantal = item.Aantal;
                wwsObjecten.Add(newObject);
            }
            return View(wwsObjecten);
        }

        [HttpGet]
        public JsonResult GetArticles()
        {
            OfferteGenerator.Library.DBConnect newSqlConnection = new Library.DBConnect("OfferteGenerator");
            return Json(newSqlConnection.Select("WwsObjecten", 0, 20), JsonRequestBehavior.AllowGet);
        }
    }
}