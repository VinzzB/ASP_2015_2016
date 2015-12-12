using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Klanten")]
    public class Klant : _BaseInfo
    {
    //    public int Id { get; set; }
        public string Ondernemingsnr { get; set; }
        public string NaamBedrijf { get; set; }
        public virtual Adres Adres { get; set; } //adres apart? = extra (partial) view
        // gebruiker en klant is many to many relatie ? -> apart model voor link tussen gebruiker en klant?
     //   public virtual ICollection<GebruikerKlanten> Gebruiker { get; set; }
    }
}