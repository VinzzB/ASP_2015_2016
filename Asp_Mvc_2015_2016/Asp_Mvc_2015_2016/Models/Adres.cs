using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Adressen")]
    public class Adres : _BaseInfo
    {
  
        public string StraatNr { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }

        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }

    }
}