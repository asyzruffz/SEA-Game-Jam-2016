using UnityEngine;
using System.Collections;

public class TrapSpawner : MonoBehaviour {

	public GameObject trapPrefab;
	public float spawnCooldown = 1;

	private float timer = 0;
	private bool ableToSpawn = true;

	void Start () {
	
	}

	void Update () {
		if (timer <= spawnCooldown) {
			ableToSpawn = false;
			timer += Time.deltaTime;
		} else {
			ableToSpawn = true;
		}
	}

	public void SpawnTrap() {
		if (ableToSpawn) {
			timer = 0;
			GameObject trap = (GameObject)Instantiate (trapPrefab, transform.position + Vector3.up * 0.2f, Quaternion.identity);

			if (gameObject.CompareTag ("Player")) {
				trap.tag = "PlayerTrap";
				trap.layer = LayerMask.NameToLayer("PlayerTrap");
			} else if (gameObject.CompareTag ("Ghost")) {
				trap.tag = "GhostTrap";
				trap.layer = LayerMask.NameToLayer("GhostTrap");
			}
		}
	}
}
