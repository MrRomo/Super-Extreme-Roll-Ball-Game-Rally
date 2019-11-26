using UnityEngine;
using UnityEngine.UI;


namespace UnityStandardAssets.Vehicles.Ball
{
    public class BallUserControl : MonoBehaviour
    {
        private Ball ball; // Reference to the ball controller.
        public Transform player;
        private Vector3 move;
        private bool jump; // whether the jump button is currently pressed
        public Text FPS;   //muestra la Carga del Freeze
        private int FPSc=0; //Controla la carga del freeze
        private void Awake()
        {
            // Set up the reference.
            ball = GetComponent<Ball>();
            
        }


        private void Update()
        {
            ball.Fall = player.transform.position.y;

            // Get the axis and jump input.

            if (FPSc < 300)
            {
                FPSc = FPSc + 2;

                if (FPSc >= 289)
                {
                    FPS.text = "FPS: " + FPSc.ToString() + "/d Active Freeze";
                }
                else
                {
                    FPS.text = "FPS: " + FPSc.ToString() + "/d Inactive Freeze";
                }
            }
            if (ball.Fall <= -2.0000)
            {
                ball.Life = ball.Life - 1;
                ball.SetCountLife();
                ball.Respawn();
            }
            if (ball.Count >= 50)
            {
                ball.SetWin();

            }

            if ((Input.GetAxis("Cancel")>0)&&(FPSc>290))
            {
                ball.freezeSK();
                FPSc = 0;
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            jump = Input.GetButton("Jump");

           
            move = (v * Vector3.forward + h * Vector3.right).normalized;
            
        }


        private void FixedUpdate()
        {
            // Call the Move function of the ball controller
            ball.Move(move, jump);
            jump = false;
            

             }

        
    }
}
