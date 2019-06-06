using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CtrlPonto.Models;
using CtrlPonto.Models.Enuns;
using CtrlPonto.Models.Repository;

namespace CtrlPonto.Controllers
{
    public class TrabalhoController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Trabalho> trabalhos = TrabalhoRepository.listaAll();
            ViewBag.Trabalhos = trabalhos.OrderByDescending(x => x.Data).ToList();
            Trabalho trabalho = new Trabalho();
            return View(trabalho);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            Trabalho trabalho = new Trabalho();
            trabalho.Jornada = DateTime.Parse(form["Jornada.TimeOfDay"]);
            trabalho.Data = DateTime.Parse(form["Data"]);

            if (ModelState.IsValid == false)
            {
                return Redirect("Trabalho/Index");
            }

            try
            {
                TrabalhoRepository.salvar(trabalho);
                return RedirectToAction("Index", "Trabalho").Mensagem("Trabalho Salvo com Sucesso!!!");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Trabalho").Mensagem("Ocorreu um erro inesperado. " + e.Message, "Erro");
            }
        }

        [HttpGet]
        public ActionResult ApagarTrabalho(int id)
        {
            try
            {
                TrabalhoRepository.excluirPeloId(id);
                return RedirectToAction("index", "Trabalho").Mensagem("Trabalho Apagado com Sucesso!!!"); ;
            }
            catch (Exception e)
            {
                return RedirectToAction("index", "Trabalho").Mensagem("Ocorreu um erro inesperado. " + e.Message, "Erro");
            }
        }

        [HttpGet]
        public ActionResult DetalheTrabalho(int id)
        {
            try
            {
                Trabalho trabalho = TrabalhoRepository.recuperarPeloId(id);
                List<Ponto> pontos = PontoRepository.listAllByTrabalho(id);
                ViewBag.Pontos = pontos.OrderByDescending(x => x.Hora.TimeOfDay).ToList();
                ViewBag.Ponto = new Ponto();

                return View(trabalho);
            }
            catch (Exception e)
            {
                return View().Mensagem("Ocorreu um erro inesperado. " + e.Message, "Erro");
            }
        }

        public ActionResult ValidaData(DateTime Data)         
        {
            var dataFormatada = Data.ToString("MM/dd/yyyy");
            var data = TrabalhoRepository.recuperarPelaData(dataFormatada);

            if (data != null)
            {
                return Json(string.Format("A data '{0}' já foi cadastrada.", dataFormatada), JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}