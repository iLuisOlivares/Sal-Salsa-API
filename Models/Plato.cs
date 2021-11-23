using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante_sal_salsa.Models
{
    public class Plato
    {
        public int id { get; set; }
        public string restaurante_id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public string precio { get; set; }
    }
}
