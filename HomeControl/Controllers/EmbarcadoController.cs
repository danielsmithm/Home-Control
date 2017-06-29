﻿using HomeControl.Business.Service.Base.Exceptions;
using HomeControl.Business.Service.Interfaces;
using HomeControl.Domain.Dispositivos;
using HomeControl.Business.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeControl.Domain.Residencia;
using Ninject;

namespace HomeControl.Controllers
{
    public class EmbarcadoController : Controller
    {
        private IEmbarcadoService _embarcadoService = new EmbarcadoService();
        private IComodoService _comodoService = new ComodoService();

        [Inject]
        public EmbarcadoController(IEmbarcadoService embarcadoService, IComodoService comodoService)
        {
            _embarcadoService = embarcadoService;
            _comodoService = comodoService;
        }

        // GET: Embarcado
        public ActionResult Index()
        {
            return View(_embarcadoService.FindAll());
        }

        // GET: Embarcado/Details/5
        public ActionResult Details(int id)
        {

            Embarcado embarcado = _embarcadoService.Find(id);

            if (embarcado == null)
            {
                ModelState.AddModelError("", "Embarcado não encontrada");
                return RedirectToAction("Index");
            }

            return View(embarcado);
        }

        // GET: Embarcado/Create
        public ActionResult Create()
        {
            PopulateSelectListComodo();
            return View();
        }

        // POST: Embarcado/Create
        [HttpPost]
        public ActionResult Create(Embarcado embarcado)
        {
            try
            {
                _embarcadoService.Add(embarcado);

                return RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                PopulateSelectListComodo();
                AddValidationErrorsToModelState(ex.Errors);
                return View(embarcado);
            }
        }

        // GET: Embarcado/Edit/5
        public ActionResult Edit(int id)
        {
            Embarcado embarcado = _embarcadoService.Find(id);

            if (embarcado == null)
            {
                ModelState.AddModelError("", "Embarcado não encontrada");
                return RedirectToAction("Index");
            }

            PopulateSelectListComodo();
            return View(embarcado);
        }

        // POST: embarcado/Edit/5
        [HttpPost]
        public ActionResult Edit(Embarcado embarcado)
        {
            try
            {
                _embarcadoService.Update(embarcado);

                return RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                PopulateSelectListComodo();
                AddValidationErrorsToModelState(ex.Errors);
                return View(embarcado);
            }
        }

        // GET: Embarcado/Delete/5
        public ActionResult Delete(int id)
        {
            Embarcado embarcado = _embarcadoService.Find(id);
            if (embarcado == null)
            {
                ModelState.AddModelError("", "Embarcado não Encontrado");
                return RedirectToAction("Index");
            }
            return View(embarcado);
        }

        // POST: Embarcado/Delete/5
        [HttpPost]
        public ActionResult Delete(Embarcado embarcado)
        {
            try
            {
                embarcado = _embarcadoService.Find(embarcado.Id);
                _embarcadoService.Remove(embarcado);

                return RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                AddValidationErrorsToModelState(ex.Errors);
                return View(embarcado);
            }
        }

        private void PopulateSelectListComodo()
        {
            List<Comodo> comodo = _comodoService.FindAll();
            SelectList listaOpcoesComodos = new SelectList(comodo, "id", "Nome");
            ViewBag.SelectListComodo = listaOpcoesComodos;
        }

        #region helpers
        private void AddValidationErrorsToModelState(ErrorList validationErrors)
        {
            foreach (String error in validationErrors.ErrorCodes)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion
    }
}