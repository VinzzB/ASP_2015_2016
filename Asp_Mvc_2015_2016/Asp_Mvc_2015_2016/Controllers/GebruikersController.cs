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
using Microsoft.AspNet.Identity.EntityFramework;

namespace Asp_Mvc_2015_2016.Controllers
{
    //VB: Added 'Authorize' annotation, extendable with userroles ("System Administrator, ...")
    [Authorize(Roles = "Systeem Administrator, Departement Administrator")]
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
        public ActionResult Index()
        {
             //Rol zichtbaar maken in lijstweergave...
             //VB: from http://stackoverflow.com/questions/26078271/getting-a-list-of-users-with-their-assigned-role-in-identity-2
             //refactored zodat ook users waar nog geen rol aan toegewezen werd ook zichtbaar zijn... normaal moet er altijd minstens één rol zijn. 
                    var users = from u in db.Users //get alle users
                    from ur in u.Roles.DefaultIfEmpty() //get rolIDs van user (add a default on empty) (=tabel GebruikersRollen)
                    join r in db.Roles on ur.RoleId equals r.Id into tmp_ur //join met de tabel Rollen
                    from userrole in tmp_ur.DefaultIfEmpty() //for outerjoin on roles!
                                select new GebruikersIndexViewModel() { User = u, RoleName = userrole.Name }; //create our model.
            
            //Bij verplichte rol: 
                    //var users = from u in db.Users
                    //            from ur in u.Roles
                    //            join r in db.Roles on ur.RoleId equals r.Id 
                    //            select new GebruikersIndex() { User = u, Role = r };

            return View(users); //model = users data.
        }

        private string GetUserRole(string userId) {
            var roles = UserManager.GetRoles(userId);
            return roles != null && roles.Count > 0 ? roles.ElementAt(0) : null;
        }

        // GET: Gebruikers/Details/5
        //VB: param type gewijzigd naar guid string. (TODO: GUID mag ook?)
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gebruiker gebruiker = db.Users.Find(id);
            String Role = GetUserRole(id); //vb: haal rol van gebruiker op.
            if (gebruiker == null)
            {
                return HttpNotFound();
            }
            return View(new GebruikersIndexViewModel(gebruiker,Role)); //set viewModel.
        }

        // GET: Gebruikers/Create
        public ActionResult Create()
        {
            //VB: create a list for the Roles combobox (default = Gebruiker)
            ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name", "Gebruiker"); //id and value are the same! (= Both Role Name, not id)
            //bind a NewGebruiker class to the view
            NewGebruiker model = new NewGebruiker();
            return View(model);
        }

        // POST: Gebruikers/Create
        //VB: Nieuw datamodel 'NewGebruiker' gemaakt voor deze create functie. (zie parameter)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewGebruiker model)
        {

            if (ModelState.IsValid)
            {
                //Create Gebruiker object
                Gebruiker user = new Gebruiker()
                {
                    UserName = model.UserName,
                    Voornaam = model.Voornaam,
                    Achternaam = model.Achternaam,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Gsm = model.Gsm
                };
                //Save Gebruiker with UserManager (stores Password)
                //VB: ADDED ContinueWith() lambda to add the specified role after user creation.
                var result = await UserManager.CreateAsync(user, model.Password)
                    .ContinueWith(e => UserManager.AddToRoleAsync(user.Id, model.RoleName));                
                if (result != null && result.Result.Succeeded)
                {
                    //  db.Users.Add(user);
                    //  db.SaveChanges();
                    return RedirectToAction("Details", new { Id = user.Id });
                }
            }

            return View(model);
        }

        // GET: Gebruikers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gebruiker gebruiker = db.Users.Find(id);            
            if (gebruiker == null)
            {
                return HttpNotFound();
            }
            //get user role
            String role = GetUserRole(id);
            //set combobox items for Roles
            //Maak model: param 1: user class -- param2: userRole -- Param3: All Possible roles in combobox. (converteer IdentityRole naar String (p.name))
            return View(new GebruikersIndexViewModel(gebruiker, role, db.Roles.ToList().ConvertAll(p => p.Name)));
        }

        // POST: Gebruikers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User,RoleName")] GebruikersIndexViewModel EditData, string Id)
        {
            //VB: EDITED BINDS -> Nu via een ViewModel 'GebruikersIndexViewModel' (om Rol toe te voegen)
            //BINDS toegevoegd op Gebruiker classe!!!
            
            //zoek gebruiker in Db
            var gebr = db.Users.Find(EditData.User.Id);

            //changed roles? (TODO: check if user is in role!!!)
            //Haal huidige rolnaam op (via UserManager!)
            var userRoles = UserManager.GetRoles(Id);
            //heeft nog geen rol of de rol werd gewijzigd?
            if (userRoles.Count == 0 || userRoles.ElementAt(0) != EditData.RoleName)
            {
                if (userRoles.Count > 0)//verwijder huidig (als er één is)
                {
                    UserManager.RemoveFromRole(Id, userRoles.ElementAt(0)); 
                }
                UserManager.AddToRole(Id, EditData.RoleName); //add nieuwe rol
            }

            //update gebruiker met huidig model.
            TryUpdateModel(gebr,"User");

            if (ModelState.IsValid)
            {
                // db.Entry(gebruiker).State = EntityState.Modified;
                try
                {                    
                    db.SaveChanges();

                }
                //VB: for debugging sake! (TODO: return as errors on screen...)
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw ex;
                }

                return RedirectToAction("Index");
            }
            return View(EditData);
        }

        // GET: Gebruikers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gebruiker gebruiker = db.Users.Find(id);
            if (gebruiker == null)
            {
                return HttpNotFound();
            }
            return View(gebruiker);
        }

        // POST: Gebruikers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gebruiker gebruiker = db.Users.Find(id);
            db.Users.Remove(gebruiker);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
