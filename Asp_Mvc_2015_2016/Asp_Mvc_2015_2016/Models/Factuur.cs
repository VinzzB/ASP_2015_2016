using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Facturen")]
    public class Factuur : _BaseInfo
    {
     //   public int Id { get; set; }
        public int FactuurJaar { get; set; }
        public string FactuurNr { get; set; }
        public virtual Klant Klant { get; set; }
        public DateTime FactuurDatum { get; set; }
        public int Totaal { get; set; }
    }
}