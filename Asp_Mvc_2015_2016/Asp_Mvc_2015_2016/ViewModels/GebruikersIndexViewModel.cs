using Asp_Mvc_2015_2016.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp_Mvc_2015_2016.ViewModels
{
    //VB: wrapped Gebruikers classe with a Role property...
    public class GebruikersIndexViewModel
    {
        public Gebruiker User {get;set;}
        public String RoleName{get;set;}

        public List<SelectListItem> AllRoles { get; set; } //a list for the combobox.

        public GebruikersIndexViewModel()
        {

        }

        public GebruikersIndexViewModel(Gebruiker User, String RoleName, List<String> allRoles) : this(User,RoleName)
        {
            //create a list for the Role combobox.
            AllRoles = allRoles.ConvertAll(p => new SelectListItem() { Text = p, Value = p, Selected = (p == RoleName) });
        }

        public GebruikersIndexViewModel(Gebruiker User, String RoleName)
        {
            this.User = User;
            this.RoleName = RoleName;
            //ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name",role); 
        }

     //   public SelectList AllRoles() { 
            //return 
//        }

    }

    
}