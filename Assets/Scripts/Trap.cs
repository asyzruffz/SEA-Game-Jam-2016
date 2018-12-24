using UnityEngine;
using System.Collections;

public class Trap : Triggerable {

	public override void Open() {
		print ("Trap triggered!");
	}

	public override void Close() {
		
	}

	public void Stunt(GameObject target) {
		var stun = target.transform.parent.GetComponent<Stunnable> ();
        Debug.Log(target.name);
        if (stun) {
            
			stun.StunWithDuration (2);
        }
        Destroy(gameObject);
    }
}
