
using UnityEngine;
using UnityEngine.UI;


namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : MonoBehaviour
    {
        public float m_MovePower = 5; // The force added to the ball to move it.
        public bool m_UseTorque = true; // Whether or not to use torque to move the ball.
        public bool m_UseFly = true; // Whether or not to use torque to move the ball.
        public float m_MaxAngularVelocity = 50; // The maximum velocity the ball can rotate at.
        public float m_JumpPower = 2; // The force added to the ball when it jumps.
        public float Fall;// Posicion en Y, para detectar caidas del escenario
        public float Count;//Lleva la cuenta del puntaje
        public Transform player;
        public float Life; //numero de vidas restante

        private BallUserControl ball;
        private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
        private Rigidbody m_Rigidbody;
        public int LifeC;
        public Text CountText;
        public Text CountLife;
        public Text CountJump;
        public Text Winner;
        public float repulse; //Fuerza opuesta al vector de movimiento
        private int toch; //cuenta si esta en el suelo
        public Transform[] SpawnPoint; //Contiene los Spawns
   

        private void Start()
        {
            QualitySettings.antiAliasing = 4;
            Count = 0;
            repulse = 1;
            Life = 100;
            SetCountText();
            SetCountLife();
            SetCountJump();
            SetWin();
            Fall = 0;
            
            m_Rigidbody = GetComponent<Rigidbody>();
            // Set the maximum angular velocity.
            GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
            //Maxima velocidad angular
                
        }


        public void Move(Vector3 moveDirection, bool jump)
        {
            // If using torque to rotate the ball...
            if (m_UseTorque)
            {
                // ... add torque around the axis defined by the move direction.
                m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * m_MovePower);
            }
            else
            {
                // Otherwise add force in the move direction.
                m_Rigidbody.AddForce(moveDirection * m_MovePower);
            }

            // If on the ground and jump is pressed...

            
            if (Input.GetKeyDown("space") && (toch >= 1))
            {
                m_Rigidbody.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
                toch = toch - 1;
                SetCountJump();
            }
            else
            {
                if (m_UseFly == true)
                {
                    m_Rigidbody.AddForce(new Vector3(moveDirection.x / 5, 0, moveDirection.z / 5), ForceMode.VelocityChange);
                    repulse = 1;

                }
            }
        }
            
        


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PickUp"))
            {
                other.gameObject.SetActive(false);
                Count = Count + 1;
                SetCountText();
            }
            if (other.gameObject.CompareTag("PwUp"))
            {
                other.gameObject.SetActive(false);
                toch = toch + 1;
                SetCountJump();
            }




        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Floor"))
            {
                toch = 1;
                SetCountJump();
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                repulse = -40;
                Life = Life - 1;
                SetCountLife();
                toch = 1;
                LifeC = 1;
                Respawn();

            }


        }
        public void SetCountText()
        {
            CountText.text = "Score : " + Count.ToString();
        }
        public void SetWin()
        {
            if (Count >= 37)
            { Winner.text = "Felicitades Putito \n has ganado"; }
            else { Winner.text = " "; }
        }
        public void SetCountLife()
        {
            CountLife.text = "Life: " + Life.ToString();
        }
        public void SetCountJump()
        {
            CountJump.text = "Saltos: " + toch.ToString();
        }
        public void freezeSK()
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            m_Rigidbody.constraints = RigidbodyConstraints.None;
        }
        public void Respawn()
        {
            if (Life <= 0)
            {

                //Reiniciar();
            }
            if ((LifeC == 1) || (Fall <= -2))
            {
                if (Count < 20)
                {
                    transform.position = SpawnPoint[0].transform.position;
                    LifeC = 0;
                }
                if ((Count >= 20) && (Count < 27))
                {
                    player.transform.position = SpawnPoint[1].transform.position;
                    LifeC = 0;
                }
                if ((Count >= 27) && (Count < 35))
                {
                    player.transform.position = SpawnPoint[2].transform.position;
                    LifeC = 0;
                }
                if ((Count >= 36))
                {
                    player.transform.position = SpawnPoint[3].transform.position;
                    LifeC = 0;
                }



            }

            repulse = 1;
            m_Rigidbody.constraints =  RigidbodyConstraints.FreezeAll;
            m_Rigidbody.constraints = RigidbodyConstraints.None;





        }
    }
}
