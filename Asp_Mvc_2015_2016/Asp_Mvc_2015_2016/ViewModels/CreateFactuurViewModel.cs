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
        public DateTime FactuurVan { get; set; }
     
        public DateTime FactuurTot { get; set; }
        [Required]
        public Factuur factuur { get; set; }
        public UurRegistratie uurregistratie { get; set; }
        public List<FactuurDetails> factuurDetails { get; set; }
        [Display(Name = "Choose_customer", ResourceType = typeof(Resources.CultureResource))]
        public List<SelectListItem> AvailableKlanten { get; set; }
        [Required]
        [Display(Name = "Choose_customer", ResourceType = typeof(Resources.CultureResource))]
        public String SelectedKlant { get; set; }
        
    }
}