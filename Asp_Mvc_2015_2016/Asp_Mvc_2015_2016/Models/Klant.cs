﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Klanten")]
    public class Klant  : _BaseInfo
    {
        
        //[Unique(ErrorMessage = "This already exist !!")]
        //[Index("OndernNrIndex", IsUnique = true)]
        public string Ondernemingsnr { get; set; }
        public string NaamBedrijf { get; set; }
        //public virtual Adres Adres { get; set; } //adres apart? = extra (partial) view
        public string StraatNr { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }

        // gebruiker en klant: many to many relatie
        [InverseProperty("Klant")] //inverseProp is nodig in Klant of GerbuikerKlant, want anders worden er (default) twee kolommen ipv één aangemaakt
        public virtual ICollection<GebruikerKlant> Gebruikers { get; set; }
        [InverseProperty("Klant")]
        public virtual ICollection<DepartementKlant> Departementen{ get; set; }
        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }
    }
}