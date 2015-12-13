using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
namespace Asp_Mvc_2015_2016.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //VB: Merged ApplicationUser met Gebruiker. Inherts from IdentityUser!!!
    public class Gebruiker : IdentityUser
    {
     //   public string Login { get; set; }  -->  Equals 'UserName' property in IdentityUser 
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
     // public string Email { get; set; } VB--> Exists in IdentityUser class.
     //   public string Tel { get; set; }  VB  --> Exists in IdentityUser class (PhoneNumber)
        public string Gsm { get; set; }
        // gebruiker en departement is many to many relatie -> apart model voor link tussen gebruiker en departement? -> ja dus...
        //apart model voor klanten toegewezen aan departement(en)
        public virtual ICollection<DepartementGebruiker> Departementen { get; set; }
        //apart model voor klanten toegewezen aan gebruiker(s).
        public virtual ICollection<GebruikerKlanten> Klanten { get; set; }

       //TODO: Nog testen of deze properties werken... (slides: Les 4 slide 41, 44)
        [InverseProperty("CreatedBy")]
        public virtual ICollection<Departement> DepartementsCreated { get; set; }
        [InverseProperty("EditedBy")]
        public virtual ICollection<Departement> DepartementsUpdated { get; set; }

        [InverseProperty("CreatedBy")]
        public virtual ICollection<Adres> AdressenCreated { get; set; }
        [InverseProperty("EditedBy")]
        public virtual ICollection<Adres> AdressenUpdated { get; set; }

        //TODO: andere entities mappen met CreatedBy, EditedBy.


        //VB: Copied method from Applicationuser classe
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Gebruiker> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //VB: Added method to use in views (TODO: only shows one role atm)
        [NotMapped]
        public string RolesAsString {
            get { return Roles == null || Roles.Count == 0 ? null : Roles.ElementAt(0).RoleId; } 
        }
    }

    //VB: Copied from ApplicationUser but Merged with FacturatieDbContext. code is Obsolete
    //public class ApplicationDbContext : IdentityDbContext<Gebruiker>
    //{
    //    public ApplicationDbContext()
    //        : base("FacturatieDBContext", throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}
}