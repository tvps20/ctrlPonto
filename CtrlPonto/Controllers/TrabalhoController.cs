using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Query.Dynamic;
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
            Ponto.isEntrada = false;
            return View(trabalho);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                if (ModelState.IsValid == false){ return Redirect("Trabalho/Index"); }

                Trabalho trabalho = TrabalhoRepository.recuperarPeloId(Int32.Parse(form["Id"]));

                if (trabalho == null)
                {
                    trabalho = new Trabalho();
                    trabalho.Data = DateTime.Parse(form["Data"]);
                }

                trabalho.Jornada = TimeSpan.Parse(form["Jornada"]);

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
                ViewBag.Pontos = pontos.OrderByDescending(x => x.Hora).ToList();
                ViewBag.Ponto = new Ponto();

                trabalho.Horas = calculaHorasTrabalho(pontos);
                trabalho.Saldo = trabalho.Horas.Subtract(trabalho.Jornada);
                TrabalhoRepository.salvar(trabalho);

                return View(trabalho);
            }
            catch (Exception e)
            {
                return RedirectToAction("index", "Trabalho").Mensagem("Ocorreu um erro inesperado. " + e.Message, "Erro");
            }
        }

        public ActionResult ValidaData(DateTime Data, Trabalho trabalho)         
        {
            var dataFormatada = Data.ToString("dd/MM/yyyy");
            var data = TrabalhoRepository.recuperarPelaData(dataFormatada);

            if (data != null)
            {
                return Json(string.Format("A data '{0}' já foi cadastrada.", dataFormatada), JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private TimeSpan calculaHorasTrabalho(List<Ponto> listPontos)
        {
            TimeSpan saldo = new TimeSpan();
            TimeSpan entrada = new TimeSpan();

            foreach (Ponto ponto in listPontos)
            {
                if (ponto.Tipo == EnumExtensions.TipoPontoToDescriptionString(TipoPonto.ENTRADA))
                {
                    entrada = ponto.Hora.TimeOfDay;
                }
                else
                {
                    saldo += entrada - ponto.Hora.TimeOfDay;
                }
            }

            return -saldo;
        }
    }
}