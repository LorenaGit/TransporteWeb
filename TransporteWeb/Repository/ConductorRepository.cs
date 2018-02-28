using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TransporteWeb.Models;

namespace TransporteWeb.Repository
{
    public class ConductorRepository
    {

        public List<Conductor> getConductoresAll()
        {
            List<Conductor> conductor = new List<Conductor>();

            string connectionString = ConfigurationManager.ConnectionStrings["TransporteStringConnection"].ToString();


            string sqlString = " select IdConductor, Nombre, Cedula, Telefono  from Conductor ";



            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Conductor c1 = new Conductor();
                        c1.IdConductor = Convert.ToInt32(reader["IdConductor"].ToString()); //los campos del reader son los mismos del select (osea el query del select)
                        c1.Nombre = reader["Nombre"].ToString();
                        c1.Cedula = reader["Cedula"].ToString();
                        c1.Telefono = reader["Telefono"].ToString();
                        
                        conductor.Add(c1);
                    }
                    reader.Close();
                }
            }

            return conductor;
        }

        public int insertConductor(Conductor x) //Sql_inyeccion!
        {
            //out of the box = (librerias) por defecto = ya viene incluido
            string connectionString = ConfigurationManager.ConnectionStrings["TransporteStringConnection"].ToString();

            string sqlString = "Insert into Conductor (Nombre, Cedula, Telefono) values (@nombre, @cedula,  @telefono) ";  //PENDIENTE CORREGIR CON SQL INJECCION
            int retorno = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@nombre", x.Nombre);
                    cmd.Parameters.AddWithValue("@cedula", x.Cedula);
                    cmd.Parameters.AddWithValue("@telefono", x.Telefono);
                    conn.Open();

                    retorno = cmd.ExecuteNonQuery(); //esta funcion es para INSERT,UPDATE,DELETE y me devuelve el total de filas afectadas
                }
            }


            return retorno;
        }

        public Conductor getConductorById(int x) //Con seguridad contra inyeccion SQL
        {

            string connectionString = ConfigurationManager.ConnectionStrings["TransporteStringConnection"].ToString();

            Conductor c1 = new Conductor();

            
            string sqlString = "Select IdConductor, Nombre, Cedula, Telefono From Conductor WHERE IdConductor = @idConductor"; //Inyeccion SQL

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    //Inyeccion SQL
                    cmd.Parameters.AddWithValue("@idConductor", x);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        c1.IdConductor = Convert.ToInt32(reader["IdConductor"].ToString()); // los campos del reader son los campos del select
                        c1.Nombre = reader["Nombre"].ToString();
                        c1.Cedula = reader["Cedula"].ToString();
                        c1.Telefono = reader["Telefono"].ToString();
                    }
                    else
                    {
                        throw new Exception("El Id " + x + " no existe en la tabla de Conductores");
                    }
                    reader.Close();
                }
            }

            return c1;
        }

        public int updateConductor(Conductor x) //Sql_inyeccion!
        {
            //out of the box = (librerias) por defecto = ya viene incluido
            string connectionString = ConfigurationManager.ConnectionStrings["TransporteStringConnection"].ToString();

            string sqlString = "UPDATE Conductor SET Nombre = @nombre, Cedula = @cedula, Telefono = @telefono  WHERE IdConductor = @idConductor";
            int retorno = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idConductor", x.IdConductor);
                    cmd.Parameters.AddWithValue("@nombre", x.Nombre);
                    cmd.Parameters.AddWithValue("@cedula", x.Cedula);
                    cmd.Parameters.AddWithValue("@telefono", x.Telefono);

                    conn.Open();

                    retorno = cmd.ExecuteNonQuery(); //esta funcion es para INSERT,UPDATE,DELETE y me devuelve el total de filas afectadas
                }
            }

            return retorno;
        }




    }
}