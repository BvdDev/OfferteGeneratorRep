﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OfferteGenerator.Models
{
    public class AxArticle
    {
        public int Id { get; set; }

        [Display(Name ="WSP Code")]
        public string WSP_Code { get; set; }

        [Display(Name = "Artikel Code Leverancier")]
        public string Art_code_Lev { get; set; }

        [Display(Name = "Artikel Nummer Merk")]
        public string Art_nr_Merk { get; set; }
        public string Omschrijving { get; set; }
        public string Fabrikant { get; set; }

        [Display(Name = "Veel gebruikt")]
        public string Veel_Gebruikt { get; set; }

        [Display(Name = "Bruto Prijs")]
        public float Bruto_Prijs { get; set; }

        [Display(Name = "Netto Prijs")]
        public float Netto_Prijs { get; set; }
        public float Korting { get; set; }
        public string Leverancier { get; set; }
    }
}