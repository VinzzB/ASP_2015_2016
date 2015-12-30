using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Uurregistraties")]
    public class UurRegistratie : _BaseInfo
    {
        public virtual FactuurDetails factuurDetails { get; set; }
        //detailgegevens
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public virtual TypeWerk TypeWerk { get; set; }
        public bool TeFactureren { get; set; } //klaar om te factureren
        public virtual Factuur Factuur { get; set; } //gefactureerd
        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }
    }
}