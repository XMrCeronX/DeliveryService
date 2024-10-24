using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
    internal class Tools
    {
        public static double GetRandomNumber(Random random, double minimum = 0, double maximum = 25)
        {
            double randVlaue = random.NextDouble();
            return randVlaue * (maximum - minimum) + minimum;
        }
    }
}
