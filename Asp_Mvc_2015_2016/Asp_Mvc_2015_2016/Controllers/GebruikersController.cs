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

namespace Asp_Mvc_2015_2016.Controllers
{
    //VB: Added 'Authorize' annotation, extendable with userroles ("System Administrator, ...")
    [Authorize(Roles = "Systeem Administrator")]
    public class GebruikersController : BaseController // Controller
    {
        private FacturatieDBContext db = new FacturatieDBContext();
        
        //VB: Added UserManager to create users (with a given password...)
        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }  
        }
        
        // GET: Gebruikers
        //View is now with ajax calls. See ViewModel 'GebruikersIndexViewModel'
        public ActionResult Index()
        {
            return View(new GebruikersIndexViewModel<NewGebruiker>(getUsers(), db.Roles.ToList().ConvertAll(p => p.Name))); //model = ajax viewmodel! 
        }

        //VB: added for ajax calls on Create gebruiker -> updates user list
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> User_Create(GenericUserFormViewModel<NewGebruiker> viewmodel)
        {
            if (ModelState.IsValid) {
                //create the user in db and check for errors
                var r = await CreateUser(viewmodel);
                if (r.Result.Succeeded) {
                    //all OK. return new userlist.
                    return PartialView("_UsersListControl", getUsers());
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
                var gebr = await UserManager.FindByIdAsync(viewmodel.User.Id);

                if (gebr == null) {
                    return new HttpNotFoundResult("Gebruiker niet gevonden.");
                }

                //allow role changes for all users except 'admin'!
                if (gebr.UserName != "admin")
                {
                    //Haal huidige rolnaam op (via UserManager!)
                    var userRoles = await UserManager.GetRolesAsync(gebr.Id);
                    //heeft nog geen rol of de rol werd gewijzigd?
                    if (userRoles.Count == 0 || userRoles.ElementAt(0) != viewmodel.RoleName)
                    {
                        if (userRoles.Count > 0)//verwijder eerst de huidige rol (als er één is)
                        {
                            await UserManager.RemoveFromRoleAsync(gebr.Id, userRoles.ElementAt(0));
                        }
                        await UserManager.AddToRoleAsync(gebr.Id, viewmodel.RoleName); //add nieuwe rol
                    }
                }
                
                //update model met input.
                TryUpdateModel(gebr, "User");
                //save in db. (check for errors)
                var r = await UserManager.UpdateAsync(gebr);
                if (!r.Succeeded)
                {
                    Response.StatusCode = 300;
                    var jdata = new { error = r.Errors.ElementAt(0) };
                    return Json(jdata, "application/json", JsonRequestBehavior.AllowGet);
                }
                //all OK. return new user list.
                return PartialView("_UsersListControl", getUsers());
            }
            //Something happend....
            return new PartialViewResult();
        }

        /// <summary>
        /// Loads a Userform with Ajax.
        /// </summary>
        /// <param name="id">The UserID</param>
        /// <param name="type">Load form for 'new' or 'edit'</param>
        /// <returns></returns>
        public async Task<ActionResult> Load_User_Form(String id, String type) { // type can be 'new' or 'edit'
            if (ModelState.IsValid) {
                Gebruiker u = null;
                if (id!=null)
                    u = await db.Users.FirstOrDefaultAsync(p => p.Id == id); //get user async.
               //System.Threading.Thread.Sleep(500); //--> for debugging!!! (mimics a slow server response, for debugging wait messages in the browser)                
                if (u != null || type == "new") //has a user OR is type new. (type 'new' does not need a user)
                {
                    switch (type)
                    {
                        case "edit":
                            return PartialView("_FormEditUser", new GenericUserFormViewModel<Gebruiker>(u, GetUserRole(u.Id), db.Roles.ToList().ConvertAll(p => p.Name)));
                        case "new":                            
                            return PartialView("_FormCreateUser", new GenericUserFormViewModel<NewGebruiker>(new NewGebruiker(), db.Roles.ToList().ConvertAll(p => p.Name)));
                        case "del":
                            return PartialView("_UserRemove", new GenericUserFormViewModel<Gebruiker>(u, GetUserRole(u.Id)));
                        default: ///details
                            return PartialView("_UserDetails", new GenericUserFormViewModel<Gebruiker>(u, GetUserRole(u.Id)));
                    }
                }
            }
            return new HttpStatusCodeResult(300, "Fout bij ophalen van gegevens...");// new PartialViewResult();
        }


        /* DIT ZOU EEN CALL MOETEN ZIJN IN DE DB GEBRUIKER SERVICE!!! */
        private List<GenericUserFormViewModel<Gebruiker>> getUsers() {
            //Rol zichtbaar maken in lijstweergave...
            //VB: from http://stackoverflow.com/questions/26078271/getting-a-list-of-users-with-their-assigned-role-in-identity-2
            //refactored zodat ook users waar nog geen rol aan toegewezen werd ook zichtbaar zijn... normaal moet er altijd minstens één rol zijn. 

            //Bij verplichte rol: 
            //var users = from u in db.Users
            //            from ur in u.Roles
            //            join r in db.Roles on ur.RoleId equals r.Id 
            //            select new GebruikersIndex() { User = u, Role = r };

            return (from u in db.Users //get alle users
                    from ur in u.Roles.DefaultIfEmpty() //get rolIDs van user (add a default on empty) (=tabel GebruikersRollen)
                    join r in db.Roles on ur.RoleId equals r.Id into tmp_ur //join met de tabel Rollen
                    from userrole in tmp_ur.DefaultIfEmpty() //for outerjoin on roles!
                    select new GenericUserFormViewModel<Gebruiker>() 
                    { 
                        User = u, 
                        RoleName = userrole.Name 
                    }).ToList(); //create our model.
        }

        private string GetUserRole(string userId) {
            var roles = UserManager.GetRoles(userId);
            return roles != null && roles.Count > 0 ? roles.ElementAt(0) : null;
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
           var r = await UserManager.CreateAsync(user, newUser.User.Password);
            if (r.Succeeded)
                r = await UserManager.AddToRoleAsync(user.Id, newUser.RoleName);
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
            Gebruiker gebruiker = await UserManager.FindByIdAsync(id); // db.Users.Find(id);           
            if (gebruiker != null)
            {
                //is default 'admin' user? abort delete! cannot delete 'admin' user!
                if (gebruiker.UserName == "admin")
                    return new PartialViewResult();
                UserManager.Delete(gebruiker);
                //db.Users.Remove(gebruiker);
                //db.SaveChanges();
            }
            return PartialView("_UsersListControl", getUsers().ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            { db.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
