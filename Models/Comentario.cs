using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante_sal_salsa.Models
{
    public class Comentario
    {
        public int id { get; set; }
        public int cliente_id { get; set; }
        public string comentario { get; set; }
    }
}