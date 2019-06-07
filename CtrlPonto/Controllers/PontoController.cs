﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CtrlPonto.Models;
using CtrlPonto.Models.Enuns;
using CtrlPonto.Models.Repository;

namespace CtrlPonto.Controllers
{
    public class PontoController : Controller
    {
        [HttpPost]
        public ActionResult AdicionarPonto(FormCollection form)
        {
            Ponto novoPonto = new Ponto();
            novoPonto.IdTrabalho = Int32.Parse(form["Id"]);
            novoPonto.Hora = DateTime.Parse(form["pontoControle.Hora.TimeOfDay"]);
            novoPonto.atualizaPonto();       

            try
            {
                PontoRepository.salvar(novoPonto);
                return RedirectToAction("DetalheTrabalho", "Trabalho", new { id = novoPonto.IdTrabalho }).Mensagem("Ponto Adicionado com Sucesso!!!");
            }
            catch (Exception e)
            {
                return RedirectToAction("DetalheTrabalho", "Trabalho", new { id = novoPonto.IdTrabalho }).Mensagem("Ocorreu um erro inesperado. " + e.Message, "Erro");
            }
            
        }

        [HttpGet]
        public ActionResult ApagarPonto(int id, int idTrabalho)
        {
            try
            {
                Ponto pontoBd = PontoRepository.recuperarPeloId(id);
                Ponto.isEntrada = pontoBd.Tipo.Equals(EnumExtensions.TipoPontoToDescriptionString(TipoPonto.SAIDA));
                PontoRepository.excluirPeloId(id);
                return RedirectToAction("DetalheTrabalho", "Trabalho", new { id = idTrabalho }).Mensagem("Ponto Apagado com Sucesso!!!"); ;
            }
            catch (Exception e)
            {
                return RedirectToAction("DetalheTrabalho", "Trabalho", new { id = idTrabalho }).Mensagem("Ocorreu um erro inesperado. " + e.Message, "Erro");
            }
        }
    }
}