using UnityEngine;
using System.Collections;

public class TrapTrigger : ObjectTrigger {

	void OnTriggerEnter(Collider target) {
		if (ignoreTrigger)
			return;

         if (target.transform.parent.CompareTag("Ghost")) {
			if (gameObject.CompareTag("PlayerTrap")) {
				triggerObject.Open();
				((Trap)triggerObject).Stunt (target.gameObject);
			}
		}
        else if (target.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("GhostTrap"))
            {
                triggerObject.Open();
                ((Trap)triggerObject).Stunt(target.gameObject);
            }
        }
    }

	void OnTriggerExit(Collider target) {
		if (ignoreTrigger || sticky)
			return;

		if (target.gameObject.CompareTag("Player")) {
			if (gameObject.CompareTag("GhostTrap")) {
				triggerObject.Close();
			}
		} else if (target.gameObject.CompareTag("Ghost")) {
			if (gameObject.CompareTag("PlayerTrap")) {
				triggerObject.Close();
			}
		}
	}

}
