using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Facturen")]
    public class Factuur : _BaseInfo
    {
        [Display(Name = "InvoiceYear", ResourceType = typeof(Resources.CultureResource))]
        public int FactuurJaar { get; set; }
        [Display(Name = "InvoiceNumber", ResourceType = typeof(Resources.CultureResource))]
        public string FactuurNr { get; set; }
        public virtual Klant Klant { get; set; }
        [Display(Name = "InvoiceDate", ResourceType = typeof(Resources.CultureResource))]
        public DateTime FactuurDatum { get; set; }
        [Display(Name = "Total", ResourceType = typeof(Resources.CultureResource))]
        public int Totaal { get; set; }

        [InverseProperty("Factuur")]
        public virtual ICollection<UurRegistratie> Uurregistraties { get; set; }
        //public virtual ICollection<FactuurDetails> factuurdetails { get; set; }

        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }
    }
}