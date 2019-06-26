using CtrlPonto.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlPonto.Models
{
    public class DataUtil
    {
        private DataUtil() { }

        public static TimeSpan calculaHorasTrabalhadas(List<Trabalho> trabalhos)
        {
            TimeSpan horasTrabalhadas = new TimeSpan(0, 0, 0);

            trabalhos.ForEach(x => horasTrabalhadas = horasTrabalhadas.Add(x.HorasTrabalho));

            return horasTrabalhadas;
        }

        public static TimeSpan calculaSaldo(List<Trabalho> trabalhos)
        {
            TimeSpan saldo = new TimeSpan(0, 0, 0);

            trabalhos.ForEach(x =>
                saldo = saldo.Add(x.Saldo)
            );

            return saldo;
        }

        public static TimeSpan calculaSaldo(Trabalho trabalho)
        {
            Config config = Config.getInstance();
            TimeSpan saldo = trabalho.HorasTrabalho.Subtract(trabalho.Jornada);

            if (saldo <= config.ToleranciaHoraExtra && saldo >= -config.ToleranciaTrabalho)
            {
                saldo = new TimeSpan(00, 00, 00);
            }

            return saldo;
        }

        public static string formataHora(TimeSpan hora)
        {
            string horaFormatada;

            if (hora.TotalHours != 0 && hora.TotalMinutes != 0)
            {
                horaFormatada = (hora.Days * 24 + hora.Hours).ToString("D2");
                horaFormatada += ":";
                horaFormatada += (hora.Minutes > 0 ? hora.Minutes : -hora.Minutes).ToString("D2");

            }
            else
            {
                horaFormatada = "00:00";
            }

            return horaFormatada;
        }

        public static TimeSpan calculaJornada(List<Trabalho> trabalhos)
        {
            TimeSpan jornada = new TimeSpan(0, 0, 0);

            trabalhos.ForEach(x =>
                jornada = jornada.Add(x.Jornada)
            );

            return jornada;
        }

        public static TimeSpan calculaHorasTrabalho(List<Ponto> listPontos)
        {
            TimeSpan horasTrabalhadas = new TimeSpan();
            TimeSpan entrada = new TimeSpan();
            TimeSpan saida = new TimeSpan();

            foreach (Ponto ponto in listPontos)
            {
                if (ponto.Tipo == EnumExtensions.TipoPontoToDescriptionString(TipoPonto.ENTRADA))
                {
                    entrada = ponto.Hora.TimeOfDay;
                    continue;
                }
                else { saida = ponto.Hora.TimeOfDay; }

                horasTrabalhadas += saida - entrada;
            }

            return horasTrabalhadas;
        }
    }
}