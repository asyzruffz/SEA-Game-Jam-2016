using UnityEngine;
using System.Collections;

public class ObjectTrigger : MonoBehaviour {

	public Triggerable triggerObject;
	public bool ignoreTrigger;
    public bool sticky;

	private bool isOpened;

	void OnTriggerEnter(Collider target) {
		if (ignoreTrigger)
			return;

		if (target.gameObject.CompareTag("Player") || target.gameObject.CompareTag("Ghost")) {
			triggerObject.Open();
		}
	}

	void OnTriggerExit(Collider target) {
		if (ignoreTrigger || sticky)
			return;

		if (target.gameObject.CompareTag("Player") || target.gameObject.CompareTag("Ghost")) {
			triggerObject.Close();
		}
	}

	public void Toggle() {
		SwitchOn (!isOpened);
	}

	public void SwitchOn(bool on) {
		if (on) {
			isOpened = true;
			triggerObject.Open ();
		} else {
			isOpened = false;;
			triggerObject.Close ();
		}
	}

	/*void OnDrawGizmos() {
		Gizmos.color = ignoreTrigger ? Color.gray : Color.green;
		Gizmos.DrawWireCube (transform.position, transform.lossyScale);
	}*/

}
