using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante_sal_salsa.Models
{
    public class Cliente
    {
        public int id { get; set; }
        public string nombre_usuario { get; set; }
        public string contrasena { get; set; }
        public string nombre_completo { get; set; }
        public string correo { get; set; }
        public string tipo_usuario { get; set; }
    }
}
