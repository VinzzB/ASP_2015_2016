using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Asp_Mvc_2015_2016.Models
{
    public class FacturatieInitializer : DropCreateDatabaseIfModelChanges<FacturatieDBContext>
    {
        protected override void Seed(FacturatieDBContext context)
        {
            //add roles
            Rol rolDepAdmin = new Rol() { RolBenaming = "Departement Administrator" };
            Rol rolAdmin = new Rol() { RolBenaming = "Systeem Administrator" };
            Rol rolUser = new Rol() { RolBenaming = "Gebruiker" };
            context.Rol.Add(rolDepAdmin);
            context.Rol.Add(rolAdmin);
            context.Rol.Add(rolUser);

            //create and add departments
            Departement dep1 = new Departement() { Code ="Dep1", Omschrijving="Departement 1" };
            Departement dep2 = new Departement() { Code = "Dep2", Omschrijving = "Departement 2" };
            context.Departementen.Add(dep1);
            context.Departementen.Add(dep2);
            //create and add Users
            Gebruiker admin = new Gebruiker()
            {
                Login = "admin",
                Voornaam = "System",
                Achternaam = "Administrator",
                Rol = rolAdmin
                //Departementen = new List<DepartementGebruikers>() { new DepartementGebruikers() {departement=dep1 } }
            };
            context.Gebruikers.Add(admin);

            Gebruiker depAdmin = new Gebruiker()
            {
                Login = "depAdmin",
                Voornaam = "Department",
                Achternaam = "Administrator",
                Rol = rolAdmin,
                CreatedBy = admin
                //Departementen = new List<DepartementGebruikers>() { dep2 }
            };

            
            context.Gebruikers.Add(depAdmin);

            context.DepartementGebruikers.Add(new DepartementGebruiker() { Departement = dep1, Gebruiker = admin });
            context.DepartementGebruikers.Add(new DepartementGebruiker() { Departement = dep2, Gebruiker = admin });
            context.DepartementGebruikers.Add(new DepartementGebruiker() { Departement = dep2, Gebruiker = depAdmin });
            
            context.SaveChanges();

            //base.Seed(context);
        }
    }
}