using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.Models
{
    public class Adres
    {
        public int Id { get; set; }
        public string StraatNr { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }
    }
}