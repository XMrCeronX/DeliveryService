using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
    public class Order : IOrder
    {
        public const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public string Id { get; set; }
        public float Weight { get; set; }
        public OrderArea OrderArea { get; set; }
        public DateTime DeliveryTime { get; set; }
        public Order(float weight, OrderArea orderArea, DateTime deliveryTime)
        {
            Id = Guid.NewGuid().ToString("N");
            Weight = weight;
            OrderArea = orderArea;
            DeliveryTime = deliveryTime;
        }

        public Order()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public string GetSaveFormat()
        {
            return $"{Id};{Weight};{OrderArea.Id};{OrderArea.Name};{GetFormatedDeliveryTime()}";
        }

        public string GetFormatedDeliveryTime()
        {
            return DeliveryTime.ToString(DATE_TIME_FORMAT);
        }

        public override string ToString()
        {
            return $"Id={Id,5} Weight={Weight,10} OrderArea={OrderArea} DeliveryTime={GetFormatedDeliveryTime()}";
        }
    }
}
