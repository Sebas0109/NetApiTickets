using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpresarialesSQLAzure.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace EmpresarialesSQLAzure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet("GetDocString")]
        public string GetDocString(int idTicket)
        {
            var consulta = new AzureStorage();
            string res = consulta.GetImageString(idTicket);
            return res;
        }

        //METODOS INICIALES LOGGIN, REGISTRAR Y EDITAR USUARIO INICIAN Y CONTRASEÑA OLVIDADA
        [HttpGet("Loggin")]
        public Usuarios Loggin(string username, string password)
        {
            Usuarios _user = new Usuarios();
            var consulta = new AzureStorage();
            try
            {
                 _user = consulta.Loggin(username, password);
                return _user;
            }
            catch (Exception ex)
            {
                return _user;
            }
        } 

        [HttpGet("EditProfile")]
        public string EditProfile(string username, string nombre, string apellido)
        {
            var consulta = new AzureStorage();
            bool res = consulta.EditProfile(username, nombre, apellido);
            if (res == true)
            {
                return "Edicion exitosa";
            }
            else
            {
                return "Error completando la edicion";
            }

        }

        [HttpGet("Register")]
        public string Register(string username, string password, string nombre, string apellido, string correo, int tipoUsuario)
        {
            var consulta = new AzureStorage();
            bool res = consulta.Register(username, password, nombre, apellido, correo, tipoUsuario = 0);
            if (res == true)
            {
                return "Registro exitoso";
            }
            else
            {
                return "Error al registrarte";
            }
        }

        [HttpGet("Forgot")]
        public string ForgottenPassword(string usuario, string correo)
        {
            var consulta = new AzureStorage();
            var res = consulta.SendForgottenPassword(usuario, correo);
            return res.ToString();
        }

        //METODOS INICIALES LOGGIN, REGISTRAR Y EDITAR USUARIO TERMINADOS Y CONTRASEÑA OLVIDADA


        //METODOS DE USUARUIO BASICO CREAR TICKET Y VER TICKETS SUYOS INICIAN Y TRAER LOS CORREOS DE LOS USUARIOS EMPLEADOS
        // GET api/values/5
        [HttpGet("GetMyTickets")]
        public JsonResult GetMyTickets(int Id)
        {
            var consulta = new MBasicos();
            var lista = consulta.GetTicketsUser(Id);
            return new JsonResult(lista);
        }

        [HttpGet("NewTicket")]
        public int NewTicket(string titulo, string comentario, int _userId, int prioridad)
        {
            var consulta = new MBasicos();
            int res = consulta.crearTicket(titulo, comentario, _userId, prioridad);
            return res;
        }

        //public int NewTicketDoc([FromBody] string titulo, [FromBody] string comentario, [FromBody] int _userId, [FromBody]int prioridad, [FromBody] string documento)

        //CAMBIO DE GET A POST NADA MAS
        [HttpPost("NewTicketDoc")]
        public int NewTicketDoc([FromForm] string titulo, [FromForm] string comentario, [FromForm] int _userId, [FromForm]int prioridad, [FromForm] string documento)
        {
            var consulta = new MBasicos();
            int res = consulta.crearTicketDoc(titulo, comentario, _userId, prioridad, documento);
            return res;
        }

        [HttpGet("WMail")]
        public JsonResult GetWorkersEmail()
        {
            var consulta = new MBasicos();
            var lista = consulta.GetWorkersEmail();
            return new JsonResult(lista);
        }
        //METODOS DE USUARUIO BASICO CREAR TICKET Y VER TICKETS SUYOS TERMINAN



        //METODOS DE USUARIO ADMINISTRADOR GETASSIGNEDTICKETS, SELECCION DE TICKET, GENERAR UNA LISTA DE TICKETS POR PARAMETROS Y EDITAR TICKET EMPIEZAN
        //ACEPTACION D ETICKET TAMBIEN AGREGADA

        [HttpGet("GetAssignedTickets")]
        public JsonResult GetAssignedTickets(string nombre)
        {
            var consulta = new MAdministrativos();
            var lista = consulta.GetAssignedTickets(nombre);
            return new JsonResult(lista);

        }

        [HttpGet("TicketSelec")] //LO USARA TAMBIEN EL GERENTE
        public JsonResult GetSeletedTicket(int id)
        {
            var consulta = new MAdministrativos();
            var lista = consulta.SelectedTicket(id);
            return new JsonResult(lista);
        }

        [HttpGet("ListParamA")]
        public JsonResult GetListParamA(int idusuario, string fecha1, string fecha2)
        {
            var consulta = new MAdministrativos();
            var lista = consulta.GetTicketsByParameter(idusuario, fecha1, fecha2);
            return new JsonResult(lista);
        }

        [HttpGet("EditTicket")]//LO USARA TAMBIEN EL GERENTE
        public string EditarTicket(int idTicket, string comentario, int prioridad, int status)
        {
            var consulta = new MAdministrativos();
            var res = consulta.editarTicket(idTicket, comentario,prioridad,status);
            return res;            
        }

        [HttpGet("TicketAceptado")]
        public string TicketAceptado(string nombreusuario, int IdTicket)
        {
            var consulta = new MAdministrativos();
            bool res = consulta.aceptarTicket(nombreusuario, IdTicket);
            if (res == true)
            {
                return "Asignacion correctamente realizada";
            }
            else
            {
                return "Asignacion con errores no se pudo asignar ticket";
            }
        }

        //METODOS DE USUARIO ADMINISTRADOR GETALLTICKETS, SELECCION DE TICKET, GENERAR UNA LISTA DE TICKETS POR PARAMETROS Y EDITAR TICKET TERMINAN
        //ACEPTACION D ETICKET TAMBIEN AGREGADA


        //METODOS DE USUARIO GERENCIALGENERAR UNA LISTA DE TICKETS POR PARAMETROS Y BUSQUEDA DE TICKETS POR MEDIO DE ESTADO DE FINALIZACION O EN PROCESOS 
        [HttpGet("ListParamG")]
        public JsonResult GetListParamG(string nombreEmpelado)
        {
            var consulta = new MGerentes();
            var lista = consulta.GetTicketsByParameter(nombreEmpelado);
            return new JsonResult(lista);
        }

        [HttpGet("ListaStatus")]
        public JsonResult GetListByParams(int status)
        {
            var consulta = new MGerentes();
            var lista = consulta.GetTicketsByStatus(status);
            return new JsonResult(lista);
        }

        [HttpGet("GetAll")]
        public JsonResult GetAllTickets()
        {
            var consulta = new MGerentes();
            var lista = consulta.GetAllTickets();
            return new JsonResult(lista);
        }

        [HttpGet("GetMail")]
        public string GetWorkerMail(string workerName)
        {
            var consulta = new MGerentes();
            var correo = consulta.GetWorkerMail(workerName);
            return correo;
        }


        /*// DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {}

        // POST api/values
        //[HttpPost("NewTicket")]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}*/
    }//namalo re
}
