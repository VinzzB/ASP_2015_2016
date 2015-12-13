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
        // gebruiker en departement is many to many relatie -> apart model voor link tussen gebruiker en departement?
        public virtual ICollection<DepartementGebruiker> Gebruikers { get; set; }
        //apart model voor klanten in departement. 
        //Kan ook anders, maar dan zonder extra properties (createdBy,..). Zie 'Many relationship' op http://blog.staticvoid.co.nz/2012/7/17/entity_framework-navigation_property_basics_with_code_first (te bespreken)
        //ik denk dat dit beter is... (dus apart model tussen departement en klanten, idem bij gebruikers)
        public virtual ICollection<DepartementKlanten> Klanten {get; set;}

        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }

    }
}