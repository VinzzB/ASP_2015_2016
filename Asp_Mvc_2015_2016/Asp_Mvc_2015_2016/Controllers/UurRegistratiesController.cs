using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp_Mvc_2015_2016.Models;
//using Asp_Mvc_2015_2016.Models.DAL;
using Asp_Mvc_2015_2016.DAL;
using Asp_Mvc_2015_2016.DAL.Services;
using Asp_Mvc_2015_2016.ViewModels;

namespace Asp_Mvc_2015_2016.Controllers
{
    public class UurRegistratiesController : BaseController
    {
        private IUnitOfWork uow;
        private IUurRegistratieService service;

        public UurRegistratiesController(IUnitOfWork uow, IUurRegistratieService service)
        {
            this.uow = uow;
            this.service = service;
        }

        //
        // GET: /UurRegistraties/
        public ViewResult Index()
        {
            return View(uow.UurRegistratieRepository.DbSet.ToList());
        }

        public ViewResult Create(int DetailId)
        {
            FactuurDetails fd = uow.FactuurDetailsRepository.GetById(DetailId);
            return View(new UurRegistratieViewModel()
            {                
                UurRegistratie = new UurRegistratie() { FactuurDetailId = DetailId, FactuurDetail = fd},
                AvailableTypeWerk = service.AllAvailableTypenWerk() // uow.TypeWerkRepository.DbSet.Where(p=>p.GeldigVanaf>=DateTime.Today ).ToList().ConvertAll(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.WerkType })
            });
        }



        ////
        //// POST: /UurRegistraties/Create
        [HttpPost]
        public ActionResult Create(UurRegistratieViewModel viewModel, string returnPage)
        {
            try 
	        {	        		
                if (ModelState.IsValid)
                {
                    uow.UurRegistratieRepository.Add(viewModel.UurRegistratie);
                    uow.Save();
                    //get data from db including all the hours in the details.
                    viewModel.UurRegistratie = uow.UurRegistratieRepository
                        .AllIncluding(p => p.FactuurDetail.uurRegistratie, p => p.TypeWerk)
                        .SingleOrDefault(u => u.Id == viewModel.UurRegistratie.Id);
                    return getPartialResults(returnPage, viewModel.UurRegistratie.FactuurDetailId);
                    //return RedirectToAction("details", new { controller = "FactuurDetails", id = viewModel.UurRegistratie.FactuurDetailId });
                }
	        }
	        catch (Exception ex)
	        {
                Response.StatusCode = 300;
                return Json(new { error = ex.Message }, "application/json", JsonRequestBehavior.AllowGet);		        
	        }

            //Viewmodel terug opvullen met metadata als er fouten in het formulier staan.
            //viewModel.UurRegistratie.FactuurDetail = uow.FactuurDetailsRepository.GetById(viewModel.UurRegistratie.FactuurDetailId);
            //viewModel.AvailableTypeWerk = uow.TypeWerkRepository.GetAll().ConvertAll(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.WerkType });
            //return new HttpStatusCodeResult(300, "Fout bij ophalen van gegevens...");
            Response.StatusCode = 300;
            return Json(new { error = "Fout bij het opslaan van gegevens." }, "application/json", JsonRequestBehavior.AllowGet);
            //return new PartialViewResult();
            //return View(viewModel);
        }

        //
        // GET: /UurRegistraties/Details/5
        public ViewResult Details(int id)
        {
            UurRegistratie uurregistratie = uow.UurRegistratieRepository.GetById(id);
            return View(new UurRegistratieViewModel()
            {
                UurRegistratie = uurregistratie,
                AvailableTypeWerk = service.AllAvailableTypenWerk()
            });
        }
        
        ////
        //// GET: /UurRegistraties/Edit/5
        public ActionResult Edit(int id)
        {
            UurRegistratie uurregistratie = uow.UurRegistratieRepository.GetById(id);
            return View(new UurRegistratieViewModel()
            {
                UurRegistratie = uurregistratie,
                AvailableTypeWerk = service.AllAvailableTypenWerk()
            });
        }

        ////
        //// POST: /UurRegistraties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UurRegistratieViewModel viewmodel, string returnPage)
        {
            if (ModelState.IsValid)
            {
                var ur = uow.UurRegistratieRepository.AllIncluding(p => p.FactuurDetail.uurRegistratie).SingleOrDefault(u => u.Id == viewmodel.UurRegistratie.Id);
                TryUpdateModel(ur, "UurRegistratie");
                uow.Save();
                return getPartialResults(returnPage, ur.FactuurDetailId);
            }            
            return View(viewmodel);
        }

        private ActionResult getPartialResults(string returnPage, int factuurDetailId) // UurRegistratie ur)
        { 
                switch (returnPage)
                {
                    case "UnbilledList":
                        return PartialView("~/Views/FactuurDetails/_UnbilledList.cshtml", service.NietGefactureerdeFactuurDetails());
                    case "UnbilledDetailList":
                        return PartialView("_UnbilledList", service.GetFactuurDetail(factuurDetailId));
                    default:
                        return RedirectToAction("Details", new { controller = "FactuurDetails", id = factuurDetailId });
                }                         
        }

        ////
        //// GET: /UurRegistraties/Delete/5
        public ActionResult Delete(int id)
        {
            UurRegistratie uurregistratie =  uow.UurRegistratieRepository.GetById(id);
            return View(uurregistratie);
        }

        ////
        //// POST: /UurRegistraties/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UurRegistratie uurregistratie = uow.UurRegistratieRepository.GetById(id);
           // int fd_uid = uurregistratie.FactuurDetailId;
            //FactuurDetails fd = uow.FactuurDetailsRepository.GetById(uurregistratie.FactuurDetailId); // uow.FactuurDetailsRepository.AllIncluding(p => p.uurRegistratie).SingleOrDefault(p => p.dfdId == id);
            //if (uurregistratie.Factuur == null) {
                uow.UurRegistratieRepository.Delete(id);
                uow.Save();
            //}
                return getPartialResults("UnbilledDetailList",uurregistratie.FactuurDetailId);// RedirectToAction("UnbilledList", new { controller = "FactuurDetails" });
        }

        public ActionResult Load_Form(int? detailId, int? id, String type, string returnPage)
        {
            ViewBag.returnPage = returnPage;

            FactuurDetails fd = uow.FactuurDetailsRepository.GetById(detailId??-1);
            //return View(new UurRegistratieViewModel()
            //{
            //    UurRegistratie = new UurRegistratie() { FactuurDetailId = DetailId, FactuurDetail = fd },
            //    AvailableTypeWerk = uow.TypeWerkRepository.GetAll().ConvertAll(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.WerkType })
            //});

            if (ModelState.IsValid)
            {
                return PartialView("_" + type, new UurRegistratieViewModel()
                {
                    UurRegistratie = uow.UurRegistratieRepository.DbSet.SingleOrDefault(f => f.Id == id) ?? new UurRegistratie() { FactuurDetailId = detailId??-1, FactuurDetail = fd },
                    AvailableTypeWerk = service.AllAvailableTypenWerk()
                });
            }
            Response.StatusCode = 300;
            return Json(new { error = "Fout bij ophalen van gegevens..." }, "application/json", JsonRequestBehavior.AllowGet);
            //return new HttpStatusCodeResult(300, "Fout bij ophalen van gegevens...");// new PartialViewResult();
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