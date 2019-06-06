using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlPonto.Models.Domain
{
    public class Erro
    {
        private string mErro { get; set; }

        public Erro()
        {
            mErro = "";
        }
        /// <summary>
        /// Retorna a Mensagem de erro
        /// </summary>
        /// <returns></returns>
        public string getMensagemErro()
        {
            return mErro;
        }

        public void setMensagemErro(string mErro)
        {
            this.mErro = mErro;
        }


        public Boolean isErro()
        {
            if (getMensagemErro().Trim().Length > 0)
            {
                return true;
            }

            return false;
        }

        public string MensagemErroFormatada()
        {
            if (isErro())
            {
                return "Um Erro Inesperado aconteceu !!!<br />Procure departamento responsável e relate o erro abaixo<br /><br />" + getMensagemErro();
            }

            return "Operação realizada com sucesso !!!";
        }
    }
}