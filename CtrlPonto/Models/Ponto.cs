using System;
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
            this.Tipo = Enuns.TipoPonto.ENTRADA.ToString();
        }
    }
}