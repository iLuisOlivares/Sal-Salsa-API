using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante_sal_salsa.Models
{
    public class Pedido
    {
        public int id { get; set; }
        public int cliente_id { get; set; }
        public int plato_id { get; set; }
        public int cantidad { get; set; }
    }
}
