using System;
using System.Collections.Generic;
using System.Text;

namespace CalculaJuros.Domain.Shared.Retornos
{
    public class ApiBadReturn
    {
        public bool success { get; set; }
        public IEnumerable<string> errors { get; set; }
    }
}
