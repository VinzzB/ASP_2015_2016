﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asp_Mvc_2015_2016.Models;
using Asp_Mvc_2015_2016.DAL;

namespace Asp_Mvc_2015_2016.Controllers
{
    public class KlantController : BaseController
    {
        private IUnitOfWork unitOfWork;
        //KlantRepository repo;
        //private FacturatieDBContext db = new FacturatieDBContext();

        public KlantController(IUnitOfWork uow)
        {
            //repo = new KlantRepository();
            unitOfWork = uow;
        }

        // GET: Klant
        public ActionResult Index()
        {
            //return View(repo.GetAll());
            //return View(db.Klanten.ToList());
            return View(unitOfWork.KlantRepository.GetAll());
            
        }

        // GET: Klant/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Klant klant = db.Klanten.Find(id);
            Klant klant = unitOfWork.KlantRepository.GetById(id);
            if (klant == null)
            {
                return HttpNotFound();
            }
            return View(klant);
        }

        //[ChildActionOnly]
        //public PartialViewResult _KlantDepartement(Klant k)
        //{

        //    List<Departement> departementen = ((DepartementRepository)unitOfWork.DepartementRepository).getDepartementenByKlant(k);
        //    return null; //TODO !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 
        //}

        // GET: Klant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Klant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ondernemingsnr,NaamBedrijf, StraatNr, Postcode, Plaats")] Klant klant)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.KlantRepository.Add(klant);
                unitOfWork.Save();
                //db.Klanten.Add(klant);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klant);
        }

        // GET: Klant/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Klant klant = db.Klanten.Find(id);
            Klant klant = unitOfWork.KlantRepository.GetById(id);
            if (klant == null)
            {
                return HttpNotFound();
            }
            return View(klant);
        }

        // POST: Klant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //tegen spoofing, checken dat data van onze form binnenkomt
        public ActionResult Edit([Bind(Include = "Id,Ondernemingsnr,NaamBedrijf, StraatNr, Postcode, Plaats")] Klant klant) // de props Gebruikers, Departementen, CreatedBy en EditedBy worden zo excluded en kunnen niet geset worden zonder constructor die voorwaarden kan stelden aan bvb Gebruiker
        {
            if (ModelState.IsValid)
            {
                unitOfWork.KlantRepository.context.Entry(klant).State = EntityState.Modified;
                //db.Entry(klant).State = EntityState.Modified;
                unitOfWork.Save();
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klant);
        }

        // GET: Klant/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klant klant = unitOfWork.KlantRepository.GetById(id);
            //Klant klant = db.Klanten.Find(id);
            if (klant == null)
            {
                return HttpNotFound();
            }
            return View(klant);
        }

        // POST: Klant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Klant klant = db.Klanten.Find(id);
            Klant klant = unitOfWork.KlantRepository.GetById(id);
            //db.Klanten.Remove(klant);
            unitOfWork.KlantRepository.Delete(id);
            //db.SaveChanges();
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
