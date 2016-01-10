using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.ViewModels
{
    public class CreateFactuur
    {
        [Required]
        DateTime FactuurVan { get; set; }
        [Required]
        DateTime FactuurTot { get; set; }
        [Required]
        Klant klant { get; set; }
    }
}