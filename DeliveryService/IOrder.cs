using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
    internal interface IOrder
    {
        string Id { get; set; }
        float Weight { get; set; }

        DateTime DeliveryTime { get; set; }
        string GetFormatedDeliveryTime();
    }
}
