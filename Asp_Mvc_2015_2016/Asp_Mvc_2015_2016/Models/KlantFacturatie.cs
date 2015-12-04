using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.Models
{
    public class KlantFacturatie
    {
        public int Id { get; set; }
        public TypeWerk TypeWerk { get; set; }
        public DateTime GeldigVanaf { get; set; }
        public int TariefWaarde { get; set; }
    }
}