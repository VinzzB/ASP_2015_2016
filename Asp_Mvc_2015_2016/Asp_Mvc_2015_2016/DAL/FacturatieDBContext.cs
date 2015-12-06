using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Asp_Mvc_2015_2016.Models
{
    public class FacturatieDBContext : DbContext
    {
        //tabellen sql server
        public DbSet<Adres> Adressen { get; set; }
        public DbSet<Departement> Departementen { get; set; }
        public DbSet<Factuur> Facturen { get; set; }
        public DbSet<FactuurDetails> Factuurdetails { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<KlantFacturatie> KlantFacturaties { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<TypeWerk> WerkTypes { get; set; }
        public DbSet<UurRegistratie> GeregistreerdeUren { get; set; }
    }
}