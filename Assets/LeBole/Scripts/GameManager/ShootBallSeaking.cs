using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameBall{
public class ShootBallSeaking : MonoBehaviour {

	// Use this for initialization
	public GameObject PrefabShootingBall, ShootingBall1,ShootingBall2,player,Expose,PlayerOBJ,publicFall;
	public ParticleSystem ExplosionParticles;




	bool lifeball;


	void Start () {
		lifeball=true;
	}

	void OnTriggerStay(Collider Other){
		if(lifeball){
			if (Other.gameObject.CompareTag("Player")){
				lifeball=false;
				Vector3 Pos = new Vector3 (0,0,2);
				GameObject ShootingBallA = Instantiate(PrefabShootingBall) as GameObject;
				ShootingBallA.GetComponent<SearchPlayer>().WP=player;
				ShootingBallA.GetComponent<SearchPlayer>().Home=Expose;
				ShootingBallA.GetComponent<SearchPlayer>().publicfall=publicFall;

                ShootingBallA.transform.position=Expose.transform.position;

				ShootingBall1=ShootingBallA;
				GameObject ShootingBallB = Instantiate(PrefabShootingBall) as GameObject;
				ShootingBallB.GetComponent<SearchPlayer>().WP=player;
				ShootingBallB.GetComponent<SearchPlayer>().Home=Expose;
				ShootingBallB.GetComponent<SearchPlayer>().publicfall=publicFall;
                    
                ShootingBallB.transform.position=Expose.transform.position+Pos;
				ShootingBall2=ShootingBallB;
				PlayerOBJ.GetComponent<TimerDeath>().EngineAudioDeath();
				ExplosionParticles.Play();
			}
		}
	}

	void Update(){
		if((ShootingBall1==null)&&(ShootingBall2==null)) {
			lifeball=true;
		}
        if((player.GetComponent<HealtBall>().m_Dead == true))
        {
            Destroy(ShootingBall1);
            Destroy(ShootingBall2);

        }
    }
}
}
