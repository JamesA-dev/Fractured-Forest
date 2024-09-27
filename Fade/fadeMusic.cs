using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeMusic : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f;
    public AudioSource chaseMusic;
    public AudioSource ambience;

    private void OnTriggerEnter(Collider Collider)
    {
        StartCoroutine(fadeMusicOut());
    }

    IEnumerator fadeMusicOut()
    {
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;
 
        while(elapsedTime < delay) {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        if (AudioListener.volume == 0)
        {
            ambience.volume = 0;
            AudioListener.volume = 1;
        }
    }
}
