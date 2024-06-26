using Antlr.Runtime.Misc;
using ProyectoTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ProyectoTest.Logica
{
    public class ReservaLogica
    {
        private static ReservaLogica _instancia = null;

        public ReservaLogica()
        {

        }

        public static ReservaLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new ReservaLogica();
                }
                return _instancia;
            }
        }


        public int Registrar(Reserva oReserva)
        {
            int respuesta = 0;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    oConexion.Open();
                    string query = "INSERT INTO Reserva (NombreCliente,TipoDocumento, Cantidad, NumeroDocumento, Correo, Celular, Motivo, Observacion) " +
                                   "VALUES (@NombreCliente,@TipoDocumento,@Cantidad,@NumeroDocumento,@Correo,@Celular,@Motivo,@Observacion) ";
                
                    SqlCommand command = new SqlCommand(query, oConexion);
                    //command.Parameters.AddWithValue("@Local", oReserva.Tienda);
                    //command.Parameters.AddWithValue("@Fecha", oReserva.Fecha);
                    command.Parameters.AddWithValue("@NombreCliente", oReserva.NombreCliente);
                    command.Parameters.AddWithValue("@TipoDocumento", oReserva.TipoDocumento);
                    command.Parameters.AddWithValue("@Cantidad", oReserva.Cantidad);
                    command.Parameters.AddWithValue("@NumeroDocumento", oReserva.NumeroDocumento);
                    command.Parameters.AddWithValue("@Correo", oReserva.Correo);
                    command.Parameters.AddWithValue("@Celular", oReserva.Celular);
                    command.Parameters.AddWithValue("@Motivo", oReserva.Motivo);
                    command.Parameters.AddWithValue("@Observacion", oReserva.Observacion);
                    respuesta = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }


        public List<Reserva> ListarLocal()
        {
            List<Reserva> rptListaReserva = new List<Reserva>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM RESERVALOCAL", oConexion);
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        rptListaReserva.Add(new Reserva()
                        {
                            Id = Convert.ToInt32(dr["Id"].ToString()),
                            NombreLocal = dr["NombreLocal"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString()),
                            Cantidad = (dr["Cantidad"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaReserva;
                }
                catch (Exception ex)
                {
                    rptListaReserva = null;
                    return rptListaReserva;
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
        }

        public bool RegistrarLocal(Reserva oReserva)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {                
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    oConexion.Open(); 
                    string query = "INSERT INTO RESERVALOCAL (NombreLocal, Cantidad, Activo) VALUES (@NombreLocal, @Cantidad, @Activo)";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@NombreLocal", oReserva.NombreLocal);
                    cmd.Parameters.AddWithValue("@Cantidad", oReserva.Cantidad);
                    cmd.Parameters.AddWithValue("@Activo", oReserva.Activo);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    respuesta = (rowsAffected > 0);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
            return respuesta;
        }


        public bool RegistrarFecha(Reserva oReserva)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    oConexion.Open();
                    string query2 = "INSERT INTO RESERVAFECHA (IdReservaLocal, Anio, Mes, Dias, Horas) VALUES (@IdReservaLocal, @Anio, @Mes, @Dias, @Horas)";
                    SqlCommand cmd2 = new SqlCommand(query2, oConexion);
                    cmd2.Parameters.AddWithValue("@IdReservaLocal", oReserva.oFecha.IdReservaLocal);
                    cmd2.Parameters.AddWithValue("@Anio", oReserva.oFecha.Anio);
                    cmd2.Parameters.AddWithValue("@Mes", oReserva.oFecha.Mes);
                    cmd2.Parameters.AddWithValue("@Dias", oReserva.oFecha.Dias);
                    cmd2.Parameters.AddWithValue("@Horas", oReserva.oFecha.Horas);
                    int rowsAffected = cmd2.ExecuteNonQuery();
                    respuesta = (rowsAffected > 0);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
            return respuesta;
        }


        public List<Reserva> ObtenerFechas(Reserva oReserva)
        {
            List<Reserva> lst = new List<Reserva>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM RESERVAFECHAS WHERE IdReservaLocal = @IdReservaLocal AND Anio = @Anio AND Mes = @Mes", oConexion);
                    cmd.Parameters.AddWithValue("@IdReservaLocal", oReserva.oFecha.IdReservaLocal);
                    cmd.Parameters.AddWithValue("@Anio", oReserva.oFecha.Anio);
                    cmd.Parameters.AddWithValue("@Mes", oReserva.oFecha.Mes);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            lst.Add(new Reserva()
                            {
                                IdReservaLocal = dr["IdReservaLocal"].ToString(),
                                Anio = dr["Anio"].ToString(),
                                Mes = dr["Mes"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lst = new List<Reserva>();
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
            return lst;
        }

        public bool ModificarLocal(Reserva oReserva)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    oConexion.Open();
                    //string query2 = "UPDATE RESERVAFECHA SET Anio=@Anio,Mes=@Mes,Dias=@Dias,Horas=@Horas WHERE Id=@Id";
                    //SqlCommand cmd2 = new SqlCommand(query2, oConexion);                    
                    //cmd2.Parameters.AddWithValue("@Anio", oReserva.oFecha.Anio);
                    //cmd2.Parameters.AddWithValue("@Mes", oReserva.oFecha.Mes);
                    //cmd2.Parameters.AddWithValue("@Dias", oReserva.oFecha.Dias);
                    //cmd2.Parameters.AddWithValue("@Horas", oReserva.oFecha.Horas);
                    //int rowsAffected2 = cmd2.ExecuteNonQuery();

                    string query = "UPDATE RESERVALOCAL SET NombreLocal = @NombreLocal, Cantidad = @Cantidad, Activo = @Activo" +
                                   "WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Id", oReserva.Id);
                    cmd.Parameters.AddWithValue("@NombreLocal", oReserva.NombreLocal);
                    cmd.Parameters.AddWithValue("@Cantidad", oReserva.Cantidad);
                    cmd.Parameters.AddWithValue("@Activo", oReserva.Activo);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    respuesta = (rowsAffected > 0);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
            return respuesta;
        }

        public bool ModificarFecha(Reserva oReserva)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    oConexion.Open();
                    string query = "INSERT INTO RESERVAFECHA (Dias) VALUES (@Dias)";

                    using (SqlCommand cmd = new SqlCommand(query, oConexion))
                    {
                        string[] anios = { "2020-09-10", "2021", "2022" };

                        foreach (string anio in anios)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@Dias", anio);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            respuesta = respuesta && (rowsAffected > 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ex: " + ex.ToString());
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public bool EliminarLocal(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM RESERVALOCAL WHERE id = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
            return respuesta;
        }

        public List<Reserva> ListarReservaActivo()
        {
            List<Reserva> rptListaReserva = new List<Reserva>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("SELECT IdReserva, Descripcion, Activo FROM Reserva WHERE Activo = 1", oConexion);

#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        rptListaReserva.Add(new Reserva()
                        {
                            Id = Convert.ToInt32(dr["Id"].ToString()),
                            //Descripcion = dr["Descripcion"].ToString(),
                            //Activo = Convert.ToBoolean(dr["Activo"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaReserva;
                }
                catch (Exception ex)
                {
                    rptListaReserva = null;
                    return rptListaReserva;
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
        }


        public bool Modificar(Reserva oReserva)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarReserva", oConexion);
                    cmd.Parameters.AddWithValue("Id", oReserva.Id);
                    //cmd.Parameters.AddWithValue("Descripcion", oReserva.Descripcion);
                    //cmd.Parameters.AddWithValue("Activo", oReserva.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
            return respuesta;
        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from Reserva where idReserva = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
            return respuesta;
        }


        

    }
}