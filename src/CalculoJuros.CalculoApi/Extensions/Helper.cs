using System;
using System.Collections.Generic;
using System.Text;

namespace CalculoJuros.CalculoApi.Extensions
{
    public static class Helper
    {
        public static decimal Truncate(this decimal d, byte decimals)
        {
            decimal r = Math.Round(d, decimals);

            if (d > 0 && r > d)
            {
                return r - new decimal(1, 0, 0, false, decimals);
            }
            else if (d < 0 && r < d)
            {
                return r + new decimal(1, 0, 0, false, decimals);
            }

            return r;
        }

        public static string ToStringDecimal(this decimal d)
        {
            return d.ToString("0.00");
        }
    }
}
