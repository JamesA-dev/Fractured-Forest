using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notifiy : MonoBehaviour
{
    public GameObject showNormalUnlock;
    public GameObject showHardUnlock;
    public GameObject showExpertUnlock;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("hasEasyWon") == 1)
        {
            showNormalUnlock.SetActive(true);
        }
        if (PlayerPrefs.GetInt("hasNormalWon") == 1)
        {
            showHardUnlock.SetActive(true);
        }
        if (PlayerPrefs.GetInt("hasHardWon") == 1)
        {
            showExpertUnlock.SetActive(true);
        }
        if (PlayerPrefs.GetInt("hasImpossibleWon") == 1)
        {
            showNormalUnlock.SetActive(false);
            showHardUnlock.SetActive(false);
            showExpertUnlock.SetActive(false);
        }
    }
}
