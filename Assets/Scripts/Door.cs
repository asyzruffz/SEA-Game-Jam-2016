using UnityEngine;
using System.Collections;

public class Door : Triggerable {

	public float closeDelay = 0;

	void DissableCollider() {
		GetComponent<Collider>().enabled = false;
	}

	void EnableCollider() {
		GetComponent<Collider>().enabled = true;
	}

	public override void Open() {
		// print ("Door Open!");
		DissableCollider ();
		GetComponent<MeshRenderer>().material.color = Color.green;
        GetComponent<MeshRenderer>().enabled = false;

    }

	public override void Close() {
		StartCoroutine (CloseDelayed ());
	}

	private IEnumerator CloseDelayed() {
		yield return new WaitForSeconds(closeDelay);

		// print ("Door Closed!");
		EnableCollider ();
		GetComponent<MeshRenderer>().material.color = Color.red;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
