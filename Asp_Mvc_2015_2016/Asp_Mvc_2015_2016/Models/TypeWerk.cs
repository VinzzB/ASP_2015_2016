using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("TypeWerk")]
    public class TypeWerk : _BaseInfo
    {
        public string WerkType { get; set; }
        public DateTime GeldigVanaf { get; set; }
        public Decimal TariefPerUur { get; set; }

        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }
    }
}