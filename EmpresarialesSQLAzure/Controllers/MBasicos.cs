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
    public class MBasicos
    {
        public List<Tickets> ListaTickets;

        public List<string> ListaMails { get; private set; }

        //public Usuarios usuario;

        /*public void setUser(Usuarios user)
        {
            this.usuario = user;
        }*/

        //VER COMO AÑADIR LO DE DOCUMENTO A TICKET SERA AL FINAL

        public int crearTicketDoc(string titulo, string comentario, int _userId, int prioridad, string documento)
        {

            var dt = new DataTable();

            int res = 0;

            var connect = new SqlConnection("Server=tcp:sebastiangutierrezserver.database.windows.net,1433;" +
            "Initial Catalog=ServicioTickets;" +
            "Persist Security Info=False;" +
            "User ID=elsebas;" +
            "Password=SebastianGutierrez123;" +
            "MultipleActiveResultSets=False;" +
            "Encrypt=True;" +
            "TrustServerCertificate=False;" +
            "Connection Timeout=30;");

            var query = new SqlCommand("INSERT INTO Ticket(Titulo,Comentarios, IdUsuario, Prioridad, DocumentoString) VALUES " +
                "('" + titulo + "','" + comentario + "', '" + _userId + "','" + prioridad + "','" + documento + "')", connect);

            try
            {
                connect.Open();
                var da = new SqlDataAdapter(query);
                da.Fill(dt);
                connect.Close();

                res = int.Parse((dt.Rows[0]["IdTicket"]).ToString());
                return res;
            }
            catch (SqlException ex)
            {
                connect.Close();
                return res;
            }
        }

        public int crearTicket(string titulo, string comentario, int _userId, int prioridad)//AGREGAR DOCUMENTO DESPUES
        {//AJUSTAR PARA EU SE DEVUELVA EL ID DEL TICKET 

            var dt = new DataTable();

            int res = 0;

            var connect = new SqlConnection("Server=tcp:sebastiangutierrezserver.database.windows.net,1433;" +
            "Initial Catalog=ServicioTickets;" +
            "Persist Security Info=False;" +
            "User ID=elsebas;" +
            "Password=SebastianGutierrez123;" +
            "MultipleActiveResultSets=False;" +
            "Encrypt=True;" +
            "TrustServerCertificate=False;" +
            "Connection Timeout=30;");

            var query = new SqlCommand("INSERT INTO Ticket(Titulo,Comentarios, IdUsuario, Prioridad) VALUES " +
                "('" + titulo + "','" + comentario + "', '" + _userId + "','" + prioridad +"')", connect);
            try
            {
                connect.Open();
                var da = new SqlDataAdapter(query);
                da.Fill(dt);
                connect.Close();

                res = int.Parse((dt.Rows[0]["IdTicket"]).ToString());
                return res;
            }
            catch (SqlException ex)
            {
                connect.Close();
                return res;
            }
        }

        public List<Tickets> GetTicketsUser(int id)
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

            var cmd = new SqlCommand("SELECT * FROM Ticket WHERE IdUsuario = '" + id + "'", connect);

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
            catch (System.Exception ex)
            {
                return ListaTickets;
            }
        }

        public List<String> GetWorkersEmail()
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

            var cmd = new SqlCommand("SELECT Correo FROM Usuario WHERE TipoUsuario != 0", connect);

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            connect.Close();

            ListaMails = new List<String>();
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var mail = (dt.Rows[i]["Correo"].ToString());
                    ListaMails.Add(mail);
                }
                return ListaMails;

            }
            catch(System.Exception ex)
            {
                return ListaMails;
            }
        }
    }
}
