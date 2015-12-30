using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Departementen")]
    public class Departement : _BaseInfo
    {
        public string Code { get; set; }
        public string Omschrijving { get; set; }
        
        [InverseProperty("Departement")]
        public virtual ICollection<DepartementGebruiker> Gebruikers { get; set; }
        [InverseProperty("Departement")]
        public virtual ICollection<DepartementKlant> Klanten {get; set;}

        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }

    }
}