using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public float floatingSpeed = 4;
	public float amplitude = 0.1f;

	private Vector3 startPos;

	void Start () {
		startPos = transform.position;
	}

	void Update () {
		Floating ();
	}

	void OnTriggerEnter(Collider target) {

        if (target.CompareTag("Player") || target.CompareTag("Ghost"))
        {
            Collect character = target.transform.parent.GetComponent<Collect>();
            if (character)
            {
                character.ObtainCoin();

                character.GetComponent<Yogie.Player>().playerUI.energyBar.AddCollectedPerCharge();

                Destroy(gameObject);
            }
        }
	}

	private void Floating() {
		float wave = (Mathf.Sin(Time.time * floatingSpeed) + 1) * amplitude;
		transform.position = startPos + Vector3.up * wave;
	}
}
