using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Gebruikers")]
    public class Gebruiker : _BaseInfo
    {
    //    public int Id { get; set; }
        public string Login { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Gsm { get; set; }
        // gebruiker en departement is many to many relatie -> apart model voor link tussen gebruiker en departement? -> ja dus...
        //apart model voor klanten toegewezen aan departement(en)
        public virtual ICollection<DepartementGebruiker> Departementen { get; set; }
        //apart model voor klanten toegewezen aan gebruiker(s).
        public virtual ICollection<GebruikerKlanten> Klanten { get; set; }
     //   public int KlantId { get; set; }
        public virtual Rol Rol { get; set; }


    }
}