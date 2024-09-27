using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashLightFlicker : MonoBehaviour
{
    public GameObject flashlight;
    private Flashlight flashlightScript;
    public AudioSource source1;

    private void Start()
    {
        flashlightScript = flashlight.GetComponent<Flashlight>();
    }

    void OnTriggerEnter(Collider other) {
        flashlight.GetComponent<Flashlight>().ReplaceBattery(-3.5f);
        flashlightScript.drainOverTime = true;
        StartCoroutine(waitUnitPlayed());
    }

    IEnumerator waitUnitPlayed()
    {
        yield return new WaitForSeconds(3f);
        source1.Play();
    }
}
