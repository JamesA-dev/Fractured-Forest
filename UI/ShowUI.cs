using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{

    public GameObject uiObject;

    void Start()
    {
        uiObject.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "player")
        {

            uiObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Pplayer")
        {
            uiObject.SetActive(false);
        }
    }
}