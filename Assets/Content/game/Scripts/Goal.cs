using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameBall {

public class Goal : MonoBehaviour
{
    public UnityEvent OnWin;
    public GameObject celebrationGameObject;
		public GameObject player;

		private bool breakCelebration;



    void Awake() {
      celebrationGameObject.SetActive(false);
			breakCelebration = true;
    }

  	void start(){
  		breakCelebration = true;
  	}

    void Update(){
      if(Input.GetKeyDown(KeyCode.Y)){
        celebrationGameObject.SetActive(true);
  			Debug.Log ("Celebration");
        OnWin.Invoke();
  			player.transform.position = transform.position+new Vector3(0,0,0);
      }
    }
    void OnTriggerEnter(Collider collider)
	  {
  		if ((player.GetComponent<Collider>() != null)&&(breakCelebration))
      {
  			celebrationGameObject.SetActive(true);
  			Debug.Log ("Celebration");
        OnWin.Invoke();
  			player.transform.position = transform.position+new Vector3(0,1,0);
      }
    }

}
}
