using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp_Mvc_2015_2016.ViewModels
{
    public class CreateFactuurViewModel
    {
        [Required]
        public DateTime FactuurVan { get; set; }

        [Required]
        public DateTime FactuurTot { get; set; }
        [Required]
        public Factuur Factuur { get; set; }
     //   public UurRegistratie uurregistratie { get; set; }
     //   public List<FactuurDetails> factuurDetails { get; set; }
       
        public List<SelectListItem> AvailableKlanten { get; set; }
        //[Required]
        //[Display(Name = "Choose_customer", ResourceType = typeof(Resources.CultureResource))]
     
        //Kan naar Factuur.klantId
        //public String SelectedKlant { get; set; }
        public CreateFactuurViewModel()
        {
            FactuurVan = DateTime.Now.AddDays(-1);
            FactuurTot = DateTime.Now;
        }
    }
}