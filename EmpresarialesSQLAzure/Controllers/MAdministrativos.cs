using EmpresarialesSQLAzure.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EmpresarialesSQLAzure.Controllers
{
    public class MAdministrativos
    {
        public List<Tickets> ListaTickets;
        public Tickets _Ticket;



        //SE MUESTRAN TODOS LOS TICKETS
        public List<Tickets> GetAssignedTickets(string nombre)
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

            var cmd = new SqlCommand("SELECT * FROM Ticket WHERE EncargadoTicket = '" + nombre + "'", connect);

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
                                    FechaCreacion =  (dr["FechaCreacion"].ToString()).Split(' ').GetValue(0).ToString(),
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

        //SE MUESTRA SOLO UN TICKET EL QUE ESCOJE POR ID SE TOMARA EL TICKET
        public List<Tickets> SelectedTicket(int id)
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

            var cmd = new SqlCommand("SELECT * FROM Ticket WHERE IdTicket= '" + id + "'", connect);

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            connect.Close();

            ListaTickets = new List<Tickets>();
            _Ticket = new Tickets();

            _Ticket.Id = int.Parse((dt.Rows[0]["IdTicket"]).ToString());
            _Ticket.Titulo = (dt.Rows[0]["Titulo"]).ToString();
            _Ticket.FechaCreacion = (dt.Rows[0]["FechaCreacion"]).ToString();
            _Ticket.Comentarios = (dt.Rows[0]["Comentarios"]).ToString();
            _Ticket.IdUsuario = int.Parse((dt.Rows[0]["IdUsuario"]).ToString());
            _Ticket.Prioridad = int.Parse((dt.Rows[0]["Prioridad"]).ToString());
            _Ticket.Documento = Encoding.UTF8.GetBytes((dt.Rows[0]["Documento"]).ToString());
            _Ticket.EncargadoTicket = (dt.Rows[0]["EncargadoTicket"]).ToString();
            _Ticket.Status = int.Parse((dt.Rows[0]["Status"]).ToString());

            ListaTickets.Add(_Ticket);
            return ListaTickets;
        }


        //BUSQUEDA POR FECHA O NOMBRE DE CLIENTE 
        //QUITE BUSQUEDA POR NOMBRE D EUSSUARIO POR QUE ELIMINARIA POSIBILIDAD DE TENER MAS D EUN CLIENTE CON EL MI
        public List<Tickets> GetTicketsByParameter(int IdUsuario = 10000, string fecha1="NULO" , string fecha2="NULO") //ARREGLART EL ECHO DE QUE SOLO TRAERA UNO
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

            if (IdUsuario != 10000 && fecha1 == "NULO" && fecha2 =="NULO")
            {
                cmd.CommandText = "SELECT * FROM Ticket WHERE IdUsuario = '" + IdUsuario + "'";
                cmd.Connection = connect;
            }
            else if (fecha1 != "NULO" && fecha2 != "NULO" && IdUsuario == 10000)
            {
                cmd.CommandText = " SELECT * FROM Ticket WHERE FechaCreacion BETWEEN '" + fecha1 + "'AND '" + fecha2 + "'";
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


        //EDICION DE TICKET(ESTATUS A EN PROCESO COMENTARIOS ETC.) ENVIAR CORREOS MAS APARTE ADICION DE QUIEN SE ENCARGA DE EL 
        public string editarTicket(int idTicket, string comentarios, int prioridad, int status)
        {
            var dt = new DataTable();

            var res = "";

            var connect = new SqlConnection("Server=tcp:sebastiangutierrezserver.database.windows.net,1433;" +
            "Initial Catalog=ServicioTickets;" +
            "Persist Security Info=False;" +
            "User ID=elsebas;" +
            "Password=SebastianGutierrez123;" +
            "MultipleActiveResultSets=False;" +
            "Encrypt=True;" +
            "TrustServerCertificate=False;" +
            "Connection Timeout=30;");

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE Ticket SET Comentarios = '" + comentarios + "', Prioridad = '" + prioridad + "', Status = '" + status + "' WHERE IdTicket = '" + idTicket + "'";
            cmd.Connection = connect;

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            //cmd.ExecuteNonQuery();
            connect.Close();

            try
            {
                res = (dt.Rows[0]["Correo"]).ToString();
                return res;
            }
            catch (SqlException ex)
            {
                connect.Close();
                return res;
            }
        }

        public bool aceptarTicket(string nombreUsuario ,int IdTicket)
        {
            var connect = new SqlConnection("Server=tcp:sebastiangutierrezserver.database.windows.net,1433;" +
            "Initial Catalog=ServicioTickets;" +
            "Persist Security Info=False;" +
            "User ID=elsebas;" +
            "Password=SebastianGutierrez123;" +
            "MultipleActiveResultSets=False;" +
            "Encrypt=True;" +
            "TrustServerCertificate=False;" +
            "Connection Timeout=30;");

            var query = new SqlCommand("UPDATE Ticket SET EncargadoTicket = '" + nombreUsuario + "' WHERE IdTicket = '" + IdTicket + "'", connect);
            try
            {
                connect.Open();
                query.ExecuteNonQuery();
                connect.Close();
                return true;
            }
            catch (SqlException ex)
            {
                connect.Close();
                return false;
            }
        }

    }

}
