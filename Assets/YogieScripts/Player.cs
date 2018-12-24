using UnityEngine;
using System.Collections;

namespace Yogie
{
    public class Player : MonoBehaviour
    {
        public const string IDLE = "idle";
        public const string WALK = "walk";

        public enum PlayerType
        {
            HUMAN,
            GHOST
        }

        public enum MoveDirection
        {
            UP = 0,
            DOWN = 1,
            SIDE = 2,
            LEFT = 3,
            RIGHT = 4
        }

        [Header("Player Attributes")]
        public PlayerType playerType;

        public float movementSpeed;
        public float playerHealth = 100;
        public float attackPower = 10;

        public float curMovementSpeed;
        public float currentPlayerHealth;
        public float currentAttackPower;

        [Header("Player Component")]
        public tk2dSprite sprite;
        public tk2dSpriteAnimator animator;

        public Collider wallCollider;

        public Transform graphic;

        public Rigidbody rigidBody;

        public PlayerUI playerUI;

        public string curState = IDLE;
        public string prevState = IDLE;

        protected System.Action curStateFunction;

        protected float stateElapsed = 0;

        public bool isDead;

        private Vector3 curMovementVector;

        public LightTrigger currentActiveLightTrigger;

        public MoveDirection currentMoveDirection = MoveDirection.SIDE;

        public bool IsSkillActivated;

        public virtual void ResetProperties(bool resetParameter = true)
        {
            stateElapsed = 0;

            curMovementSpeed = movementSpeed;
            currentPlayerHealth = playerHealth;
            currentAttackPower = attackPower;

            currentMoveDirection = MoveDirection.SIDE;

            isDead = false;

            animator.AnimationEventTriggered = AnimationEventTrigger;
            animator.AnimationCompleted = AnimatorComplete;

            rigidBody.velocity = Vector3.zero;

            prevState = IDLE;
            ChangeState(IDLE);

            if (animator.Paused)
                animator.Resume();

            if (resetParameter)
                ResetParameters();

            currentActiveLightTrigger = null;

            IsSkillActivated = false;
        }

        public virtual void ResetParameters()
        {
            curMovementSpeed = movementSpeed;
        }

        public virtual void ChangeState(string state)
        {
            prevState = curState;
            curState = state;

            stateElapsed = 0;

            switch (curState)
            {
                case IDLE:

                    curStateFunction = ManageIdle;

                    break;
                case WALK:

                    curStateFunction = ManageRun;

                    break;
            }

            PlayAnim(curState);
        }

        void PlayAnim(string _curState)
        {
            animator.Play(_curState+"_"+(int)currentMoveDirection);
        }

        void FixedUpdate()
        {
            if (curStateFunction != null)
                curStateFunction();

            if (curState == IDLE)
            {
                if (rigidBody.velocity != Vector3.zero)
                    rigidBody.velocity = Vector3.zero;
            }
        }

        void Update()
        {
            if (!Camera.main.orthographic)
            {
                Vector3 scale = graphic.localScale;

                if (scale.y != 1)
                {
                    scale.y = 1;

                    graphic.localScale = scale;
                }

                graphic.rotation = Camera.main.transform.rotation;
            }
            else
            {
                if (graphic.rotation != Quaternion.identity)
                    graphic.rotation = Quaternion.identity;

                Vector3 scale = graphic.localScale;

                if (scale.y != 4.08f)
                {
                    scale.y = 4.08f;

                    graphic.localScale = scale;
                }
            }
        }

        public void damage(float dam)
        {
            playerHealth = Mathf.Clamp(playerHealth-=dam, 0, 100);

            if( playerHealth <= 0)
            {
                isDead = true;

                playerUI.dead.gameObject.SetActive(true);
            }
        }

        protected void Dead()
        {
            isDead = true;
        }

        public void StartMoving(Vector3 moveVector)
        {
            curMovementVector = moveVector;

            if (curMovementVector.z != 0)
            {
                if (curMovementVector.z >= 1)
                {
                    currentMoveDirection = MoveDirection.UP;
                }
                else
                {
                    currentMoveDirection = MoveDirection.DOWN;
                }
            }
            else
            {
                if (curMovementVector.x >= 1)
                {
                    currentMoveDirection = MoveDirection.RIGHT;
                }
                else
                {
                    currentMoveDirection = MoveDirection.LEFT;
                }
            }

            if (currentMoveDirection == MoveDirection.LEFT || currentMoveDirection == MoveDirection.RIGHT)
                currentMoveDirection = MoveDirection.SIDE;

            if (curMovementVector.x >= 1)
            {
                graphic.transform.localScale = new Vector3(1, graphic.transform.localScale.y, 1);
            }
            else
            {
                graphic.transform.localScale = new Vector3(-1, graphic.transform.localScale.y, 1);
            }

            if (curState == IDLE)
            {
                ChangeState(WALK);
            }

            TransformDirection();
        }

        public void UseSkill()
        {
            if (playerUI.energyBar.UseCharge())
            {
                if (playerType == PlayerType.HUMAN)
                {
                    GameObject[] ghostTraps = GameObject.FindGameObjectsWithTag("GhostTrap");

                    int ln = ghostTraps.Length;

                    if (ln > 0)
                    {
                        for (int i = 0; i < ln; i++)
                        {
                            Destroy(ghostTraps[i].gameObject);
                        }
                    }
                }
                else
                {
                    StartCoroutine(GhostSkill());
                }
            }
        }

        IEnumerator GhostSkill()
        {
            float duration = 5;
            float elapsed = 0;

            wallCollider.enabled = false;
            IsSkillActivated = true;

            do
            {
                elapsed += Time.deltaTime;

                if (elapsed >= duration)
                {
                    elapsed = duration;
                }

                yield return null;
            }
            while (elapsed < duration);

            IsSkillActivated = false;
            wallCollider.enabled = true;
        }

        void TransformDirection()
        {
            animator.Play(curState + "_" + (int)currentMoveDirection);
        }

        public void SetActiveLightTrigger(LightTrigger lightTrigger)
        {
            currentActiveLightTrigger = lightTrigger;

            if (currentActiveLightTrigger != null && IsSkillActivated && playerType == PlayerType.GHOST)
            {
                TriggerCurrentActiveLight();
            }
        }

        public void RemoveActiveLightTrigger()
        {
            currentActiveLightTrigger = null;
        }

        public void TriggerCurrentActiveLight()
        {
            if (currentActiveLightTrigger != null)
            {
                currentActiveLightTrigger.Trigger(this);
            }
        }

        public void StopMoving()
        {
            if (curState == WALK)
            {
                ChangeState(IDLE);

                StopAnyMovement();
            }
        }

        void StopAnyMovement()
        {
            rigidBody.velocity = Vector3.zero;
        }

        public virtual void Broadcast(string message = "") 
        {
        
        }

        protected void AnimationEventTrigger(tk2dSpriteAnimator arg1, tk2dSpriteAnimationClip arg2, int arg3) 
        { 
        
        }

        protected void AnimatorComplete(tk2dSpriteAnimator arg1, tk2dSpriteAnimationClip arg2) 
        {
            
        }

        protected void OnCollisionEnter2D(Collision2D col) 
        {
            
        }

        protected void ManageIdle() 
        {
            
        }

        protected void ManageRun() 
        {
            if (curMovementVector != Vector3.zero)
            {
                if (curMovementVector.x != 0 && curMovementVector.z != 0)
                    curMovementVector *= 0.75f;

                rigidBody.velocity = curMovementVector * curMovementSpeed;
            }
        }
    }
}
