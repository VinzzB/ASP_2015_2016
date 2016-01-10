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

        [Required]
        public List<String> DepartementIds { get; set; }

        public List<SelectListItem> AllDepartments { get; set; } //a list for the listbox.
        public List<SelectListItem> AllRoles { get; set; } //a list for the combobox.
     //   public IEnumerable<Departement> AllDepartments { get; set; }

        /// <summary>
        /// DO NOT USE! PARAMETERLESS LINQ QUERIES REQUIRES A DEFAULT CTOR
        /// </summary>
        public GenericUserFormViewModel() {  }

        public GenericUserFormViewModel(T User, 
                                        String RoleName = "Gebruiker", 
                                        List<String> allRoles = null, 
                                        List<Departement> allDepartments = null,
                                        List<Departement> selectedDepartments = null) {
            this.User = User;
            
            this.RoleName = RoleName;
            if (allRoles != null)
                AllRoles = allRoles.ConvertAll(p => new SelectListItem() { Text = p, Value = p});
            if (allDepartments != null)
                AllDepartments =  allDepartments.ConvertAll(p => new SelectListItem() 
                { 
                    Value = p.Id.ToString(),
                    Text = String.Format("{0} - {1}", p.Code, p.Omschrijving), 
                  //  Selected = (selectedDepartments != null && selectedDepartments.Contains(p))
                });
            if (selectedDepartments != null)
            {
                DepartementIds = selectedDepartments.ToList().ConvertAll(p => p.Id.ToString());
            }
        }
    }       
}