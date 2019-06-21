using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpresarialesSQLAzure.Modelos
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string FechaRegistro { get; set; }
        public int TipoUsuario { get; set; }
    }
}
