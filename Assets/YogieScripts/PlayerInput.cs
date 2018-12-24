using UnityEngine;
using System.Collections;

namespace Yogie
{
    public class PlayerInput : MonoBehaviour
    {
        public enum PlayerInputMode
        {
            PLAYER1,
            PLAYER2,
            PLAYER3,
            PLAYER4
        }

        protected bool leftTrigger = false;
        protected bool rightTrigger = false;
        protected bool downTrigger = false;
        protected bool upTrigger = false;

        public PlayerInputMode inputMode;

        public Player player;

        private Vector3 playerMoveVector;

        public bool playerMoving;

        void Update()
        {
            if (player == null)
                return;

            if (player.isDead) 
            {
                player.gameObject.SetActive(false);
                return;
            }

            if (inputMode == PlayerInputMode.PLAYER1)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    leftTrigger = true;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    rightTrigger = true;
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    upTrigger = true;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    downTrigger = true;
                }
                if (Input.GetKeyUp(KeyCode.A))
                {
                    leftTrigger = false;
                }

                if (Input.GetKeyUp(KeyCode.D))
                {
                    rightTrigger = false;
                }

                if (Input.GetKeyUp(KeyCode.W))
                {
                    upTrigger = false;
                }

                if (Input.GetKeyUp(KeyCode.S))
                {
                    downTrigger = false;
                }

                if (Input.GetKeyUp(KeyCode.Q))
                {
                    player.TriggerCurrentActiveLight();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GetComponent<TrapSpawner>().SpawnTrap();
                }

                if (Input.GetKeyUp(KeyCode.R))
                {
                    player.UseSkill();
                }
            }
            else if (inputMode == PlayerInputMode.PLAYER2)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    leftTrigger = true;
                }
                else if (Input.GetKeyDown(KeyCode.L))
                {
                    rightTrigger = true;
                }

                if (Input.GetKeyDown(KeyCode.I))
                {
                    upTrigger = true;
                }
                else if (Input.GetKeyDown(KeyCode.K))
                {
                    downTrigger = true;
                }

                if (Input.GetKeyUp(KeyCode.J))
                {
                    leftTrigger = false;
                }

                if (Input.GetKeyUp(KeyCode.L))
                {
                    rightTrigger = false;
                }

                if (Input.GetKeyUp(KeyCode.I))
                {
                    upTrigger = false;
                }

                if (Input.GetKeyUp(KeyCode.K))
                {
                    downTrigger = false;
                }

                if (Input.GetKeyUp(KeyCode.U))
                {
                    player.TriggerCurrentActiveLight();
                }

                if (Input.GetKeyDown(KeyCode.O))
                {
                    GetComponent<TrapSpawner>().SpawnTrap();
                }

                if (Input.GetKeyUp(KeyCode.P))
                {
                    player.UseSkill();
                }
            }
            else if (inputMode == PlayerInputMode.PLAYER3)
            {
                if (Input.GetAxis("Horizontal") <= -0.5f)
                {
                    leftTrigger = true;
                }
                else if (Input.GetAxis("Horizontal") >= 0.5f)
                {
                    rightTrigger = true;
                }

                if (Input.GetAxis("Vertical") >= 0.5f)
                {
                    upTrigger = true;
                }
                else if (Input.GetAxis("Vertical") <= -0.5f)
                {
                    downTrigger = true;
                }

                if (Input.GetAxis("Horizontal") == 0)
                {
                    leftTrigger = false;
                    rightTrigger = false;
                }

                if (Input.GetAxis("Vertical") == 0)
                {
                    upTrigger = false;
                    downTrigger = false;
                }

                int number = 1;

                if (Input.GetKeyDown("joystick "+number+" button 1"))
                {
                    player.TriggerCurrentActiveLight();
                }

                if (Input.GetKeyDown("joystick " + number + " button 0"))
                {
                    GetComponent<TrapSpawner>().SpawnTrap();
                }

                if (Input.GetKeyDown("joystick " + number + " button 3"))
                {
                    player.UseSkill();
                }
            }
            else if (inputMode == PlayerInputMode.PLAYER4)
            {
                if (Input.GetAxis("Horizontal2") <= -0.5f)
                {
                    leftTrigger = true;
                }
                else if (Input.GetAxis("Horizontal2") >= 0.5f)
                {
                    rightTrigger = true;
                }

                if (Input.GetAxis("Vertical2") >= 0.5f)
                {
                    upTrigger = true;
                }
                else if (Input.GetAxis("Vertical2") <= -0.5f)
                {
                    downTrigger = true;
                }

                if (Input.GetAxis("Horizontal2") == 0)
                {
                    leftTrigger = false;
                    rightTrigger = false;
                }

                if (Input.GetAxis("Vertical2") == 0)
                {
                    upTrigger = false;
                    downTrigger = false;
                }

                int number = 2;

                if (Input.GetKeyDown("joystick " + number + " button 1"))
                {
                    player.TriggerCurrentActiveLight();
                }

                if (Input.GetKeyDown("joystick " + number + " button 0"))
                {
                    GetComponent<TrapSpawner>().SpawnTrap();
                }

                if (Input.GetKeyDown("joystick " + number + " button 3"))
                {
                    player.UseSkill();
                }
            }

            bool moving = false;

            playerMoveVector = Vector3.zero;

            if (rightTrigger)
            {
                playerMoveVector.x = 1;
                moving = true;
            }
            else if (leftTrigger)
            {
                playerMoveVector.x = -1;
                moving = true;
            }

            if (upTrigger)
            {
                playerMoveVector.z = 1;
                moving = true;
            }
            else if (downTrigger)
            {
                playerMoveVector.z = -1;
                moving = true;
            }

			var trapped = GetComponent<Stunnable> ();
			bool isStunned;
			if (trapped) {
				isStunned = trapped.stunned;
			} else {
                print("No Stunnable component!");
				isStunned = false;
			}

			if (moving && !isStunned)
            {
                player.StartMoving(playerMoveVector);

                playerMoving = true;
            }
            else
            {
                ResetMoveParams();
            }
        }

        private void ResetInput()
        {
            leftTrigger = false;
            rightTrigger = false;
            downTrigger = false;
            upTrigger = false;
        }

        private void ResetMoveParams()
        {
            playerMoving = false;

            player.StopMoving();
        }
    }
}
