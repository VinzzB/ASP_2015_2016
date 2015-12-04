using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.Models
{
    public class Klant
    {
        public int Id { get; set; }
        public string Ondernemingsnr { get; set; }
        public string NaamBedrijf { get; set; }
        public Adres Adres { get; set; }
        // gebruiker en klant is many to many relatie ? -> apart model voor link tussen gebruiker en klant?
        public int GebruikerId { get; set; }
        public List<Gebruiker> Gebruiker { get; set; }
    }
}