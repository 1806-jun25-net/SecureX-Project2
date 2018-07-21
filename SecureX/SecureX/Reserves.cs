using System;
using System.Collections.Generic;
using System.Text;

namespace SecureXLibrary
{
    class Reserves
    {
        public Reserves(int funds)
        {
            Funds = funds;
        }

        private int Funds { get; set; } = 1000000; //one million
    }
}
