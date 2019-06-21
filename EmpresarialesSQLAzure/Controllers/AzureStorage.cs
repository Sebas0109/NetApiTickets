using EmpresarialesSQLAzure.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmpresarialesSQLAzure.Controllers
{
    public class AzureStorage
    {
        public Usuarios usuario;

        //CHECAR EL TIPO DE DATO VARBINARY COMO TRANSFORMALRO A TRING PARA DESPUES APSARLO A VARBINARY EN EL SQL

        public String GetImageString(int idTicket)
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

            var mail = "";
            var dt = new DataTable();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT DocumentoString FROM Ticket WHERE IdTicket = '" + idTicket + "'";
            cmd.Connection = connect;

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            connect.Close();

            mail = (dt.Rows[0]["DocumentoString"]).ToString();

            return mail;
        }

        public Usuarios Loggin(string username, string password)
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

            var dt = new DataTable();

            var query = new SqlCommand("SELECT * FROM Usuario WHERE Username = '" + username + "'AND Password = '" + password + "'", connect);

            connect.Open();
            var da = new SqlDataAdapter(query);
            da.Fill(dt);

            usuario = new Usuarios();
            usuario.Id = int.Parse((dt.Rows[0]["IdUsuario"]).ToString());
            usuario.Username = (dt.Rows[0]["Username"]).ToString();
            usuario.Password = (dt.Rows[0]["Password"]).ToString();
            usuario.Nombre = (dt.Rows[0]["Nombre"]).ToString();
            usuario.Apellido = (dt.Rows[0]["Apellido"]).ToString();
            usuario.Correo = (dt.Rows[0]["Correo"]).ToString();
            usuario.FechaRegistro = (dt.Rows[0]["FechaRegistro"]).ToString();
            usuario.TipoUsuario = int.Parse((dt.Rows[0]["TipoUsuario"]).ToString());
            return usuario;
        }

        //PONER FUNION DE MODIFICACION DE PERFIL
        public bool EditProfile(string username, string nombre, string apellido)
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

            var query = new SqlCommand("UPDATE Usuario SET Nombre = '" + nombre + "', Apellido = '" + apellido + "' WHERE Username = '" + username + "'",connect);
            try
            {
                connect.Open();
                query.ExecuteNonQuery();
                connect.Close();
                return true;
            } catch(SqlException ex)
            {
                connect.Close();
                return false;
            }
        }

        public bool Register(string username, string password, string nombre, string apellido, string correo,  int tipoUsuario = 0)
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

            var query = new SqlCommand("INSERT INTO Usuario(Username, Password, Nombre, Apellido, Correo, TipoUsuario)" +
                "VALUES('" + username + "', '" + password + "','" + nombre + "', '" + apellido + "', '" + correo + "','" + tipoUsuario + "')", connect);

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

        public string SendForgottenPassword(string username, string correo = "")
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

            var query = new SqlCommand("SELECT Password FROM Usuario WHERE Username = '" + username + "'", connect);

            connect.Open();
            var da = new SqlDataAdapter(query);
            da.Fill(dt);
            connect.Close();
            try
            {
                res = (dt.Rows[0]["Password"]).ToString();
            }
            catch (SqlException ex)
            {

                res = "Error la obtencion de su contraseña";
            }

            return res;
        }
    }
}