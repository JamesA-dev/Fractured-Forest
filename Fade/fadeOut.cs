using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadeOut : MonoBehaviour
{
    public GameObject fadeOutObject;

    void OnTriggerEnter(Collider other) {
        fadeOutObject.SetActive(true);
        StartCoroutine(startFadeTimer());
    }

    IEnumerator startFadeTimer()
    {
        yield return new WaitForSeconds(2f);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene("mainMenu");
    }
}
