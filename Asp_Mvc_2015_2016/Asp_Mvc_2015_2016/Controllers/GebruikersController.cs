using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asp_Mvc_2015_2016.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Asp_Mvc_2015_2016.ViewModels;
using Asp_Mvc_2015_2016.DAL;
using Asp_Mvc_2015_2016.DAL.Services;
using System.Data.Entity.Infrastructure;

namespace Asp_Mvc_2015_2016.Controllers
{
    //VB: Added 'Authorize' annotation, extendable with userroles ("System Administrator, ...")
    [Authorize(Roles = "Systeem Administrator")]
    public class GebruikersController : BaseController // Controller
    {
        private IUnitOfWork uow;
        private IGebruikerService service;

        public GebruikersController() { }
        /// <summary>CTOR with DI !</summary>
        /// <param name="uow">DI !!</param>
        /// <param name="service">DI !!</param>
        public GebruikersController(IUnitOfWork uow, IGebruikerService service)
        {
            this.uow = uow;
            this.service = service;
        }
                
        // GET: Gebruikers
        //View is now with ajax calls. See ViewModel 'GebruikersIndexViewModel'
        public ActionResult Index()
        {
            return View(new GebruikersIndexViewModel<NewGebruiker>(service.getUsers(), service.getRoles())); //model = ajax viewmodel! 
        }

        //VB: added for ajax calls on Create gebruiker -> updates user list
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> User_Create(GenericUserFormViewModel<NewGebruiker> viewmodel)
        {
            if (ModelState.IsValid) {
                //create the user in db and check for errors
                var r = await CreateUser(viewmodel); //USER IS SAVED IMMEDIATELY BY THE USERMANAGER!
                if (r.Result.Succeeded) {

                  //  service.AddDepartments(r.CreatedUser, viewmodel.DepartementIds);                    
                    uow.Save();

                    //all OK. return new userlist.
                    return PartialView("_UsersListControl", service.getUsers());
                } else if (r.Result.Errors != null && r.Result.Errors.Count() > 0) {
                    //errored -> return json with error object.
                    Response.StatusCode = 300;
                    return Json(new { error = r.Result.Errors.ElementAt(0) }, "application/json", JsonRequestBehavior.AllowGet);
                }
            }
            return new PartialViewResult();
        }
        //VB: added for ajax calls on Edit Gebruiker -> updates user list.
        [ValidateAntiForgeryToken]
         public async Task<ActionResult> User_Edit(GenericUserFormViewModel<Gebruiker> viewmodel)
        {
            if (ModelState.IsValid) {               

                //zoek gebruiker in Db
                var gebr = service.getUserById(viewmodel.User.Id);
                
                if (gebr == null) {
                    return new HttpNotFoundResult("Gebruiker niet gevonden.");
                }

                //allow role changes for all users except 'admin'!
                if (gebr.UserName != "admin")
                {
                    //Haal huidige rolnaam op (via UserManager!)
                    var userRoles = await service.UserManager.GetRolesAsync(gebr.Id);
                    //heeft nog geen rol of de rol werd gewijzigd?
                    if (userRoles.Count == 0 || userRoles.ElementAt(0) != viewmodel.RoleName)
                    {
                        if (userRoles.Count > 0)//verwijder eerst de huidige rol (als er één is)
                        {
                            await service.UserManager.RemoveFromRoleAsync(gebr.Id, userRoles.ElementAt(0));
                        }
                        await service.UserManager.AddToRoleAsync(gebr.Id, viewmodel.RoleName); //add nieuwe rol
                    }
                }
                
                //update model met input.
                TryUpdateModel(gebr, "User");
                //add departments
                service.AddDepartments(gebr, viewmodel.DepartementIds);
                   try
                   {
                        uow.Save(); //save all changes.
                   }
                    catch (DbUpdateException ex)
                    {
                            Response.StatusCode = 300;
                            var jdata = new { error = ex.Message };
                            return Json(jdata, "application/json", JsonRequestBehavior.AllowGet);
                        throw;
                    }                    
                //all OK. return new user list.
                return PartialView("_UsersListControl", service.getUsers());
            }
            //Something happend....
            return new PartialViewResult();
        }

        /// <summary>Loads a Userform with Ajax.</summary>
        /// <param name="id">The UserID</param>
        /// <param name="type">Load form for 'new' or 'edit'</param>
        /// <returns></returns>
        public ActionResult Load_User_Form(String id, String type)
        { // type can be 'new' or 'edit'
        //public async Task<ActionResult> Load_User_Form(String id, String type) { // type can be 'new' or 'edit'
            if (ModelState.IsValid) {
                Gebruiker u = null;
                if (id != null)
                    u = service.getUserById(id); // await db.Users.FirstOrDefaultAsync(p => p.Id == id); //get user async.
               //System.Threading.Thread.Sleep(500); //--> for debugging!!! (mimics a slow server response, for debugging wait messages in the browser)                
                if (u != null || type == "new") //has a user OR is type new. (type 'new' does not need a user)
                {
                    switch (type)
                    {
                        case "edit":
                            return PartialView("_FormEditUser", 
                                new GenericUserFormViewModel<Gebruiker>(u, 
                                    service.GetUserRole(u.Id), 
                                    service.getRoles(),
                                    service.getDepartments(), u.Departementen.Select(p => p.Departement).ToList()));
                        case "new":                            
                            return PartialView("_FormCreateUser", 
                                new GenericUserFormViewModel<NewGebruiker>(new NewGebruiker(),
                                    "Gebruiker", 
                                    service.getRoles(), service.getDepartments()));
                        case "del":
                            return PartialView("_UserRemove", new GenericUserFormViewModel<Gebruiker>(u, service.GetUserRole(u.Id)));
                        default: ///details
                            return PartialView("_UserDetails", new GenericUserFormViewModel<Gebruiker>(u, service.GetUserRole(u.Id)));
                    }
                }
            }
            return new HttpStatusCodeResult(300, "Fout bij ophalen van gegevens...");// new PartialViewResult();
        }

        //Vb: added new (async) method om een gebruiker aan te maken. Retourneert een zelf aangemaakte (internal) classe 'CreateUserResult'!
        private async Task<CreateUserResult> CreateUser([Bind(Include = "User,RoleName")] GenericUserFormViewModel<NewGebruiker> newUser)
        {
            //Create Gebruiker object
            Gebruiker user = new Gebruiker()
            {
                UserName = newUser.User.UserName,
                Voornaam = newUser.User.Voornaam,
                Achternaam = newUser.User.Achternaam,
                Email = newUser.User.Email,
                PhoneNumber = newUser.User.PhoneNumber,
                Gsm = newUser.User.Gsm
            };            

            //Save Gebruiker with UserManager (stores Password)
            //VB: ADDED ContinueWith() lambda to add the specified role after user creation.
            var r = await service.UserManager.CreateAsync(user, newUser.User.Password);
           if (r.Succeeded)
           {
               r = await service.UserManager.AddToRoleAsync(user.Id, newUser.RoleName);
               service.AddDepartments(user, newUser.DepartementIds);
           }
           return new CreateUserResult() { CreatedUser = user, Result = r };
        }
        //Added internal class om zowel het resultaat als de nieuw aangemaakte gebruiker terug te geven van de async method 'CreateUser'.
        private class CreateUserResult {
            internal Gebruiker CreatedUser { get; set; }
            internal IdentityResult Result { get; set; }
        }

        // POST: Gebruikers/Create
        //VB: Nieuw datamodel 'NewGebruiker' gemaakt voor deze create functie. (zie parameter)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "User,RoleName")] GenericUserFormViewModel<NewGebruiker> model)
        {
            if (ModelState.IsValid)
            {
                //code verplaatst naar nieuwe private method CreateUser()...
                var result = await CreateUser(model);
                if (result != null && result.Result.Succeeded)
                {
                    //get UID van nieuwe gebruiker en redirect naar detail pagina van gebruiker.
                    return RedirectToAction("Details", new { Id = result.CreatedUser.Id });
                }
            }
            return View(model);
        }

        // POST: Gebruikers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Gebruiker gebruiker = await service.UserManager.FindByIdAsync(id); // db.Users.Find(id);           
            if (gebruiker != null)
            {
                //is default 'admin' user? abort delete! cannot delete 'admin' user!
                if (gebruiker.UserName == "admin")
                    return new PartialViewResult();
                service.UserManager.Delete(gebruiker);
                //db.Users.Remove(gebruiker);
                //db.SaveChanges();
            }
            return PartialView("_UsersListControl", service.getUsers());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            { uow.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
