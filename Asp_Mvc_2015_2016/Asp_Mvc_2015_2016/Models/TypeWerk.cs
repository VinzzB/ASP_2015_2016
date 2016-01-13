using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("TypeWerk")]
    public class TypeWerk : _BaseInfo
    {
        [Required]
        public string WerkType { get; set; }
        [Required]
        public DateTime GeldigVanaf { get; set; }
        [Required]        
        public Decimal TariefPerUur { get; set; }

        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }
        public TypeWerk()
        {
            GeldigVanaf = DateTime.Today;
        }
    }
}