using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante_sal_salsa.Models
{
    public class Reserva
    {
        public int id { get; set; }
        public int cliente_id { get; set; }
        public int servicio_id { get; set; }
        public string nombre_referencia { get; set; }
        public string estado { get; set; }
        public string fecha { get; set; }
        public string asunto { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }
        public int cantidad_personas { get; set; }


    }
}
