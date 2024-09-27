using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flashlight1 : MonoBehaviour
{
    Light m_light;
    public bool canUseFlashlight = false;
    public AudioSource audioSource;
    public bool drainOverTime;
    public float maxBrightness;
    public float minBrightness;
    public float drainRate;
    public float percentage;
    public bool ready = true;
    public int newPercentage;
    public dificultyLocked DificultyLocked;
    public bool shiftTipShown = false;

    // Start is called before the first frame update
    void Start()
    {
        ready = true;
        m_light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        m_light.intensity = Mathf.Clamp(m_light.intensity,minBrightness,maxBrightness);
        if(drainOverTime == true && m_light.enabled == true)
        {
            if (m_light.intensity > minBrightness)
            {
                if (ready == true)
                {
                StartCoroutine(batteryCooldown());
                }
            }
        }
        if (canUseFlashlight == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                m_light.enabled = !m_light.enabled;
                audioSource.Play();
                if (shiftTipShown == false)
                {
                    DificultyLocked.flashlightTip.SetActive(false);
                    DificultyLocked.sprintTip.SetActive(true);
                    shiftTipShown = true;
                }
            }
        }
        int newPercentage = (int) percentage;

    }

    public void ReplaceBattery(float amount)
    {
        m_light.intensity += amount;
    }

    IEnumerator batteryCooldown()
    {
        ready = false;
        m_light.intensity -= 0.02f;
        percentage = m_light.intensity * 25;
        yield return new WaitForSeconds(0.7f);
        ready = true;
    }
}
