using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiP3Demo.Services
{
    public class TaxService
    {
        public decimal ComputeTax(string isoCode, decimal bedrag)
        {
            switch (isoCode)
            {
                case "BE":
                case "BEL":
                    return bedrag * 0.21M;
                default:
                    throw new NotSupportedException($"Land {isoCode} wordt nog niet ondersteund." );
            }
        }
    }
}
