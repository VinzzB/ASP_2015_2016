using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("FactuurDetails")]
    public class FactuurDetails  : _BaseInfo
    {
        //hoofdgegevens
        public string Omschrijving { get; set; }      
        [Required]
        public string Titel { get; set; }
        [Required]
        public int KlantId { get; set; }
        public virtual Klant Klant { get; set; }

        //tijd gegevens? overzicht alle start en eindtijden?
        [InverseProperty("FactuurDetail")]
        public ICollection<UurRegistratie> uurRegistratie {get;set;}
        //netto lijnwaarde?
        public int lijnwaarde { get; set; }

        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }

        [NotMapped]
        public List<UurRegistratie> GefactureerdeUren {
            get {
                if (uurRegistratie != null)
                    return (from u in uurRegistratie where u.Factuur != null select u).ToList();
                return new List<UurRegistratie>();
            }
        }

        [NotMapped]
        public List<UurRegistratie> NietGefactureerdeUren
        {
            get
            {
                if(uurRegistratie != null)
                    return (from u in uurRegistratie where u.Factuur == null select u).ToList();
                return new List<UurRegistratie>();
            }
        }
    }
}