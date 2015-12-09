using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("TypeWerk")]
    public class TypeWerk
    {
        public int Id { get; set; }
        public string WerkType { get; set; }
        public DateTime GeldigVanaf { get; set; }
        public Decimal TariefPerUur { get; set; }
    }
}