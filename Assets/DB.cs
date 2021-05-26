using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite; //Se añade para usar la DB de SQLITE.
using System.Data;

public class DB : MonoBehaviour
{
    public InputField NombreARMA; // Añade el InputField para añadir el nombre del arma.
    private string DBInventario = $"URI=file:{Application.streamingAssetsPath}/Inventario.db";
    // Start is called before the first frame update
    void Start()
    {
        //Carga el Método de Crear la Base de Datos.
        CrearDB();

    }

    // Update is called once per frame
    public void CrearDB()
    {
        using (var connection = new SqliteConnection(DBInventario))
        {
            connection.Open();

            //Crea un Objeto y le asigna el control de la base de datos.
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS weapons (nombre VARCHAR(20) NOT NULL);";
                command.ExecuteReader();
                //Carga el comando SQL.
            }
            connection.Close(); //Cierra la Conexion con la DB.
        }
    }

    public void AddArma(string nombreArma)
    {
        using (var connection = new SqliteConnection(DBInventario))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO weapons (nombre) VALUES ('" + nombreArma + "');"; //Añade a la tabla el nombre del arma.
                command.ExecuteNonQuery(); //Carga el comando SQL.
            }
            connection.Close(); //Cierra la Conexion con la DB.
        }
    }
    public void ensenarArma()
    {
        using (var connection = new SqliteConnection(DBInventario))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM weapons;"; //SELECCIONA TODAS LAS ARMAS DE LA BASE DE DATOS.

                //Enseña en un DEBUG el nombre del arma y el daño que hace en cada fila, usa un READER para sustituir el "nombre" por el nombre del arma que está en la tabla.
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        Debug.Log("nombre: " + reader["nombre"]);
                    reader.Close();
                }
            }
            connection.Close();
        }
       
    }

    public void AnadirArmaBoton()
    {
        AddArma(NombreARMA.text); //Añade Como nombre "Lo escrito en el InputField.".
    }
}
