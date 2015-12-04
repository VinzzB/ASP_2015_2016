using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.Models
{
    public class Factuur
    {
        public int Id { get; set; }
        public int FactuurJaar { get; set; }
        public string FactuurNr { get; set; }
        public int KlantId { get; set; }
        //public Klant Klant { get; set; }
        public DateTime FactuurDatum { get; set; }
        public int Totaal { get; set; }
    }
}