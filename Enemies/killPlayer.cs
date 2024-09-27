using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class killPlayer : MonoBehaviour
{
    public GameObject gui;
    public GameObject Image;
    public GameObject videoPlayer;
    public AudioSource source;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    void Start () 
    {
        videoPlayer.SetActive(false);
    }

        void OnTriggerEnter(Collider player)
        {
            if (player.gameObject.layer == 3)
            {
                gui.SetActive(false);
                videoPlayer.SetActive(true);
                source.Stop();
                Image.SetActive(true);
                StartCoroutine(EndGame(2.5f));
            }   
        }

    private IEnumerator EndGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene("mainMenu");
    }
}
