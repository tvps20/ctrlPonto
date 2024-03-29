﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CtrlPonto.Models.Enuns;
using Microsoft.Ajax.Utilities;

namespace CtrlPonto.Models
{
    public class Ponto
    {
        public static bool isEntrada;
        public int Id { get; set; }

        [Display(Name = "Hora")]
        [Required(ErrorMessage = "O hora deve ser informada.")]
        public DateTime Hora { get; set; }

        [Display(Name = "Tipo de Ponto")]
        [Required(ErrorMessage = "O tipo deve ser escolhido.")]
        public string Tipo { get; set; }
        public bool Ativo { get; set; }
        public int IdTrabalho { get; set; }
        public virtual Trabalho Trabalho { get; set; }

        public Ponto()
        {
            this.Hora = DateTime.Now;
            this.Id = 1;
            this.Ativo = true;
            isEntrada = true;
            this.iniciaTipo();
        }

        public void atualizaPonto()
        {
            if (isEntrada)
            {
                this.Tipo = EnumExtensions.TipoPontoToDescriptionString(TipoPonto.ENTRADA);
            }
            else
            {
                this.Tipo = EnumExtensions.TipoPontoToDescriptionString(TipoPonto.SAIDA);
            }       
        }

        public static void atualizaEntrada(Ponto ponto)
        {
            if(ponto != null)
            {
                if (ponto.Tipo.Equals(EnumExtensions.TipoPontoToDescriptionString(TipoPonto.ENTRADA)))
                {
                    isEntrada = false;
                }
                else
                {
                    isEntrada = true;
                }
            } else
            {
                isEntrada = true;
            }
            
        }

        public void iniciaTipo()
        {
            this.Tipo = !isEntrada ? EnumExtensions.TipoPontoToDescriptionString(TipoPonto.ENTRADA) : EnumExtensions.TipoPontoToDescriptionString(TipoPonto.SAIDA);
        }
    }
}