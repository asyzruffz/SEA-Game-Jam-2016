using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour {

	public GameObject item;
	public Vector2 areaSize = Vector2.one;
	public float spawnInterval = 1;

	void Start () {
		Invoke ("SpawnCoin", spawnInterval);
	}

	void SpawnCoin() {
		Vector3 spawnPosition = new Vector3(Random.Range(-areaSize.x / 2, areaSize.x / 2), 0, Random.Range(-areaSize.y / 2, areaSize.y / 2));
		GameObject coin = (GameObject)Instantiate (item, spawnPosition, Quaternion.Euler(Vector3.right * 45f));
		coin.transform.parent = transform;

		Invoke ("SpawnCoin", spawnInterval);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube (transform.position, new Vector3 (areaSize.x, 0, areaSize.y));
	}
}
