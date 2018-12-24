using UnityEngine;
using System.Collections;

namespace Yogie
{
    public class LightTrigger : MonoBehaviour
    {
        public Light light;

        public GameObject spriteCover;

        public bool IsCoolDown;

        void OnTriggerStay(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                Player player = col.transform.GetComponent<PlayerCollider>().player;

                player.SetActiveLightTrigger(this);
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                Player player = col.transform.GetComponent<PlayerCollider>().player;

                player.RemoveActiveLightTrigger();
            }
        }

        public void Trigger(Player player)
        {
            //Debug.Log(IsCoolDown+" "+player.playerType);

            if (IsCoolDown)
                return;

            bool cooldown = false;

            if (player.playerType == Player.PlayerType.HUMAN)
            {
                if (!light.enabled)
                {
                    light.enabled = true;
                    cooldown = true;
                }

                if (spriteCover != null)
                {
                    if (spriteCover.activeSelf)
                    {
                        spriteCover.SetActive(false);
                        cooldown = true;
                    }
                }
            }
            else
            {
                if (light.enabled)
                {
                    light.enabled = false;
                    cooldown = true;
                }

                if (spriteCover != null)
                {
                    if (!spriteCover.activeSelf)
                    {
                        spriteCover.SetActive(true);
                        cooldown = true;
                    }
                }
            }

            if (cooldown)
            {
                StopCoroutine(IECoolDown());
                StartCoroutine(IECoolDown());
            }
        }

        IEnumerator IECoolDown()
        {
            IsCoolDown = true;

            float coolDown = 1.0f;
            float elapsed = 0;

            do
            {
                elapsed += Time.deltaTime;

                if (elapsed >= coolDown)
                    elapsed = coolDown;

                yield return null;
            }
            while (elapsed < coolDown);

            IsCoolDown = false;
        }
    }
}
