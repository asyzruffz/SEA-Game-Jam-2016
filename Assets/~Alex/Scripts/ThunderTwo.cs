using UnityEngine;
using System.Collections;

public class ThunderTwo : MonoBehaviour {

    public GameObject ThunderSource;

    // Use this for initialization
    void Start()
    {
        Invoke("doLightingWrapper", 0);
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

        Invoke("doLightingWrapper", UnityEngine.Random.Range(5, 8));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
