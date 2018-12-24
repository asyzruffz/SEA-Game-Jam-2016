using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	public ObjectTrigger[] triggers;
	public bool sticky;
    public bool autoReset;
    public float stickyDuration;
	public bool down;

	private float timer = 0;
    //private Animator animator;
    private bool entered = false;

    void Start () {
		//animator = GetComponent<Animator> ();
	}
	
	void Update () {
		if (autoReset) {
			if (down) {
				if (timer >= stickyDuration) {
					timer = 0;
					//animator.SetInteger("AnimState", 0);
					down = false;

					foreach (ObjectTrigger trigger in triggers) {
						if (trigger != null)
							trigger.SwitchOn (false);
					}
				}

				timer += Time.deltaTime;
			}
		}
    }

	protected void OnTriggerEnter(Collider target){
        if (target.gameObject.tag == "Player")
        {
            //animator.SetInteger("AnimState", 1);
            down = true;
        }

		foreach (ObjectTrigger trigger in triggers) {
			if (trigger != null) {
				if(!autoReset)
					trigger.Toggle ();
				else
					trigger.SwitchOn (true);
			}
		}
	}

	protected void OnTriggerExit(Collider target){

		if (sticky && down)
			return;

		//animator.SetInteger ("AnimState", 0);
		down = false;
        entered = false;


        foreach (ObjectTrigger trigger in triggers) {
			if(trigger != null)
				trigger.SwitchOn(false);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = sticky ? Color.red : Color.blue;

		foreach (ObjectTrigger trigger in triggers) {
			if(trigger != null)
				Gizmos.DrawLine(transform.position, trigger.triggerObject.transform.position);
		}

	}

    public bool isDown()
    {
        if (down && !entered)
        {
            entered = true;
            return true;
        }

        return false;
    }

}
