using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showText2 : MonoBehaviour
{
    public GameObject monster1;
    public dificultyLocked DificultyLocked;

    private void OnTriggerEnter(Collider text1)
    {
        if (text1.gameObject.layer == 3)
        {
            monster1.SetActive(false);
            DificultyLocked.sprintTip.SetActive(false);
            DificultyLocked.crouchTip.SetActive(true);
        }
    }
}
