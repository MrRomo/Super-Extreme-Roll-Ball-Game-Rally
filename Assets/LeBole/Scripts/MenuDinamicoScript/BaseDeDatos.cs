using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;

namespace GameBall
{
	public class BaseDeDatos : MonoBehaviour {

	public Text TextoMostrado;
	public String BDataFile;
	// Use this for initialization
	void Start () {
		BDataFile ="URI=file:" + Application.dataPath + "/Gameball_SQLite.sqlite";
		//BDataFile ="URI=file:" +  Application.persistentDataPath + "/" + "Gameball_SQLite.sqlite";

    TextoMostrado.text="";
		MostrarRegistros();

	}

	public void  MostrarRegistros(){
		using(IDbConnection ConexionBD = new SqliteConnection(BDataFile)) //Creamos conexion con base de datos
		{
			ConexionBD.Open(); //abrimos la conexion con la base de datos.
			using(IDbCommand comandoBD = ConexionBD.CreateCommand()) //Creamos conexion que va a enviar el comando que consultara en la base de datos
			{
				string consultaSQL = "SELECT rowid, * FROM Items WHERE tipo = 'Skin'"; //Montamos la query
				comandoBD.CommandText = consultaSQL;

				using (IDataReader puntero = comandoBD.ExecuteReader())
				{
					while(puntero.Read()){
						TextoMostrado.text= TextoMostrado.text + "\n" + puntero.GetString(3);
					}
					ConexionBD.Close();
					puntero.Close();
				}
			}
		}
	}
}
}
