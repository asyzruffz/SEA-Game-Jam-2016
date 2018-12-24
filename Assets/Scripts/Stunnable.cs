using UnityEngine;
using System.Collections;

public class Stunnable : MonoBehaviour {
    
	public GameObject dizzyParticle;
	public Vector3 offset = new Vector3(0f, 1f, 0);
    public bool stunned;

	private float stunDuration;

	public void StunWithDuration (float duration) {
		stunDuration = duration;
		StartCoroutine ("Stunned");
	}

	IEnumerator Stunned() {
		stunned = true;
		GameObject stars = (GameObject)Instantiate (dizzyParticle, transform.position, Quaternion.identity);
		stars.transform.parent = transform;
        stars.transform.localPosition = offset;
        yield return new WaitForSeconds (stunDuration);
		Destroy (stars);
		stunned = false;
	}
}
