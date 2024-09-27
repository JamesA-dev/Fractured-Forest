using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dificultyLocked : MonoBehaviour
{
    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt("hasEasyWon", 0);

        PlayerPrefs.SetInt("hasNormalWon", 0);

        PlayerPrefs.SetInt("hasHardWon", 0);

        PlayerPrefs.SetInt("hasImpossibleWon", 0);

    }
    //easy = 1 normal = 2 hard = 3 impossible/expert = 4
    public int dificulty = 1;
    public easyButton EasyButtonScript;
    public normalButton NormalButtonScript;
    public hardButton HardButtonScript;
    public impossibleButton ImpossibleButtonScript;
    public bool reset = false;
    public bool easyWon = false;
    public bool normalWon = false;
    public bool hardWon = false;
    public bool impossibleWon = false;
    public GameObject easyCrown;
    public GameObject normalCrown;
    public GameObject hardCrown;
    public GameObject impossibleCrown;
    public GameObject normalLock;
    public GameObject hardLock;
    public GameObject impossibleLock;
    public GameObject flashlightTip;
    public GameObject sprintTip;
    public GameObject crouchTip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EasyButtonScript.easyButtonEnabled == true && EasyButtonScript.easyButtonPressed == true)
        {
            flashlightTip.SetActive(true);
            dificulty = 1;
        }
        if (NormalButtonScript.normalButtonEnabled == true && NormalButtonScript.normalButtonPressed == true)
        {
            dificulty = 2;
        }
        if (HardButtonScript.hardButtonEnabled == true && HardButtonScript.hardButtonPressed == true)
        {
            dificulty = 3;
        }
        if (ImpossibleButtonScript.impossibleButtonEnabled == true && ImpossibleButtonScript.impossibleButtonPressed == true)
        {
            dificulty = 4;
        }
        if (reset == true)
        {
            PlayerPrefs.DeleteAll();
        }

        if (easyWon == true)
        {
            PlayerPrefs.SetInt("hasEasyWon", 1);
            PlayerPrefs.GetInt("hasEasyWon");
        }

        if (normalWon == true)
        {
            PlayerPrefs.SetInt("hasNormalWon", 1);
            PlayerPrefs.GetInt("hasNormalWon");
        }

        if (hardWon == true)
        {
            PlayerPrefs.SetInt("hasHardWon", 1);
            PlayerPrefs.GetInt("hasHardWon");
        }

        if (impossibleWon == true)
        {
            PlayerPrefs.SetInt("hasImpossibleWon", 1);
            PlayerPrefs.GetInt("hasImpossibleWon");
        }

        if (PlayerPrefs.GetInt("hasEasyWon") == 1)
        {
            easyCrown.SetActive(true);
            normalLock.SetActive(false);
            NormalButtonScript.normalButtonEnabled = true;
        }
        if (PlayerPrefs.GetInt("hasNormalWon") == 1)
        {
            normalCrown.SetActive(true);
            hardLock.SetActive(false);
            HardButtonScript.hardButtonEnabled = true;
        }
        if (PlayerPrefs.GetInt("hasHardWon") == 1)
        {
            hardCrown.SetActive(true);
            impossibleLock.SetActive(false);
            ImpossibleButtonScript.impossibleButtonEnabled = true;
        }
        if (PlayerPrefs.GetInt("hasImpossibleWon") == 1)
        {
            impossibleCrown.SetActive(true);
        }
    }
}
