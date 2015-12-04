using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.Models
{
    public class FactuurDetails
    {
        public int Id { get; set; }
        public int IdFactuur { get; set; }
        public string Omschrijving { get; set; }
        //tijd gegevens? overzicht alle start en eindtijden?
        //public List<UurRegistratie> uurRegistratie;
        public int UurRegistratieId { get; set; }
        //netto lijnwaarde?
        public int lijnwaarde { get; set; }
    }
}