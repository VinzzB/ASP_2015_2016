using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp_Mvc_2015_2016.ViewModels
{
    public class EditKlantViewModels
    {
        public Klant klant { get; set; }
        public List<SelectListItem> AvailableDepartments { get; set; }
        public List<String> SelectedDepartments { get; set; }
    }
}