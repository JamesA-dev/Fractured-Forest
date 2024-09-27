using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class skipStart : MonoBehaviour
{
    public Flashlight flashlightScript;
    public GameObject diologue;
    public GameObject barrierWall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            flashlightScript.canUseFlashlight = true;
            barrierWall.SetActive(false);
            diologue.SetActive(false);
        }
    }
}
