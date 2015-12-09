using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("FactuurDetails")]
    public class FactuurDetails
    {
        public int Id { get; set; }
        public Factuur Factuur { get; set; }

        //hoofdgegevens
        public string Omschrijving { get; set; }        
        public string Titel { get; set; }
        public virtual Klant Klant { get; set; }

        //tijd gegevens? overzicht alle start en eindtijden?
        public ICollection<UurRegistratie> uurRegistratie {get;set;}
        //netto lijnwaarde?
        public int lijnwaarde { get; set; }
    }
}