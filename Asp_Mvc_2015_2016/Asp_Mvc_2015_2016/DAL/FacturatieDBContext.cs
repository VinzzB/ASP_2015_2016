using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace Asp_Mvc_2015_2016.Models
{
    //VB: inherit van IdentityDbContext<Gebruiker>
    // comment existing entities (Gebruikers, Rol) --> IdentityUser, IdentityRole
    public class FacturatieDBContext :  IdentityDbContext<Gebruiker> // DbContext
    {
        //tabellen sql server
        public DbSet<Adres> Adressen { get; set; }
        public DbSet<Departement> Departementen { get; set; }
        public DbSet<Factuur> Facturen { get; set; }
        public DbSet<FactuurDetails> Factuurdetails { get; set; }
        //     public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        //    public DbSet<KlantFacturatie> KlantFacturaties { get; set; }
   //     public DbSet<Rol> Rol { get; set; }
        public DbSet<TypeWerk> WerkTypes { get; set; }
        public DbSet<UurRegistratie> GeregistreerdeUren { get; set; }
        public DbSet<DepartementGebruiker> DepartementGebruikers { get; set; }
        public DbSet<DepartementKlanten> DepartementKlanten { get; set; }
        public DbSet<GebruikerKlanten> GebruikersKlanten { get; set; }

        /*
         * Lost probleem op met de abstracte classe '_baseInfo' op de 'Gebruiker' classe: 
         * An exception of type 'System.InvalidOperationException' occurred in EntityFramework.dll but was not handled in user code
         * Additional information: Unable to determine the principal end of an association between the types 
         * 'Asp_Mvc_2015_2016.Models.Gebruiker' and 'Asp_Mvc_2015_2016.Models.Gebruiker'. 
         * The principal end of this association must be explicitly configured using either the relationship fluent API or 
         * data annotations.
         */
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
            : base("FacturatieDBContext", throwIfV1Schema: false)
        { }

    }
    
}