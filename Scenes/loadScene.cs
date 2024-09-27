using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    public GameObject fadeOutTrigger;
    public AudioSource menuMusic;


    public void Start()
    {
        fadeOutTrigger.SetActive(false);
    }
    public void LoadScene(string sceneName)
    {   
        StartCoroutine(waitBeforeLoad());
    }

    IEnumerator waitBeforeLoad()
    {
        fadeOutTrigger.SetActive(true);
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;
 
        while(elapsedTime < delay) {
            elapsedTime += Time.deltaTime;
            IEnumerator fadeSound1 = AudioFadeOut.FadeOut (menuMusic, 0.5f);
            StartCoroutine (fadeSound1);
            StopCoroutine (fadeSound1);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("game");
    }
}