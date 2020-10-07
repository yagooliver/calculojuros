using System;
using System.Collections.Generic;
using System.Text;

namespace CalculaJuros.Domain.Shared.Notificacao
{
    public class NotificacaoDominio : Evento
    {
        public NotificacaoDominio(string key, string value)
        {
            Chave = key;
            Valor = value;
        }

        public string Chave { get; set; }
        public string Valor { get; set; }
    }
}
