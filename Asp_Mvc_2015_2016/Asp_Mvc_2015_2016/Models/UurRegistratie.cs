using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.Models
{
    public class UurRegistratie
    {
        //hoofdgegevens
        public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public Klant Klant { get; set; }
        //detailgegevens
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public TypeWerk TypeWerk { get; set; }
        public bool TeFactureren { get; set; }
    }
}