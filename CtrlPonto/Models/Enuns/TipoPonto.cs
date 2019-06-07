using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CtrlPonto.Models.Enuns
{
    public enum TipoPonto: byte
    {
        [Description("Entrada")]
        ENTRADA = 1,

        [Description("Saída")]
        SAIDA = 2
    }
}