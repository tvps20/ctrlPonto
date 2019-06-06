using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CtrlPonto.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtrlPonto.Models
{
    public class Trabalho
    {
        [Display(Name = "Codigo do Trabalho")]
        [Range(minimum:1, maximum:50, ErrorMessage = "Codigo deve esta entre 1 e 50.")]
        public int Id { get; set; }

        [Display(Name = "Dia de Trabalho")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataBrasil(ErrorMessage = "Data inválida.", DataRequerida = true)]
        [Remote("ValidaData", "Trabalho", ErrorMessage = "Data já cadastrada.")]
        public DateTime Data { get; set; }

        [Display(Name = "Jornada de Trabalho")]
        [Required(ErrorMessage = "A jornada deve ser informado.")]
        public DateTime Jornada { get; set; }

        [Display(Name = "Saldo do dia")]
        public DateTime Saldo { get; set; }

        public bool Ativo { get; set; }

        public Trabalho()
        {
            this.Ativo = true;
            this.Data = DateTime.Now;
            this.Saldo = new DateTime(1999, 01, 01, 00, 00, 00);
            this.Jornada = new DateTime(1999, 01, 01, 08, 00, 00);
            this.Id = 1;
        }
    }
}