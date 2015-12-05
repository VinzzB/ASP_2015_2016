using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.Models
{
    public class FactuurDetails
    {
        public int Id { get; set; }
        public Factuur Factuur { get; set; }

        //hoofdgegevens
        public string Omschrijving { get; set; }        
        public string Titel { get; set; }
        public Klant Klant { get; set; }

        //tijd gegevens? overzicht alle start en eindtijden?
        public List<UurRegistratie> uurRegistratie {get;set;}
        //netto lijnwaarde?
        public int lijnwaarde { get; set; }
    }
}