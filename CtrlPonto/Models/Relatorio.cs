using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlPonto.Models
{
    public class Relatorio
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public string SaldoFormatado { get; set; }
        public string HorasFormatada { get; set; }
        public TimeSpan Horas { get; set; }
        public TimeSpan Saldo { get; set; }
        public List<Trabalho> Trabalhos { get; set; }

        public Relatorio()
        {
            this.Trabalhos = new List<Trabalho>();
        }
    }
}