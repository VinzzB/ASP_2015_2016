using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Uurregistraties")]
    public class UurRegistratie : _BaseInfo
    {
        [Required]
        public int FactuurDetailId { get; set; }
        public virtual FactuurDetails FactuurDetail { get; set; }
        //detailgegevens        
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDatum { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EindDatum { get; set; }
        [Required]
        public int TypeWerkId { get; set; }
        public virtual TypeWerk TypeWerk { get; set; }
        public bool TeFactureren { get; set; } //klaar om te factureren

        public int? FactuurId { get; set; }
        public virtual Factuur Factuur { get; set; } //gefactureerd
    //    public virtual Klant klant { get; set; }
        /* Virtuals from FK in base class */
        public virtual Gebruiker CreatedBy { get; set; }
        public virtual Gebruiker EditedBy { get; set; }

        public UurRegistratie()
        {
            StartDatum = DateTime.Now;
            EindDatum = DateTime.Now.AddHours(1);
        }

        [NotMapped]
        public decimal Total {
            get {
                if (TypeWerk != null)
                {
                    var diffInMinutes = (int)EindDatum.Subtract(StartDatum).TotalMinutes;
                    return (TypeWerk.TariefPerUur / 60) * diffInMinutes;
                }
                return 0;
            }
        }
    }
}