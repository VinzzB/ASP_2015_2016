using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace Asp_Mvc_2015_2016.Models.DAL
{
    //VB: inherit van IdentityDbContext<Gebruiker>
    // comment existing entities (Gebruikers, Rol) --> IdentityUser, IdentityRole
    public class FacturatieDBContext :  IdentityDbContext<Gebruiker> // DbContext
    {
        //tabellen sql server
     //   public DbSet<Adres> Adressen { get; set; }
        public DbSet<Departement> Departementen { get; set; }
        public DbSet<Factuur> Facturen { get; set; }
        public DbSet<FactuurDetails> Factuurdetails { get; set; }
        //     public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        //    public DbSet<KlantFacturatie> KlantFacturaties { get; set; }
   //     public DbSet<Rol> Rol { get; set; }
        public DbSet<TypeWerk> WerkTypes { get; set; }
        public DbSet<UurRegistratie> GeregistreerdeUren { get; set; }
        // Deze datasets worden normaal automatisch aangemaakt door collection op te nemen bij de modelklassen
        public DbSet<DepartementGebruiker> DepartementGebruikers { get; set; }
        public DbSet<DepartementKlant> DepartementKlanten { get; set; }
        public DbSet<GebruikerKlant> GebruikersKlanten { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Wijzig tabelnamen van AspUser tabellen.
            modelBuilder.Entity<Gebruiker>().ToTable("Gebruikers");           
            modelBuilder.Entity<IdentityUserRole>().ToTable("GebruikersRollen");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("GebruikersLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("GebruikersClaim");
            modelBuilder.Entity<IdentityRole>().ToTable("Rollen");
        }
        //VB: Used by OWIN to initialize the DbContext in the UserManager
        public static FacturatieDBContext Create()
        {
            return new FacturatieDBContext();
        }
        //VB: Create DbContext (and map as SQL-Server in VS).
        public FacturatieDBContext()
            : base("FacturatieDBContext")
        { }

    }
    
}