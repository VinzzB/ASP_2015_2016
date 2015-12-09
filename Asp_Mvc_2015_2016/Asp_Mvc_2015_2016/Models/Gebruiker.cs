using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Gebruikers")]
    public class Gebruiker
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Gsm { get; set; }
        // gebruiker en departement is many to many relatie -> apart model voor link tussen gebruiker en departement?
        public virtual ICollection<Departement> Departementen { get; set; }

        public virtual ICollection<Klant> Klanten { get; set; }
     //   public int KlantId { get; set; }
        public virtual Rol Rol { get; set; }


    }
}