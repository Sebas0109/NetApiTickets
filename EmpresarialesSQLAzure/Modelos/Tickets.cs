using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpresarialesSQLAzure.Modelos
{
    public class Tickets
    {
        internal string[] fecha;

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string FechaCreacion { get; set; }
        public string Comentarios { get; set; }
        public int IdUsuario { get; set; }
        public int Prioridad { get; set; }
        public byte[] Documento { get; set; }
        public string EncargadoTicket { get; set; }
        public int Status { get; set; }
    }
}
