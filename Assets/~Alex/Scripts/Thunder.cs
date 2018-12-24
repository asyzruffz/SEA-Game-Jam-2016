using UnityEngine;
using System.Collections;

public class Thunder : MonoBehaviour {

    public GameObject ThunderSource;

	// Use this for initialization
	void Start () {
        Invoke("doLightingWrapper", UnityEngine.Random.Range(3,6));
        //doLightingWrapper();
    }
	
    void doLightingWrapper()
    {
        StartCoroutine(doLighting());
    }

    IEnumerator doLighting()
    {
        ThunderSource.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        ThunderSource.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        ThunderSource.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        ThunderSource.SetActive(false);

        Invoke("doLightingWrapper", UnityEngine.Random.Range(3, 12));
    }

	// Update is called once per frame
	void Update () {
	
	}
}
