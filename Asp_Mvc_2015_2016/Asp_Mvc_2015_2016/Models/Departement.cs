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
    //    public int Id { get; set; }
        public string Code { get; set; }
        public string Omschrijving { get; set; }
        // gebruiker en departement is many to many relatie -> apart model voor link tussen gebruiker en departement?
        public virtual ICollection<DepartementGebruiker> Gebruikers { get; set; }
        //apart model voor klanten in departement.
        public virtual ICollection<DepartementKlanten> Klanten {get; set;}
    }
}