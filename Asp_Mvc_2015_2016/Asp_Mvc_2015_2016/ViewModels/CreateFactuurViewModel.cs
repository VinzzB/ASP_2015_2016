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
        DateTime FactuurVan { get; set; }
        [Required]
        DateTime FactuurTot { get; set; }
        [Required]
        Klant klant { get; set; }
        public List<SelectListItem> AvailableKlanten { get; set; }
        public String SelectedKlant { get; set; }
        
    }
}