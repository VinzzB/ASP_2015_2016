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

namespace Asp_Mvc_2015_2016.Controllers
{
    //VB: Added 'Authorize' annotation, extendable with userroles ("System Administrator, ...")
    [Authorize]
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
            return View(db.Users.ToList());
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
            if (gebruiker == null)
            {
                return HttpNotFound();
            }
            return View(gebruiker);
        }

        // GET: Gebruikers/Create
        public ActionResult Create()
        {
            return View();
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
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
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
            return View(gebruiker);
        }

        // POST: Gebruikers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Voornaam,Achternaam,Email,PhoneNumber,Gsm")] Gebruiker gebruiker)
        {
            //zoek gebruiker in Db
            var gebr = db.Users.Find(gebruiker.Id);
            //update gebruiker met huidig model.
            TryUpdateModel(gebr);

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
            return View(gebruiker);
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
