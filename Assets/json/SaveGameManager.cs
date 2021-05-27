using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    //Creamos un Array de Enemigos
    public Enemy[] enemies;

   
    public void Guardar()
    {
        //Declaramos un JObject en el que vamos a guardar nuestro archivo.
        JObject jSaveGame = new JObject();

        //Combinar strings serializados y guardarlos en el disco duro
        for (int i = 0; i < enemies.Length; i++)
        {
            //Hacemos un enemigo a raíz del array creado anteriormente y los añadimos.
            Enemy currEnemy = enemies[i];
            JObject serializedEnemy = currEnemy.Serialize();
            jSaveGame.Add(currEnemy.name, serializedEnemy);
        }
        //Creamos un FilePath De Gaurdado con su ruta correspondiente
        string filePath = Application.persistentDataPath + "/savemultipleGameObjects.sav";
        StreamWriter sw = new StreamWriter(filePath); //Creamos un StreamWriter para el Guardado del SAVEGAME
        Debug.Log("Saving to: " + filePath);
        sw.WriteLine(jSaveGame.ToString());
        sw.Close();
    }

    public void Cargar()
    {
        //Creamos un FilePath De Gaurdado con su ruta correspondiente
        string filePath = Application.persistentDataPath + "/savemultipleGameObjects.sav";

        //Creamos un StreamReader con el Path del guardado anterior, para que lea el archivo creado anteriormente.
        StreamReader sr = new StreamReader(filePath);
        string jsonString = sr.ReadToEnd();
        Debug.Log("Loading from: " + filePath + (jsonString));
        sr.Close();

        JObject jSaveGame = JObject.Parse(jsonString);

        //Lee los enemigos del array que creamos anteriormente.
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy currEnemy = enemies[i];
            string enemyJsonString = jSaveGame[currEnemy.name].ToString();
            currEnemy.Deserialize(enemyJsonString);
        }
    }
}
