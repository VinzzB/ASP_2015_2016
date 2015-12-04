using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.Models
{
    public class Departement
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Omschrijving { get; set; }
        // gebruiker en departement is many to many relatie -> apart model voor link tussen gebruiker en departement?
        public int GebruikerId { get; set; }
        public List<Gebruiker> Gebruiker { get; set; }
    }
}