using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.ViewModels
{
    /// <summary>
    /// This class bundles data for the 'Gebruikers' index screen. (ajax-based)
    /// </summary>
    //// <typeparam name="T">The </typeparam>
    public class GebruikersIndexViewModel<T> //where T : new()
    {
        public List<GenericUserFormViewModel<Gebruiker>> Users { get; set; }
        public GenericUserFormViewModel<NewGebruiker> User { get; set; }
        public List<TypeWerk> AllWerkTypen { get; set; }
     //   public GenericUserFormViewModel<Gebruiker> EditUser { get; set; }
        //public String FormPatialViewName { get; private set; }

        /// <summary>
        /// INTERNAL CTOR: DO NOT USE!
        /// </summary>
        public GebruikersIndexViewModel()
        {
        //    /*DEFAULT CTOR */
            Users = new List<GenericUserFormViewModel<Gebruiker>>();
            User = new GenericUserFormViewModel<NewGebruiker>(); //(new T());
        }

        public GebruikersIndexViewModel(List<GenericUserFormViewModel<Gebruiker>> users, List<String> allRoles, String userRoleName = "Gebruiker")
        {
            Users = users;
            User = new GenericUserFormViewModel<NewGebruiker>(new NewGebruiker(), userRoleName,allRoles);
            //NewUser = new GenericUserFormViewModel<T>(new NewGebruiker(), userRoleName, allRoles);
        }
    }
}