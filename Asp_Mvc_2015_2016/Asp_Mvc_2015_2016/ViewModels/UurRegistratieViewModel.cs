using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp_Mvc_2015_2016.ViewModels
{
    public class UurRegistratieViewModel
    {
        public UurRegistratie UurRegistratie { get; set; }
        public List<SelectListItem> AvailableTypeWerk { get; set; }

    }
}