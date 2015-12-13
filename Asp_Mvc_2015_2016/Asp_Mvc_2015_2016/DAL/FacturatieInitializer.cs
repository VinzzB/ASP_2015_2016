using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace Asp_Mvc_2015_2016.Models
{
    public class FacturatieInitializer : DropCreateDatabaseIfModelChanges<FacturatieDBContext>
    {
        protected override void Seed(FacturatieDBContext context)
        {
            //add roles
            //VB: Changed Rol to Asp's internal 'IdentityRole' class.
            IdentityRole rolDepAdmin = new IdentityRole() { Name = "Departement Administrator" };
            IdentityRole rolAdmin = new IdentityRole() { Name = "Systeem Administrator" };
            IdentityRole rolUser = new IdentityRole() { Name = "Gebruiker" };
            context.Roles.Add(rolDepAdmin);
            context.Roles.Add(rolAdmin);
            context.Roles.Add(rolUser);
            //create and add departments
            Departement dep1 = new Departement() { Code ="Dep1", Omschrijving="Departement 1" };
            Departement dep2 = new Departement() { Code = "Dep2", Omschrijving = "Departement 2" };
            context.Departementen.Add(dep1);            
            context.Departementen.Add(dep2);
            
            //VB: Commented: it is possible to add users this way, but you 
            //first need the passwordHash and SecurityStamp.
            //Just register a new account.

            //create and add Users
            //Gebruiker admin = new Gebruiker()
            //{
            //    Id = "admin",
            //    Voornaam = "System",
            //    Achternaam = "Administrator",
            //    //Role = rolAdmin
            //    //Departementen = new List<DepartementGebruikers>() { new DepartementGebruikers() {departement=dep1 } }
            //};
            //context.Gebruikers.Add(admin);

            //Gebruiker depAdmin = new Gebruiker()
            //{
            //    Id = "depAdmin",
            //    Voornaam = "Department",
            //    Achternaam = "Administrator"                
            //    //Rol = rolAdmin,
            //  //  CreatedBy = admin
            //    //Departementen = new List<DepartementGebruikers>() { dep2 }
            //};

            
            //context.Gebruikers.Add(depAdmin);

            //context.DepartementGebruikers.Add(new DepartementGebruiker() { Departement = dep1, Gebruiker = admin });
            //context.DepartementGebruikers.Add(new DepartementGebruiker() { Departement = dep2, Gebruiker = admin });
            //context.DepartementGebruikers.Add(new DepartementGebruiker() { Departement = dep2, Gebruiker = depAdmin });
            
            context.SaveChanges();

            //base.Seed(context);
        }
    }
}