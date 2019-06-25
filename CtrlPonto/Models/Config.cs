using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace CtrlPonto.Models
{
    public class Config
    {
        private static Config config;

        public TimeSpan HoraAlmoco { get; set; }
        public TimeSpan ToleranciaTrabalho { get; set; }
        public TimeSpan ToleranciaHoraExtra { get; set; }

        private Config()
        {
            this.HoraAlmoco = new TimeSpan(01, 00, 00);
            this.ToleranciaTrabalho = new TimeSpan(00, 10, 00);
            this.ToleranciaHoraExtra = new TimeSpan(00, 46, 00);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Config getInstance()
        {
            if(config == null)
            {
                config = new Config();
            }

            return config;
        }
    }
}