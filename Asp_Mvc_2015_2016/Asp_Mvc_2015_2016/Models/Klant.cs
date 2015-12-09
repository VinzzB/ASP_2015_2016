using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Klanten")]
    public class Klant
    {
        public int Id { get; set; }
        public string Ondernemingsnr { get; set; }
        public string NaamBedrijf { get; set; }
        public Adres Adres { get; set; }
        // gebruiker en klant is many to many relatie ? -> apart model voor link tussen gebruiker en klant?
        public ICollection<Gebruiker> Gebruiker { get; set; }
    }
}