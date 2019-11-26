using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace GameBall
{
    public class MenuInicio : MonoBehaviour
    {
		private Scene NameScene;
		public GameObject MenuObject,PlayerOBJ,MenuNombre, CanvasLevelWindow;

		public MeshRenderer Player;
		private bool continues, setWindowC;


		public void Start(){
      Debug.Log("Escena = " + SceneManager.GetActiveScene ().name + " \nCheckName: " + PlayerPrefs.GetInt("CheckName",0));
		  if (SceneManager.GetActiveScene ().name == "MenuInicio") {
        PlayerOBJ.SetActive(false);
        MenuNombre.SetActive (false);
				MenuObject.SetActive(true);
        setWindowC=false;
        CanvasLevelWindow.SetActive(false);

				if (PlayerPrefs.GetInt("CheckName",0)== 0) {
					MenuNombre.SetActive (true);
          MenuObject.SetActive(false);
        }
      }
		}
    public void setWindowLevel(){
      setWindowC=!setWindowC;
      CanvasLevelWindow.SetActive(setWindowC);
    }

		public void CargaNivel(GameObject pNivel)
		{
			SceneManager.LoadScene(pNivel.name);
		}

		public void RetunrNivel(string pR_Nivel){
			SceneManager.LoadScene (pR_Nivel, LoadSceneMode.Single);
		}

		public void Levels(){
      SceneManager.LoadScene("MenuInicio", LoadSceneMode.Single);
      PlayerPrefs.SetInt("continue", 1);
      Debug.Log("continue? " + PlayerPrefs.GetInt("continue",0));
		}

		public void opcionesGraficas(int i){
			string[] names = QualitySettings.names;
			QualitySettings.SetQualityLevel (i, true);
		}

    public void Salir()
    {
        Application.Quit();
        Debug.Log("SalirDeJuego");
    }

    }


}
