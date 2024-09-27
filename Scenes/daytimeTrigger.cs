using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daytimeTrigger : MonoBehaviour
{
    public GameObject diffObject;
    public dificultyLocked diffLockedScript;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject flashlight;
    public GameObject sun;
    public GameObject postProcess;
    public GameObject ambience;
    public Camera playerCam;
    public AudioSource caveSound;
    public float speed = 0.01f;
    public float startNumber = 0f;
    public float endNumber = 4f;
    public Color startColor;
    public Color endColor;
    float startTime;
    public bool startLerp = false;

    void Update()
    {
        if (startLerp == true)
        {
            float t = (Time.time - startTime) * speed;
            playerCam.GetComponent<Camera>().backgroundColor = Color.Lerp(startColor, endColor, t);
            sun.GetComponent<Light>().intensity = Mathf.Lerp(startNumber, endNumber, t);
        }
    }

    void OnTriggerEnter(Collider other) {
        startLerp = true;
        startTime = Time.time;
        caveSound.Stop();
        enemy1.SetActive(false);
        enemy2.SetActive(false);
        flashlight.SetActive(false); 
        ambience.SetActive(false); 
        postProcess.SetActive(false); 
        sun.SetActive(true);

        if (diffLockedScript.dificulty == 1)
        {
            diffLockedScript.easyWon = true;
        }
        if (diffLockedScript.dificulty == 2)
        {
            diffLockedScript.normalWon = true;
        }
        if (diffLockedScript.dificulty == 3)
        {
            diffLockedScript.hardWon = true;
        }
        if (diffLockedScript.dificulty == 4)
        {
            diffLockedScript.impossibleWon = true;
        }
    }
}
