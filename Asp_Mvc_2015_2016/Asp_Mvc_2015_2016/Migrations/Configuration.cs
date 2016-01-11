namespace Asp_Mvc_2015_2016.Migrations
{
    using Asp_Mvc_2015_2016.DAL;
    using Asp_Mvc_2015_2016.Models;
    // using Asp_Mvc_2015_2016.Models.DAL;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FacturatieDBContext>
    {
        private readonly static String AdminRoleName = "Systeem Administrator";
        private readonly static String DepAdminRoleName = "Departement Administrator";
        private readonly static String UserRoleName = "Gebruiker";

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Asp_Mvc_2015_2016.DAL.FacturatieDBContext";            
        }
        

        protected override void Seed(FacturatieDBContext context)
        {

            try
            {
                //create a role manager and store the roles (if not exists)
                RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> roleMgr = new RoleManager<IdentityRole>(roleStore);
                //admin role
                if (!context.Roles.Any(r => r.Name == AdminRoleName))
                    roleMgr.Create(new IdentityRole(AdminRoleName));
                //depAdmin role
                if (!context.Roles.Any(r => r.Name == DepAdminRoleName))
                    roleMgr.Create(new IdentityRole(DepAdminRoleName));
                //User role
                if (!context.Roles.Any(r => r.Name == UserRoleName))
                    roleMgr.Create(new IdentityRole(UserRoleName));

                //Admin user does not exist? Create an admin with password = 'Password').
                var userStore = new UserStore<Gebruiker>(context);
                var userManager = new UserManager<Gebruiker>(userStore);
                Gebruiker admin = userManager.FindByName("admin");
                if (admin == null)
                {                    
                    admin = new Gebruiker() { UserName = "admin", Voornaam = "Systeem", Achternaam = "Administrator", Email="admin@system.com"};
                    userManager.Create(admin, "Password");
                    //add admin role!
                    userManager.AddToRole(admin.Id, AdminRoleName);
                }
                Departement dep1 = context.Departementen.FirstOrDefault(d => d.Code == "1N3GA");
                if (dep1 == null)
                {
                    dep1 = new Departement() { Code = "1N3GA", Omschrijving = "Inspectie RWO" };
                    context.Departementen.Add(dep1);
                    context.DepartementGebruikers.Add(new DepartementGebruiker() { Departement = dep1, Gebruiker = admin });
                }
                if (!context.Departementen.Any(d => d.Code == "1N3GB"))
                    context.Departementen.Add(new Departement() { Code = "1N3GB", Omschrijving = "Inspectie RWO - Toezicht" });

                if (!context.Klanten.Any(d => d.NaamBedrijf == "GroepT")) {
                    Klant k1 = new Klant() { NaamBedrijf = "GroepT", Ondernemingsnr = "156045406" };
                    context.Klanten.Add(k1);
                    context.GebruikersKlanten.Add(new GebruikerKlant() { Klant = k1, Gebruiker = admin });
                    context.DepartementKlanten.Add(new DepartementKlant() { Klant = k1, Departement = dep1 });
                }

            }
            catch (DbEntityValidationException ex)
            {
#if(DEBUG)
                //LAUNCH DEBUG!
                if (System.Diagnostics.Debugger.IsAttached == false)
                    System.Diagnostics.Debugger.Launch();
#endif
                Console.WriteLine(ex.EntityValidationErrors.ElementAt(0).ValidationErrors.ElementAt(0).ErrorMessage);
               // throw;
            }
        }
    }
}
