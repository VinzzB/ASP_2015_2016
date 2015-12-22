using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Asp_Mvc_2015_2016.Models;
namespace Asp_Mvc_2015_2016.Models
{
    //VB: Gebruikt bij het invoegen van een nieuwe persoon door een andere gebruiker.
    // inherited van RegisterViewModal
    public class NewGebruiker : RegisterViewModel
    {
        public string PhoneNumber { get; set; }
        public string Gsm { get; set; }
        public String RoleName { get; set; } //not the id, but the real Role name!!!
    }
}
