using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetQuality : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityDropdown;

    public void SetQualityLevelDropdown(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
    }
}
