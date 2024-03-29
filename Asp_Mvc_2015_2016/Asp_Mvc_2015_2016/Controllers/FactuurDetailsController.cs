using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp_Mvc_2015_2016.Models;
using Asp_Mvc_2015_2016.DAL;
using Asp_Mvc_2015_2016.DAL.Services;
using Asp_Mvc_2015_2016.ViewModels;
using Microsoft.AspNet.Identity;
namespace Asp_Mvc_2015_2016.Controllers
{
    public class FactuurDetailsController : BaseController
    {
        private IUnitOfWork uow;
        private IUurRegistratieService service;

        public FactuurDetailsController(IUnitOfWork uow, IUurRegistratieService service)
        {
            this.uow = uow;
            this.service = service;
        }

        //  GET: /FactuurDetails/
        public ViewResult Index()
        {
            return UnbilledList();
        }

        public ViewResult UnbilledList()
        {
            return View(service.NietGefactureerdeFactuurDetails().ToList());
        }

        public ViewResult BilledList()
        {
            return View(service.GefactureerdeFactuurDetails().ToList());
        }                       

        //
        // GET: /FactuurDetails/Details/5
        public ViewResult Details(int id)
        {
            var res = uow.FactuurDetailsRepository.AllIncluding(p=> p.uurRegistratie).SingleOrDefault(u => u.Id == id);
            return View(res); 
        }

        //
        // GET: /FactuurDetails/Create

        public ActionResult Create()
        {           
            return View(new ViewModels.FactuurDetailsViewModel() 
            {                 
                FactuurDetails = new FactuurDetails(), 
                AvailableKlanten = service.GetGebruikerKlanten()
            });
        } 

        //
        // POST: /FactuurDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FactuurDetailsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    uow.FactuurDetailsRepository.Add(viewModel.FactuurDetails);
                    uow.Save();
                   // return RedirectToAction("Details", new { id = viewModel.FactuurDetails.Id });
                    return Json(new 
                    { 
                        redirect = Url.Action("Details", new { id = viewModel.FactuurDetails.Id }) 
                    }, "application/json", JsonRequestBehavior.AllowGet);
                    //return PartialView("_UnbilledList", service.NietGefactureerdeFactuurDetails().ToList());
                }
                catch (Exception ex)
                {
                    //errored -> return json with error object.
                    Response.StatusCode = 300;
                    return Json(new { error = ex.Message }, "application/json", JsonRequestBehavior.AllowGet);
                }
            }
            return new PartialViewResult();
        }
        
        //
        // GET: /FactuurDetails/Edit/5
        public ActionResult Edit(int id)
        {
            var fd = uow.FactuurDetailsRepository.AllIncluding(p => p.uurRegistratie).SingleOrDefault(u => u.Id == id); // uow.FactuurDetailsRepository.GetById(id);
            return View(new FactuurDetailsViewModel()
            {
                FactuurDetails = fd,
                AvailableKlanten = service.GetGebruikerKlanten()
            });
        }

        //
        // POST: /FactuurDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FactuurDetailsViewModel viewModel, string returnPage)
        {
            if (ModelState.IsValid)
            {
                var fd = uow.FactuurDetailsRepository.GetById(viewModel.FactuurDetails.Id);
                TryUpdateModel(fd, "FactuurDetails");
                uow.Save();
                switch (returnPage)
                {
                    case "UnbilledList":
                        return PartialView("_UnbilledList", service.NietGefactureerdeFactuurDetails());
                    case "DetailPage":
                        return PartialView("_DetailsElement", fd);
                    default:
                         return RedirectToAction("Details", new { id = fd.Id });;
                }

                //if (returnPage == "UnbilledList") {
                //    return PartialView("_UnbilledList", service.NietGefactureerdeFactuurDetails());
                //} else {
                //    return RedirectToAction("Details", new { id = fd.Id });
                //}                
            }
            viewModel.AvailableKlanten = service.GetGebruikerKlanten();
            return View(viewModel);
        }

        ////
        //// GET: /FactuurDetails/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var fd = uow.FactuurDetailsRepository.GetById(id);
        //    return View(new FactuurDetailsViewModel() { 
        //        FactuurDetails = fd
        //    });
        //}

        //
        // POST: /FactuurDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            uow.FactuurDetailsRepository.Delete(id);
            uow.Save();
            return PartialView("_UnbilledList", service.NietGefactureerdeFactuurDetails());
            //return RedirectToAction("UnbilledList");
        }


        public ActionResult Load_Form(int? id, String type, string returnPage)
        {
            ViewBag.returnPage = returnPage;
            if (ModelState.IsValid)
            {
                    return PartialView("_" + type, new FactuurDetailsViewModel()
                    {
                        FactuurDetails = uow.FactuurDetailsRepository.AllIncluding(p => p.uurRegistratie).SingleOrDefault(f => f.Id == id) ?? new FactuurDetails(),
                        AvailableKlanten = service.GetGebruikerKlanten()
                    });
            }
            return new HttpStatusCodeResult(300, "Fout bij ophalen van gegevens...");// new PartialViewResult();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

