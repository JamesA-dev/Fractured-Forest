using UnityEngine;
using UnityEngine.UI;

public class fade_out : MonoBehaviour
{
    public GameObject batteryText;
    public AudioSource hit;

    private void OnTriggerEnter(Collider battery1)
    {
        if (battery1.gameObject.layer == 3)
        {
            batteryText.SetActive(true);
            hit.Play();
        }
    }
}