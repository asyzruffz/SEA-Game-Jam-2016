using UnityEngine;
using System.Collections;

namespace Yogie
{
    public class PlayerDamageSensor : MonoBehaviour
    {
        public Player player;

        private float dps = 0.5f;
        private float dpsElapsed = 0;

        /*
        void Start()
        {
            StartCoroutine(CheckAndDamage());
        }

        IEnumerator CheckAndDamage()
        {
            if (player == null)
                yield return null;

            while (true)
            {


                yield return null;
            }
        }
        */

        void OnTriggerStay(Collider col)
        {
            if (col.CompareTag("PlayerSensor"))
            {
                Player target = col.transform.GetComponent<PlayerDamageSensor>().player;

                //Debug.Log(target.playerType+" - "+player.playerType);

                if (target.playerType != player.playerType)
                {
                    if (player.currentActiveLightTrigger != null)
                    {
                        bool damagePlayer = false;

                        if (player.currentActiveLightTrigger.light.enabled && target.playerType == Player.PlayerType.GHOST)
                            damagePlayer = true;
                        else if (!player.currentActiveLightTrigger.light.enabled && target.playerType == Player.PlayerType.HUMAN)
                            damagePlayer = true;

                        if (dpsElapsed == 0 && damagePlayer)
                        {
                            dpsElapsed = dps;

                            target.playerHealth -= player.currentAttackPower;
                        }
                    }
                }
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.CompareTag("PlayerSensor"))
            {
                Player player = col.transform.GetComponent<PlayerDamageSensor>().player;

                player.RemoveActiveLightTrigger();
            }
        }

        void Update()
        {
            if (dpsElapsed > 0)
            {
                dpsElapsed -= Time.deltaTime;

                if (dpsElapsed <= 0)
                {
                    dpsElapsed = 0;
                }
            }
        }

    }
}
