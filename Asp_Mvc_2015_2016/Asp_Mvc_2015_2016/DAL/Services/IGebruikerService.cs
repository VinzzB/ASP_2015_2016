using System;
namespace Asp_Mvc_2015_2016.DAL.Services
{
    public interface IGebruikerService
    {
        void AddDepartments(Asp_Mvc_2015_2016.Models.Gebruiker gebr, System.Collections.Generic.List<string> departmentIds);
        System.Collections.Generic.List<Asp_Mvc_2015_2016.Models.Departement> getDepartments();
        System.Collections.Generic.List<string> getRoles();
        Asp_Mvc_2015_2016.Models.Gebruiker getUserById(string userId);
        string GetUserRole(string userId);
        System.Collections.Generic.List<Asp_Mvc_2015_2016.ViewModels.GenericUserFormViewModel<Asp_Mvc_2015_2016.Models.Gebruiker>> getUsers();
        Microsoft.AspNet.Identity.Owin.SignInManager<Asp_Mvc_2015_2016.Models.Gebruiker, string> SignInManager { get; }
        Microsoft.AspNet.Identity.UserManager<Asp_Mvc_2015_2016.Models.Gebruiker> UserManager { get; }
    }
}
