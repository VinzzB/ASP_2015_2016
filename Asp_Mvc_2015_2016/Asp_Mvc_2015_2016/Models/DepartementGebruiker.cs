using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Asp_Mvc_2015_2016.Models
{
    public class DepartementGebruiker : _BaseInfo
    {
        public int DepartementId { get; set; }
        public string GebruikerId { get; set; }
        //deze klasse moet toch niet geschreven worden? EF maakt automatisch een joint table?        
        [Required]
        public virtual Departement Departement { get; set; }
        [Required]
        public virtual Gebruiker Gebruiker { get; set; }

        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }
    }
}