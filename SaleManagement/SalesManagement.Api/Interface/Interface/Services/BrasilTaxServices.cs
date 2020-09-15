using System;
using System.Collections.Generic;
using System.Text;

namespace Interface.Services
{
    class BrasilTaxServices : ITaxService
    {
        public double Tax(double amount)
        {
            if(amount <= 100.0)
            {
                return amount * 0.2;
            }
            else
            {
                return amount * 0.15;
            }
        }
    }
}
