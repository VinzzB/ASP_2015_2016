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
    //    public DbSet<KlantFacturatie> KlantFacturaties { get; set; }
        public DbSet<Rol> Rol { get; set; }
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
            //Maak een self referencing Entity (CF) voor CreatedBy
            modelBuilder.Entity<Gebruiker>()
                        .HasOptional(o => o.CreatedBy)      //allow null
                        .WithMany();                         //not unique
                        //.HasForeignKey(i => i.CreatedById); //map with FK field.

            //Maak een self referencing Entity (CF) voor EditedBy
            modelBuilder.Entity<Gebruiker>()
                        .HasOptional(o => o.EditedBy)
                        .WithMany();  
            
        }
    }
    
}