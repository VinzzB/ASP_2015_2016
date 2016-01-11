using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp_Mvc_2015_2016.ViewModels
{
    public class FactuurDetailsViewModel
    {
        public FactuurDetails FactuurDetails { get; set; }

        public List<SelectListItem> AvailableKlanten { get; set; }
     //   [Required]
     //   public int SelectedKlant { get; set; }

    }
}