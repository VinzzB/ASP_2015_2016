using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
namespace Asp_Mvc_2015_2016.Models
{
    [Table("GebruikersKlanten")]
    public class GebruikerKlant  : _BaseInfo 
    {
        [Required]
        public virtual Gebruiker Gebruiker { get; set; }
        [Required]
        public virtual Klant Klant { get; set; }

        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }
    }
}