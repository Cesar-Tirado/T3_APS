﻿using ProyectoTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ProyectoTest.Logica
{
    public class ProductoLogica
    {
        private static ProductoLogica _instancia = null;

        public ProductoLogica()
        {

        }

        public static ProductoLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new ProductoLogica();
                }

                return _instancia;
            }
        }
        public List<Producto> Listar2()
        {
            List<Producto> rptListaProducto = new List<Producto>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_obtenerProducto2", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaProducto.Add(new Producto()
                        {
                            IdProducto = Convert.ToInt32(dr["IdProducto"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            oMarca = new Marca() { Descripcion = dr["DescripcionMarca"].ToString() },
                            oCategoria = new Categoria() { Descripcion = dr["DescripcionCategoria"].ToString() },
                            Precio = Convert.ToDecimal(dr["Precio"].ToString(), new CultureInfo("es-PE")),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString()),
                        });
                    }
                    dr.Close();

                    return rptListaProducto;
                }
                catch (Exception)
                {
                    rptListaProducto = null;
                    return rptListaProducto;
                }
            }
        }
        public List<Producto> Listar()
        {

            List<Producto> rptListaProducto = new List<Producto>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_obtenerProducto", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaProducto.Add(new Producto()
                        {
                            IdProducto = Convert.ToInt32(dr["IdProducto"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            OpcionesConCosto = dr["OpcionesConCosto"].ToString(),
                            OpcionesSinCosto = dr["OpcionesSinCosto"].ToString(),
                            OpcionExcluyente = dr["OpcionExcluyente"].ToString(),
                            MaxOpcionesSinCosto = Convert.ToInt32(dr["MaxOpcionesSinCosto"].ToString()), // Agregar el nuevo campo
                            oMarca = new Marca() { IdMarca = Convert.ToInt32(dr["IdMarca"].ToString()), Descripcion = dr["DescripcionMarca"].ToString() },
                            oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"].ToString()), Descripcion = dr["DescripcionCategoria"].ToString() },
                            Precio = Convert.ToDecimal(dr["Precio"].ToString(), new CultureInfo("es-PE")),
                            RutaImagen = dr["RutaImagen"].ToString(),

                            Activo = Convert.ToBoolean(dr["Activo"].ToString()),
                        });
                    }
                    dr.Close();

                    return rptListaProducto;

                }
                catch (Exception)
                {
                    rptListaProducto = null;
                    return rptListaProducto;
                }
            }
        }

        public List<Imagen> ListarImagenes()
        {
            List<Imagen> rptListaImagenes = new List<Imagen>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("SELECT Id, RutaImagen, Activo FROM IMAGENES", oConexion);
                cmd.CommandType = CommandType.Text;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaImagenes.Add(new Imagen()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            RutaImagen = dr["RutaImagen"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString()),
                        });
                    }

                    dr.Close();
                    return rptListaImagenes;
                }
                catch (Exception)
                {
                    rptListaImagenes = null;
                    return rptListaImagenes;
                }
            }
        }




        public int Registrar(Producto oProducto)
        {
            int respuesta = 0;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {

                    oConexion.Open();

                        string sqlInsert = "INSERT INTO PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, RutaImagen, OpcionesConCosto, OpcionesSinCosto, OpcionExcluyente, MaxOpcionesSinCosto) " +
                                           "VALUES (@Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio, @RutaImagen, @OpcionesConCosto, @OpcionesSinCosto, @OpcionExcluyente, @MaxOpcionesSinCosto); " +
                                           "SELECT SCOPE_IDENTITY()";

                        using (SqlCommand command = new SqlCommand(sqlInsert, oConexion))
                        {
                            // Configura los parámetros
                            command.Parameters.AddWithValue("@Nombre", oProducto.Nombre);
                            command.Parameters.AddWithValue("@Descripcion", oProducto.Descripcion);
                            command.Parameters.AddWithValue("@IdMarca", oProducto.oMarca.IdMarca);
                            command.Parameters.AddWithValue("@IdCategoria", oProducto.oCategoria.IdCategoria);
                            command.Parameters.AddWithValue("@Precio", oProducto.Precio);
                            command.Parameters.AddWithValue("@RutaImagen", oProducto.RutaImagen);
                            command.Parameters.AddWithValue("@OpcionesConCosto", string.IsNullOrEmpty(oProducto.OpcionesConCosto) ? (object)DBNull.Value : oProducto.OpcionesConCosto);
                            command.Parameters.AddWithValue("@OpcionesSinCosto", string.IsNullOrEmpty(oProducto.OpcionesSinCosto) ? (object)DBNull.Value : oProducto.OpcionesSinCosto);
                            command.Parameters.AddWithValue("@OpcionExcluyente", string.IsNullOrEmpty(oProducto.OpcionExcluyente) ? (object)DBNull.Value : oProducto.OpcionExcluyente);
                            command.Parameters.AddWithValue("@MaxOpcionesSinCosto", oProducto.MaxOpcionesSinCosto);

                            // Ejecuta la inserción y obtiene el ID del registro insertado
                            int newProductId = Convert.ToInt32(command.ExecuteScalar());

                            return  newProductId;
                            //SqlCommand cmd = new SqlCommand("sp_registrarProducto", oConexion);
                            //cmd.Parameters.AddWithValue("Nombre", oProducto.Nombre);
                            //cmd.Parameters.AddWithValue("Descripcion", oProducto.Descripcion);
                            //cmd.Parameters.AddWithValue("IdMarca", oProducto.oMarca.IdMarca);
                            //cmd.Parameters.AddWithValue("IdCategoria", oProducto.oCategoria.IdCategoria);
                            //cmd.Parameters.AddWithValue("Precio", oProducto.Precio);
                            //cmd.Parameters.AddWithValue("RutaImagen", oProducto.RutaImagen);
                            //cmd.Parameters.AddWithValue("OpcionesConCosto", string.IsNullOrEmpty(oProducto.OpcionesConCosto) ? (object)DBNull.Value : oProducto.OpcionesConCosto);
                            //cmd.Parameters.AddWithValue("OpcionesSinCosto", string.IsNullOrEmpty(oProducto.OpcionesSinCosto) ? (object)DBNull.Value : oProducto.OpcionesSinCosto);
                            //cmd.Parameters.AddWithValue("OpcionExcluyente", string.IsNullOrEmpty(oProducto.OpcionExcluyente) ? (object)DBNull.Value : oProducto.OpcionExcluyente);
                            //cmd.Parameters.AddWithValue("MaxOpcionesSinCosto", oProducto.MaxOpcionesSinCosto); // Agregar el nuevo valor
                            //cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                            //cmd.CommandType = CommandType.StoredProcedure;

                            //oConexion.Open();

                            //cmd.ExecuteNonQuery();

                            //respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);

                        }
                    
                }
                catch (Exception)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }


        public bool Modificar(Producto oProducto)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_editarProducto", oConexion);
                    cmd.Parameters.AddWithValue("IdProducto", oProducto.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", oProducto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", oProducto.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", oProducto.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", oProducto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", oProducto.Precio);
                    cmd.Parameters.AddWithValue("OpcionesConCosto", oProducto.OpcionesConCosto);
                    cmd.Parameters.AddWithValue("OpcionesSinCosto", oProducto.OpcionesSinCosto);
                    cmd.Parameters.AddWithValue("OpcionExcluyente", oProducto.OpcionExcluyente);
                    cmd.Parameters.AddWithValue("Activo", oProducto.Activo);
                    cmd.Parameters.AddWithValue("MaxOpcionesSinCosto", oProducto.MaxOpcionesSinCosto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }



        public bool ModificarImagenInicial(Imagen oProducto)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_editarInicial", oConexion);
                    cmd.Parameters.AddWithValue("Id", oProducto.Id);
                    cmd.Parameters.AddWithValue("Activo", oProducto.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public bool ActualizarRutaImagen(Producto oProducto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_actualizarRutaImagen", oConexion);
                    cmd.Parameters.AddWithValue("IdProducto", oProducto.IdProducto);
                    cmd.Parameters.AddWithValue("RutaImagen", oProducto.RutaImagen);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ActualizarRutaImagenInicial(Imagen oImagen)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_actualizarRutaImagenInicial", oConexion);
                    cmd.Parameters.AddWithValue("Id", oImagen.Id);
                    cmd.Parameters.AddWithValue("RutaImagen", oImagen.RutaImagen);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ApagarTienda()
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update Producto set Activo = 0", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public bool EncenderTienda()
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update Producto set Activo = 1", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from Producto where idProducto = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

    }
}