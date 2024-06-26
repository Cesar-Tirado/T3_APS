using ProyectoTest.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace ProyectoTest.Logica
{
    public class TiendasLogica
    {
        private static TiendasLogica _instancia = null;

        public TiendasLogica()
        {

        }

        public static TiendasLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new TiendasLogica();
                }

                return _instancia;
            }
        }

        public List<Tiendas> Listar()
        {
            List<Tiendas> rptListaTiendas = new List<Tiendas>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Tiendas", oConexion);
                cmd.CommandType = CommandType.Text;
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaTiendas.Add(new Tiendas()
                        {
                            IdTiendas = Convert.ToInt32(dr["IdTiendas"].ToString()),
                            Descripcion = dr["Descripcion"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaTiendas;
                }
                catch (Exception ex)
                {
                    rptListaTiendas = null;
                    return rptListaTiendas;
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
        }


        public bool Modificar(Configuracion oConiguracion)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarConfiguracion", oConexion);
                    cmd.Parameters.AddWithValue("HoraAperturaR", oConiguracion.HoraAperturaR);
                    cmd.Parameters.AddWithValue("HoraCierreR", oConiguracion.HoraCierreR);
                    cmd.Parameters.AddWithValue("HoraAperturaD", oConiguracion.HoraAperturaD);
                    cmd.Parameters.AddWithValue("HoraCierreD", oConiguracion.HoraCierreD);
                    cmd.Parameters.AddWithValue("CostoEnvio", oConiguracion.CostoEnvio);
                    cmd.Parameters.AddWithValue("PrecioMinimo", oConiguracion.PrecioMinimo);
                    cmd.Parameters.AddWithValue("PagoTarjeta", oConiguracion.PagoTarjeta);
                    cmd.Parameters.AddWithValue("PagoPOS", oConiguracion.PagoPOS);
                    cmd.Parameters.AddWithValue("PagoEfectivo", oConiguracion.PagoEfectivo);
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



        public List<Configuracion> ListarConf()
        {
            List<Configuracion> rptListaTiendas = new List<Configuracion>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CONFIG", oConexion);
                cmd.CommandType = CommandType.Text;
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaTiendas.Add(new Configuracion()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            HoraAperturaR= dr["horaAperturaR"].ToString(),
                            HoraCierreR = dr["horaCierreR"].ToString(),
                            HoraAperturaD = dr["horaAperturaD"].ToString(),
                            HoraCierreD = dr["horaCierreD"].ToString(),
                            CostoEnvio = Convert.ToDecimal(dr["costoEnvio"].ToString()),   
                            PagoTarjeta = Convert.ToBoolean(dr["pagoTarjeta"].ToString()),
                            PagoPOS = Convert.ToBoolean(dr["pagoPOS"].ToString()),
                            PagoEfectivo = Convert.ToBoolean(dr["pagoEfectivo"].ToString()),
                            PrecioMinimo = Convert.ToDecimal(dr["precioMinimo"].ToString())

                        });
                    }
                    dr.Close();
                    return rptListaTiendas;
                }
                catch (Exception ex)
                {
                    rptListaTiendas = null;
                    return rptListaTiendas;
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
        }
        public List<Tiendas> ListarTiendasActivo()
        {
            List<Tiendas> rptListaTiendas = new List<Tiendas>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("SELECT IdTiendas, Descripcion, Direccion, Activo FROM Tiendas WHERE Activo = 1", oConexion);

#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaTiendas.Add(new Tiendas()
                        {
                            IdTiendas = Convert.ToInt32(dr["IdTiendas"].ToString()),
                            Descripcion = dr["Descripcion"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaTiendas;

                }
                catch (Exception ex)
                {
                    rptListaTiendas = null;
                    return rptListaTiendas;
                }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            }
        }


        public bool Registrar(Tiendas oTiendas)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarTiendas", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", oTiendas.Descripcion);
                    cmd.Parameters.AddWithValue("Direccion", oTiendas.Direccion); 
                    cmd.Parameters.AddWithValue("Activo", oTiendas.Activo);
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

        public bool Modificar(Tiendas oTiendas)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarTiendas", oConexion);
                    cmd.Parameters.AddWithValue("IdTiendas", oTiendas.IdTiendas);
                    cmd.Parameters.AddWithValue("Descripcion", oTiendas.Descripcion);
                    cmd.Parameters.AddWithValue("Direccion", oTiendas.Direccion); 
                    cmd.Parameters.AddWithValue("Activo", oTiendas.Activo);
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM Tiendas WHERE idTiendas = @id", oConexion);
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