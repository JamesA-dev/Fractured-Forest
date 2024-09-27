using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showText : MonoBehaviour
{
    public GameObject TextTip;
    void Start()
    {
        TextTip.SetActive(false);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            TextTip.SetActive(true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            TextTip.SetActive(false);
        }
    }
}
