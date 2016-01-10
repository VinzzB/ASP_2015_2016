using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp_Mvc_2015_2016.ViewModels
{
    public class EditKlantViewModels
    {
        public Klant klant { get; set; }
        //available departments zijn de departments waarover een gebruiker mag beslissen
        public List<SelectListItem> AvailableDepartments { get; set; }
        //selected departments zijn de departments die aan een klant zijn toegewezen.
        [Display(Name = "Departments", ResourceType = typeof(Resources.CultureResource))]
        public List<String> SelectedDepartments { get; set; }
    }
}