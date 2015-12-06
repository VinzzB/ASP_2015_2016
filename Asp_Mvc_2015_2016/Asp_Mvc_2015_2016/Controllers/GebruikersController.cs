using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asp_Mvc_2015_2016.Models;

namespace Asp_Mvc_2015_2016.Controllers
{
    public class GebruikersController : CultureController // Controller
    {
        private FacturatieDBContext db = new FacturatieDBContext();

        // GET: Gebruikers
        public ActionResult Index()
        {
            return View(db.Gebruikers.ToList());
        }

        // GET: Gebruikers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gebruiker gebruiker = db.Gebruikers.Find(id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,Voornaam,Achternaam,Email,Tel,Gsm")] Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                db.Gebruikers.Add(gebruiker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gebruiker);
        }

        // GET: Gebruikers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gebruiker gebruiker = db.Gebruikers.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Login,Voornaam,Achternaam,Email,Tel,Gsm")] Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gebruiker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gebruiker);
        }

        // GET: Gebruikers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gebruiker gebruiker = db.Gebruikers.Find(id);
            if (gebruiker == null)
            {
                return HttpNotFound();
            }
            return View(gebruiker);
        }

        // POST: Gebruikers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gebruiker gebruiker = db.Gebruikers.Find(id);
            db.Gebruikers.Remove(gebruiker);
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
