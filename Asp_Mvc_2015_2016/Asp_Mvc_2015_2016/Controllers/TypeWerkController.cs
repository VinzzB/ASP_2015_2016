using Asp_Mvc_2015_2016.DAL;
using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp_Mvc_2015_2016.Controllers
{
    [Authorize(Roles="Systeem Administrator")]
    public class TypeWerkController : Controller
    {
        private readonly IUnitOfWork uow;

        public TypeWerkController() { }
        public TypeWerkController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        // POST: TypeWerk/Create
        [HttpPost]
        public ActionResult Create(TypeWerk model)
        {
            try
            {
                // TODO: Add insert logic here
                uow.TypeWerkRepository.Add(model);
                uow.Save();
                return PartialView("_TypeWerkList", uow.TypeWerkRepository.GetAll());
            }
            catch
            {
                Response.StatusCode = 300;
                return Json(new { error = "Fout bij opslaan van gegevens..." }, "application/json", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: TypeWerk/Edit/5
        [HttpPost]
        public ActionResult Edit(TypeWerk model)
        {
            try
            {
                TypeWerk dbModel = uow.TypeWerkRepository.GetById(model.Id);
                TryUpdateModel(dbModel);
                uow.Save();
                return PartialView("_TypeWerkList", uow.TypeWerkRepository.GetAll());
            }
            catch
            {
                Response.StatusCode = 300;
                return Json(new { error = "Fout bij opslaan van gegevens..." }, "application/json", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: TypeWerk/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                uow.TypeWerkRepository.Delete(id);
                uow.Save();
                return PartialView("_TypeWerkList", uow.TypeWerkRepository.GetAll());
            }
            catch
            {
                Response.StatusCode = 300;
                return Json(new { error = "Fout bij verwijderen van gegevens..." }, "application/json", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Load_Form(int? id, String type, string createOrEdit = "Create")
        {
            ViewBag.PageType = createOrEdit;
            if (ModelState.IsValid)
            {
                return PartialView("_" + type, uow.TypeWerkRepository.GetById(id??-1)??new TypeWerk());
            }
            Response.StatusCode = 300;
            return Json(new { error = "Fout bij ophalen van gegevens..." }, "application/json", JsonRequestBehavior.AllowGet);
        }

    }
}
