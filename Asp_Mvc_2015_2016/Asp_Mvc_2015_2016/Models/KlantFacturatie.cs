﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("KlantFacturaties")]
    public class KlantFacturatie
    {
        public int Id { get; set; }
        public TypeWerk TypeWerk { get; set; }
        public DateTime GeldigVanaf { get; set; }
        public int TariefWaarde { get; set; }
    }
}