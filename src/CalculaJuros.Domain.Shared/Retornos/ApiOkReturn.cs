using System;
using System.Collections.Generic;
using System.Text;

namespace CalculaJuros.Domain.Shared.Retornos
{
    public class ApiOkReturn<T>
    {
        public bool success { get; set; }
        public T valor { get; set; }
    }
}
