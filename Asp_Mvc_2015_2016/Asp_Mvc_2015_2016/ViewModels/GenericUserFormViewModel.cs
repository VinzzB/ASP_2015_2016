using Asp_Mvc_2015_2016.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Asp_Mvc_2015_2016.ViewModels
{
    //VB: wrapped Gebruikers or NewGebruiker classe with a Role property...
    public class GenericUserFormViewModel<T> where T : new()
    {

        public T User {get;set;}

        [Required]
        [Display(Name="Role", ResourceType=typeof(Resources.CultureResource))]
        public String RoleName{get;set;}

        public List<SelectListItem> AllRoles { get; set; } //a list for the combobox.

        /// <summary>
        /// DO NOT USE! PARAMETERLESS LINQ QUERIES REQUIRES A DEFAULT CTOR
        /// </summary>
        public GenericUserFormViewModel() {  }

        public GenericUserFormViewModel(T User, String RoleName = "Gebruiker", List<String> allRoles = null) : this(User,allRoles, RoleName)
        {
            //create a list for the Role combobox.
            //AllRoles = allRoles.ConvertAll(p => new SelectListItem() { Text = p, Value = p, Selected = (p == RoleName) });
        }

        public GenericUserFormViewModel(T User, List<String> allRoles, String RoleName = "Gebruiker")
        {
            this.User = User;
            this.RoleName = RoleName;
            if (allRoles != null)
                AllRoles = allRoles.ConvertAll(p => new SelectListItem() { Text = p, Value = p, Selected = (p == RoleName) });
            //ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name",role); 
        }

     //   public SelectList AllRoles() { 
            //return 
//        }

    }

    
}