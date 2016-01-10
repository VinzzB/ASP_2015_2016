using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace Asp_Mvc_2015_2016.Models
{
    [Table("DepartementKlanten")]
    public class DepartementKlant  : _BaseInfo
    {
        public int DepartementId { get; set; }
        [Required]
        public virtual Departement Departement { get; set; }
        public int KlantId { get; set; }
        [Required]
        public virtual Klant Klant { get; set; }

        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }
    }
}