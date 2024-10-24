using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace DeliveryService
{
    public class OrderArea : IOrderArea
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public OrderArea(string name, string id = null)
        {
            if (id == null)
            {
                Id = Guid.NewGuid().ToString("N");
            }
            else
            {
                Id = id;
            }
            Name = name;
        }

        public OrderArea() { }

        public override string ToString()
        {
            return $"Id={Id} Name={Name}";
        }
    }
}
