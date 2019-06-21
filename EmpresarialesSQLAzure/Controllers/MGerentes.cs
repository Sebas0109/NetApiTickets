using EmpresarialesSQLAzure.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresarialesSQLAzure.Controllers
{
    public class MGerentes
    {
        public List<Tickets> ListaTickets;
        public Tickets _Ticket;


        public List<Tickets> GetTicketsByStatus(int status)
        {
            var dt = new DataTable();

            var connect = new SqlConnection("Server = tcp:sebastiangutierrezserver.database.windows.net,1433; " +
                "Initial Catalog = ServicioTickets;" +
                " Persist Security Info = False;" +
                " User ID = elsebas;" +
                " Password =SebastianGutierrez123;" +
                " MultipleActiveResultSets = False;" +
                " Encrypt = True; " +
                "TrustServerCertificate = False;" +
                " Connection Timeout = 30;");

            var cmd = new SqlCommand("SELECT * FROM Ticket WHERE Status = '" + status + "'", connect);

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            connect.Close();

            ListaTickets = new List<Tickets>();
            try
            {
                ListaTickets = (from DataRow dr in dt.Rows
                                select new Tickets()
                                {
                                    Id = int.Parse(dr["IdTicket"].ToString()),
                                    Titulo = dr["Titulo"].ToString(),
                                    FechaCreacion = (dr["FechaCreacion"].ToString()).Split(' ').GetValue(0).ToString(),
                                    Comentarios = dr["Comentarios"].ToString(),
                                    IdUsuario = int.Parse(dr["IdUsuario"].ToString()),
                                    Prioridad = int.Parse(dr["Prioridad"].ToString()),
                                    Documento = Encoding.UTF8.GetBytes(dr["Documento"].ToString()),
                                    EncargadoTicket = dr["EncargadoTicket"].ToString(),
                                    Status = int.Parse(dr["Status"].ToString())
                                }).ToList();
                return ListaTickets;

            }
            catch (Exception ex)
            {
                return ListaTickets;
                throw;
            }
        }

        public List<Tickets> GetTicketsByParameter(string nombreEmpleado = "ABCDE")
        {
            var dt = new DataTable();

            var connect = new SqlConnection("Server = tcp:sebastiangutierrezserver.database.windows.net,1433; " +
                "Initial Catalog = ServicioTickets;" +
                " Persist Security Info = False;" +
                " User ID = elsebas;" +
                " Password =SebastianGutierrez123;" +
                " MultipleActiveResultSets = False;" +
                " Encrypt = True; " +
                "TrustServerCertificate = False;" +
                " Connection Timeout = 30;");

            SqlCommand cmd = new SqlCommand();


            if (nombreEmpleado != "ABCDE")
            {
                cmd.CommandText = "SELECT * FROM Ticket WHERE EncargadoTicket = '" + nombreEmpleado + "'";
                cmd.Connection = connect;
            }

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            connect.Close();

            ListaTickets = new List<Tickets>();
            _Ticket = new Tickets();

            _Ticket.Id = int.Parse((dt.Rows[0]["IdTicket"]).ToString());
            _Ticket.Titulo = (dt.Rows[0]["Titulo"]).ToString();
            _Ticket.FechaCreacion = ((dt.Rows[0]["FechaCreacion"]).ToString()).Split(' ').GetValue(0).ToString();
            _Ticket.Comentarios = (dt.Rows[0]["Comentarios"]).ToString();
            _Ticket.IdUsuario = int.Parse((dt.Rows[0]["IdUsuario"]).ToString());
            _Ticket.Prioridad = int.Parse((dt.Rows[0]["Prioridad"]).ToString());
            _Ticket.Documento = Encoding.UTF8.GetBytes((dt.Rows[0]["Documento"]).ToString());
            _Ticket.EncargadoTicket = (dt.Rows[0]["EncargadoTicket"]).ToString();
            _Ticket.Status = int.Parse((dt.Rows[0]["Status"]).ToString());

            ListaTickets.Add(_Ticket);
            return ListaTickets;
        }

        public List<Tickets> GetAllTickets()
        {

            var dt = new DataTable();

            var connect = new SqlConnection("Server = tcp:sebastiangutierrezserver.database.windows.net,1433; " +
                "Initial Catalog = ServicioTickets;" +
                " Persist Security Info = False;" +
                " User ID = elsebas;" +
                " Password =SebastianGutierrez123;" +
                " MultipleActiveResultSets = False;" +
                " Encrypt = True; " +
                "TrustServerCertificate = False;" +
                " Connection Timeout = 30;");

            var cmd = new SqlCommand("SELECT * FROM Ticket", connect);

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            connect.Close();

            ListaTickets = new List<Tickets>();
            try
            {
                ListaTickets = (from DataRow dr in dt.Rows
                                select new Tickets()
                                {
                                    Id = int.Parse(dr["IdTicket"].ToString()),
                                    Titulo = dr["Titulo"].ToString(),
                                    FechaCreacion = (dr["FechaCreacion"].ToString()).Split(' ').GetValue(0).ToString(),
                                    Comentarios = dr["Comentarios"].ToString(),
                                    IdUsuario = int.Parse(dr["IdUsuario"].ToString()),
                                    Prioridad = int.Parse(dr["Prioridad"].ToString()),
                                    Documento = Encoding.UTF8.GetBytes(dr["Documento"].ToString()),
                                    EncargadoTicket = dr["EncargadoTicket"].ToString(),
                                    Status = int.Parse(dr["Status"].ToString())
                                }).ToList();
                return ListaTickets;

            }
            catch (Exception ex)
            {
                return ListaTickets;
                throw;
            }

        }

        public string GetWorkerMail(string workerName)
        {
            var mail = "";

            var dt = new DataTable();

            var connect = new SqlConnection("Server = tcp:sebastiangutierrezserver.database.windows.net,1433; " +
                "Initial Catalog = ServicioTickets;" +
                " Persist Security Info = False;" +
                " User ID = elsebas;" +
                " Password =SebastianGutierrez123;" +
                " MultipleActiveResultSets = False;" +
                " Encrypt = True; " +
                "TrustServerCertificate = False;" +
                " Connection Timeout = 30;");

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT Correo FROM Usuario WHERE Nombre = '" + workerName + "'";
            cmd.Connection = connect;

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            connect.Close();

            mail = (dt.Rows[0]["Correo"]).ToString();

            return mail;
        }
    }
}
