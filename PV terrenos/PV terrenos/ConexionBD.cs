/*  
            ConexionBD.cs    Ver 1.0   12/08/2013
 *      Esta clase va a servir para poder hacer el manejo de la base de datos
 *      en PosrgresSQL a partir de dos funciones:
 *          -ejecutarQuery()
 *          -consultarQuery()
 *          
 *      Pekoso Garcia 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Windows.Forms;
using System.Data;
using System.Configuration;

namespace PV_terrenos
{
    class ConexionBD
    {
        //Estas son las variables globales de la clase, los valores seran almacenados en App.config como XML
        String servidor, usuario,BaseDeDatos;
        IDbConnection conexionDB;
        NpgsqlConnection conexionDBnpg;
        
        public ConexionBD()
        {
            try
            {
                //Primero leemos los atributos del XML App.config
                servidor = ConfigurationManager.AppSettings["server"].ToString();
                //usuario = ConfigurationManager.AppSettings["user"].ToString();
                usuario = "Usuario";
                BaseDeDatos = ConfigurationManager.AppSettings["baseDeDatos"].ToString();

                //Una vez leidos, creamos las conexiones con la base de datos. Uno para insertar y otro para consultar
                conexionDB = new NpgsqlConnection("Server=" + servidor + ";Port=5432;Database=" + BaseDeDatos + ";User=" + usuario + ";Timeout=20");
                conexionDBnpg = new NpgsqlConnection("Server=" + servidor + ";Port=5432;Database=" + BaseDeDatos + ";User=" + usuario + ";Timeout=20");
            }
            catch (Exception Ee)//En caso de exception, se manda un mensaje y se cierra el programa alv!
            {
                MessageBox.Show("Error al crear la conexion con la Base de Datos\n" + Ee.ToString(), "Error!!", MessageBoxButtons.OK);
                Environment.Exit(0);
            }
        }

        /*
         *      Este metodo servira para ejecutar un query que no tenga resultset (resultados uties), como por
         *      ejemplo:
         *          -insert
         *          -update
         *          -delete
         *      
         *      Si el Query es ejecutado correctamente, te devolvera un valor TRUE.
         */
        public bool ejecutaQuery(String query)
        {
            bool correcto=false;
            try
            {
                if (query.Length > 10)
                {
                    conexionDB.Open();
                    IDbCommand comando = conexionDB.CreateCommand();
                    comando.CommandText=query;
                    IDataReader leector = comando.ExecuteReader();
                    conexionDB.Close();
                    correcto = true;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error al ejecutar el comando\n" + ee.ToString(), "Error de Conexion", MessageBoxButtons.OK);
                correcto = false;
            }

            return correcto;
        }

        /*
         *   Este metodo sirve para obtener el resultado de un query. Te los regresará en tipo de dato DataTable para
         *   ser usado para llenar una tabla.
         */
        public DataTable consultarQuery(String query)
        {
            bool correcto = false;
            DataTable elResultado = new DataTable();
            try
            {
                if (query.Length > 10)
                {
                    conexionDB.Open();
                    NpgsqlDataAdapter adaptador = new NpgsqlDataAdapter(query, conexionDBnpg);
                    adaptador.Fill(elResultado);
                    correcto = true;
                }
            }
            catch (Exception ee)    
            {
                MessageBox.Show("Error al ejecutar el comando\n" + ee.ToString(), "Error de Conexion", MessageBoxButtons.OK);
                correcto = false;
            }
            if (correcto)
                return elResultado;
            else
                return null;
        }
    }
}
