using Asp_Mvc_2015_2016.Models;
using Asp_Mvc_2015_2016.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.DAL.Services
{
    public class GebruikerService : Asp_Mvc_2015_2016.DAL.Services.IGebruikerService
    {
        IUnitOfWork uow;

        public GebruikerService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public List<String> getRoles() {
            return uow.RolesRepository.AllRolesAsStringList(); //.GebruikerRepository.Context.Roles.ToList().ConvertAll(p => p.Name);
        }

        public Gebruiker getUserById(string userId) {
            return uow.GebruikerRepository.GetById(userId);
        }

        public List<Departement> getDepartments() {
            return uow.DepartementRepository.GetAll();
        }

        public string GetUserRole(string userId)
        {
            var roles = UserManager.GetRoles(userId);
            return roles != null && roles.Count > 0 ? roles.ElementAt(0) : null;
        }    

        //ik weet niet zeker of dit hier moet of in de repository classe...
        public List<GenericUserFormViewModel<Gebruiker>> getUsers()
        {
            //Rol zichtbaar maken in lijstweergave...
            //VB: from http://stackoverflow.com/questions/26078271/getting-a-list-of-users-with-their-assigned-role-in-identity-2
            //refactored zodat ook users waar nog geen rol aan toegewezen werd ook zichtbaar zijn... normaal moet er altijd minstens één rol zijn. 

            //Bij verplichte rol: 
            //var users = from u in db.Users
            //            from ur in u.Roles
            //            join r in db.Roles on ur.RoleId equals r.Id 
            //            select new GebruikersIndex() { User = u, Role = r };

            var res =  (from u in uow.GebruikerRepository.DbSet //get alle users
                    from ur in u.Roles.DefaultIfEmpty() // context.Roles.DefaultIfEmpty() //get rolIDs van user (add a default on empty) (=tabel GebruikersRollen)
                    join r in uow.RolesRepository.DbSet on ur.RoleId equals r.Id into tmp_ur //join met de tabel Rollen
                    from userrole in tmp_ur.DefaultIfEmpty() //for outerjoin on roles!
                    select new GenericUserFormViewModel<Gebruiker>()
                    {
                        User = u,
                        RoleName = userrole.Name
                    }).ToList(); //create our model.
            return res;
        }

        public void setRoles(Gebruiker USER, List<string> rolenames) { 
        

        }

        public void SetDepartments(Gebruiker gebr,  List<String> departmentIds) {
            if (gebr.Departementen != null)
            {
                foreach (DepartementGebruiker item in gebr.Departementen.Reverse())
                {
                    if (!departmentIds.Contains(item.DepartementId.ToString()))
                    {
                        uow.GebruikerDepartementRepository.Delete(item.Id);
                    }
                }
            }
            //insert new Departements.
            foreach (String d in departmentIds) {
                if (gebr.Departementen == null || !gebr.Departementen.Any(p => p.DepartementId == int.Parse(d))) {
                    Departement dep = uow.DepartementRepository.GetById(int.Parse(d)); // db.Departementen.Find(int.Parse(d));
                    DepartementGebruiker dg = new DepartementGebruiker() { Departement = dep, Gebruiker = gebr };
                    dep.Gebruikers.Add(dg);
                    //gebr.Departementen.Add(dg);
                    uow.GebruikerRepository.Update(gebr);
                }
            }        
        }

        public SignInManager<Gebruiker, String> SignInManager
        {
            get { return uow.GebruikerRepository.SignInManager; }
        }

        public UserManager<Gebruiker> UserManager
        {
            get { return uow.GebruikerRepository.UserManager; }
        }
    }
}