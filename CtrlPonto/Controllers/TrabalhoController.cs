﻿using System;
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
using System.Web.WebPages;
using PagedList;

namespace CtrlPonto.Controllers
{
    public class TrabalhoController : Controller
    {
        [HttpGet]
        public ActionResult Index(int? pagina, string sortOrder = "Dia", string searchString = null)
        {
            int numeroPagina = pagina ?? 1;
            int tamanhoPagina = 5;

            List<Trabalho> trabalhos = TrabalhoRepository.listaAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                trabalhos = trabalhos.Where(t => t.Data.ToString().Contains(searchString)
                                               || t.Jornada.ToString().Contains(searchString)
                                               || t.Saldo.ToString().Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "Dia":
                    trabalhos = trabalhos.OrderByDescending(t => t.Data).ToList();
                    break;
                case "Jornada":
                    trabalhos = trabalhos.OrderBy(t => t.Jornada).ToList();
                    break;
                case "Saldo":
                    trabalhos = trabalhos.OrderBy(t => t.Saldo).ToList();
                    break;
                default:
                    trabalhos = trabalhos.OrderByDescending(t => t.Data).ToList();
                    break;
            }

            ViewBag.Trabalhos = trabalhos.ToPagedList(numeroPagina, tamanhoPagina);
            ViewBag.active = "Trabalho";
            ViewBag.SaldoFormatado = DataUtil.formataHora(DataUtil.calculaSaldo(trabalhos.ToPagedList(numeroPagina, tamanhoPagina).ToList()));
            ViewBag.Saldo = DataUtil.calculaSaldo(trabalhos.ToPagedList(numeroPagina, tamanhoPagina).ToList());
            ViewBag.Horas = DataUtil.formataHora(DataUtil.calculaHorasTrabalhadas(trabalhos.ToPagedList(numeroPagina, tamanhoPagina).ToList()));
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

                var trabalho = TrabalhoRepository.recuperarPeloId(Int32.Parse(form["Id"]));

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
                List<Ponto> pontos = PontoRepository.listAllByTrabalho(id).OrderBy(x => x.Hora).ToList();
                Ponto ponto = new Ponto();
                ViewBag.Pontos = pontos;
                ViewBag.active = "Trabalho";
                ViewBag.Ponto = ponto;

                Ponto.atualizaEntrada(pontos.Count > 0 ? pontos[pontos.Count-1] : null);
                ponto.atualizaPonto();
                trabalho.HorasTrabalho = DataUtil.calculaHorasTrabalho(pontos);
                trabalho.Saldo = DataUtil.calculaSaldo(trabalho);
                TrabalhoRepository.salvar(trabalho);

                return View(trabalho);
            }
            catch (Exception e)
            {
                return RedirectToAction("index", "Trabalho").Mensagem("Ocorreu um erro inesperado. " + e.Message, "Erro");
            }
        }

        public ActionResult ValidaData(string Data)
        {

            DateTime dataFormatada = DateTime.Parse(Data, new CultureInfo("pt-BR"));
                
            var trabalho = TrabalhoRepository.recuperarPelaData(dataFormatada);

            if (trabalho != null)
            {
                return Json(string.Format("A data '{0}' já foi cadastrada.", dataFormatada.ToString("dd/MM/yyyy")), JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }   
    }
}